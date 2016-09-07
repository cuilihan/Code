using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.BF;
using DRP.BF.Glo;
using DRP.BF.ResMrg;
using DRP.Framework.Core;
using DRP.BF.Order;

namespace DRP.WEB.Module.Res
{
    public partial class GuideEdit : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFile(KeyID);
                BindData();}
        }
        private void BindData()
        {
            GuideLevel.DataSource = new BasicInfo_BF().GetBasicInfo(BasicType.Res_GuideGrade);
            GuideLevel.DataTextField = "Name";
            GuideLevel.DataValueField = "Name";
            GuideLevel.DataBind();

            DepartureID.DataSource = new Departure_BF().GetDeparture();
            DepartureID.DataValueField = "ID";
            DepartureID.DataTextField = "Name";
            DepartureID.DataBind();

            var e = new Guide_BF().Get(KeyID);
            LoadData(e, pnlContainer); 
        }

        private void BindFile(string orderID)
        {
            rptFile.DataSource = new Order_BF().GetOrderFile(orderID);
            rptFile.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var fileId = FileID.Value;
            var isOk = new Guide_BF().Save(KeyID, pnlContainer, DepartureID.SelectedItem.Text, fileId);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }


        protected override string NavigateID
        {
            get
            {
                return "guide";
            }
        }
    }
}