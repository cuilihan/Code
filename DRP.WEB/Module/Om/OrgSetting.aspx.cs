using DRP.BF;
using DRP.BF.OmMrg;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.WEB.Module.Om
{
    public partial class OrgSetting : Pagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var org = new OrgInfo_BF().Get(KeyID);
            OrgName.Text = org.Name;
            cSData.Text = DateTime.Now.ToString("yyyy-MM-dd");
            var list = new OrgSetting_BF().Get(org.ID);
            if (list.Count > 0)
            {
                #region 订单审核
                var e = list.Find(x => x.xType == (int)OrgSettingType.OrderAudit);
                OrderAudit.Checked = e == null ? false : e.xVal == 1;
                #endregion

                #region 开通微网站
                e = list.Find(x => x.xType == (int)OrgSettingType.OpenWechat);
                MicroSite.Checked = e == null ? false : e.xVal == 1;
                #endregion

                #region 订单收款流程配置
                e = list.Find(x => x.xType == (int)OrgSettingType.OrderCollectedSign);
                OrderCollectedSign.Checked = e == null ? false : e.xVal == 1;
                #endregion

                #region 订单导出使用日期
                e = list.Find(x => x.xType == (int)OrgSettingType.EffectiveData);
                cEDate.Text = e == null ? "" : string.Format("{0:yyyy-MM-dd}", e.effectiveData);
                #endregion

                #region 开通参与人员
                e = list.Find(x => x.xType == (int)OrgSettingType.Participant);
                Participant.Checked = e == null ? false : e.xVal == 1;
                #endregion

                #region 开通订单文件上传
                e = list.Find(x => x.xType == (int)OrgSettingType.UploadFile);
                UploadFile.Checked = e == null ? false : e.xVal == 1;
                #endregion
            }
        }

        protected override string NavigateID
        {
            get
            {
                return "orginfo";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var list = new List<DRP.BF.OmMrg.OrgSetting_BF.SettingType>();

            #region 订单审核
            var entity = new DRP.BF.OmMrg.OrgSetting_BF.SettingType();
            entity.xVal = OrderAudit.Checked ? 1 : 0;
            entity.xType = OrgSettingType.OrderAudit;
            entity.effectiveData = DateTime.Now;
            list.Add(entity);
            #endregion

            #region 开通微站
            entity = new DRP.BF.OmMrg.OrgSetting_BF.SettingType();
            entity.xVal = MicroSite.Checked ? 1 : 0;
            entity.xType = OrgSettingType.OpenWechat;
            entity.effectiveData = DateTime.Now;
            list.Add(entity);
            #endregion

            #region 订单收款流程配置
            entity = new DRP.BF.OmMrg.OrgSetting_BF.SettingType();
            entity.xVal = OrderCollectedSign.Checked ? 1 : 0;
            entity.xType = OrgSettingType.OrderCollectedSign;
            entity.effectiveData = DateTime.Now;
            list.Add(entity);
            #endregion

            #region 订单导出使用日期
            entity = new DRP.BF.OmMrg.OrgSetting_BF.SettingType();
            entity.xVal = OrderCollectedSign.Checked ? 1 : 0;
            entity.xType = OrgSettingType.EffectiveData;
            if (cEDate.Text!="")
            {
                entity.effectiveData = DateTime.Parse(cEDate.Text); 
            }
            else
            {
                entity.effectiveData = DateTime.Now;
            }
            list.Add(entity);
            #endregion

            #region 开通参与人员
            entity = new DRP.BF.OmMrg.OrgSetting_BF.SettingType();
            entity.xVal = Participant.Checked ? 1 : 0;
            entity.xType = OrgSettingType.Participant;
            entity.effectiveData = DateTime.Now;
            list.Add(entity);
            #endregion

            #region 开通订单文件上传
            entity = new DRP.BF.OmMrg.OrgSetting_BF.SettingType();
            entity.xVal = UploadFile.Checked ? 1 : 0;
            entity.xType = OrgSettingType.UploadFile;
            entity.effectiveData = DateTime.Now;
            list.Add(entity);
            #endregion

            var isOk = new OrgSetting_BF().Save(KeyID, list);
            JScript.CloseDialogWin(isOk ? "保存成功" : "保存失败");
        }
    }
}
