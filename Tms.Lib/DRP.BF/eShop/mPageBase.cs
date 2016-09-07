using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.BF.eShop
{
    /// <summary>
    /// 微网站基类
    /// </summary>
    public class mPageBase : Page
    {
        protected string OrgID
        {
            get
            {
                return Request["appid"];
            }
        }

        /// <summary>
        /// 跳转错误页面
        /// </summary>
        /// <param name="msg"></param>
        protected void UrlReturn(string msg = "")
        {
            var url = "/Error.aspx";
            if (!string.IsNullOrEmpty(msg))
                url += "?msg=" + Security.Base64Encrypt(msg);
            Response.Redirect(url);
        }

        /// <summary>
        /// 当前页面位置
        /// </summary>
        protected virtual string LocationName
        {
            get;
            set;
        }

        private void SetLocation()
        {
            var lblName = (Literal)Master.FindControl("lblName");
            if (lblName != null)
                lblName.Text = LocationName;
        }

        #region << 登录用户信息 >>

        private static Dictionary<string, mUserEntity> eShopUserCollection = new Dictionary<string, mUserEntity>();

        /// <summary>
        /// 当前登录用户的身份信息
        /// </summary>
        public mUserEntity CurrentUser
        {
            get
            {
                var userID = Page.User.Identity.Name;
                mUserEntity user = null;
                if (eShopUserCollection.ContainsKey(userID))
                    user = eShopUserCollection[userID];
                if (user != null) return user;
                var entity = new eShopMemeber().Get(userID);
                if (entity != null)
                {
                    user = new mUserEntity()
                    {

                        UserID = entity.ID,
                        UserName = entity.UserName,
                        UserAcct = entity.UserAcct,
                        Mobile = entity.Mobile,
                        OrgID = entity.OrgID,
                        NickName = entity.NickName,
                        Photo = entity.Photo,
                        WechatAppID = entity.AppTokenID
                    };
                    eShopUserCollection.Add(userID, user);
                }
                return user;
            }
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static mUserEntity UserInfo
        {
            get
            {
                return new mPageBase().CurrentUser;
            }
        }

        /// <summary>
        /// 清除登录身份的状态
        /// </summary>
        /// <param name="acctID"></param>
        public static void RemoveUserKey(string userID)
        {
            if (eShopUserCollection != null && eShopUserCollection.ContainsKey(userID))
            {
                eShopUserCollection.Remove(userID);
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetLocation();
        }
    }
}
