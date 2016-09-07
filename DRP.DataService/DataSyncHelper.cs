using DRP.BF;
using DRP.DAL;
using DRP.DataService.Entity;
using Newtonsoft.Json;
using SubSonic.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;

namespace DRP.DataService
{
    /// <summary>
    /// 将八爪鱼订单数据写入本地数据库
    /// </summary>
    public class DataSyncHelper
    {
        /// <summary>
        /// 同步订单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SyncData(BTBDataEntity entity)
        {
            #region 1.写入日志
            if (entity == null)
            {
                FileLog.Instance.Write("BTB推送的订单内容为空值");
                return false;
            }
            else
            {
                FileLog.Instance.Write("AppId=" + entity.AppId + ",Sign=" + entity.Sign + ",Expires=" + entity.Expires + ",xmlData:" +
                    System.Environment.NewLine + entity.OrderXml);
            }
            #endregion

            #region 2.同步配置信息
            var ota = GetOTASetting();
            if (ota == null)
            {
                FileLog.Instance.Write("未查询到主账号" + entity.MastAcct + "对应的配置同步信息");
                return false;
            }
            #endregion

            #region 3.订单写入DB

            bool isOk = true;
            try
            {
                var t = new OrderXmlToEntity().ConvertXmlToEntity(entity.OrderXml, ota.OTAName);

                UsingTransactionScope(() =>
                {
                    //3.1增加订单主表信息，若主键存在，则根据配置的参数是否同步更新
                    bool isAdd = false;
                    AddOrderInfo(t, out isAdd, ota.SyncType == 1);

                    //3.2写入订单的游客信息
                    AddCustomerInfo(t, isAdd);

                    //3.3写入订单的成本信息
                    AddOrderCost(t, ota);

                    //3.4订单日志 
                    AddOrderLog(t.ID, isAdd);

                    //3.5消息提醒
                    AddMessage(isAdd, t);
                });
            }
            catch (Exception ex)
            {
                isOk = false;
                FileLog.Instance.Write("写入订单数据时发生错误:" + System.Environment.NewLine + ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
            #endregion

            return isOk;
        }

        /// <summary>
        /// 根据同步主账号查询相关的配置信息
        /// </summary>
        /// <param name="syncAcct"></param>
        /// <returns></returns>
        private DAL.OTA_Setting GetOTASetting()
        {
            //此处指定对接的平台为三三旅游与八爪鱼平台，在SaaS系统中考虑怎么获取当前SaaS系统对应的OTA
            return DAL.OTA_Setting.SingleOrDefault(x => x.OTAID == Guid.Parse("8B0CEA3D-CD8D-4DE8-B0BE-6A292BCF745E"));
        }

        /// <summary>
        /// 订单基本信息
        /// </summary>
        /// <param name="isSyncUpdate">同步更新订单，true时同步更新订单，否则只创建新订单</param>
        /// <param name="entity"></param>
        private void AddOrderInfo(BTBOrderEntity e, out bool isAdd, bool isSyncUpdate = true)
        {
            var order = DAL.Ord_OrderInfo.SingleOrDefault(x => x.ID == e.ID.ToString());
            isAdd = order == null;
            var sql = string.Empty;
            if (order == null)
            {
                #region Insert
                sql = @"INSERT INTO [OrderInfo]([ID],[OrderType],[AreaID],[AreaPath],[AreaPathID],[RouteName],[AdultNum],[AdultAmt],[ChildNum],[ChildAmt],[AdjustAmt],[Receivable] ,[TourDate] ,[TourDays],[Comment],[CreateDate],[Creator],[CreatorID],[DeptID],[Modifier],[ModifyDate],[DataStatus],[Schedule],[Standard],[Venue],[CollectionTime],[JAmount],[SAmount],[Period],[OrderCategory],[RouteType],IsSync,SyncDate,OTAOrderState,OTAName) VALUES (@ID,@OrderType,@AreaID,@AreaPath,@AreaPathID,@RouteName,@AdultNum,@AdultAmt,@ChildNum,@ChildAmt ,@AdjustAmt ,@Receivable ,@TourDate,@TourDays,@Comment ,@CreateDate,@Creator,@CreatorID ,@DeptID,@Modifier,@ModifyDate,@DataStatus,@Schedule,@Standard,@Venue,@CollectionTime,@JAmount ,@SAmount ,@Period,@OrderCategory,@RouteType,@IsSync,@SyncDate,@OTAOrderState,@OTAName)";

                new SubSonic.Query.CodingHorror(sql, e.ID, e.OrderType, e.AreaID, e.AreaPath, e.AreaPathID, e.RouteName, e.AdultNum, e.AdultAmt, e.ChildNum, e.ChildAmt, e.AdjustAmt, e.Receivable, e.TourDate, e.TourDays, e.Comment, e.CreateDate, e.Creator, e.CreatorID, e.DeptId, e.Modifier, e.ModifyDate, e.DataStatus, e.Schedule, e.Standard, e.Venue, e.CollectionTime, e.JAmount, e.SAmount, e.Period, e.OrderCategory, e.RouteType, e.IsSync, e.SyncDate, e.OTAOrderStatus, e.OTAName).Execute();
                #endregion
            }
            else if (!isSyncUpdate)
            {
                #region Update
                sql = @"UPDATE [OrderInfo] SET [AreaID] = @AreaID ,[AreaPath] = @AreaPath  ,[AreaPathID]=@AreaPathID ,[RouteName] = @RouteName ,[AdultNum] = @AdultNum ,[AdultAmt] = @AdultAmt ,[ChildNum] = @ChildNum  ,[AdjustAmt] = @AdjustAmt ,[Receivable] = @Receivable ,[TourDate] = @TourDate ,[TourDays] = @TourDays ,[Comment] = @Comment ,[Modifier] = @Modifier ,[ModifyDate] = @ModifyDate  ,[Schedule]=@Schedule,[Standard]=@Standard,[Venue]=@Venue,[CollectionTime]=@CollectionTime,[JAmount]=@JAmount ,[SAmount]=@SAmount ,[OrderCategory]=@OrderCategory ,[RouteType]=@RouteType,IsSync=@IsSync,SyncDate=@SyncDate,OTAOrderState=@OTAOrderState,OTAName=@OTAName WHERE [ID] = @ID";

                new SubSonic.Query.CodingHorror(sql, e.AreaID, e.AreaPath, e.AreaPathID, e.RouteName, e.AdultNum, e.AdultAmt, e.ChildNum, e.AdjustAmt, e.Receivable, e.TourDate, e.TourDays, e.Comment, e.Modifier, e.ModifyDate, e.Schedule, e.Standard, e.Venue, e.CollectionTime, e.JAmount, e.SAmount, e.OrderCategory, e.RouteType, e.IsSync, e.SyncDate, e.OTAOrderStatus, e.OTAName,e.ID).Execute();
                #endregion
            }
        }

        /// <summary>
        /// 游客信息
        /// </summary>
        /// <param name="entity"></param>
        private void AddCustomerInfo(BTBOrderEntity order, bool isAddOrder)
        {
            var sql = "Delete OrderVisitor where OrderID=@ID";
            new SubSonic.Query.CodingHorror(sql, order.ID).Execute();
            var list = order.CustomerList;
            foreach (var c in list)
            {
                if (c == null || string.IsNullOrEmpty(c.cName)) continue;

                // l = "INSERT INTO [OrderVisitor]([ID],[OrderID],[CustomerID],[cName],[cSex],[cPhone],[pSort],[Company],[IDCard],[Comment],[IsLeader]) VALUES (@ID,@OrderID,@CustomerID,@cName,@cSex,@cPhone,@pSort,@Company,@IDCard,@Comment,@IsLeader)";
                
                //客户主表
                if (isAddOrder)
                {
                    //todo:根据姓名和手机号判断用户是否重复，如果手机号为空根据身份证
                    bool HasOne = false;
                    if (!string.Empty.Equals(c.cPhone) || c.cPhone == null)
                    {
                        var cus = DAL.Crm_Customer.SingleOrDefault(x => x.Phone == c.cPhone && c.cName == x.Name);
                        if (cus != null)
                        {
                            HasOne = true;
                        }
                    }
                    else
                    {
                        var cus = DAL.Crm_Customer.SingleOrDefault(x => x.IDNum == c.IDCard && c.cName == x.Name);
                        if (cus != null)
                        {
                            HasOne = true;
                        }
                    }
                    if (HasOne)
                    {
                        return;
                    }

                    sql = string.Format("INSERT INTO [Customer]([ID],[cName],[Sex],[Mobile],[Comment],[Company],[CreateDate],[Creator],[CreatorID],[DeptID]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", c.CustomerID, c.cName, (c.cSex == "0"?"女":"男"), c.cPhone, c.Comment, c.Company, DateTime.Now, order.Creator, order.CreatorID, order.DeptId);
                    new SubSonic.Query.CodingHorror(sql).Execute();
                }

                sql = string.Format("INSERT INTO [OrderVisitor]([ID],[OrderID],[CustomerID],[cName],[cSex],[cPhone],[pSort],[Company],[IDCard],[Comment],[IsLeader]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", c.ID, c.OrderID, c.CustomerID, c.cName, (c.cSex == "0" ? "女" : "男"), c.cPhone, c.pSort, c.Company, c.IDCard, c.Comment, 0);
                new SubSonic.Query.CodingHorror(sql).Execute();

                if (isAddOrder) //需要根据CustomerID 更新这个人的备注
                {
                    var str = order.TourDate.ToString("yyyy年MM月dd日") + " 到 " + order.AreaPath + "  散客订单（成交） ";
                    sql = @"INSERT INTO [CustomerRemark]([ID],[CustomerID],[Recomment],[ReDate],[CreateDate],[Creator],[CreatorID],[DeptID]) VALUES(@ID,@CustomerID,@Recomment,@ReDate,@CreateDate,@Creator,@CreatorID,@DeptID)";
                    new SubSonic.Query.CodingHorror(sql, Guid.NewGuid(), c.CustomerID, str, DateTime.Now, DateTime.Now, order.Creator, order.CreatorID, order.DeptId).Execute();
                }
            }
        }

        /// <summary>
        /// 订单成本
        /// </summary>
        /// <param name="order"></param>
        private void AddOrderCost(BTBOrderEntity order, OTA_Setting ota)
        {
            var sql = "Delete OrderCost where OrderID=@ID and [Comment] like '%自动同步%结算价%'";
            new SubSonic.Query.CodingHorror(sql, order.ID).Execute();

            sql = @"INSERT INTO [OrderCost]([ID],[OrderID],[ItemType],[ItemID],[Cost],[pSort],[Comment]) VALUES(@ID,@OrderID,@ItemType,@ItemID,@Cost,@pSort,@Comment)";
            new SubSonic.Query.CodingHorror(sql, Guid.NewGuid(), order.ID, "地接社", ota.OTAID, order.OrderCost, 0, "自动同步" + ota.OTAName + "结算价").Execute();

        }

        /// <summary>
        /// 订单日志
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="uname"></param>
        private void AddOrderLog(Guid orderID, bool isAddOrder)
        {
            var title = isAddOrder ? "同步并创建新订单" : "同步修改订单";
            try
            {
                //var username = "系统自动同步";
                //var log = new OrderLog();
                //log.ID = Guid.NewGuid();
                //log.OrderID = orderID;
                //log.LogTitle = title;
                //log.LogContent = "同步OTA订单信息";
                //log.LogDate = DateTime.Now;
                //log.LogIP = "";
                //log.Creator = username;
                //log.Save();
            }
            catch { }
        }

        /// <summary>
        /// 创建新订单时发消息提醒
        /// </summary>
        /// <param name="isAddOrder"></param>
        /// <param name="order"></param>
        private void AddMessage(bool isAddOrder, BTBOrderEntity order)
        {
            if (!isAddOrder) return;
            try
            {
                //var url = "/Module/Order/tOrderInfo.aspx?id=" + order.ID.ToString();
                //IDRPMessage msg = DRPIM.CreateMessage(MessageType.PopMessage);
                //var users = new Remind_BF().QueryNewOrderRemind(order.RouteType);//新订单提醒：有目的地查询权限的计调才能收到提醒
                //var title = isAddOrder ? "同步创建新订单" : "同步修改订单";
                //foreach (var u in users)
                //{
                //    DRPMessage m = new DRPMessage();
                //    m.MessageID = Guid.NewGuid();
                //    m.Subject = title + ":" + order.RouteName;
                //    m.Url = url + "&mid=" + m.MessageID;
                //    m.Target = "_blank";
                //    m.Sender = order.Creator;
                //    m.SenderID = order.CreatorID;
                //    m.Receiver = u.UserName;
                //    m.ReceiverID = u.UserID;
                //    msg.Send(m);
                //}
            }
            catch { }
        }

        /// <summary>
        /// 使用事务写入数据，失败时自动尝试3次
        /// </summary>
        /// <param name="act"></param>
        private void UsingTransactionScope(Action act)
        {
            using (var ts = new TransactionScope())
            {
                var retries = 3;//高并发时线程阻塞导致失败时重试3次
                var succeeded = false;
                while (!succeeded)
                {
                    try
                    {
                        act();
                        ts.Complete();//事务完成
                        succeeded = true;
                    }
                    catch (Exception ex)
                    {
                        if (retries >= 0)
                            retries--;
                        else
                            throw ex;
                    }
                }
            }
        }
    }
}
