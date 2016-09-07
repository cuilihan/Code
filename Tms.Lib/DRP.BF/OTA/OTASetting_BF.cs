using DRP.DAL;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DRP.BF.SysAdmin
{
    public class OTASetting_BF
    {
        DRPDB db = new DRPDB();

        private string QueryCondition(OTAQuery qry)
        {
            var strWhere = "1=1";
            if (!string.IsNullOrEmpty(qry.Supplier))
            {
                strWhere += string.Format(" and OTAID='{0}'", qry.Supplier);
            }

            return strWhere;
        }

        public DAL.OTA_Setting GetDetail(Guid id)
        {
            return DAL.OTA_Setting.SingleOrDefault(x => x.ID == id);
        }



        public bool SaveOTASetting(string id, WebControl pnlControl, Guid OTAID, int syncType, string OTAName,string pwd)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;

            try
            {
                var entity = new DRP.DAL.OTA_Setting();
                if (string.IsNullOrEmpty(id))
                {
                    entity.ID = Guid.NewGuid();
                    entity.DataStatus = 0;
                    entity.Creator = user.UserName;
                    entity.CreatorID = Guid.Parse(user.UserID);
                    entity.DeptID = Guid.Parse(user.DeptID);
                    entity.CreateDate = DateTime.Now;
                }
                else
                {
                    entity = GetDetail(Guid.Parse(id));
                }

                entity = new ReflectHelper<DAL.OTA_Setting>().AssignEntity(entity, pnlControl);
                entity.OTAID = OTAID;
                entity.OTAName = OTAName;
                entity.SyncType = syncType;
                if (string.IsNullOrWhiteSpace(pwd))
                {
                    entity.AcctPwd = pwd;
                }
                entity.OrgID = Guid.Parse(user.OrgID);

                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                //SysLog_BF.Write("OTA接口设置", "保存时发生错误", ex);
            }


            return isSuccess;
        }

        public DataTable OTAQuery(OTAQuery qry, out int record)
        {
            record = 0;
            var strWhere = QueryCondition(qry);
            var fields = "ID,OTAID,OTAName,AcctID,AppId,AppKey,OTA,SyncType,DataStatus,Comment,Creator,OTAServiceUrl";
            var dt = db.GetPagination("OTA_Setting", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress, fields);
            return dt;
        }

        public bool Delete(Guid id)
        {
            bool isOk = true;
            try
            {
                DAL.OTA_Setting.Delete(x => x.ID == id);
            }
            catch (Exception ex)
            {
                isOk = false;
                //SysLog_BF.Write("OTA接口设置", "删除时发生错误", ex);
            }
            return isOk;
        }
    }
}
