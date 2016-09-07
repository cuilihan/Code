using DRP.BF.Core;
using DRP.BF.OmMrg;
using DRP.BF.TmsApi.Core;
using DRP.Framework.Core;
using DRP.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace DRP.BF
{
    /// <summary>
    /// 快捷机票接口服务
    /// </summary>
    public class AirTicketOrderService : ITmsServiceProvider
    {
        #region 接口参数

        /// <summary>
        /// 接口URL地址
        /// </summary>
        private string Ticket_Api_Url
        {
            get
            {
                return ConfigHelper.GetAppSettingValue("Ticket_Api_Url");
            }
        }

        /// <summary>
        /// 快捷机票接口：Token
        /// </summary>
        private string P_Token
        {
            get
            {
                return ConfigHelper.GetAppSettingValue("P_Token");
            }
        }

        /// <summary>
        /// 快捷机票接口：TokenID
        /// </summary>
        private string P_Token_ID
        {
            get
            {
                return ConfigHelper.GetAppSettingValue("P_Token_ID");
            }
        }

        /// <summary>
        /// 快捷机票接口Api登录账号ID：Api_User_ID
        /// </summary>
        private string Api_User_ID
        {
            get
            {
                return ConfigHelper.GetAppSettingValue("Api_User_ID");
            }
        }
        /// <summary>
        /// 快捷机票接口Api登录账号ID：Api_User_ID
        /// </summary>
        private string Api_User_PWD
        {
            get
            {
                return ConfigHelper.GetAppSettingValue("Api_User_Pwd");
            }
        }
        #endregion

        #region 快捷机票->TMS

        public string GetData(string taskGuid, string dataType, string xmlData)
        {
            var str = string.Empty;
            switch (dataType.ToUpper())
            {
                default:
                    str = ApiException.DataTypeEmpty();
                    break;
            }
            return str;
        }

        public string SetData(string taskGuid, string dataType, string xmlData)
        {
            throw new NotImplementedException();
        }

        public string TransferData(string taskGuid, string dataType, string xmlData)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TMS->快捷机票

        /// <summary>
        /// 机票接口Api登录
        /// </summary>
        /// <returns>模拟用户登录，非空值为cookie即sessionid，空值登录失败</returns>
        public string ApiLogin()
        {
            var postData = "p_token_id={0}&p_token={1}&p_api_key={2}&p_username={3}&p_password={4}";
            postData = string.Format(postData, P_Token_ID, P_Token, GetApiKey(), Api_User_ID, Api_User_PWD);
            var netHelper = new HttpHelper();
            var ret = netHelper.AgencyPostData(Ticket_Api_Url + "?m=platform&c=Platform&a=apiLogin", postData).Equals("1");
            return ret ? netHelper.CookieHeader : "";
        }

        #region << 同步机构与用户信息 >>

        /// <summary>
        /// 同步机构信息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="isAdd"></param>
        /// <returns></returns>
        public bool SyncOrgInfo(DAL.Om_OrgInfo e, bool isAdd = true)
        {
            var ret = "";
            try
            {
                var cookieHeader =  ApiLogin();//先登录系统                 
                if (string.IsNullOrEmpty(cookieHeader)) //用户登录失败
                    return false;
                ret = isAdd ? Org_SyncToKjYou_Add(e, cookieHeader) : Org_SyncToKjYou_Edit(e, cookieHeader);
            }
            catch
            {
                ret = "-2";
            }
            return ret.Equals("1");
        }

        /// <summary>
        /// 快捷机票接口Api_Key
        /// </summary>
        /// <returns></returns>
        private string GetApiKey()
        {
            var ts = DateTime.Today.AddHours(-8) - DateTime.Parse("1970-1-1");
            var arr = new string[] { P_Token_ID, Api_User_PWD, ts.TotalSeconds.ToString(), Api_User_ID, P_Token };
            var tempStr = string.Join("", arr);
            tempStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tempStr, "SHA1").ToLower();
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(tempStr);
            tempStr = Convert.ToBase64String(encbuff);
            tempStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tempStr, "MD5").ToLower();
            return tempStr;
        }

        /// <summary>
        /// 旅行社机构同步至快捷游系统-新增用户接口
        /// </summary>
        /// <returns></returns>
        private string Org_SyncToKjYou_Add(DAL.Om_OrgInfo e, string cookie)
        {
            var userInfo = new OmUser_BF().Get(e.ID);
            if (userInfo == null) return "-1";
            var phone = e.ContactPhone.Replace(" ", ",").Replace("，", ",").Replace("、", ",");
            var list = new List<string>();
            list.Add(string.Format("p_username={0}", userInfo.UserName));
            list.Add(string.Format("p_password={0}", Security.DecrypByRijndael(userInfo.UserPwd)));
            list.Add(string.Format("p_uid={0}", userInfo.ID));//等于机构主键ID
            list.Add(string.Format("p_parent_uid={0}", ""));
            list.Add(string.Format("p_cst_com={0}", e.Name));
            list.Add(string.Format("p_cst_start_time={0}", e.OpenDate.ToString("yyyy-MM-dd")));
            list.Add(string.Format("p_cst_end_time={0}", e.ExpiryDate.ToString("yyyy-MM-dd")));
            list.Add(string.Format("p_lnk_name={0}", e.OrgContact));
            list.Add(string.Format("p_lnk_mobile={0}", phone.Trim().Length == 11 ? phone : ""));
            list.Add(string.Format("p_lnk_tel={0}", phone.Length != 11 ? phone : ""));
            list.Add("p_staff=1");
            var postData = string.Join("&", list);
            return new HttpHelper().AgencyPostData(Ticket_Api_Url + "?m=platform&c=Platform&a=addUser", postData, cookie);
        }

        /// <summary>
        /// 旅行社机构同步至快捷游系统-编辑用户接口
        /// </summary>
        /// <returns></returns>
        private string Org_SyncToKjYou_Edit(DAL.Om_OrgInfo e, string cookie)
        {
            var userInfo = new OmUser_BF().Get(e.ID);
            if (userInfo == null) return "-1";
            var phone = e.ContactPhone.Replace(" ", ",").Replace("，", ",").Replace("、", ",");

            var list = new List<string>();
            list.Add(string.Format("p_token_id={0}", P_Token_ID));
            list.Add(string.Format("p_uid={0}", e.ID));
            list.Add(string.Format("p_staff={0}", "1"));
            list.Add(string.Format("p_password={0}", Security.DecrypByRijndael(userInfo.UserPwd)));//默认888888 
            list.Add(string.Format("p_cst_com={0}", e.Name));
            list.Add(string.Format("p_cst_start_time={0}", e.OpenDate.ToString("yyyy-MM-dd")));
            list.Add(string.Format("p_cst_end_time={0}", e.ExpiryDate.ToString("yyyy-MM-dd")));
            list.Add(string.Format("p_lnk_name={0}", e.OrgContact));
            list.Add(string.Format("p_lnk_mobile={0}", phone.Trim().Length == 11 ? phone : ""));
            list.Add(string.Format("p_lnk_tel={0}", phone.Length != 11 ? phone : ""));
            var postData = string.Join("&", list);
            return new HttpHelper().AgencyPostData(Ticket_Api_Url + "?m=platform&c=Platform&a=editUser", postData, cookie);
        }

        #endregion
        
        #endregion
    }
}
