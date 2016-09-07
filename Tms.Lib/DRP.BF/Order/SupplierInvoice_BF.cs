using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.DAL.DataAccess;
using DRP.Framework.Core;

namespace DRP.BF.Order
{
    /// <summary>
    /// 供应商发票管理
    /// </summary>
    public class SupplierInvoice_BF
    {
        /// <summary>
        /// 订单供应商发票查询
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_SupplierInvoice> GetInovice(string orderID)
        {
            return DAL.Ord_SupplierInvoice.Find(x => x.OrderID == orderID).OrderBy(x => x.CreateDate).ToList();
        }

        public DAL.Ord_SupplierInvoice Get(string keyID)
        {
            return DAL.Ord_SupplierInvoice.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, string orderID, WebControl pnlControl)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var entity = Get(keyID);
                    if (entity == null)
                    {
                        entity = new Ord_SupplierInvoice();
                        entity.OrgID = user.OrgID;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CreateDate = DateTime.Now;
                        entity.OrderID = orderID;
                    }
                    entity = new ReflectHelper<DAL.Ord_SupplierInvoice>().AssignEntity(entity, pnlControl);
                    entity.ID = keyID;
                    entity.Save();

                    //更新订单表付款开票金额
                    var order = new Order_BF().GetOrderInfo(orderID);
                    if (order == null)
                    {
                        var tOrder = new TicketOrder_BF().Get(orderID); //机票订单
                        if (tOrder != null)
                        {
                            tOrder.CostInvoiceAmt = GetInvoiceAmt(orderID);
                            tOrder.Save();
                        }
                    }
                    else //旅游订单
                    {
                        order.CostInvoiceAmt = GetInvoiceAmt(orderID);
                        order.Save();
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "保存供应商发票时发生错误");
            }
            return isSuccess;
        }

        private decimal GetInvoiceAmt(string orderID)
        {
            return new SupplierInvoiceDAL().GetInvoiceAmt(orderID, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public bool Delete(string[] ids)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var dal = new Order_BF();
                    foreach (var id in ids)
                    {
                        var e = Get(id);
                        //更新订单开票金额
                        var order = dal.GetOrderInfo(e.OrderID);
                        order.CostInvoiceAmt -= e.InvoiceAmt;
                        order.Save();

                        e.Delete();
                    }
                    scope.Complete();

                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "删除付款发票时发生错误");
                return false;
            }
        }
    }
}
