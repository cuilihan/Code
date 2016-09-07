﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.BF.OmMrg;

namespace DRP.WEB.Module.Order
{
    public partial class zTourNotice : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var org = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            if (org != null)
            {
                OrgName.Text = org.Name;
                OrgBrand.Text = string.IsNullOrEmpty(org.Brand) ? org.Name : org.Brand;
            }

            var e = new TourInfo_BF().Get(KeyID);
            LoadData(e, pnlWraper);
            if (e != null)
            {
                Page.Title = e.TourName + "-出团任务书";
                var route = new RouteInfo_BF().Get(e.RouteID);
                var tourExtend = new Order_BF().GetOrderExtend(e.ID);
                if (route != null)
                {
                    PriceInclude.Text = route.PriceInclude;
                    PriceNonIncude.Text = route.PriceNonIncude;
                }
                if (tourExtend != null)
                {
                    VisitorNum.Text = (tourExtend.AdultNum + tourExtend.ChildNum).ToString();
                }

                #region 导游
                var guideList = new Order_BF().GetOrderGuide(e.ID);
                var data = new List<string>();
                guideList.ForEach(x =>
                {
                    data.Add(x.GuideName + "(" + x.Mobile + ")");
                });
                GuideName.Text = string.Join(",", data);
                #endregion

                #region 行程
                rptData.DataSource = new RouteInfo_BF().GetRouteSchedule(route.ID);
                rptData.DataBind();
                #endregion

                UserName.Text = AuthenticationPage.UserInfo.UserName;
                printDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss");

                #region 预算备注
                if (!string.IsNullOrEmpty(route.Comment))
                    lblComment.Text = route.Comment;
                #endregion
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }


        protected override string NavigateID
        {
            get
            {
                return "salesorderzzb";
            }
        }
    }
}