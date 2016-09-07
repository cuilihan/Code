using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF.GloMrg;
using System.Text;

namespace DRP.WEB.Module.Glo
{
    public partial class NoticeInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            Notice_BF dao = new Notice_BF();
            var e = dao.Get(Request["id"]);
            if (e != null)
            {
                dao.SignReader(e.ID);

                Subject.Text = e.Subject;
                nContext.Text = e.nContent;
                CreateUserName.Text = e.CreateUserName;
                CreateDate.Text = ((DateTime)e.CreateDate).ToString("yyyy-MM-dd");

                Page.Title = e.Subject + "-通知公告";

                var sb = new StringBuilder();
                var list = dao.GetNoticeTrace(e.ID);
                list.ForEach(x =>
                {
                    sb.AppendFormat("<span title='阅读时间：{0}'>{1}</span>", x.CreateDate.ToString("yyyy-MM-dd hh:MM:ss"), x.UserName);
                });
                lblUserList.Text = sb.ToString();
            }
        }
    }
}