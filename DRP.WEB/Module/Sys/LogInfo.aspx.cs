using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.SysMrg;
using DRP.Log;

namespace DRP.WEB.Module.Sys
{
    public partial class LogInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new Log_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                switch ((LogType)e.LogType)
                {
                    case LogType.Error: Lv.Text = "错误"; break;
                    case LogType.Info: Lv.Text = "记录"; break;
                    case LogType.Fatal: Lv.Text = "崩溃"; break;
                    case LogType.Warn: Lv.Text = "警告"; break;
                    case LogType.Debug: Lv.Text = "调试"; break;
                }
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "syslog";
            }
        }
    }
}