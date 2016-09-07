using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.ResMrg;

namespace DRP.WEB.Module.Res
{
    public partial class OtherResEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var e = new Other_BF().Get(KeyID);
            LoadData(e, pnlContainer);
            if (e != null)
            {
                rptData.DataSource = new ResourceUtility().GetBizContact(e.ID);
                rptData.DataBind();
            }
        } 

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lblNo = e.Item.FindControl("lblNo") as Literal;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "otherres";
            }
        }
    }
}