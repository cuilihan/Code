﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.CheckAccount;
using DRP.BF.Order;
using DRP.BF.ProMrg;
using DRP.BF.ResMrg;
using DRP.DAL.DataAccess;
using DRP.DAL.Model;
using DRP.Framework;

namespace DRP.WEB.Module.Order
{
    public partial class OrderBudgetFinal : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                BindFile(KeyID);
            }
        }

        private void BindData()
        {
            if (OrderType.QYT == (OrderType)Request["xType"].ToInt())
            {
                var e = new Order_BF().GetOrderInfo(KeyID);
                if (e == null) return;
                lblOrderName.Text = e.OrderName;
                btnSave.Visible = e.BudgetStatus != 7;
            }
            else
            {
                var e = new TourInfo_BF().Get(KeyID);
                if (e == null) return;
                lblOrderName.Text = e.TourName;
                var entity = new Order_BF().GetOrderExtend(KeyID);
                if (entity != null)
                    btnSave.Visible = entity.BudgetStatus != 7;
            }

            lnkBill.NavigateUrl = "BudgetBill.aspx?xType=" + Request["xType"] + "&id=" + KeyID;
            var budget = new OrderBudget_BF().GetBudget(KeyID, 2);//决算
            if (budget != null)
            {
                OrderAmt.Text = budget.OrderAmt.ToString();
                AdultNum.Text = budget.AdultNum.ToString();
                ChildNum.Text = budget.ChildNum.ToString();
                txtComment.Text = budget.Comment;
            }
            else //未决算时，人数从导游报账中获取
            {
                var m_Entity = new CheckAccount_BF().GetOrderVisitorNum(KeyID);
                if (m_Entity != null)
                {
                    AdultNum.Text = m_Entity.AdultNum.ToString();
                    ChildNum.Text = m_Entity.ChildNum.ToString();
                }
            }
            BindCostItem(KeyID);
            BindBudgetComment(KeyID);
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
            listBudget = dal.GetOrderCost(budget.ID);//预算成本项目
            if (final != null) listFinal = dal.GetOrderCost(final.ID); //决算成本项目

            #region 导游报账项目金额,决算时将导游报账的金额导入至决算表中
            var listItem = new List<GuideCheckAccountEntity>();
            if (final == null) //已做过决算不再导入
            {
                listItem = new GuideCheckAccountDAL().OrderCheckAccountItem(orderID);
            }
            #endregion

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

                var itemAmt = e == null ? 0 : e.CostAmt;
                var comment = e == null ? "" : e.Comment;

                #region 导游报账金额
                GuideCheckAccountEntity m_Entity = null;
                switch (x.ItemName)
                {
                    case "地接社":
                        m_Entity = listItem.Find(a => a.ItemName == "综费");
                        break;
                    case "景点门票":
                        m_Entity = listItem.Find(a => a.ItemName == "门票");
                        break;
                    case "导游":
                        m_Entity = listItem.Find(a => a.ItemName == "导服");
                        break;
                }
                if (m_Entity != null)
                {
                    itemAmt = m_Entity.ItemAmt;
                    comment = "导游报账(" + m_Entity.ItemName + ")金额";
                    listItem.Remove(m_Entity);
                }
                #endregion

                //决算
                sb.AppendFormat("<td>{0}</td>", QueryCostItemSupplier(x.ItemType, e == null ? "" : e.SupplierID));
                sb.AppendFormat("<td><input name='amt' style='width:80px;height:26px; text-align:right; padding-right:10px;' class='checkInt textbox' value='{0}' /></td>", itemAmt);
                sb.AppendFormat("<td><input style='width:96%; height:26px;' type='text' class='textbox' value='{0}' /></td>", comment);
                sb.AppendFormat("<td style='text-align:center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>");
                sb.Append("</tr>");
                if (e != null)
                    listFinal.Remove(e);
            });

            #region 未做预算而做决算的项目
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
            #endregion

            #region 导游报账而预算没有的项目
            listItem.ForEach(x =>
            {
                sb.Append("<tr>");
                //预算
                sb.AppendFormat("<td style='text-align:center;'>{0}<input type='hidden' /></td>", idx++);
                sb.AppendFormat("<td>{0}<input type='hidden' value='' /></td>", x.ItemName);
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");

                //决算 
                var itemType = (int)ResourceType.OtherRes;
                if (x.ItemName == "门票") itemType = (int)ResourceType.ScenicTicket;
                if (x.ItemName == "综费") itemType = (int)ResourceType.TravelAgency;
                if (x.ItemName == "导服") itemType = (int)ResourceType.Guide;

                sb.AppendFormat("<td>{0}</td>", QueryCostItemSupplier(itemType, ""));
                sb.AppendFormat("<td><input name='amt' value='{0}' style='width:80px;height:26px; text-align:right; padding-right:10px;' class='checkInt textbox' /></td>", x.ItemAmt);
                sb.AppendFormat("<td><input value='{0}' style='width:96%; height:26px;' type='text' class='textbox' /></td>", "导游报账项目：" + x.ItemName);
                sb.AppendFormat("<td style='text-align:center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>");
                sb.Append("</tr>");

            });
            #endregion

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
                return "anonymous";
            }
        }

        private void BindFile(string orderID)
        {
            rptFile.DataSource = new Order_BF().GetOrderFile(orderID);
            rptFile.DataBind();
        }
    }
}