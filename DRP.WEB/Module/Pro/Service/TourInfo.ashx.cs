using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using DRP.BF;
using DRP.BF.PageBase;
using DRP.BF.ProMrg;
using DRP.Framework;
using DRP.Framework.Core;
using System.Text;
using DRP.BF.Order;

namespace DRP.WEB.Module.Pro.Service
{
    /// <summary>
    /// 团次管理
    /// </summary>
    public class TourInfo : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1: //线路开班
                    CreateTourInfo(context);
                    break;
                case 2://团次查询
                    SearchTour(context);
                    break;
                case 3://查看团次的详细价格
                    QueryTourPrice(context);
                    break;
                case 4://删除团次
                    DeleteTour(context);
                    break;
                case 5://修改团次
                    UpdateTourInfo(context);
                    break;
                case 6://团次销售状态
                    SetTourStatus(context);
                    break;
                case 7://占座设置（团次管理）
                    OccupySeatSetting(context);
                    break;
                case 8://保存预留座位号
                    SaveLockSeat(context);
                    break;
            }
        }

        #region << 创建团次 >>

        /// <summary>
        /// 创建团次信息
        /// </summary>
        /// <param name="context"></param>
        private void CreateTourInfo(HttpContext context)
        {
            DAL.Pro_TourInfo entity = new DAL.Pro_TourInfo();
            entity.ID = Guid.NewGuid().ToString();
            entity.RouteID = context.Request["RouteID"];
            entity.TourStatus = 1;//默认为销售状态
            entity.TourName = context.Request["TourName"];
            entity.SeatNum = context.Request["SeatNum"].ToInt();
            entity.PlanNum = context.Request["PlanNum"].ToInt();
            entity.ClusterNum = context.Request["ClusterNum"].ToInt();
            entity.DefaultPrice = context.Request["DefaultPrice"].ToInt();

            var e = new CreateTourInfo();
            e.TourEntity = entity;
            e.StartDate = Convert.ToDateTime(context.Request["StDate"]);//开班日期范围
            e.EndDate = Convert.ToDateTime(context.Request["EtDate"]);
            e.EndDays = context.Request["ExpiryDate"].ToInt();//报名截止提前天数
            e.DailyTourNum = context.Request["TourNum"].ToInt();//每天开班数
            e.xType = (RuleType)context.Request["RuleType"].ToInt(); //开班规则
            e.RuleData = context.Request["RuleData"];//开班规则对应的数据
            e.TourPrices = ToTourPriceEntity(context.Request["XmlPrice"]);
            e.TourVenues = ToTourVenueEntity(context.Request["XmlVenue"]);
            var isOk = new TourInfo_BF().CreateTour(e);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 转化价格体系
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public List<DAL.Pro_TourPrice> ToTourPriceEntity(string xmlData)
        {
            var list = new List<DAL.Pro_TourPrice>();
            var xml = new XmlDocument();
            xml.LoadXml(xmlData);
            var nodes = xml.SelectNodes("document/data");
            foreach (XmlNode node in nodes)
            {
                var e = new DAL.Pro_TourPrice();
                var id = node.GetNodeValue("id");
                e.ID = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
                e.Name = node.GetNodeValue("name");
                e.SalePrice = node.GetNodeValue("salePrice").ToInt();
                e.Rebate = node.GetNodeValue("rebate").ToInt();
                e.RoomRate = node.GetNodeValue("roomRate").ToInt();
                e.IsSeat = node.GetNodeValue("isSeat").ToBoolen();
                e.IsChild = node.GetNodeValue("isChild").ToBoolen();
                e.IsDefault = node.GetNodeValue("isDefault").ToBoolen();
                e.CreateDate = e.UpdateDate = DateTime.Now;
                list.Add(e);
            }
            return list;
        }
        /// <summary>
        /// 转化集合地点
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public List<DAL.Pro_TourVenue> ToTourVenueEntity(string xmlData)
        {
            var list = new List<DAL.Pro_TourVenue>();
            var xml = new XmlDocument();
            xml.LoadXml(xmlData);
            var nodes = xml.SelectNodes("document/data");
            foreach (XmlNode node in nodes)
            {
                var e = new DAL.Pro_TourVenue();
                e.Departure = node.GetNodeValue("departure");
                e.DepartureID = node.GetNodeValue("departureID");
                e.Name = node.GetNodeValue("venueName");
                e.MeetTime = node.GetNodeValue("meetTime");
                e.PickAmt = node.GetNodeValue("pickAmt").ToInt();
                e.SendAmt = node.GetNodeValue("sendAmt").ToInt();
                list.Add(e);
            }
            return list;
        }

        #endregion

        #region << 团次查询 >>
        /// <summary>
        /// 绑定网格数据
        /// </summary>
        /// <param name="context">HTTP上下文对象</param>
        private void SearchTour(HttpContext context)
        {
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var effectiveDays = context.Request["effectiveDays"].ToInt();
            var qry = new TourCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.RouteID = context.Request["routeID"];
            qry.EffectiveDays = effectiveDays == 1 ? false : true;
            qry.IsDisabled = context.Request["IsDisabled"].ToInt() == 1;
            var dateScope = new DateScope();
            dateScope.sDate = sDate;
            dateScope.eDate = eDate;
            qry.TourDateScope = dateScope;
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TourDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new TourInfo_BF().QueryTour(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        #endregion

        #region 团次价格体系查询
        /// <summary>
        /// 团次价格查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryTourPrice(HttpContext context)
        {
            var tourID = context.Request["tourID"];
            var list = new TourInfo_BF().GetTourPrice(tourID);
            context.Response.Write(ConvertJson.ListToJson(list));
        }
        #endregion

        #region << 删除团次 >>
        /// <summary>
        /// 删除团次
        /// </summary>
        /// <param name="context"></param>
        private void DeleteTour(HttpContext context)
        {
            var ids = context.Request["id"];
            if (string.IsNullOrEmpty(ids))
                return;
            var list = new List<string>();
            list.AddRange(ids.Split(','));
            var i = new TourInfo_BF().Delete(list);
            var a = i > 0 ? 2 : i;
            context.Response.Write(a.ToString());
        }
        #endregion

        #region << 更新团次 >>

        private void UpdateTourInfo(HttpContext context)
        {
            var tour = new DAL.Pro_TourInfo();
            tour.ID = context.Request["TourID"];
            tour.TourName = context.Request["TourName"];
            tour.PlanNum = context.Request["PlanNum"].ToInt();
            tour.ClusterNum = context.Request["ClusterNum"].ToInt();
            tour.ExpiryDate = Convert.ToDateTime(context.Request["ExpiryDate"]);
            tour.SeatNum = context.Request["SeatNum"].ToInt();
            var strPrice = context.Request["XmlPrice"];
            var strVeneu = context.Request["XmlVenue"];
            var listPrice = ToTourPriceEntity(strPrice);
            var listVenue = ToTourVenueEntity(strVeneu);
            var isOk = new TourInfo_BF().UpdateTour(tour, listVenue, listPrice);
            context.Response.Write(isOk ? "1" : "0");
        }
        #endregion

        #region << 团次销售状态 >>
        /// <summary>
        /// 更新团次状态
        /// </summary>
        /// <param name="context"></param>
        private void SetTourStatus(HttpContext context)
        {
            var tourID = context.Request["tourID"];
            var status = context.Request["Status"].ToInt();
            var isOk = new TourInfo_BF().SetTourStatus(tourID, status);
            context.Response.Write(isOk ? "1" : "");
        }
        #endregion


        #region << 占座设置 >>

        /// <summary>
        /// 占座设置
        /// </summary>
        /// <param name="context"></param>
        private void OccupySeatSetting(HttpContext context)
        {
            var tourId = context.Request["tourID"];
            var tour = new TourInfo_BF().Get(tourId);
            if (tour.SeatNum == 0) context.Response.Write("<tr><td colspan='5'>座位数为0，不能设置</td></tr>");
            else
            {
                context.Response.Write(SeatChartDrawing(tour.SeatNum, tourId));
            }
        }

        #region << 绘制座位表 >>

        /// <summary>
        /// 短线座位表
        /// </summary>
        /// <param name="context"></param>
        private string SeatChartDrawing(int seatNum, string tourID)
        {
            var sb = new StringBuilder();
            var rows = seatNum % 4 == 0 ? seatNum / 4 : (seatNum / 4 + 1);
            var listOrderSeat = new Order_BF().GetTourOrderSeat(tourID);//已预订占座
            var listLockSeat = new TourInfo_BF().GetLockSeat(tourID);//预留座
            for (int i = 0; i < rows; i++)
            {
                var num = i * 4 + 1;
                sb.Append("<tr>");
                var a = SetSeatNo(seatNum, num);
                var clsName = GetClassName(listOrderSeat, listLockSeat, num);//是否被预订
                if (clsName == "yzz" || clsName == "bczt" || clsName == "lock") a = "";
                sb.AppendFormat("<td tag='{2}' style='text-align:center;' class='{0}'>{1}</td>", clsName, a, num);

                num++;

                a = SetSeatNo(seatNum, num);
                clsName = GetClassName(listOrderSeat, listLockSeat, num);
                if (clsName == "yzz" || clsName == "bczt" || clsName == "lock") a = "";
                sb.AppendFormat("<td tag='{2}' style='text-align:center;' class='{0}'>{1}</td>", clsName, a, num);

                sb.Append("<td style='text-align:center;' class='empty'></td>");

                num++;
                a = SetSeatNo(seatNum, num);
                clsName = GetClassName(listOrderSeat, listLockSeat, num);
                if (clsName == "yzz" || clsName == "bczt" || clsName == "lock") a = "";
                sb.AppendFormat("<td tag='{2}' style='text-align:center;' class='{0}'>{1}</td>", clsName, a, num);

                num++;
                a = SetSeatNo(seatNum, num);
                clsName = GetClassName(listOrderSeat, listLockSeat, num);
                if (clsName == "yzz" || clsName == "bczt" || clsName == "lock") a = "";
                sb.AppendFormat("<td tag='{2}' style='text-align:center;' class='{0}'>{1}</td>", clsName, a, num);

                sb.Append("</tr>");
            }
            return sb.ToString();
        }

        private string SetSeatNo(int seatNum, int n)
        {
            return n <= seatNum ? n.ToString() : "";
        }

        private string GetClassName(List<DAL.Ord_OrderSeat> list, List<DAL.Pro_TourSeatLock> lockSeat, int no)
        {
            var e = list.Find(x => x.SeatNum == no);
            if (e == null)
            {
                var isLocked = lockSeat.Exists(x => x.SeatNo == no);
                if (isLocked) return "lock";
                else return "seat";
            }
            else
            {
                return "yzz";
            }
        }
        #endregion

        /// <summary>
        /// 保存预留座位号
        /// </summary>
        /// <param name="context"></param>
        private void SaveLockSeat(HttpContext context)
        {
            var tourID = context.Request["tourID"];
            var seat = context.Request["SeatNo"];
            var isOk = new TourInfo_BF().SaveLockSeat(tourID, seat);
            context.Response.Write(isOk ? "1" : "");
        }

        #endregion

        protected override string NavigateID
        {
            get
            {
                return "routeinfo";
            }
        }
    }
}