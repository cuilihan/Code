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
    public partial class OpenTour : Pagebase
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            var route = new RouteInfo_BF().Get(KeyID);
            if (route == null) return;
            txtTourName.Text = route.RouteName;
            if (route.RouteType.IndexOf("短线") == -1)
            {
                txtSeatNum.Enabled = false;
            } 

            #region 报名截止日期
            var dict = new Dictionary<string, string>();
            for (int i = 1; i < 30; i++)
            {
                ddlExpiryDate.Items.Add(new ListItem("提前"+i+"天",i.ToString()));               
            }
            ddlExpiryDate.Items.Add(new ListItem("提前1个月","30"));
            ddlExpiryDate.Items.Add(new ListItem("提前1.5个月", "45"));
            ddlExpiryDate.Items.Add(new ListItem("提前2个月", "60"));
            ddlExpiryDate.Items.Add(new ListItem("提前3个月", "90")); 
            #endregion 

            #region 出发地
            Departure.DataSource = new Departure_BF().GetDeparture();
            Departure.DataTextField = "Name";
            Departure.DataValueField = "ID";
            Departure.DataBind();
            #endregion

            BindVenue(route.RouteTypeID);
        }

        /// <summary>
        /// 绑定集合地点
        /// </summary>
        private void BindVenue(string routeTypeID)
        {
            rptVenue.DataSource = new Venue_BF().GetVenue(routeTypeID);  
            rptVenue.DataBind();
        } 

        protected override string NavigateID
        {
            get
            {
                return "routeinfo";
            }
        }
    }
}