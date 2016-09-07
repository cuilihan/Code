using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.GloMrg;
using DRP.BF.Init;
using DRP.BF.My;
using DRP.BF.OmMrg;
using DRP.Log;
using DRP.Message.Core;
using DRP.Framework.Core;
using DRP.BF.SysMrg;

namespace DRP.WEB.Module.Guide
{
    public partial class Index : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (new Init_BF().IsNeededInit())
                {
                    Response.Redirect("Help.aspx");
                }
                else
                {
                    BindUserFavorites();
                    BindUserInfo();
                    BindNotice();
                    BindOmTools();
                    BindMessage();
                    SetDataModulePermission();
                }
            }
        }


        /// <summary>
        /// 登录用户信息
        /// </summary>
        private void BindUserInfo()
        {
            var user = AuthenticationPage.UserInfo;
            lblUserName.Text = user.UserName;
            lblYear.Text = DateTime.Today.Year.ToString() + "年";
        }

        /// <summary>
        /// 用户常用链接
        /// </summary>
        private void BindUserFavorites()
        {
            var sb = new StringBuilder();
            var list = new Favorites_BF().GetUserFavorite(10);
            list.ForEach(x =>
            {
                sb.AppendFormat("<li><i class='black_arrow'></i><a href='{0}' target='_blank'>{1}</a></li>", x.URL, x.Name);
            });
            lblUserFavorites.Text = sb.ToString();
        }

        /// <summary>
        /// 通知公告
        /// </summary>
        private void BindNotice()
        {
            var sb = new StringBuilder();
            var list = new Notice_BF().GetTopNotice(5);
            var idx = 0;
            list.ForEach(x =>
            {
                var ts = new TimeSpan();
                ts = DateTime.Today - (DateTime)x.CreateDate;
                sb.Append("<li>");
                sb.AppendFormat("<i class='{0}'></i>", idx++ < 3 ? "red_arrow" : "black_arrow");
                sb.AppendFormat("<a href='/Module/Glo/NoticeInfo.aspx?id={0}' target='_blank'>{1}</a>", x.ID, x.Subject);
                // sb.AppendFormat("<span class='noticedate'>({0})</span>",((DateTime)x.CreateDate).ToString("yyyy-MM-dd"));
                if (ts.Days < 3)
                {
                    sb.Append("<span class='new'><span>");
                }
                sb.Append("</li>");
            });
            lblNotice.Text = sb.ToString();
        }

        /// <summary>
        /// 运维推送工具栏
        /// </summary>
        private void BindOmTools()
        {
            var sb = new StringBuilder();
            var list = new ToolSetting_BF().GetOmTools();
            list.ForEach(x =>
            {
                sb.AppendFormat("<li class='{4}'><i class='red_arrow'></i><a href='{0}' target='{1}' title='{2}'>{3}</a></li>", x.URL, x.Target, x.Comment, x.Name, x.IconCls);
            });
            lblOMPushTools.Text = sb.ToString();
        }

        /// <summary>
        /// 未处理消息
        /// </summary>
        private void BindMessage()
        {
            var list = new MessageBiz().GetTopMessage(AuthenticationPage.UserInfo.UserID, 4);
            var sb = new StringBuilder();
            var idx = 1;
            list.ForEach(x =>
            {
                sb.AppendFormat("<li><span>{0}</span><a href='{1}' target='{2}'>{3}</a></li>", idx++, x.URL, x.Target, x.MsgTitle);
            });
            lblMessage.Text = sb.ToString();
        }

        private void SetDataModulePermission()
        {
            var p = new Permission_BF();
            pnlStatistic.Visible = p.DataModulePermission(9);
        }

        protected override string NavigateID
        {
            get
            {
                return "index";
            }
        }
    }
}