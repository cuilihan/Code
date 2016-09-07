using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using DRP.DAL;
using System.Xml;

namespace DRP.Message.Core
{
    /// <summary>
    /// 短信执行结果
    /// </summary>
    public class SmsResult
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public void AddCode(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("<document>");
            sb.Append("<sms>");
            sb.AppendFormat("<code>{0}</code>", Code);
            sb.AppendFormat("<message>{0}</message>", Message);
            sb.Append("</sms>");
            sb.Append("</document>");
            return sb.ToString();
        }
    }

    /// <summary>
    /// 手机短信
    /// </summary>
    public class SmsBiz : IMessage
    {
        private static SmsConfig _instance;

        /// <summary>
        /// 短信配置实例
        /// </summary>
        private static SmsConfig GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new SmsConfig().CreateInstance();
                var ver = Convert.ToDouble(ConfigurationManager.AppSettings["SmsVersion"]);
                if (_instance.SmsVersion != ver)
                    _instance = new SmsConfig().CreateInstance();

                return _instance;
            }
        }

        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string SendSmsMethod(MessageEntity entity)
        {
            var config = SmsBiz.GetInstance;
            var sms = new SmsService.SmsService();
            sms.Url = config.SmsServerHost;
            var sms_user_id = "";
            var sms_user_p = "";
            var sms_content = entity.MsgContent;
            if (entity.IsTemplateSms)
            {
                sms_user_id = config.T_Account_ID;
                sms_user_p = config.T_Account_Pwd;
                var smsContent = "";
                if (config.SmsTemplate.TryGetValue(entity.SmsType, out smsContent))
                {
                    sms_content = string.Format(smsContent, entity.MsgContent);

                    //如果是验证类短信，须将验证码写入数据库
                    if (entity.SmsType == T_Sms_Type.Validate)
                    {
                        SaveSmsValidCode(entity.MsgContent, entity.RecMobile);
                    }
                }
                else
                {
                    return "601";
                }
            }
            else
            {
                sms_user_id = config.N_Account_ID;
                sms_user_p = config.N_Account_Pwd;
            }
            if (entity.RecMobile.IndexOf(",") > -1) //群发:以英文逗号分隔
                return sms.SendSmsGroup(sms_user_id, sms_user_p, entity.RecMobile, sms_content, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "");
            else //单条发送
                return sms.SendSms(sms_user_id, sms_user_p, entity.RecMobile, sms_content, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "");
        }

        #region IMessage 成员

        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Send(MessageEntity entity)
        {
            return SendSms(entity).Code.Equals("504");
        }

        #endregion

        #region 发短信扩展方法

        /// <summary>
        /// 发送手机短信(推荐使用此方法)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SmsResult SendSms(MessageEntity entity)
        {
            var ret = new SmsResult();
            if (!EnableSendSms(entity.OrgID))
            {
                ret.AddCode("600", "机构已欠费，系统拒绝");
            }
            else
            {
                try
                {
                    var code = SendSmsMethod(entity);
                    switch (code)
                    {
                        case "501": ret.AddCode("501", "账号或密码错误"); break;
                        case "504": ret.AddCode("504", "发送成功"); break;
                        case "505": ret.AddCode("505", "发送失败"); break;
                        case "506": ret.AddCode("506", "短信内容为空"); break;
                        case "601": ret.AddCode("601", "短信模板未定义"); break;
                    }
                }
                catch (Exception ex)
                {
                    ret.AddCode("602", "未知错误:" + ex.Message);
                }
                finally
                {
                    if (ret.Code.Equals("504")) //短信发成功，须做以下操作
                    {
                        SaveSmsToDb(entity); //1、将短信存储至数据库
                        var smsCount = entity.RecMobile.Split(',').Length;//群发时短信记录数
                        UpdatePlatformSmsCount(entity.OrgID, smsCount); //2、更新平台的短信记录数
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 是否还可以发送短信
        /// </summary>
        /// <returns>false:欠费不可以发</returns>
        private bool EnableSendSms(string orgID)
        {
            var e = DAL.Om_OrgInfo.SingleOrDefault(x => x.ID == orgID);
            if (e == null) return false;
            if (e.SmsCount == null) return false;
            var smsCount = (int)e.SmsCount;
            var sendCount = e.SendSmsCount == null ? 0 : (int)e.SendSmsCount;
            return smsCount > sendCount;
        }

        /// <summary>
        /// 数据库存储已发短信
        /// </summary>
        /// <param name="entity"></param>
        private void SaveSmsToDb(MessageEntity entity)
        {
            try
            {
                #region 存储DB
                var arr = entity.RecMobile.Split(','); //支持群发
                foreach (var s in arr)
                {
                    var e = new DAL.Sms_Message();
                    e.ID = Guid.NewGuid().ToString();
                    e.OrgID = entity.OrgID;
                    e.SendUserID = entity.SendUserID;
                    e.SendUserName = entity.SendUserName;
                    e.MsgContent = entity.MsgContent;
                    e.CreateDate = DateTime.Now;
                    e.RecMobile = s;
                    e.Save();
                }
                #endregion
            }
            catch
            {
            }
        }

        /// <summary>
        /// 更新平台的已发短信数量
        /// </summary>
        /// <param name="entity"></param>
        private void UpdatePlatformSmsCount(string orgID, int iCount = 1)
        {
            try
            {
                var e = DAL.Om_OrgInfo.SingleOrDefault(x => x.ID == orgID);
                e.SendSmsCount = e.SendSmsCount == null ? iCount : ((int)e.SendSmsCount + iCount);
                e.Save();
            }
            catch
            {
            }
        }


        #endregion

        #region 验证类操作

        /// <summary>
        /// 验证码是否有效（一般用于注册或确认）,5分钟内有效
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mobile"></param>
        /// <param name="minute">有效分钟</param>
        /// <returns></returns>
        public bool IsValidCode(string code, string mobile, int minute = 5)
        {
            try
            {
                var sql = "SELECT * FROM Sms_ValidateCode WHERE DATEDIFF(minute ,CreateDate,GETDATE())<{0} AND DataStatus=0 AND Mobile='{1}' and Code='{2}'";
                sql = string.Format(sql, minute, mobile, code);
                var list = new SubSonic.Query.CodingHorror(sql).ExecuteTypedList<DAL.Sms_ValidateCode>();
                if (list.Count > 0) //验证后将验证码设为无效
                {
                    list.ForEach(x =>
                    {
                        x.DataStatus = 1;
                        x.Save();
                    });
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 保存验证码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mobile"></param>
        private void SaveSmsValidCode(string code, string mobile)
        {
            var e = new DAL.Sms_ValidateCode();
            e.ID = Guid.NewGuid().ToString();
            e.Code = code;
            e.Mobile = mobile;
            e.CreateDate = DateTime.Now;
            e.DataStatus = 0;
            e.Save();
        }
        #endregion

        #region 用户短信查询

        /// <summary>
        /// 短消息详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Sms_Message Get(string keyID)
        {
            return DAL.Sms_Message.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 短消息列表
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public List<DAL.Sms_Message> GetUserMessage(MessageQueryBase qry, out int totalRecord)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and SendUserID='{0}'", qry.SendUserID);
            if (!string.IsNullOrEmpty(qry.MessageTitle))
                sb.AppendFormat(" and MsgContent like '%{0}%'", qry.MessageTitle);
            if (!string.IsNullOrEmpty(qry.sDate))
                sb.AppendFormat(" and CreateDate>='{0}'", qry.sDate);
            if (!string.IsNullOrEmpty(qry.eDate))
                sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            return new DRPDB().GetPaginationList<DAL.Sms_Message>("Sms_Message", qry.PageIndex, qry.PageSize, out totalRecord, sb.ToString(), qry.SortExpress);
        }
        #endregion

        #region 余额查询

        /// <summary>
        /// 查询账号余额
        /// </summary>
        /// <param name="login_id"></param>
        /// <param name="login_pwd"></param>
        /// <returns></returns>
        private string GetSmsBalance(string login_id, string login_pwd, string url)
        {
            var sms = new SmsService.SmsService();
            sms.Url = url;
            var ret = sms.GetBalance(login_id, login_pwd);
            if (ret == "501")
            {
                return "账号或密码错误";
            }
            else
            {
                var doc = new XmlDocument();
                doc.LoadXml(ret.ToLower());
                var node = doc.SelectSingleNode("result/balance");
                if (node == null) return "未查询到余额";
                else
                {
                    var a = node.InnerText;
                    decimal d = 0;
                    if (decimal.TryParse(a, out d))
                    {
                        return (d / 1000).ToString("f2");
                    }
                    return "查询余额失败";
                }
            }
        }

        /// <summary>
        /// 模板短信账号余额
        /// </summary>
        /// <returns></returns>
        public string GetTemplateSmsBalance()
        {
            var instance = SmsBiz.GetInstance;
            return GetSmsBalance(instance.T_Account_ID, instance.T_Account_Pwd, instance.SmsServerHost);
        }

        /// <summary>
        /// 常规短信账号余额
        /// </summary>
        /// <returns></returns>
        public string GetNormalSmsBalance()
        {
            var instance = SmsBiz.GetInstance;
            return GetSmsBalance(instance.N_Account_ID, instance.N_Account_Pwd, instance.SmsServerHost);
        }

        #endregion

        #region 短信测试报告《运营商返回的结果》
        /// <summary>
        /// 常规短信(账号szsskj2)状态报告
        /// </summary>
        public string GetSmsStateReport_N()
        {
            var instance = SmsBiz.GetInstance;
            var sms = new SmsService.SmsService();
            sms.Url = instance.SmsServerHost;
            var ret = sms.GetSmsStateReport(instance.N_Account_ID, instance.N_Account_Pwd);
            if (ret == "501")
            {
                return "账号或密码错误";
            }
            else
            {
                if (ret == "503")
                    return "没有记录";
                else
                    return ret;
            }
        }
        #endregion
    }
}
