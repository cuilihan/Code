using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.ProMrg;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Pro
{
    public partial class VenueEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDeparture();
                BindRouteType();
                BindData();
            }
        }

        /// <summary>
        /// 绑定出发地
        /// </summary>
        private void BindDeparture()
        {
            DepartureID.DataSource = new Departure_BF().GetDeparture();
            DepartureID.DataTextField = "Name";
            DepartureID.DataValueField = "ID";
            DepartureID.DataBind();
        }


        /// <summary>
        /// 绑定线路类型
        /// </summary>
        private void BindRouteType()
        {
            RouteTypeID.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            RouteTypeID.DataValueField = "ID";
            RouteTypeID.DataTextField = "Name";
            RouteTypeID.DataBind();
        }

        /// <summary>
        /// 绑定表单数据
        /// </summary>
        private void BindData()
        {
            var e = new Venue_BF().Get(KeyID);
            LoadData(e, pnlContainer);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var departureName = DepartureID.SelectedItem.Text;
            var routeTypeName = GetRouteTypeName();
            var isOk = new Venue_BF().Save(KeyID, pnlContainer, departureName, routeTypeName);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }

        /// <summary>
        /// 线路类型名称
        /// </summary>
        private string GetRouteTypeName()
        {
            var list = new List<string>();
            foreach (ListItem item in RouteTypeID.Items)
            {
                if (item.Selected)
                {
                    list.Add(item.Text);
                }
            }
            return string.Join(",", list);
        }

        protected override string NavigateID
        {
            get
            {
                return "venue";
            }
        }
    }
}