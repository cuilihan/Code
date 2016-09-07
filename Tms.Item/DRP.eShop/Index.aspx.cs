using DRP.BF.eShop;
using DRP.BF.Glo;
using DRP.BF.OmMrg;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop
{
    public partial class Index : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new eShopConfig().Get(OrgID);
            if (e != null)
            {
                if (!string.IsNullOrEmpty(e.Logo))
                {
                    ImgLogo.ImageUrl = e.Logo;
                    ImgLogo.Visible = true;
                    ImgLogo.ToolTip = e.TravelName;
                }
                if (e.IsShowRoute)
                    BindRouteType(e.OrgID);
                BindAd(e.OrgID);
                lblTravelName.Text = e.TravelName;
                lblPhone.Text = string.Format("<a href='tel:{0}'>{0}</a>", e.HotLine);
                lblReg.Text = string.Format("<a style=\"text-decoration:underline; color:blue;\" href=\"Register.aspx?appid={0}\">注册</a>", OrgID);
                lblMyTravel.Text = string.Format("<a class='btn btn-lg btn-primary btn-block' href=\"/Module/My/MyTravel.aspx?appid={0}\">登录</a>", OrgID);

                //内置菜单
                var sb = new StringBuilder();
                sb.AppendFormat("<li><a class=\"menu_mytravel\" href='/Module/My/MyTravel.aspx?appid={0}'>旅行记录</a></li>",e.OrgID);
                sb.AppendFormat("<li><a class=\"menu_ticketorder\" href='/Module/Order/Ticket.aspx?appid={0}'>门票订单</a></li>",e.OrgID);
                sb.AppendFormat("<li><a class=\"menu_myinfo\" href='/Module/My/MyInfo.aspx?appid={0}'>个人中心</a></li>",e.OrgID);
                sb.AppendFormat("<li><a class=\"menu_ticketbook\" href='/Module/Sale/Ticket.aspx?appid={0}'>门票预订</a></li>",e.OrgID);
                sb.AppendFormat("<li><a class=\"menu_recommand\" href='/Module/Route/RouteList.aspx?appid={0}'>线路推荐</a></li>",e.OrgID);
                sb.AppendFormat("<li><a class=\"menu_about\" href='/AboutUs.aspx?appid={0}'>关于我们</a></li>",e.OrgID);
                lblMenus.Text = sb.ToString();
            }
        }

        /// <summary>
        /// 线路类型查询
        /// </summary>
        /// <param name="orgID"></param>
        private void BindRouteType(string orgID)
        {
            var sb = new StringBuilder();
            var list = new MenuHelper().GetRouteType(orgID);
            list.ForEach(x =>
            {
                var clsName = "nav_o";
                if (x.Name.Contains("出境") || x.Name.Contains("出国") || x.Name.Contains("境外"))
                    clsName = "nav_foreign";
                if (x.Name.Contains("国内") || x.Name.Contains("长线"))
                    clsName = "nav_civil";
                if (x.Name.Contains("短线") || x.Name.Contains("周边"))
                    clsName = "nav_bus";
                if (x.Name.Contains("自由") || x.Name.Contains("机票"))
                    clsName = "nav_free";
                if (x.Name.Contains("游轮") || x.Name.Contains("邮轮"))
                    clsName = "nav_ship";
                if (x.Name.Contains("海岛"))
                    clsName = "nav_island";
                if (x.Name.Contains("签证"))
                    clsName = "nav_visa";
                sb.AppendFormat("<a class='{3}' href='/Module/Route/RouteList.aspx?id={0}&appid={1}'>{2}</a>", x.ID, orgID, x.Name, clsName);
            }); 
            lblButtons.Text = sb.ToString();
        }

        /// <summary>
        /// 广告轮播
        /// </summary>
        /// <param name="orgID"></param>
        private void BindAd(string orgID)
        {
            var list = new eShopConfig().GetAd(orgID);
            var sb = new StringBuilder();
            list.ForEach(x =>
            {
                sb.AppendFormat("<img src=\"{0}\" onclick='location.href=\"{1}\"' />", x.ImgSrc, x.LinkUrl);
            });
            lblImages.Text = sb.ToString();
        }
    }
}