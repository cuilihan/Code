using System;
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
    public partial class sTourNotice : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            //var setting = new OrgInfo_BF().Get(AuthenticationPage.UserInfo.OrgID);
            //if (!string.IsNullOrEmpty(setting.logo))
            //    img.Src = setting.logo;

            var e = new Order_BF().GetOrderInfo(KeyID);
            LoadData(e, pnlWraper);
            if (e != null)
            {
                Page.Title = e.OrderName + "-出团任务书";
                BindOrderCustomer(e.ID);

                #region 行程及标准
                var tour = new TourInfo_BF().Get(e.TourID);
                if (tour != null)
                {
                    var route = new RouteInfo_BF().Get(tour.RouteID);
                    rptSchedule.DataSource = new RouteInfo_BF().GetRouteSchedule(route.ID);
                    rptSchedule.DataBind();
                    PriceInclude.Text = route.PriceInclude;
                    PriceNonIncude.Text = route.PriceNonIncude;
                    
                    #region 特别提醒
                    if (!string.IsNullOrEmpty(route.Remind))
                    {
                        var sb = new StringBuilder();
                        sb.Append("<div style=\"margin: 20px 0px 10px 0px; font-weight: bold;\">");
                        sb.Append("特别提醒");
                        sb.Append("</div>");
                        sb.AppendFormat("<div style='border:1px solid #000;line-height:24px; padding:10px;'>{0}</div>", route.Remind);
                        lblRemind.Text = sb.ToString();
                    }
                    #endregion

                    #region 线路备注
                    if (!string.IsNullOrEmpty(route.Comment))
                    {
                        var sb = new StringBuilder();
                        sb.Append("<div style=\"margin: 20px 0px 10px 0px; font-weight: bold;\">");
                        sb.Append("备注");
                        sb.Append("</div>");
                        sb.AppendFormat("<div style='border:1px solid #000;line-height:24px; padding:10px;'>{0}</div>", route.Comment);
                        lblComment.Text = sb.ToString();
                    }
                    #endregion

                }
                #endregion
            }
        }

        private void BindOrderCustomer(string orderID)
        {
            var list = new Order_BF().GetOrderCustomer(orderID);
            rptData.DataSource = list;
            rptData.DataBind();
            
            //座位号
            var coll = new Order_BF().GetOrderSeat(orderID);
            if (coll.Count > 0)
            {
                lblSeat.Text = string.Format("<tr><td colspan='6'>座位号：{0}</td></tr>",GetOrderSeat(coll));
            }


            UserName.Text = AuthenticationPage.UserInfo.UserName;
            printDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss");
        }


        private string GetOrderSeat(List<DAL.Ord_OrderSeat> list)
        {
            var arr = new List<int>();
            foreach (var e in list)
            {
                arr.Add(e.SeatNum);
            }
            return SeatNoGroup(arr.ToArray());
        }

        /// <summary>
        /// 座位号分组显示
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private string SeatNoGroup(int[] arrData)
        {
            var arr = new List<string>();
            var len = arrData.Length;
            for (var i = 0; i < len; i++)
            {
                var list = new List<int>();
                list.Add(arrData[i]);
                for (var j = i + 1; j < arrData.Length; j++, i++)
                {
                    var __v = arrData[j];
                    var __preVal = arrData[j - 1];
                    if (__preVal + 1 != __v)
                    {
                        if (!list.Exists(x => x.Equals(__preVal)))
                            list.Add(__preVal);
                        break;
                    }
                    else if (j == len - 1)
                        list.Add(arrData[len - 1]);

                }
                arr.Add(list.Count == 1 ? list.First().ToString() : string.Join("-", list));
            }
            return string.Join(",", arr);
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
                return "salesorderown";
            }
        }
    }
}