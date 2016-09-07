using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.OmMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Om
{
    public partial class OrgInfoEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }
        private void BindData()
        { 
            NavGroupID.DataSource = new NavGroup_BF().GetNavGroup();
            NavGroupID.DataTextField = "NavGroup";
            NavGroupID.DataValueField = "ID";
            NavGroupID.DataBind();

            var e = new OrgInfo_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e == null)
            {
                ProName.Text = ConfigHelper.GetAppSettingValue("OrgProductName");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var isOk = new OrgInfo_BF().Save(KeyID, pnlContainer, AreaID.Value, NavGroupID.SelectedItem.Text);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }


        protected override string NavigateID
        {
            get
            {
                return "orginfo";
            }
        }
    }
}