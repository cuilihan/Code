using DRP.BF;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.BF.SysMrg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Order
{
    public partial class tOrdersToWord : Pagebase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            var tour = new TourInfo_BF().Get(KeyID);
            if (tour != null)
            {
                OrderName.Text = tour.TourName;
                TourDate.Text = tour.TourDate.ToString("yyyy-MM-dd");
                ReturnDate.Text = tour.ReturnDate.ToString("yyyy-MM-dd");

                var route = new RouteInfo_BF().Get(tour.RouteID);
                if (route != null)
                {
                    DestinationName.Text = route.Destination;
                }
            }
            TourOrderList();

            ToWordAction(Page.Title);
        }

        private void ToWordAction(string title)
        {
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + title + DateTime.Now.ToString("yyyyMMdd") + ".doc");
            Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/ms-word";
            this.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            this.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();
        }

        private void TourOrderList()
        {
            var sb = new StringBuilder();
            var tourID = KeyID;

            var deptDAL = new Dept_BF();
            var orderDAL = new Order_BF();
            var orderList = orderDAL.GetTourOrderList(tourID);
            orderList.ForEach(x =>
            {
                var customerList = orderDAL.GetOrderCustomer(x.ID);
                var rowSpan = customerList.Count;
                var dept = deptDAL.Get(x.DeptID);
                var seatList = orderDAL.GetOrderSeat(x.ID);//座位号

                #region 散客订单
                sb.Append("<tr>");
                sb.AppendFormat("<td rowspan='{0}'>", rowSpan);
                sb.AppendFormat("<div>{0}</div>", x.OrderNo);
                sb.AppendFormat("<div>收客部门：{0}</div>", dept == null ? "" : dept.Name);
                sb.AppendFormat("<div>业务员：{0}</div>", x.CreateUserName);
                sb.Append("</td>");
                sb.AppendFormat("<td rowspan='{0}'>", rowSpan);
                sb.AppendFormat(" {0}&nbsp;{1}", x.VenueName, x.CollectTime);
                sb.Append("</td>");
                sb.AppendFormat("<td rowspan='{0}'>", rowSpan);
                sb.AppendFormat("<div>成人:{0} 儿童:{1}</div>", x.AdultNum, x.ChildNum);
                var seat = GetOrderSeat(seatList);
                if (!string.IsNullOrEmpty(seat))
                    sb.AppendFormat("<div>座号：{0}</div>", seat);
                sb.Append("</td>");

                var c = rowSpan > 0 ? customerList.First() : null;
                sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>", c == null ? "" : c.Name);
                sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>", c == null ? "" : c.Mobile);
                sb.AppendFormat("<td>{0}</td>", c == null ? "" : c.IDNo);
                sb.AppendFormat("<td>{0}</td>", c == null ? "" : c.Comment);
                sb.Append("</tr>");

                for (var i = 1; i < rowSpan; i++)
                {
                    var entity = customerList[i];
                    sb.Append("<tr>");
                    sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>", entity.Name);
                    sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>", entity.Mobile);
                    sb.AppendFormat("<td>{0}</td>", c == null ? "" : entity.IDNo);
                    sb.AppendFormat("<td>{0}</td>", c == null ? "" : entity.Comment);
                    sb.Append("</tr>");
                }
                #endregion
            });

            #region 按上车地点合计
            sb.Append("<tr>");
            sb.Append("<th colspan=\"6\">上车地点</th>");
            sb.Append("<th>人数合计</th>");
            sb.Append("</tr>");
            sb.Append(SetVenueInfo(orderList));
            #endregion

            tblData.InnerHtml = sb.ToString();
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

        private string SetVenueInfo(List<DAL.Ord_OrderInfo> list)
        {
            var dict = new Dictionary<string, int>();
            list.ForEach(x =>
            {
                var key = x.VenueName + "@" + x.CollectTime;
                var n = x.AdultNum + x.ChildNum;
                if (dict.ContainsKey(key))
                {
                    dict[key] = dict[key] + n;
                }
                else
                    dict[key] = n;
            });
            var sb = new StringBuilder();
            foreach (var kp in dict)
            {
                var arr = kp.Key.Split('@');
                var venue = string.IsNullOrEmpty(arr[0]) ? "未知" : arr[0];
                sb.Append("<tr>");
                sb.AppendFormat("<td colspan='6'>{0}{1}</td>", venue, arr[1]);
                sb.AppendFormat("<td style='text-align:center;'>{0}</td>", kp.Value);
                sb.Append("</tr>");
            }
            return sb.ToString();
        }

        protected override string NavigateID
        {
            get
            {
                return "salesorderzzb";
            }
        }


        protected override bool IsWechat
        {
            get
            {
                return true;
            }
        }
    }
}