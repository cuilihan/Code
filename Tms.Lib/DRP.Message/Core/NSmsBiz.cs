using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace DRP.Message.Core
{

    /// <summary>
    /// 短信执行结果
    /// </summary>
    public class NSmsResult
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

    public class NSmsBiz : IMessage
    {
        private static NSmsConfig _instance;

        /// <summary>
        /// 短信配置实例
        /// </summary>
        private static NSmsConfig GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new NSmsConfig().CreateInstance();
                var ver = Convert.ToDouble(ConfigurationManager.AppSettings["SmsVersion"]);
                if (_instance.SmsVersion != ver)
                    _instance = new NSmsConfig().CreateInstance();

                return _instance;
            }
        }

        public bool Send(MessageEntity entity)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private int SendSmsMethod(MessageEntity entity)
        {
            var config = NSmsBiz.GetInstance;
            var sms = new XWWebReference.WebService();
            sms.Url = config.SmsServerHost;
            var sms_user_id = "";
            var sms_user_p = "";
            var sms_content = entity.MsgContent;
            if (entity.IsTemplateSms)
            {
                sms_user_id = config.Account_ID;
                sms_user_p = config.Account_Pwd;
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
                    return 601;
                }
            }
            else
            {
                sms_user_id = config.Account_ID;
                sms_user_p = config.Account_Pwd;
            }
            if (entity.RecMobile.IndexOf(",") > -1) //群发:以英文逗号分隔
                return sms.PostMass(sms_user_id, sms_user_p, entity.RecMobile.Split(','), sms_content, string.Empty);
            else //单条发送
                return sms.PostSingle(sms_user_id, sms_user_p, entity.RecMobile, sms_content, string.Empty);
        }

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
                string result = "";
                try
                {
                    var code = SendSmsMethod(entity);
                    switch (code)
                    {
                        case 0:
                            result = "成功";
                            break;
                        case -1:
                            result = "账号无效";
                            break;
                        case -12:
                            result = "余额不足";
                            break;
                        case -6:
                            result = "用户名密码错误";
                            break;
                        case -9:
                            result = "资金账户不存在";
                            break;
                        case -11:
                            result = "包号码数量超过最大限制";
                            break;
                        case -2:
                            result = "参数无效";
                            break;
                        case -99:
                            result = "系统内部错误";
                            break;
                        case -100:
                            result = "其它错误";
                            break;
                        case -3:
                            result = "连接不上服务器";
                            break;
                        case -5:
                            result = "无效的短信数据，包括短信内容过长、号码格式不对";
                            break;
                        case -7:
                            result = "旧密码不正确";
                            break;
                        default:
                            result = "成功";
                            break;
                    }
                    ret.AddCode(code.ToString(), result);
                }
                catch (Exception ex)
                {
                    ret.AddCode("602", "未知错误:" + ex.Message);
                }
                finally
                {
                    if (ret.Code.Equals("0")) //短信发成功，须做以下操作
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



        #region 余额查询

        /// <summary>
        /// 查询账号余额
        /// </summary>
        /// <param name="login_id"></param>
        /// <param name="login_pwd"></param>
        /// <returns></returns>
        private string GetSmsBalance(string login_id, string login_pwd, string url)
        {
            var sms = new XWWebReference.WebService();
            sms.Url = url;
            var ret = sms.GetAccountInfo(login_id, login_pwd);
            if (ret == null)
            {
                return "账号或密码错误";
            }
            else
            {
                return ret.Balance.ToString();
            }
        }

        /// <summary>
        /// 模板短信账号余额
        /// </summary>
        /// <returns></returns>
        public string GetTemplateSmsBalance()
        {
            var instance = NSmsBiz.GetInstance;
            return GetSmsBalance(instance.Account_ID, instance.Account_Pwd, instance.SmsServerHost);
        }

        #endregion

        #region 短信测试报告《运营商返回的结果》
        /// <summary>
        /// 常规短信状态报告
        /// </summary>
        public string GetSmsStateReport_N(int count)
        {
            var instance = NSmsBiz.GetInstance;
            var sms = new XWWebReference.WebService();
            sms.Url = instance.SmsServerHost;
            var sb=new StringBuilder();
            DRP.Message.XWWebReference.MOMsg[] info = sms.GetMOMessage(instance.Account_ID, instance.Account_Pwd, count);

            if (info != null)
            {
                for (int i = 0; i < info.Length; i++)
                {
                   sb.Append("手机号码：" + info[i].Phone);
                   sb.Append("内容：" + info[i].Content);
                   sb.Append("接收时间：" + info[i].ReceiveTime);
                   sb.Append("消息类型：" + info[i].MsgType);
                   sb.Append("业务代码：" + info[i].ServiceType + "\n");
                }
                if (info.Length == 0)
                {
                    sb.Append("null");
                }
            }
            else
            {
                sb.Append("null");
            }
            return sb.ToString();

        }
        #endregion
    }
}

