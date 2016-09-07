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
    public partial class OmAreaEdit :Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        protected override string NavigateID
        {
            get
            {
                return "omarea";
            }
        }

        private void BindData()
        {
            var parentId = Request["pid"];
            if (string.IsNullOrEmpty(parentId)) //更新或新增菜单
            {
                var e = new OmArea_BF().Get(KeyID);
                if (e == null)
                    lblNavName.Text = "根目录";
                else
                {
                    LoadData(e, pnlContainer);
                    e = new OmArea_BF().Get(e.ParentID);
                    lblNavName.Text = e == null ? "无" : e.AreaName;
                }
            }
            else //增加子菜单
            {
                var e = new OmArea_BF().Get(parentId);
                if (e != null)
                    lblNavName.Text = e.AreaName;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var parentId = Request["pid"];
            if (string.IsNullOrEmpty(parentId)) //更新或新增菜单
            {
                var isOk = new OmArea_BF().Save(KeyID, pnlContainer);
                JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
            }
            else //增加子菜单
            {
                var isOk = new OmArea_BF().Save(KeyID, pnlContainer, parentId);
                JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
            }
        }
    }
}