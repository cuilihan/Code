using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.SysMrg;
using DRP.Framework.Core;
using DRP.Framework;

namespace DRP.WEB.Module.Sys
{
    public partial class DeptEdit :Pagebase
    {
        Dept_BF dal = new Dept_BF();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }
         

        private void BindData()
        {
            var parentId = Request["pid"];
            if (string.IsNullOrEmpty(parentId)) //更新或新增菜单
            {
                var e = dal.Get(KeyID);
                if (e == null)
                    lblParentName.Text = "无";
                else
                {
                    LoadData(e, pnlContainer);
                    e = dal.Get(e.ParentID);
                    lblParentName.Text = e == null ? "无" : e.Name;
                }
            }
            else //增加子菜单
            {
                var e = dal.Get(parentId);
                if (e != null)
                    lblParentName.Text = e.Name;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var parentId = Request["pid"];
            if (string.IsNullOrEmpty(parentId)) //更新或新增菜单
            {
                var isOk = dal.Save(KeyID, Name.Text, OrderIndex.Text.ToInt(), DataStatus.Text.ToInt());
                JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
            }
            else //增加子菜单
            {
                var isOk = dal.Save(KeyID, Name.Text, OrderIndex.Text.ToInt(), DataStatus.Text.ToInt(), parentId);
                JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "sysdept";
            }
        }
    }
}