using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Order;
using DRP.BF.ResMrg;
using DRP.Framework;
using DRP.BF.ProMrg;


namespace DRP.WEB.Module.Fin
{
    public partial class OrderBudgetFinal : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFile(KeyID);
                BindData();
            }
        }

        private void BindData()
        {
            var orderType =(OrderType) Request["xType"].ToInt();
            if (orderType == OrderType.QYT)
            {
                var e = new Order_BF().GetOrderInfo(KeyID);
                if (e == null) return;

                lblOrderName.Text = e.OrderName;
                lnkBill.NavigateUrl = "/Module/Order/BudgetBill.aspx?xType=" + Request["xType"] + "&id=" + e.ID;
                var budget = new OrderBudget_BF().GetBudget(e.ID, 2);//预算
                if (budget != null)
                {
                    OrderAmt.Text = budget.OrderAmt.ToString();
                    AdultNum.Text = budget.AdultNum.ToString();
                    ChildNum.Text = budget.ChildNum.ToString();
                    txtComment.Text = budget.Comment;
                }
                BindCostItem(e.ID);
                BindBudgetComment(e.ID);
            }
            else //自主班班
            {
                var e = new TourInfo_BF().Get(KeyID);
                if (e == null) return;

                lblOrderName.Text = e.TourName;
                lnkBill.NavigateUrl = "/Module/Order/BudgetBill.aspx?xType=" + Request["xType"] + "&id=" + e.ID;
                var budget = new OrderBudget_BF().GetBudget(e.ID, 2);//预算
                if (budget != null)
                {
                    OrderAmt.Text = budget.OrderAmt.ToString();
                    AdultNum.Text = budget.AdultNum.ToString();
                    ChildNum.Text = budget.ChildNum.ToString();
                    txtComment.Text = budget.Comment;
                }
                BindCostItem(e.ID);
                BindBudgetComment(e.ID);
            }
        }

        #region 决算成本


        /// <summary>
        /// 预算成本
        /// </summary>
        /// <param name="budgetID"></param>
        private void BindCostItem(string orderID)
        {
            var sb = new StringBuilder();
            var dal = new OrderBudget_BF();
            var budget = dal.GetBudget(orderID, 1);//预算
            var final = dal.GetBudget(orderID, 2);//决算 
            var listBudget = new List<DAL.Ord_OrderCostItem>();
            var listFinal = new List<DAL.Ord_OrderCostItem>();//决算成本
            if (budget == null) return;
            listBudget = dal.GetOrderCost(budget.ID);
            if (final != null) listFinal = dal.GetOrderCost(final.ID);

            var idx = 1;
            listBudget.ForEach(x =>
            {
                //决算实体
                var e = FindFinalCostItem(listFinal, x.ItemType, x.SupplierID);

                sb.Append("<tr>");
                //预算
                sb.AppendFormat("<td style='text-align:center;'>{0}<input type='hidden' value='{1}' /></td>", idx++, e == null ? "" : e.ID);
                sb.AppendFormat("<td>{0}<input type='hidden' value='{1}' /></td>", x.ItemName, x.ItemType);
                sb.AppendFormat("<td>{0}</td>", x.Supplier);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", x.CostAmt);
                sb.AppendFormat("<td>{0}</td>", x.Comment);

                //决算
                sb.AppendFormat("<td>{0}</td>", QueryCostItemSupplier(x.ItemType, e == null ? "" : e.SupplierID));
                sb.AppendFormat("<td><input name='amt' style='width:80px;height:26px; text-align:right; padding-right:10px;' class='checkInt textbox' value='{0}' /></td>"
                    , e == null ? "" : e.CostAmt.ToString());
                sb.AppendFormat("<td><input style='width:96%; height:26px;' type='text' class='textbox' value='{0}' /></td>", e == null ? "" : e.Comment);
                sb.AppendFormat("<td style='text-align:center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>");
                sb.Append("</tr>");
                if (e != null)
                    listFinal.Remove(e);
            });

            listFinal.ForEach(x =>
            {
                sb.Append("<tr>");
                //预算
                sb.AppendFormat("<td style='text-align:center;'>{0}<input type='hidden' value='{1}' /></td>", idx++, x.ID);
                sb.AppendFormat("<td>{0}<input type='hidden' value='{1}' /></td>", x.ItemName, x.ItemType);
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");

                //决算

                sb.AppendFormat("<td>{0}</td>", QueryCostItemSupplier(x.ItemType, x.SupplierID));
                sb.AppendFormat("<td><input name='amt' value='{0}' style='width:80px;height:26px; text-align:right; padding-right:10px;' class='checkInt textbox' /></td>", x.CostAmt);
                sb.AppendFormat("<td><input value='{0}' style='width:96%; height:26px;' type='text' class='textbox' /></td>", x.Comment);
                sb.AppendFormat("<td style='text-align:center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>");
                sb.Append("</tr>");
            });

            lblCostItem.Text = sb.ToString();
        }

        private DAL.Ord_OrderCostItem FindFinalCostItem(List<DAL.Ord_OrderCostItem> list, int itemType, string supplierID)
        {
            return list.Find(x => x.ItemType == itemType && x.SupplierID == supplierID);
        }


        /// <summary>
        /// 成本供应商
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="supplierID"></param>
        private string QueryCostItemSupplier(int itemType, string supplierID)
        {
            var coll = new StringBuilder();
            coll.Append("<select name='supplier' style='width:240px; height:26px;'>");
            coll.Append("<option value=''>请选择</option>");
            var dt = new ResourceUtility().QueryResource(itemType);
            foreach (DataRow row in dt.Rows)
            {
                var id = row["ID"].ToString();
                var spell = row["Spell"].ToString();
                var name = row["Name"].ToString();
                var opt = "<option value='" + id + "'>" + spell + "-" + name + "</option>";
                if (id == supplierID)
                    opt = "<option selected='selected' value='" + id + "'>" + spell + "-" + name + "</option>";
                coll.Append(opt);
            }
            coll.Append("</select>");
            return coll.ToString();
        }

        #endregion

        #region 预算备注

        private void BindBudgetComment(string orderID)
        {
            var list = new OrderBudget_BF().GetBudgetComment(orderID);
            var sb = new StringBuilder();
            var i = 1;
            list.ForEach(x =>
            {
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center;'>" + i++ + "</td>");
                sb.Append("<td>" + x.Name + "</td>");
                sb.AppendFormat("<td>{0}</td>", x.Comment);
                sb.Append("</tr>");
            });

            lblComment.Text = sb.ToString();
        }

        #endregion

        protected override string NavigateID
        {
            get
            {
                var xType = Request["xType"].ToInt();
                return xType == (int)OrderType.QYT ? "finteamorder" : "finorderzzbt";
            }
        }

        private void BindFile(string orderID)
        {
            rptFile.DataSource = new Order_BF().GetOrderFile(orderID);
            rptFile.DataBind();
        }
    }
}