using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Fin;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Fin.Service
{
    /// <summary>
    /// 银行收入流水明细管理
    /// </summary>
    public class CollectedItem : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询
                    QueryData(context);
                    break;
                case 2://收款确认
                    CollectedConfirm(context);
                    break;
                case 3://取消收款确认
                    CollectedCanceled(context);
                    break;
                case 4://删除收款明细
                    DeleteData(context);
                    break;
                case 5://取消认领
                    CollectedClaimCanceled(context);
                    break;
                case 6://取消认领时删除
                    DeleteClaim(context);
                    break;
                case 7:
                    DeleteOrderCollected(context);
                    break;
            }
        }


        /// <summary>
        /// 银行收入查询
        /// </summary>
        /// <param name="context"></param>
        private void QueryData(HttpContext context)
        {
            var qry = new CollectionItemCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.BankName = context.Request["BankName"];
            qry.sDate = context.Request["sDate"];
            qry.eDate = context.Request["eDate"];
            qry.MaxIncome = context.Request["MaxAmt"].ToDecimal();
            qry.MinIncome = context.Request["MinAmt"].ToDecimal();
            qry.FromBank = context.Request["FromBank"];
            qry.FromAcct = context.Request["FromAcct"];
            qry.DataStatus = (CollectedStatus)context.Request["Status"].ToInt();

            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TradeDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new CollectedItem_BF().QueryData(qry, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }


        #region 收款确认与取消

        /// <summary>
        /// 订单收款确认 [从订单中确认]
        /// </summary>
        /// <param name="context"></param>
        private void CollectedConfirm(HttpContext context)
        {
            try
            {
                var id = context.Request["id"];
                var isSign = context.Request["isSign"].ToInt() == 1;
                var isFin = context.Request["src"].Equals("Fin");//是否是从银行收款明细管理中确认
                var dal = new CollectedItem_BF();
                var isOk = dal.CollectedClaimConfirmed(id, isSign, isFin);
                context.Response.Write(isOk ? "1" : "");
            }
            catch
            {
                context.Response.Write("");
            }
        }

        /// <summary>
        /// 取消已确认的收款
        /// </summary>
        /// <param name="context"></param>
        private void CollectedCanceled(HttpContext context)
        {
            var keyID = context.Request["id"];
            var isSign = context.Request["isSign"].ToInt() == 1;
            var strFin = context.Request["src"];
            var isFin = string.IsNullOrEmpty(strFin) ? false : strFin.Equals("Fin");//是否是从银行收款明细管理中取消
            var isOk = new CollectedItem_BF().CollectedConfirmedCanceled(keyID, isSign, isFin);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 取消认领
        /// </summary>
        /// <param name="context"></param>
        private void CollectedClaimCanceled(HttpContext context)
        {
            var keyID = context.Request["id"];
            var isOk = new CollectedItem_BF().CollectedClaimCanceled(keyID);
            context.Response.Write(isOk ? "1" : "0");
        }

        #endregion


        /// <summary>
        /// 删除收款明细
        /// </summary>
        /// <param name="context"></param>
        private void DeleteData(HttpContext context)
        {
            var id = context.Request["id"];
            if (string.IsNullOrEmpty(id))
                return;
            new CollectedItem_BF().Delete(id.Split(','));
            context.Response.Write("1");
        }

        private void DeleteClaim(HttpContext context)
        {
            var id = context.Request["id"];
            var claimID = context.Request["ClaimID"];
            var isOk = new CollectedItem_BF().DeleteClaimCanceled(id, claimID);
            context.Response.Write(isOk ? "1" : "0");
        }

        private void DeleteOrderCollected(HttpContext context)
        {
            var id = context.Request["id"];
            var isSign = context.Request["isSign"].ToInt() == 1;
            var strFin = context.Request["src"];
            var isFin = string.IsNullOrEmpty(strFin) ? false : strFin.Equals("Fin");//是否是从银行收款明细管理中取消
            var isOk = new CollectedItem_BF().DeleteOrderCollectedList(id, isSign, isFin);
            context.Response.Write(isOk ? "1" : "0");
        }

        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }
    }
}