using DRP.BF;
using DRP.BF.Fin;
using DRP.BF.Glo;
using DRP.BF.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.WEB.Module.Order
{
    public partial class CollectedSign : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var id = Request["id"];
            var list = new Order_BF().QueryOrderNo(id.Split(','));
            OrderNo.Text = string.Join(",", list);
            CollectType.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Fin_CollectedType);
            CollectType.DataTextField = CollectType.DataValueField = "Name";
            CollectType.DataBind();
            CollectDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            CollectAmt.Text = Request["amt"];
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var orderIds = Request["id"].Split(',');
            var entity = new DAL.Ord_OrderCollection();
            entity.ID = Guid.NewGuid().ToString();
            entity.CollectAmt = CollectAmt.Text.ToDecimal();
            entity.CollectType = CollectType.SelectedValue;
            entity.SrcBank = SrcBank.Text;
            entity.CollectBill = CollectBill.Text;
            entity.CollectDate = (DateTime)CollectDate.Text.ToDate();
            entity.Comment = Comment.Text;
            var isOk = new CollectedItem_BF().OrderCollectedSign(orderIds, entity, (OrderType)Request["orderType"].ToInt());
            JScript.CloseDialogWin(isOk ? "收款登记成功" : "收款登记失败");
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