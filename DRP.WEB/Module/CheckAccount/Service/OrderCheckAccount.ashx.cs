using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DRP.BF.CheckAccount;
using DRP.BF.Order;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.CheckAccount.Service
{
    /// <summary>
    /// 导游报账
    /// </summary>
    public class OrderCheckAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 0://查询当月订单
                    QueryOrder(context);
                    break;
                case 1://一月前订单
                    QueryOrder(context);
                    break;
                case 3://导游提交报账数据
                    SaveOrderBalance(context);
                    break;
                case 4://报账数据修改
                    QueryOrderBalance(context);
                    break;
                case 5://查询已报账的数据
                    QueryOrderBalanceView(context);
                    break;
            }
        }

        #region 导游报账的订单

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="context"></param>
        private void QueryOrder(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            var list = new CheckAccount_BF().GetOrderCheckAccount(action);
            var json = ConvertJson.ListToJson(list);
            context.Response.Write(json);
        }
        #endregion

        #region 导游提交报账数据
        /// <summary>
        /// 保存导游报账数据
        /// </summary>
        /// <param name="context"></param>
        private void SaveOrderBalance(HttpContext context)
        {
            var e = new DRP.DAL.Ord_OrderBalance();
            e.ID = string.IsNullOrEmpty(context.Request["ID"]) ? Guid.NewGuid().ToString() : context.Request["ID"];
            e.OrderGuideID = context.Request["OrderGuideID"];
            e.OrderID = context.Request["OrderID"];
            e.YLTK = context.Request["YLTK"].ToDecimal();
            e.YLTK_Remark = context.Request["YLTKRemak"];
            e.XSTK = context.Request["XSTK"].ToDecimal();
            e.XSTK_Remark = context.Request["XSTKRemark"];
            e.YHZZ = context.Request["YHZZ"].ToDecimal();
            e.YHZZ_Remark = context.Request["YHZZRemark"];
            e.QTSR = context.Request["QTSR"].ToDecimal();
            e.QTSR_Remark = context.Request["QTSRRemark"];
            e.DataStatus = context.Request["DataStatus"].ToInt();
            e.GuideMobile = context.Request["GuideMobile"];
            e.GuideName = context.Request["GuideName"];
            e.AdultNum = context.Request["AdultNum"].ToInt();
            e.ChildNum = context.Request["ChildNum"].ToInt();
            e.OrderType = context.Request["OrderType"].ToInt();
            string xmlData = context.Request["Data"];
            var isOk = new CheckAccount_BF().Save(e, xmlData);
            context.Response.Write(isOk ? "1" : "0");
        }
        #endregion

        #region 查询已报账数据
        /// <summary>
        /// 报账主表数据
        /// </summary>
        /// <param name="context"></param>
        private void QueryOrderBalance(HttpContext context)
        {
            var keyID = context.Request["ID"];
            var dal = new CheckAccount_BF();
            var e = dal.GetOrderBalance(keyID);
            var listItem = dal.GetOrderBalanceItem(keyID);
            var listSubItem = dal.GetOrderBalanceItemData(keyID);
            var sb = new StringBuilder();

            #region header
            sb.Append("<tr>");
            sb.Append("<th style=\"width: 30px; text-align: center;\">人数</th>");
            sb.AppendFormat("<td colspan='7' style='padding-left:20px;'>成人：<input style='width:60px;' value='{0}' id='txtAdult' class='txt checkNum' />儿童：<input style='width:60px;' id='txtChild' class='txt checkNum' value='{1}' /></td>", e.AdultNum, e.ChildNum);
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th rowspan=\"4\" style=\"width: 30px; text-align: center;\">收<br />入</th>");
            sb.Append("<th style=\"width: 80px; text-align: center;\">预领团款</th>");
            sb.AppendFormat("<td colspan='5'><input style='width:96%;' id='txtYLTK' class='txt checkNum' value='{0}' /></td>", e == null ? 0 : e.YLTK);
            sb.AppendFormat("<td style='width:80px;'><input style='width:96%;' class='txt' id='txtYLTKRemark'  value='{0}' /></td>", e == null ? "" : e.YLTK_Remark);
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th style=\"text-align: center;\">现收团款</th>");
            sb.AppendFormat("<td colspan='5'><input style='width:96%;' id='txtXSTK' class='txt checkNum' value='{0}' /></td>", e == null ? 0 : e.XSTK);
            sb.AppendFormat("<td><input style='width:96%;' class='txt' id='txtXSTKRemark' value='{0}' /></td>", e == null ? "" : e.XSTK_Remark);
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th style=\"text-align: center;\">汇地接社款</th>");
            sb.AppendFormat("<td colspan='5'><input style='width:96%;' id='txtYHZZ' class='txt checkNum' value='{0}' /></td>", e == null ? 0 : e.YHZZ);
            if (e != null && e.YHZZ_Remark.Equals("1"))
                sb.Append("<td style='text-align:center;'><input type='checkbox' id='chkToTravelAmt' checked='checked' />计算收入</td>");
            else
                sb.Append("<td style='text-align:center;'><input type='checkbox' id='chkToTravelAmt'/>计算收入</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th style=\"text-align: center;\">其他收入</th>");
            sb.AppendFormat("<td colspan='5'><input style='width:96%;' id='txtQTSR' class='txt checkNum' value='{0}' /></td>", e == null ? 0 : e.QTSR);
            sb.AppendFormat("<td><input style='width:96%;' class='txt' id='txtQTSRRemark' value='{0}' /></td>", e == null ? "" : e.QTSR_Remark);
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.AppendFormat("<th rowspan=\"{0}\">支<br />出</th>", listSubItem.Count + 2);
            sb.Append("<th>&nbsp;</th>");
            sb.Append("<th>景点</th>");
            sb.Append("<th style=\"width: 70px;\">单价</th>");
            sb.Append("<th style=\"width: 70px;\">数量</th>");
            sb.Append("<th style=\"width: 70px;\">总价</th>");
            sb.Append("<th style=\"width: 70px;\">是否签单</th>");
            sb.Append("<th>是否有发票</th>");
            sb.Append("</tr>");
            #endregion

            #region body
            foreach (var t in listItem)
            {
                var hfCtrl = "<input type='hidden' value='" + t.ItemType + "' />"; //大类标识
                var idx = 1;
                var itemColl = listSubItem.FindAll(x => x.ItemID == t.ID);
                foreach (var item in itemColl)
                {
                    sb.Append("<tr>");
                    if (idx++ == 1)
                        sb.AppendFormat("<th{0}>{1}{2}</th>", itemColl.Count == 1 ? "" : " rowspan='" + itemColl.Count + "'", hfCtrl, t.ItemName);
                    sb.AppendFormat("<td style=\"text-align: center;\"><input type=\"text\" class=\"txt\" style=\"width: 96%;\" value='{0}' /></td>",
                        item.ItemName);
                    sb.AppendFormat("<td style=\"text-align: center;\"><input type=\"text\" class=\"txt checkNum txtRight\" style=\"width: 65px;\" value='{0}' /></td>",
                        item.ItemNum == 0 ? "" : item.ItemPrice.ToString());
                    sb.AppendFormat("<td style=\"text-align: center;\"><input type=\"text\" class=\"txt checkNum txtRight\" style=\"width: 65px;\" value='{0}' /></td>",
                        item.ItemPrice == 0 ? "" : item.ItemNum.ToString());

                    sb.AppendFormat("<td style=\"text-align: center;\"><input type=\"text\" name='amt' class=\"txt checkNum txtRight\" style=\"width: 65px;\" value='{0}' /></td>", item.ItemAmt.ToString());
                    if (item.IsSign)
                        sb.Append("<td style=\"text-align: center;\"><input type=\"checkbox\" checked='checked' name=\"chkSignature\" onclick='t.fnCalculate()' /></td>");
                    else
                        sb.Append("<td style=\"text-align: center;\"><input type=\"checkbox\" name=\"chkSignature\" onclick='t.fnCalculate()'/></td>");
                    if (item.IsInvoice)
                        sb.Append("<td style=\"text-align: center;\"><input type=\"checkbox\" name=\"chkInvoice\" checked='checked' /></td>");
                    else
                        sb.Append("<td style=\"text-align: center;\"><input type=\"checkbox\" name=\"chkInvoice\"/></td>");
                    sb.Append("</tr>");
                }
            }
            #endregion

            #region footer
            sb.Append("<tr>");
            sb.Append("<th>总计支出</th>");
            sb.Append("<td style=\"text-align: right; font-family:arial; font-weight:bold;\" colspan=\"4\" id='TotalCost'></td>");
            sb.Append("<td colspan='2' style=\"text-align: center;\"></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th colspan=\"2\">收支盈余</th>");
            sb.Append("<td style=\"text-align: right;font-family:arial;font-weight:bold;\" colspan=\"4\" id='TotalProfit'></td>");
            sb.Append("<td colspan='2' style=\"text-align: center;\"></td>");
            sb.Append("</tr>");
            #endregion

            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 查看报账单据
        /// </summary>
        /// <param name="context"></param>
        private void QueryOrderBalanceView(HttpContext context)
        {
            var keyID = context.Request["ID"];
            var dal = new CheckAccount_BF();
            var e = dal.GetOrderBalance(keyID);
            var listItem = dal.GetOrderBalanceItem(keyID);
            var listSubItem = dal.GetOrderBalanceItemData(keyID);
            var sb = new StringBuilder();

            decimal tIncome = 0;
            decimal tCost = 0;

            #region header
            if (e != null)
            {
                tIncome += (decimal)e.YLTK + (decimal)e.XSTK + (decimal)e.QTSR;
                if (e.YHZZ_Remark.Equals("1")) //汇地接社款是否算收入
                    tIncome += (decimal)e.YHZZ;
            }
            sb.Append("<tr>");
            sb.Append("<th rowspan=\"4\" style=\"width: 30px; text-align: center;\">收<br />入</th>");
            sb.Append("<th style=\"width: 80px; text-align: center;\">预领团款</th>");
            sb.AppendFormat("<td colspan='5'>{0}</td>", e == null ? 0 : e.YLTK);
            sb.AppendFormat("<td style='width:80px;'>{0}</td>", e == null ? "" : e.YLTK_Remark);
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th style=\"text-align: center;\">现收团款</th>");
            sb.AppendFormat("<td colspan='5'>{0}</td>", e == null ? 0 : e.XSTK);
            sb.AppendFormat("<td>{0}</td>", e == null ? "" : e.XSTK_Remark);
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th style=\"text-align: center;\">汇地接社款</th>");
            sb.AppendFormat("<td colspan='5'>{0}</td>", e == null ? 0 : e.YHZZ);
            sb.AppendFormat("<td style='text-align:center;'>{0}</td>", e == null ? "不计收入" : e.YHZZ_Remark.Equals("1") ? "计算收入" : "不计收入");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th style=\"text-align: center;\">其他收入</th>");
            sb.AppendFormat("<td colspan='5'>{0}</td>", e == null ? 0 : e.QTSR);
            sb.AppendFormat("<td>{0}</td>", e == null ? "" : e.QTSR_Remark);
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.AppendFormat("<th rowspan=\"{0}\" style='text-align:center;'>支<br />出</th>", listSubItem.Count + 2);
            sb.Append("<th>&nbsp;</th>");
            sb.Append("<th style='text-align:center;'>景点</th>");
            sb.Append("<th style=\"width: 70px; text-align:center;\">单价</th>");
            sb.Append("<th style=\"width: 70px; text-align:center;\">数量</th>");
            sb.Append("<th style=\"width: 70px; text-align:center;\">总价</th>");
            sb.Append("<th style=\"width: 70px; text-align:center;\">是否签单</th>");
            sb.Append("<th text-align:center;>是否有发票</th>");
            sb.Append("</tr>");
            #endregion

            #region body
            foreach (var t in listItem)
            {
                var idx = 1;
                var itemColl = listSubItem.FindAll(x => x.ItemID == t.ID);
                foreach (var item in itemColl)
                {
                    sb.Append("<tr>");
                    if (idx++ == 1)
                        sb.AppendFormat("<th{0} style='text-align:center;'>{1}</th>", itemColl.Count == 1 ? "" : " rowspan='" + itemColl.Count + "'", t.ItemName);
                    sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>",
                        item.ItemName);
                    sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>",
                        item.ItemNum == 0 ? "" : item.ItemPrice.ToString());
                    sb.AppendFormat("<td style=\"text-align: center;\">{0}</td>",
                        item.ItemPrice == 0 ? "" : item.ItemNum.ToString());
                    sb.AppendFormat("<td style=\"text-align: right; font-family:arial;\">{0}</td>",
                        item.ItemAmt == 0 ? "" : item.ItemAmt.ToString());
                    if (item.IsSign)
                        sb.Append("<td style=\"text-align: center;\">√</td>");
                    else
                        sb.Append("<td style=\"text-align: center;\">&nbsp;</td>");
                    if (item.IsInvoice)
                        sb.Append("<td style=\"text-align: center;\">√</td>");
                    else
                        sb.Append("<td style=\"text-align: center;\">&nbsp;</td>");
                    sb.Append("</tr>");
                    if (!item.IsSign) //签单的不计算成本支出
                    {
                        tCost += (decimal)item.ItemAmt;
                    }
                }
            }
            #endregion

            #region footer
            sb.Append("<tr>");
            sb.Append("<th style='text-align:center;'>总计支出</th>");
            sb.AppendFormat("<td style=\"text-align: right; font-family:arial; font-weight:bold;\" colspan=\"4\" id='TotalCost'>{0}</td>", tCost.ToString());
            sb.Append("<td colspan='2' style=\"text-align: center;\"></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<th colspan=\"2\" style='text-align:center;'>收支盈余</th>");
            sb.AppendFormat("<td style=\"text-align: right;font-family:arial;font-weight:bold;\" colspan=\"4\">{0}</td>", (tIncome - tCost).ToString());
            sb.Append("<td colspan='2' style=\"text-align: center;\"></td>");
            sb.Append("</tr>");
            #endregion

            context.Response.Write(sb.ToString());
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}