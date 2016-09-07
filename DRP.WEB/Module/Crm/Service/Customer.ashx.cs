using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using DRP.BF;
using DRP.BF.Crm;
using DRP.BF.CrmMrg;
using DRP.BF.Glo;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Crm.Service
{
    /// <summary>
    /// 客户管理
    /// </summary>
    public class Customer : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://查询
                    QueryCustomerInfo(context);
                    break;
                case 2://删除
                    Delete(context);
                    break;
                case 3://客户销售线路（销售机会）类型
                    CustomerTraceType(context);
                    break;
                case 4://验证手机号码是否存在
                    ValidMobile(context);
                    break;
                case 5://保存客户
                    SaveCustomerInfo(context);
                    break;
                case 6://删除销售线索
                    DeleteTrace(context);
                    break;
                case 7://查询客户的销售线索
                    QueryCustomerTrace(context);
                    break;
                case 8://客户的订单查询
                    TradeOrder(context);
                    break;
                case 9://客户生日提醒查询
                    QueryCustomerBirthData(context);
                    break;
                case 10://客户证件类型
                    QueryCredentialType(context);
                    break;
                case 11://默认只显示自己的客户，否则可以查询所有用户
                    QueryMyCustomer(context);
                    break;
            }
        }

        /// <summary>
        /// 查询客户
        /// </summary>
        private void QueryCustomerInfo(HttpContext context)
        {
            var qry = new CustomCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.DeptID = context.Request["deptID"];
            qry.CreatorID = context.Request["userID"];
            qry.Name = context.Request["key"];
            qry.CustomerType = context.Request["customerType"];
            qry.Mobile = context.Request["mobile"];
            qry.Company = context.Request["company"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var list = new Customer_BF().QueryData(qry, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        /// <summary>
        /// 客户生日提醒查询
        /// </summary>
        private void QueryCustomerBirthData(HttpContext context)
        {
            var qry = new CustomCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var key = context.Request["key"];
            var birthdayType = BirthDayType.CurrentMonth;
            if (key.Equals("c"))
            {
                birthdayType = BirthDayType.CurrentMonth;
            }
            else if (key.Equals("d"))
            {
                birthdayType = BirthDayType.Today;
            }
            else
            {
                birthdayType = BirthDayType.NextMonth;
            }
            var total = 0;
            var list = new Customer_BF().QueryCustomerBirth(qry, birthdayType, out total);
            var json = ConvertJson.ListToJson(list);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }



        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            var keyID = context.Request["id"];
            if (string.IsNullOrEmpty(keyID))
                context.Response.Write("0");
            else
            {
                var list = new List<string>();
                foreach (var s in keyID.Split(','))
                {
                    list.Add(string.Format("'{0}'", s));
                }
                new Customer_BF().Delete(list);
                context.Response.Write("1");
            }
        }

        /// <summary>
        /// 销售机会类型
        /// </summary>
        /// <param name="context"></param>
        private void CustomerTraceType(HttpContext context)
        {
            var list = new BasicInfo_BF().GetBasicInfo(BasicType.Crm_SalesTraceType);
            context.Response.Write(ConvertJson.ListToJson(list));
        }

        /// <summary>
        /// 手机号是否存在
        /// </summary>
        /// <param name="context"></param>
        private void ValidMobile(HttpContext context)
        {
            var mobile = context.Request["mobile"];
            var isOk = new Customer_BF().ExistMobile(mobile, AuthenticationPage.UserInfo.OrgID);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <param name="context"></param>
        private void SaveCustomerInfo(HttpContext context)
        {
            var entity = new DAL.Crm_Customer();
            var keyID = context.Request["KeyID"];
            entity.ID = string.IsNullOrEmpty(keyID) ? Guid.NewGuid().ToString() : keyID;
            entity.Name = context.Request["Name"];
            entity.EngName = context.Request["EngName"];
            entity.CustomerType = context.Request["CustomerType"];
            entity.Sex = context.Request["Sex"];
            entity.Mobile = context.Request["Mobile"];
            entity.Phone = context.Request["Phone"];
            entity.Fax = context.Request["Fax"];
            entity.Mail = context.Request["Mail"];
            entity.QQ = context.Request["QQ"];
            entity.IDNum = context.Request["IDNum"];
            entity.Company = context.Request["Company"];
            entity.Addr = context.Request["Addr"];
            entity.Remark = context.Request["Comment"];
            entity.BirthDay = context.Request["BirthDay"].ToDate();
            var strData = context.Request["Item"];//拜访线索
            var strIDInfo = context.Request["IDInfo"];//证件信息
            var list = ConvertToVisitTrace(strData);
            var listCertificate = ConvertToCertificate(strIDInfo);
            var isOk = new Customer_BF().Save(entity, list, listCertificate);
            context.Response.Write(isOk ? "1" : "0");
        }

        /// <summary>
        /// 转化销售机会记录
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        private List<DAL.Crm_VisitTrace> ConvertToVisitTrace(string xmlData)
        {
            var list = new List<DAL.Crm_VisitTrace>();
            if (string.IsNullOrEmpty(xmlData))
                return list;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var nodes = doc.SelectNodes("document/data");
            foreach (XmlNode node in nodes)
            {
                var e = new DAL.Crm_VisitTrace();
                e.ID = node.GetNodeValue("keyID");
                e.ItemName = node.GetNodeValue("itemName");
                e.ItemType = node.GetNodeValue("itemType");
                e.Contact = node.GetNodeValue("contact");
                var d = node.GetNodeValue("tradeDate");
                e.TradeDate = string.IsNullOrEmpty(d) ? (DateTime?)null : (DateTime)Convert.ToDateTime(d);
                e.Comment = node.GetNodeValue("comment");
                list.Add(e);
            }
            return list;
        }

        /// <summary>
        /// 转化客户证件类型
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        private List<DAL.Crm_CustomerCertificate> ConvertToCertificate(string xmlData)
        {
            var list = new List<DAL.Crm_CustomerCertificate>();
            if (string.IsNullOrEmpty(xmlData))
                return list;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            var nodes = doc.SelectNodes("document/data");
            foreach (XmlNode node in nodes)
            {
                var e = new DAL.Crm_CustomerCertificate();
                e.ItemType = node.GetNodeValue("itemName");
                e.ItemVal = node.GetNodeValue("itemVal");
                list.Add(e);
            }
            return list;
        }

        /// <summary>
        /// 删除销售线索
        /// </summary>
        /// <param name="context"></param>
        private void DeleteTrace(HttpContext context)
        {
            var traceID = context.Request["ID"];
            new CustomerTrace_BF().DeleteTrace(traceID);
            context.Response.Write("1");
        }

        /// <summary>
        /// 查询客户的销售线索(时间轴数据)
        /// </summary>
        /// <param name="context"></param>
        private void QueryCustomerTrace(HttpContext context)
        {
            var customerID = context.Request["id"];
            var list = new CustomerTrace_BF().GetCusotmerTrace(customerID);

            var arrYear = new List<int>();//年份集合
            var outJson = new List<string>();
            foreach (var e in list)
            {
                if (!arrYear.Exists(x => x.Equals(e.CreateDate.Year)))
                    arrYear.Add(e.CreateDate.Year);
            }
            foreach (var y in arrYear)
            {
                var coll = list.FindAll(x => x.CreateDate.Year == y);
                var arrJson = new List<string>();
                foreach (var a in coll)
                {
                    var tDate = a.TradeDate;
                    var strDate = "";
                    if (tDate != null)
                    {
                        if (!string.IsNullOrEmpty(tDate.ToString()))
                            strDate = Convert.ToDateTime(tDate).ToString("yyyy-MM-dd");
                    }
                    var json = "\"date\":\"" + a.CreateDate.ToString("MM.dd") + "\",\"itemType\":\"" + a.ItemType + "\",\"creator\":\"" + a.CreateUserName + "\",\"customer\":\"" + a.Contact + "\",\"createdate\":\"" + a.CreateDate.ToString("yyyy-MM-dd") + "\",\"itemName\":\"" + a.ItemName + "\",\"Comment\":\"" + a.Comment + "\",\"TradeDate\":\"" + strDate + "\"";
                    arrJson.Add("{" + json + "}");
                }
                var strJson = "\"year\":\"" + y + "\",\"info\":[" + string.Join(",", arrJson) + "]";
                outJson.Add("{" + strJson + "}");
            }
            var s = "[" + string.Join(",", outJson) + "]";
            context.Response.Write(s);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="context"></param>
        private void TradeOrder(HttpContext context)
        {
            var qry = new QueryCriteriaBase();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.Keyword = context.Request["key"];
            var sDate = context.Request["sDate"];
            var eDate = context.Request["eDate"];
            var customerID = context.Request["customerID"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "TourDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var dt = new Customer_BF().TradeOrder(qry, sDate, eDate, customerID, out total);
            var json = ConvertJson.ToJson(dt);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }

        protected override string GetNavigateID(HttpContext context)
        {
            var xType = context.Request["xType"].ToInt();
            return xType == 1 ? "anonymous" : "customer";//1：选择客户时不验证权限
        }

        private void QueryCredentialType(HttpContext context)
        {
            var xType = (BasicType)17;
            var list = new BasicInfo_BF().GetBasicInfo(xType);
            var s = ConvertJson.ListToJson(list);
            context.Response.Write(s);
        }

        private void QueryMyCustomer(HttpContext context)
        {
            var qry = new CustomCriteria();
            qry.pageSize = context.Request["rows"].ToInt();
            qry.pageIndex = context.Request["page"].ToInt();
            qry.DeptID = context.Request["deptID"];
            qry.CreatorID = context.Request["userID"];
            qry.Name = context.Request["key"];
            qry.CustomerType = context.Request["customerType"];
            qry.Mobile = context.Request["mobile"];
            var sortName = context.Request["sort"];
            var sortOrder = context.Request["order"];
            sortName = string.IsNullOrEmpty(sortName) ? "CreateDate" : sortName;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            qry.SortExpress = string.Format(" Order by {0} {1}", sortName, sortOrder);
            var total = 0;
            var d = new Customer_BF().QueryMyCusomterData(qry, out total);
            var json = ConvertJson.ToJson(d);
            var s = "{\"total\":" + total + ",\"rows\":" + json + "}";
            context.Response.Write(s);
        }
    }
}