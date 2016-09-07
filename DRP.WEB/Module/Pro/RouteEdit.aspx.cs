using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.ProMrg;

namespace DRP.WEB.Module.Pro
{
    public partial class RouteEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRouteType();
                BindRouteSource();
                BindData();
            }
        }

        private void BindRouteSource()
        {
            RouteSource.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteSource);
            RouteSource.DataTextField = "Name";
            RouteSource.DataValueField = "ID";
            RouteSource.DataBind();
        }

        private void BindRouteType()
        {
            RouteType.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            RouteType.DataTextField = "Name";
            RouteType.DataValueField = "ID";
            RouteType.DataBind();
        }

        private void BindData()
        {
            var e = new RouteInfo_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                RouteType.SelectedValue = e.RouteTypeID;
                RouteSource.SelectedValue = e.RouteSourceID;
                BindSchedule();
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "routeinfo";
            }
        }

        #region << 线路行程 >>

        private void BindSchedule()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                rptSchedule.DataSource = new RouteInfo_BF().GetRouteSchedule(KeyID);
                rptSchedule.DataBind();
            }
        }

        protected void rptSchedule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblDayNum") as Literal;
                lblNo.Text = DataBinder.Eval(e.Item.DataItem, "DayNum").ToString();
            }
        }

        #endregion
    }
}