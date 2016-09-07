using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.BF.ResMrg;
using DRP.Framework;

namespace DRP.WEB.Module.Order
{
    public partial class OrderBudget : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            if (OrderType.QYT == (OrderType)Request["xType"].ToInt())
            {
                var e = new Order_BF().GetOrderInfo(KeyID);
                if (e == null) return;
                lblOrderName.Text = e.OrderName;
                btnSave.Visible = e.BudgetStatus < 5;
            }
            else
            {
                var e = new TourInfo_BF().Get(KeyID);
                if (e == null) return;
                lblOrderName.Text = e.TourName;
                var entity = new Order_BF().GetOrderExtend(KeyID);
                if (entity != null)
                    btnSave.Visible = entity.BudgetStatus < 5;
            }

            BindDrawMoneyMethod();

            var budget = new OrderBudget_BF().GetBudget(KeyID, 1);//预算
            if (budget != null)
            {
                OrderAmt.Text = budget.OrderAmt.ToString();
                AdultNum.Text = budget.AdultNum.ToString();
                ChildNum.Text = budget.ChildNum.ToString();

                BindCostItem(budget.ID);

                hfIsEdit.Value = "1";
            }
            else
            {
                if (OrderType.ZZBT == (OrderType)Request["xType"].ToInt()) //自主班团自动关联自主班散客订单的金额与人数
                {
                    var dt = new Order_BF().CalculateZZBOrder(KeyID);
                    if (dt.Rows.Count > 0)
                    {
                        OrderAmt.Text = dt.Rows[0]["OrderAmt"].ToString();
                        AdultNum.Text = dt.Rows[0]["AdultNum"].ToString();
                        ChildNum.Text = dt.Rows[0]["ChildNum"].ToString();
                    }
                }
            }
            //备用金
            var drawmoneyEntity = new OrderBudget_BF().GetDrawMoney(KeyID);
            if (drawmoneyEntity != null)
            {
                DrawMoney.Text = drawmoneyEntity.Amount.ToString();
                Method.SelectedValue = drawmoneyEntity.Method;
                Comment.Text = drawmoneyEntity.Comment;
            }

            BindBudgetComment(KeyID);
        }

        #region 预算成本
        /// <summary>
        /// 预算成本
        /// </summary>
        /// <param name="budgetID"></param>
        private void BindCostItem(string budgetID)
        {
            var sb = new StringBuilder();
            var list = new OrderBudget_BF().GetOrderCost(budgetID);
            var idx = 1;
            list.ForEach(x =>
            {
                var coll = new StringBuilder();
                coll.Append("<select name='supplier' style='width:240px; height:26px;'>");
                coll.Append("<option value=''>请选择</option>");
                var dt = new ResourceUtility().QueryResource(x.ItemType);
                foreach (DataRow row in dt.Rows)
                {
                    var id = row["ID"].ToString();
                    var spell = row["Spell"].ToString();
                    var name = row["Name"].ToString();
                    var opt = "<option value='" + id + "'>" + spell + "-" + name + "</option>";
                    if (id == x.SupplierID)
                        opt = "<option selected='selected' value='" + id + "'>" + spell + "-" + name + "</option>";
                    coll.Append(opt);
                }
                coll.Append("</select>");


                sb.Append("<tr>");
                sb.AppendFormat("<td style='text-align:center;'>{0}<input type='hidden' value='{1}' /></td>", idx++, x.ID);
                sb.AppendFormat("<td>{0}<input type='hidden' value='{1}' /></td>", x.ItemName, x.ItemType);
                sb.AppendFormat("<td>{0}</td>", coll);
                sb.AppendFormat("<td><input name='amt' style='width:90px;height:26px; text-align:right; padding-right:10px;' class='checkInt textbox' value='{0}' /></td>", x.CostAmt);
                sb.AppendFormat("<td><input style='width:96%; height:26px;' type='text' class='textbox' value='{0}' /></td>", x.Comment);
                sb.AppendFormat("<td style='text-align:center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>");
                sb.Append("</tr>");
            });
            lblCostItem.Text = sb.ToString();
        }
        #endregion


        #region 预算备注

        private void BindBudgetComment(string orderID)
        {
            var list = new OrderBudget_BF().GetBudgetComment(orderID);
            var sb = new StringBuilder();
            var i = 1;
            sb.AppendFormat(AppendBudgetComment(list, "住宿", i++));
            sb.AppendFormat(AppendBudgetComment(list, "用餐", i++));
            sb.AppendFormat(AppendBudgetComment(list, "购票方式", i++));
            sb.AppendFormat(AppendBudgetComment(list, "用车", i++));
            sb.AppendFormat(AppendBudgetComment(list, "帽子胸贴水", i++));
            sb.AppendFormat(AppendBudgetComment(list, "地接付款", i++));
            sb.AppendFormat(AppendBudgetComment(list, "代收款", i++));
            sb.AppendFormat(AppendBudgetComment(list, "其他", i++));
            lblComment.Text = sb.ToString();
        }

        private string AppendBudgetComment(List<DAL.Ord_BudgetComment> list, string name, int i)
        {
            var v = list.Find(x => x.Name == name);
            var sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<td style='text-align:center;'>" + i + "</td>");
            sb.Append("<td>" + name + "</td>");
            sb.AppendFormat("<td><input type='text' class='textbox' style='height:26px; width:90%;' value='{0}' /></td>", v == null ? "" : v.Comment);
            sb.Append("</tr>");

            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// 导游领款方式
        /// </summary>
        private void BindDrawMoneyMethod()
        {
            Method.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Ord_DrawMoneyMethod);
            Method.DataTextField = Method.DataValueField = "Name";
            Method.DataBind();
        }

        protected override string NavigateID
        {
            get
            {
                var xType = Request["xType"].ToInt();
                return xType == (int)OrderType.QYT ? "salesorderqy" : "salesorderzzb";
            }
        }
    }
}