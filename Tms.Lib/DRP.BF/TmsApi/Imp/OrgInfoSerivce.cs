using DRP.BF.Core;
using DRP.BF.GloMrg;
using DRP.BF.OmMrg;
using DRP.BF.TmsApi;
using DRP.BF.TmsApi.Core;
using DRP.Framework.Core;
using DRP.Message.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DRP.BF
{
    /// <summary>
    /// 机构注册服务
    /// </summary>
    public class OrgInfoSerivce : ITmsServiceProvider
    {
        public string GetData(string taskGuid, string dataType, string xmlData)
        {
            var str = string.Empty;
            switch (dataType.ToUpper())
            {
                case "ORGCOLLECTION"://机构集合列表
                    str = GetOrgList();
                    break;
                case "AREALIST": //获取区域
                    str = GetTmsArea();
                    break;
                case "SENDVALIDCODE"://发送手机验证码
                    str = SendMobileValid(xmlData);
                    break;
                case "VALIDCODE"://验证验证码是否有效
                    str = ValidCode(xmlData);
                    break;
                case "TMSUPDATELOG"://TMS更新日志
                    str = GetTMSUpdateLog();
                    break;
                case "ONLINESERVICE"://在线QQ客服
                    str = GetOnLineService();
                    break;
                default:
                    str = ApiException.DataTypeEmpty();
                    break;
            }
            return str;
        }

        public string SetData(string taskGuid, string dataType, string xmlData)
        {
            var str = string.Empty;
            switch (dataType.ToUpper())
            {
                case "REGISTERORG":  //机构用户注册
                    str = RegisterTmsOrg(xmlData);
                    break;
            }
            return str;
        }

        public string TransferData(string taskGuid, string dataType, string xmlData)
        {
            throw new NotImplementedException();
        }

        #region Get 实现方法

        /// <summary>
        /// TMS覆盖区域
        /// </summary>
        /// <remarks>返回JSON</remarks> 
        /// <param name="context"></param> 
        private string GetTmsArea()
        {
            var list = new OmArea_BF().GetOmArea();
            return new ApiHelper().ListToDocument<DAL.Om_Area>(list);
        }

        /// <summary>
        /// 获取机构列表（最新降序排序）
        /// </summary>
        /// <returns></returns>
        public string GetOrgList()
        {
            var list = new OrgInfo_BF().GetOrgInfo();
            return new ApiHelper().ListToDocument<DAL.Om_OrgInfo>(list);
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="context"></param>
        private string SendMobileValid(string xmlData)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var node = doc.SelectSingleNode("Document/Mobile");
            if (node == null) return ApiHelper.ErrorMessage(2, "手机号码为空");
            var mobile = node.InnerText;
            if (string.IsNullOrEmpty(mobile)) return ApiHelper.ErrorMessage(2, "手机号码为空");
            if (mobile.Length != 11) return ApiHelper.ErrorMessage(3, "手机号码格式错误");
            var entity = new MessageEntity();
            entity.KeyID = Guid.NewGuid().ToString();
            entity.SendUserID = Guid.Empty.ToString();
            entity.SendUserName = "system";
            entity.MsgContent = new Random().Next(100000, 999999).ToString();
            entity.OrgID = ConfigHelper.GetAppSettingValue("OmOrgID"); ;//苏州赛思科技
            entity.DataStatus = 0;
            entity.RecMobile = mobile;
            entity.IsTemplateSms = true;
            entity.SmsType = T_Sms_Type.Validate;
            var r = new NSmsBiz().SendSms(entity);
            if (r.Code == "0") return ApiHelper.SuccessMessage(r.Message);
            return ApiHelper.ErrorMessage(3, "发送短信失败");
        }

        /// <summary>
        /// 验证码是否正确[5分钟内有效]
        /// 1：验证成功，0：验证失败
        /// </summary>
        /// <param name="context"></param>
        private string ValidCode(string xmlData)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var node = doc.SelectSingleNode("Document/Mobile");
            if (node == null) return ApiHelper.ErrorMessage(2, "手机号码为空");
            var mobile = node.InnerText;
            if (string.IsNullOrEmpty(mobile)) return ApiHelper.ErrorMessage(2, "手机号码为空");
            if (mobile.Length != 11) return ApiHelper.ErrorMessage(3, "手机号码格式错误");
            node = doc.SelectSingleNode("Document/Code");
            if (node == null) return ApiHelper.ErrorMessage(4, "验证码为空值");
            var code = node.InnerText;
            if (string.IsNullOrEmpty(code))
                return ApiHelper.ErrorMessage(4, "验证码为空值");
            var isOk = new SmsBiz().IsValidCode(code, mobile);
            if (!isOk) return ApiHelper.ErrorMessage(5, "验证码错误或已失效");
            return ApiHelper.SuccessMessage("验证成功");
        }

        /// <summary>
        /// 验证域名是否重复
        /// </summary>
        /// <param name="context"></param>
        private string ValidDomain(string xmlData)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var node = doc.SelectSingleNode("Document/Domain");
            if (node == null) return ApiHelper.ErrorMessage(2, "域名为空值");
            var domain = node.InnerText;
            if (string.IsNullOrEmpty(domain)) return ApiHelper.ErrorMessage(2, "域名为空值");
            var r = new OrgInfo_BF().ExistDomain(domain);
            if (!r) ApiHelper.ErrorMessage(3, "域名已使用");
            return ApiHelper.SuccessMessage("域名有效");
        }

        /// <summary>
        /// 更新日志查询
        /// </summary>
        /// <returns></returns>
        private string GetTMSUpdateLog()
        {
            var list = new UpdateLog_BF().GetUpdateLog();
            return new ApiHelper().ListToDocument<DAL.Glo_UpdateLog>(list);
        }

        /// <summary>
        /// 在线客服QQ查询
        /// </summary>
        /// <returns></returns>
        private string GetOnLineService()
        {
            var list = new QQ_BF().GetData();
            return new ApiHelper().ListToDocument<DAL.Glo_QQ>(list);
        }
        #endregion

        #region Set 实现方法

        /// <summary>
        /// 网站自助注册旅管家用户机构
        /// </summary>
        /// <param name="context"></param>
        private string RegisterTmsOrg(string xmlData)
        {
            if (string.IsNullOrEmpty(xmlData)) return ApiException.XmlDataEmpty();
            var r = new OrgInfo_BF().RegOrgInfoBySelf(xmlData);
            if (!r) return ApiHelper.ErrorMessage(3, "提交数据失败");
            else return ApiHelper.SuccessMessage("提交数据成功");
        }

        #endregion

    }
}
