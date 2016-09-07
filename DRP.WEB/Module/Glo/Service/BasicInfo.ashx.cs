using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Glo;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Glo.Service
{
    /// <summary>
    /// BasicInfo 的摘要说明
    /// </summary>
    public class BasicInfo : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询
                    QueryData(context);
                    break;
                case 2://删除
                    Delete(context);
                    break;
            }
        }

        private void QueryData(HttpContext context)
        {
            var xType = (BasicType)context.Request["xType"].ToInt();
            var list = new BasicInfo_BF().GetBasicInfo(xType);
            var s = ConvertJson.ListToJson(list);
            context.Response.Write(s);
        }

        private void Delete(HttpContext context)
        {           
            var ids=context.Request["id"];
            if (string.IsNullOrEmpty(ids))
            {
                context.Response.Write("0");
                return;
            }
            var list = new List<string>();
            foreach (var id in ids.Split(','))
            {
                list.Add(string.Format("'{0}'", id));
            }
            var isOk = new BasicInfo_BF().Delete(list);
            context.Response.Write(isOk ? "1" : "0");
        }

        protected override string GetNavigateID(HttpContext context)
        {
            var xType = (BasicType)context.Request["xType"].ToInt();
            var pageID = "";
            switch (xType)
            {
                case BasicType.Crm_CustomerType:
                    pageID = "customertype";
                    break;
                case BasicType.Pro_RouteType:
                    pageID = "routetype";
                    break;
                case BasicType.Res_GuideGrade:
                    pageID = "guidelv";
                    break;
                case BasicType.Res_HotelStar:
                    pageID = "hotelstar";
                    break;
                case BasicType.Crm_SalesTraceType:
                    pageID = "tracetype";
                    break;
                case BasicType.Fin_CollectedType:
                    pageID = "collectedtype";
                    break;
                case BasicType.Ord_OrderSource:
                    pageID = "ordersource";
                    break;
                case BasicType.Fin_InvoiceItem:
                    pageID = "invoiceitem";
                    break;
                case BasicType.Fin_InvoiceFetchType:
                    pageID = "invoicefetchtype";
                    break;
                case BasicType.Res_MotorcadeScale:
                    pageID = "motorcadescale";
                    break;
                case BasicType.Pro_QuotationStay:
                    pageID = "quotationstay";
                    break;
                case BasicType.Pro_QuotationDinner:
                    pageID = "quotationdinner";
                    break;
                case BasicType.Ord_DrawMoneyMethod:
                    pageID = "ordguidedrawmoney";
                    break;
                case BasicType.Ord_SingleBizType:
                    pageID = "ordsinglebiz";
                    break;
                case BasicType.CheckIn_IncomeSign:
                    pageID = "gloincomesign";
                    break;
                case BasicType.CheckIn_PayableSign:
                    pageID = "glopayablesign";
                    break;
                case BasicType.Crm_CredentialType:
                    pageID = "credentialType";
                    break;
                case BasicType.Pro_RouteSource:
                    pageID = "routeSource";
                    break;
            }
            return pageID;
        }

    }
}