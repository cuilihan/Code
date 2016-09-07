using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.DAL;
using DRP.Framework;
using DRP.BF.Order;

namespace DRP.BF.ProMrg
{
    /// <summary>
    /// 开班规则
    /// </summary>
    public enum RuleType
    {
        /// <summary>
        /// 按天
        /// </summary>
        Daily = 1,
        /// <summary>
        /// 周几
        /// </summary>
        WeekDay = 2,
        /// <summary>
        /// 日期
        /// </summary>
        Date = 3
    }

    /// <summary>
    /// 创建开班发团的条件（相关参数数据）
    /// 创建人：李金友
    /// 创建日期：2014-10-14
    /// </summary>
    public class CreateTourInfo
    {
        /// <summary>
        /// 团次实例
        /// </summary>
        public DAL.Pro_TourInfo TourEntity { get; set; }
        /// <summary>
        /// 开班日期范围（开始日期）
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 开班日期范围（结束日期）
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 报名截止提前日期
        /// </summary>
        public int EndDays { get; set; }
        /// <summary>
        /// 每天开班数
        /// </summary>
        public int DailyTourNum { get; set; }
        /// <summary>
        /// 开班规则
        /// </summary>
        public RuleType xType { get; set; }

        /// <summary>
        /// 对应开班规则的条件
        /// </summary>
        public string RuleData { get; set; }

        /// <summary>
        /// 团次价格体系
        /// </summary>
        public List<DAL.Pro_TourPrice> TourPrices { get; set; }

        /// <summary>
        /// 团次的集合地点
        /// </summary>
        public List<DAL.Pro_TourVenue> TourVenues { get; set; }
    }

    /// <summary>
    /// 团次查询条件
    /// </summary>
    public class TourCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 线路ID
        /// </summary>
        public string RouteID { get; set; }

        /// <summary>
        /// 团次日期范围
        /// </summary>
        public DateScope TourDateScope { get; set; }

        private bool effectiveDays = true;

        /// <summary>
        /// 是否有效的团次（即出团日期小于当前日期）
        /// </summary>
        public bool EffectiveDays
        {
            get { return effectiveDays; }
            set { effectiveDays = value; }
        }

        private bool _IsDisabled = false;

        /// <summary>
        /// 是否显示已禁用的团次
        /// </summary>
        public bool IsDisabled
        {
            get { return _IsDisabled; }
            set { _IsDisabled = value; }
        }

        

        /// <summary>
        /// 线路名称
        /// </summary>
        public string TourName { get; set; }

        /// <summary>
        /// 线路类型
        /// </summary>
        public string RouteTypeID { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string DestinationID { get; set; }
    }

    /// <summary>
    /// 团次管理
    /// </summary>
    public class TourInfo_BF
    {
        DRPDB db = new DRPDB();

        #region << 开班发团 >>

        /// <summary>
        /// 开班
        /// </summary>
        /// <returns></returns>
        public bool CreateTour(CreateTourInfo e)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var route = new RouteInfo_BF().Get(e.TourEntity.RouteID);
                if (route == null) throw new Exception("未找到相关的线路信息");

                switch (e.xType)
                {
                    case RuleType.Daily://按每天发团
                        CreateTourByDaily(e, route, user);
                        break;
                    case RuleType.WeekDay: //按周几发团
                        CreateTourWeekDay(e, route, user);
                        break;
                    case RuleType.Date: //选择日期发团
                        CreateTourWeekDate(e, route, user);
                        break;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "开班发团失败");
            }
            return isSuccess;
        }

        /// <summary>
        /// 按每天发团
        /// </summary>
        /// <param name="e"></param>
        private void CreateTourByDaily(CreateTourInfo e, DAL.Pro_RouteInfo route, UserInfo user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var tourEntity = e.TourEntity;
                var dailyNum = e.DailyTourNum == 0 ? 1 : e.DailyTourNum;
                while (e.StartDate <= e.EndDate)
                {
                    for (int i = 0; i < dailyNum; i++)  //1、每天发几班
                    {
                        var tour = new DAL.Pro_TourInfo();
                        tour.ID = Guid.NewGuid().ToString();
                        tour.TourDate = e.StartDate;
                        tour.ExpiryDate = e.StartDate.AddDays(e.EndDays * -1);
                        tour.TourDays = route.ScheduleDays;
                        tour.ReturnDate = e.StartDate.AddDays(route.ScheduleDays - 1);
                        tour.CreateDate = tour.UpdateDate = DateTime.Now;
                        tour.CreateUserID = tour.UpdateUserID = user.UserID;
                        tour.CreateUserName = tour.UpdateUserName = user.UserName;
                        tour.DeptID = user.DeptID;
                        tour.OrgID = user.OrgID;
                        tour.RouteID = route.ID;
                        tour.TourNo = SerialNumberHelper.GetInstance().CreateTourNo(route, tour);
                        tour.TourStatus = tourEntity.TourStatus;
                        tour.TourName = tourEntity.TourName;
                        tour.SeatNum = tourEntity.SeatNum;
                        tour.PlanNum = tourEntity.PlanNum;
                        tour.ClusterNum = tourEntity.ClusterNum;
                        tour.TourDays = route.ScheduleDays;
                        tour.DefaultPrice = tourEntity.DefaultPrice;

                        #region 价格体系
                        foreach (var p in e.TourPrices)
                        {
                            var pEntity = new DAL.Pro_TourPrice();
                            pEntity.ID = Guid.NewGuid().ToString();
                            pEntity.TourID = tour.ID;
                            pEntity.CreateDate = pEntity.UpdateDate = DateTime.Now;
                            pEntity.CreateUserID = pEntity.UpdateUserID = user.UserID;
                            pEntity.CreateUserName = pEntity.UpdateUserName = user.UserName;
                            pEntity.DeptID = user.DeptID;
                            pEntity.OrgID = user.OrgID;
                            pEntity.Name = p.Name;
                            pEntity.SalePrice = p.SalePrice;
                            pEntity.Rebate = p.Rebate;
                            pEntity.RoomRate = p.RoomRate;
                            pEntity.IsSeat = p.IsSeat;
                            pEntity.IsChild = p.IsChild;
                            pEntity.IsDefault = p.IsDefault;
                            pEntity.Save();
                        }
                        #endregion

                        #region 集合地点
                        foreach (var v in e.TourVenues)
                        {
                            var venueEntity = new DAL.Pro_TourVenue();
                            venueEntity.ID = Guid.NewGuid().ToString();
                            venueEntity.TourID = tour.ID;
                            venueEntity.CreateUserName = venueEntity.UpdateUserName = user.UserName;
                            venueEntity.CreateDate = venueEntity.UpdateDate = DateTime.Now;
                            venueEntity.CreateUserID = venueEntity.UpdateUserID = user.UserID;
                            venueEntity.DeptID = user.DeptID;
                            venueEntity.OrgID = user.OrgID;
                            venueEntity.Name = v.Name;
                            venueEntity.MeetTime = v.MeetTime;
                            venueEntity.PickAmt = v.PickAmt;
                            venueEntity.SendAmt = v.SendAmt;
                            venueEntity.Departure = v.Departure;
                            venueEntity.DepartureID = v.DepartureID;

                            venueEntity.Save();
                        }
                        #endregion

                        tour.Save();
                    }
                    e.StartDate = e.StartDate.AddDays(e.RuleData.ToInt() + 1);
                }

                scope.Complete();

                BizUtility.WriteLog(user, "按天数开班发团");
            }
        }

        /// <summary>
        /// 按周几发团
        /// </summary>
        /// <param name="tour"></param>
        private void CreateTourWeekDay(CreateTourInfo e, DAL.Pro_RouteInfo route, UserInfo user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var tourEntity = e.TourEntity;
                var dailyNum = e.DailyTourNum == 0 ? 1 : e.DailyTourNum;
                while (e.StartDate <= e.EndDate)
                {
                    var a = (int)e.StartDate.DayOfWeek;
                    if (e.RuleData.Contains((a).ToString()))
                    {
                        for (int i = 0; i < dailyNum; i++)  //1、每天发几班
                        {
                            var tour = new DAL.Pro_TourInfo();
                            tour.ID = Guid.NewGuid().ToString();
                            tour.TourDate = e.StartDate;
                            tour.ExpiryDate = e.StartDate.AddDays(e.EndDays * -1);
                            tour.TourDays = route.ScheduleDays;
                            tour.ReturnDate = e.StartDate.AddDays(route.ScheduleDays - 1);
                            tour.CreateDate = tour.UpdateDate = DateTime.Now;
                            tour.CreateUserID = tour.UpdateUserID = user.UserID;
                            tour.CreateUserName = tour.UpdateUserName = user.UserName;
                            tour.DeptID = user.DeptID;
                            tour.OrgID = user.OrgID;
                            tour.RouteID = route.ID;
                            tour.TourNo = SerialNumberHelper.GetInstance().CreateTourNo(route, tour);

                            tour.TourStatus = tourEntity.TourStatus;
                            tour.TourName = tourEntity.TourName;
                            tour.SeatNum = tourEntity.SeatNum;
                            tour.PlanNum = tourEntity.PlanNum;
                            tour.ClusterNum = tourEntity.ClusterNum;
                            tour.TourDays = route.ScheduleDays;
                            tour.DefaultPrice = tourEntity.DefaultPrice;

                            #region 价格体系
                            foreach (var p in e.TourPrices)
                            {
                                var pEntity = new DAL.Pro_TourPrice();
                                pEntity.ID = Guid.NewGuid().ToString();
                                pEntity.TourID = tour.ID;
                                pEntity.CreateDate = pEntity.UpdateDate = DateTime.Now;
                                pEntity.CreateUserID = pEntity.UpdateUserID = user.UserID;
                                pEntity.CreateUserName = pEntity.UpdateUserName = user.UserName;
                                pEntity.DeptID = user.DeptID;
                                pEntity.OrgID = user.OrgID;
                                pEntity.Name = p.Name;
                                pEntity.SalePrice = p.SalePrice;
                                pEntity.Rebate = p.Rebate;
                                pEntity.RoomRate = p.RoomRate;
                                pEntity.IsSeat = p.IsSeat;
                                pEntity.IsChild = p.IsChild;
                                pEntity.IsDefault = p.IsDefault;
                                pEntity.Save();
                            }
                            #endregion

                            #region 集合地点
                            foreach (var v in e.TourVenues)
                            {
                                var venueEntity = new DAL.Pro_TourVenue();
                                venueEntity.ID = Guid.NewGuid().ToString();
                                venueEntity.TourID = tour.ID;
                                venueEntity.CreateUserName = venueEntity.UpdateUserName = user.UserName;
                                venueEntity.CreateDate = venueEntity.UpdateDate = DateTime.Now;
                                venueEntity.CreateUserID = venueEntity.UpdateUserID = user.UserID;
                                venueEntity.DeptID = user.DeptID;
                                venueEntity.OrgID = user.OrgID;
                                venueEntity.Name = v.Name;
                                venueEntity.MeetTime = v.MeetTime;
                                venueEntity.PickAmt = v.PickAmt;
                                venueEntity.SendAmt = v.SendAmt;
                                venueEntity.Departure = v.Departure;
                                venueEntity.DepartureID = v.DepartureID;

                                venueEntity.Save();
                            }
                            #endregion

                            tour.Save();
                        }
                    }
                    e.StartDate = e.StartDate.AddDays(1);
                }

                scope.Complete();

                BizUtility.WriteLog(user, "按周几开班发团");
            }
        }

        /// <summary>
        /// 按日期发团
        /// </summary>
        /// <param name="tour"></param>
        private void CreateTourWeekDate(CreateTourInfo e, DAL.Pro_RouteInfo route, UserInfo user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var tourEntity = e.TourEntity;
                var arrDate = e.RuleData.Split(',');
                var dailyNum = e.DailyTourNum == 0 ? 1 : e.DailyTourNum;
                foreach (var s in arrDate)
                {
                    var tourDate = Convert.ToDateTime(s);
                    for (int i = 0; i < dailyNum; i++)  //1、每天发几班
                    {
                        var tour = new DAL.Pro_TourInfo();
                        tour.ID = Guid.NewGuid().ToString();
                        tour.TourDate = tourDate;
                        tour.ExpiryDate = tourDate.AddDays(e.EndDays * -1);
                        tour.TourDays = route.ScheduleDays;
                        tour.ReturnDate = tourDate.AddDays(route.ScheduleDays - 1);
                        tour.CreateDate = tour.UpdateDate = DateTime.Now;
                        tour.CreateUserID = tour.UpdateUserID = user.UserID;
                        tour.CreateUserName = tour.UpdateUserName = user.UserName;
                        tour.DeptID = user.DeptID;
                        tour.OrgID = user.OrgID;
                        tour.RouteID = route.ID;
                        tour.TourNo = SerialNumberHelper.GetInstance().CreateTourNo(route, tour);

                        tour.TourStatus = tourEntity.TourStatus;
                        tour.TourName = tourEntity.TourName;
                        tour.SeatNum = tourEntity.SeatNum;
                        tour.PlanNum = tourEntity.PlanNum;
                        tour.ClusterNum = tourEntity.ClusterNum;
                        tour.TourDays = route.ScheduleDays;
                        tour.DefaultPrice = tourEntity.DefaultPrice;

                        #region 价格体系
                        foreach (var p in e.TourPrices)
                        {
                            var pEntity = new DAL.Pro_TourPrice();
                            pEntity.ID = Guid.NewGuid().ToString();
                            pEntity.TourID = tour.ID;
                            pEntity.CreateDate = pEntity.UpdateDate = DateTime.Now;
                            pEntity.CreateUserID = pEntity.UpdateUserID = user.UserID;
                            pEntity.CreateUserName = pEntity.UpdateUserName = user.UserName;
                            pEntity.DeptID = user.DeptID;
                            pEntity.OrgID = user.OrgID;
                            pEntity.Name = p.Name;
                            pEntity.SalePrice = p.SalePrice;
                            pEntity.Rebate = p.Rebate;
                            pEntity.RoomRate = p.RoomRate;
                            pEntity.IsSeat = p.IsSeat;
                            pEntity.IsChild = p.IsChild;
                            pEntity.IsDefault = p.IsDefault;
                            pEntity.Save();
                        }
                        #endregion

                        #region 集合地点
                        foreach (var v in e.TourVenues)
                        {
                            var venueEntity = new DAL.Pro_TourVenue();
                            venueEntity.ID = Guid.NewGuid().ToString();
                            venueEntity.TourID = tour.ID;
                            venueEntity.CreateUserName = venueEntity.UpdateUserName = user.UserName;
                            venueEntity.CreateDate = venueEntity.UpdateDate = DateTime.Now;
                            venueEntity.CreateUserID = venueEntity.UpdateUserID = user.UserID;
                            venueEntity.DeptID = user.DeptID;
                            venueEntity.OrgID = user.OrgID;
                            venueEntity.Name = v.Name;
                            venueEntity.MeetTime = v.MeetTime;
                            venueEntity.PickAmt = v.PickAmt;
                            venueEntity.SendAmt = v.SendAmt;
                            venueEntity.Departure = v.Departure;
                            venueEntity.DepartureID = v.DepartureID;

                            venueEntity.Save();
                        }
                        #endregion

                        tour.Save();
                    }
                }

                scope.Complete();

                BizUtility.WriteLog(user, "按日期开班发团");
            }
        }
        #endregion

        #region << 团次查询 >>

        public DAL.Pro_TourInfo Get(string keyID)
        {
            return DAL.Pro_TourInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 查询团次
        /// </summary> 
        /// <returns></returns>
        public DataTable QueryTour(TourCriteria qry, out int record)
        {
            var user = AuthenticationPage.UserInfo;
            var sb = new StringBuilder();
            sb.AppendFormat("1=1 and OrgID='{0}' and RouteID='{1}' and TourStatus >0", user.OrgID, qry.RouteID);
            if (qry.TourDateScope != null)
            {
                if (!string.IsNullOrEmpty(qry.TourDateScope.sDate))
                {
                    sb.AppendFormat(" and TourDate>='{0}'", qry.TourDateScope.sDate);
                }
                if (!string.IsNullOrEmpty(qry.TourDateScope.eDate))
                {
                    sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(qry.TourDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
                }
            }
            if (qry.EffectiveDays)
                sb.Append(" and EffectiveDays>=0"); 
            return db.GetPagination("V_TourInfo", qry.pageIndex, qry.pageSize, out record, sb.ToString(), qry.SortExpress);
        }


        /// <summary>
        /// 团次价格查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public List<DAL.Pro_TourPrice> GetTourPrice(string tourID)
        {
            return DAL.Pro_TourPrice.Find(x => x.TourID == tourID).OrderBy(x => x.CreateDate).ToList();
        }

        /// <summary>
        /// 团次价格详情
        /// </summary>
        /// <param name="tourPriceID"></param>
        /// <returns></returns>
        public DAL.Pro_TourPrice GetTourPriceDetail(string tourPriceID)
        {
            return DAL.Pro_TourPrice.SingleOrDefault(x => x.ID == tourPriceID);
        }

        /// <summary>
        /// 团次的上车地点
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public List<DAL.Pro_TourVenue> GetTourVenue(string tourID, string departureID)
        {
            return DAL.Pro_TourVenue.Find(x => x.TourID == tourID && x.DepartureID == departureID).OrderBy(x => x.CreateDate).ToList();
        }

        /// <summary>
        /// 团次的上车地点
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public List<DAL.Pro_TourVenue> GetTourVenue(string tourID)
        {
            return DAL.Pro_TourVenue.Find(x => x.TourID == tourID).OrderBy(x => x.CreateDate).ToList();
        }
        #endregion

        #region 团次操作

        /// <summary>
        /// 团次下是否有订单
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public bool HasOrder(string tourID)
        {
            return DAL.Ord_OrderInfo.Exists(x => x.TourID == tourID);
        }

        public bool HasVisitor(string tourID)
        {
            var sql = string.Format("select * from V_TourInfo where ID='{0}'", tourID);

            var dt = new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
            DataRow dr = dt.Rows[0];
            if ((int)dr["VisitorNum"] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除团次
        /// </summary>
        /// <param name="listTourID"></param>
        /// <returns>返回有订单的团次记录数</returns>
        public int Delete(List<string> listTourID)
        {
            var iCount = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    listTourID.ForEach(x =>
                    {
                        if (HasVisitor(x)) //有报名人数的不能删除
                        {
                            //DAL.Pro_TourVenue.Delete(v => v.TourID == x);
                            //DAL.Pro_TourPrice.Delete(p => p.TourID == x);
                            //DAL.Pro_TourInfo.Delete(t => t.ID == x);
                            var sql = string.Format("update Pro_TourInfo set TourStatus=-1 where id='{0}'", x);
                            new SubSonic.Query.CodingHorror(sql).Execute();
                        }
                        else
                            iCount++;
                    });
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除团次失败");
            }
            return iCount;
        }

        /// <summary>
        /// 更新团次状态
        /// </summary>
        /// <param name="tourID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool SetTourStatus(string tourID, int status)
        {
            try
            {
                var tour = Get(tourID);
                tour.TourStatus = status;
                tour.Save();
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "更新团次状态时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 更新团次
        /// </summary>
        /// <param name="e"></param>
        /// <param name="xmlVenue"></param>
        /// <param name="xmlPrice"></param>
        /// <returns></returns>
        public bool UpdateTour(DAL.Pro_TourInfo e, List<DAL.Pro_TourVenue> listVenue, List<DAL.Pro_TourPrice> listPrice)
        {
            var user = AuthenticationPage.UserInfo;
            var tourIDArr = new List<string>();
            try
            {
                if (e.ID.IndexOf(",") > -1)
                {
                    tourIDArr.AddRange(e.ID.Split(','));
                }
                else
                    tourIDArr.Add(e.ID);

                using (TransactionScope scope = new TransactionScope())
                {
                    tourIDArr.ForEach(x =>
                    {
                        #region 团次主表

                        var entity = Get(x);
                        entity.TourName = e.TourName;
                        entity.PlanNum = e.PlanNum;
                        entity.ClusterNum = e.ClusterNum;
                        entity.ExpiryDate = e.ExpiryDate;
                        entity.UpdateDate = DateTime.Now;
                        entity.UpdateUserID = user.UserID;
                        entity.SeatNum = e.SeatNum;
                        entity.UpdateUserName = user.UserName;
                        #endregion

                        #region 价格体系
                        //remove repeat item
                        var tourPrice = DAL.Pro_TourPrice.Find(t => t.TourID == entity.ID).ToList();

                        foreach (var p in listPrice)
                        {
                            var __p = tourPrice.Find(item => item.ID == p.ID);
                            if (__p != null) tourPrice.Remove(__p);

                            var pEntity = GetTourPriceDetail(p.ID);
                            if (pEntity == null)
                            {
                                pEntity = new DAL.Pro_TourPrice();
                                pEntity.ID = Guid.NewGuid().ToString();
                                pEntity.TourID = entity.ID;
                                pEntity.CreateDate = DateTime.Now;
                                pEntity.CreateUserID = user.UserID;
                                pEntity.CreateUserName = user.UserName;
                                pEntity.DeptID = user.DeptID;
                                pEntity.OrgID = user.OrgID;
                            }
                            pEntity.UpdateDate = DateTime.Now;
                            pEntity.UpdateUserID = user.UserID;
                            pEntity.UpdateUserName = user.UserName;
                            pEntity.Name = p.Name;
                            pEntity.SalePrice = p.SalePrice;
                            pEntity.Rebate = p.Rebate;
                            pEntity.RoomRate = p.RoomRate;
                            pEntity.IsSeat = p.IsSeat;
                            pEntity.IsChild = p.IsChild;
                            pEntity.IsDefault = p.IsDefault;
                            pEntity.Save();

                            if (pEntity.IsDefault)//默认价格
                                entity.DefaultPrice = pEntity.SalePrice;
                        }

                        tourPrice.ForEach(item =>
                            {
                                DAL.Pro_TourPrice.Delete(i => i.ID == item.ID);
                            });

                        #endregion

                        entity.Save();//价格后面保存为了计算默认价格

                        #region 集合地点
                        DAL.Pro_TourVenue.Delete(t => t.TourID == entity.ID);

                        foreach (var v in listVenue)
                        {
                            var venueEntity = new DAL.Pro_TourVenue();
                            venueEntity.ID = Guid.NewGuid().ToString();
                            venueEntity.TourID = entity.ID;
                            venueEntity.CreateUserName = venueEntity.UpdateUserName = user.UserName;
                            venueEntity.CreateDate = venueEntity.UpdateDate = DateTime.Now;
                            venueEntity.CreateUserID = venueEntity.UpdateUserID = user.UserID;
                            venueEntity.DeptID = user.DeptID;
                            venueEntity.OrgID = user.OrgID;
                            venueEntity.Name = v.Name;
                            venueEntity.MeetTime = v.MeetTime;
                            venueEntity.PickAmt = v.PickAmt;
                            venueEntity.SendAmt = v.SendAmt;
                            venueEntity.Departure = v.Departure;
                            venueEntity.DepartureID = v.DepartureID;

                            venueEntity.Save();
                        }
                        #endregion
                    });

                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "更新团次发生错误");
                return false;
            }
        }
        #endregion

        #region 团次座位

        /// <summary>
        /// 获取预留座位
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public List<DAL.Pro_TourSeatLock> GetLockSeat(string tourID)
        {
            return DAL.Pro_TourSeatLock.Find(x => x.TourID == tourID).ToList();
        }

        public bool SaveLockSeat(string tourID, string seatNo)
        {
            var user = AuthenticationPage.UserInfo;
            var orderSeat = new Order_BF().GetTourOrderSeat(tourID);//是否被预订
            try
            {
                DAL.Pro_TourSeatLock.Delete(x => x.TourID == tourID);
                if (!string.IsNullOrEmpty(seatNo))
                {
                    var arr = seatNo.Split(',');
                    foreach (var n in arr)
                    {
                        if (!orderSeat.Exists(x => x.SeatNum == n.ToInt())) //未被预订才可以预留
                        {
                            DAL.Pro_TourSeatLock e = new Pro_TourSeatLock();
                            e.ID = Guid.NewGuid().ToString();
                            e.TourID = tourID;
                            e.CreateDate = e.UpdateDate = DateTime.Now;
                            e.CreateUserName = e.UpdateUserName = user.UserName;
                            e.CreateUserID = e.UpdateUserID = user.UserID;
                            e.OrgID = user.OrgID;
                            e.DeptID = user.DeptID;
                            e.SeatNo = n.ToInt();
                            e.Save();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "设置座位时发生错误");
                return false;
            }
        }
        #endregion
    }
}
