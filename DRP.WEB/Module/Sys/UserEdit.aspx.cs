using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.BF.SysMrg;
using DRP.Framework.Core;
using DRP.Framework;

namespace DRP.WEB.Module.Sys
{
    public partial class UserEdit : Pagebase
    {
        User_BF dal = new User_BF();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["id"]))//新增时检测用户数限制
                {
                    CheckMaxUserQuantity();
                }
                BindData();
            }
        }

        private void BindData()
        {
            var e = dal.Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                lbTips.Visible = true;
                AcctPwd.Value = Security.DecrypByRijndael(e.AcctPwd);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["id"]))//新增时判断是否存在相同的账号
            {
                if (CheckMaxUserQuantity()) //超过用户数限制
                {
                    return;
                }
                if (new User_BF().ExistAcct(AcctID.Text.Trim(), AuthenticationPage.UserInfo.OrgID))
                {
                    JScript.Alert("账号已存在");
                    return;
                }
            }
            var p = string.IsNullOrEmpty(txtAcctPwd.Text) ? AcctPwd.Value : txtAcctPwd.Text;
            var isOk = dal.SaveUser(KeyID, pnlContainer, p);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }


        /// <summary>
        /// 检查用户数量
        /// </summary>
        /// <returns>true:超过限制  false：未超限</returns>
        private bool CheckMaxUserQuantity()
        {
            var isRet = false;
            var orgEntity = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            var maxCount = orgEntity.MaxUserCount == null ? 0 : orgEntity.MaxUserCount.ToString().ToInt();
            if (maxCount == -1)
            {
                UserCountInfo.Text = "";
                lblTips.Text = "";
            }
            else
            {
                var usedUserCount = new User_BF().CalculateUserQuantity();
                var count = maxCount - usedUserCount;
                UserCountInfo.Text = string.Format("最大用户数：【{0}】，已使用数：【{1}】，还可用数：【{2}】", maxCount, usedUserCount, count);
                if (count <= 0)
                {
                    isRet = true;
                    lblTips.Text = string.Format("超过最大用户数【{0}】，如需增加用户，请联系【<a href='http://www.58datu.com' target='_blank'>{1}</a>】购买！",
                       maxCount, ConfigHelper.GetAppSettingValue("OrgName"));
                    btnSave.Visible = false;
                }
            }
            return isRet;
        }

        protected override string NavigateID
        {
            get
            {
                return "sysuser";
            }
        }
    }
}