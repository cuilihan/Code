using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.Framework;

namespace DRP.WEB.Module.Glo
{
    public partial class BasicInfo : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string NavigateID
        {
            get
            {
                var xType = (BasicType)Request["xType"].ToInt();
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
}