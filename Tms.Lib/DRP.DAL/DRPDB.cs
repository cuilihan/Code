using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SubSonic.Schema;

namespace DRP.DAL
{
    public partial class DRPDB
    {
        #region 通用分页过程
        /// <summary>
        /// 分页查询列表
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderExpress"></param>
        /// <param name="strGetFields">空：所有列</param>
        /// <returns></returns>
        public DataTable GetPagination(string tblName, int pageIndex, int pageSize, out int record, string strWhere,
            string orderExpress, string strGetFields = "*")
        {
            DataTable dt = new DataTable();
            record = 0;
            using (DbDataReader reader = DRP_Pagination_Record(tblName, strWhere).ExecuteReader())
            {
                try
                {
                    if (reader.Read())
                    {
                        record = Convert.ToInt32(reader["t"].ToString());
                        if (record == 0) return dt;
                        var ds = DRP_Pagination(tblName, strGetFields, orderExpress, strWhere, pageIndex, pageSize).ExecuteDataSet();
                        if (ds == null) return dt;
                        dt = ds.Tables[0];
                    }
                }
                catch (Exception er)
                {
                    reader.Close();
                    throw new Exception("执行分页存储过程时发生错误" + System.Environment.NewLine + er.Message, er);
                }
            }
            return dt;
        }

        /// <summary>
        /// 分页查询列表
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderExpress"></param>
        /// <param name="strGetFields">空：所有列</param>
        /// <returns></returns>
        public List<T> GetPaginationList<T>(string tblName, int pageIndex, int pageSize, out int record,
            string strWhere, string orderExpress, string strGetFields = "*") where T : new()
        {

            var list = new List<T>();
            record = 0;
            using (DbDataReader reader = DRP_Pagination_Record(tblName, strWhere).ExecuteReader())
            {
                try
                {
                    if (reader.Read())
                    {
                        record = Convert.ToInt32(reader["t"].ToString());
                        if (record == 0) return list;
                        list = DRP_Pagination(tblName, strGetFields, orderExpress, strWhere, pageIndex, pageSize).ExecuteTypedList<T>();
                    }
                }
                catch (Exception er)
                {
                    reader.Close();
                    throw new Exception("执行分页存储过程时发生错误" + System.Environment.NewLine + er.Message, er);
                }
            }
            return list;
        }


        private StoredProcedure DRP_Pagination(string tblName, string strGetFields, string strOrder, string strWhere, int pageIndex, int pageSize)
        {
            StoredProcedure sp = new StoredProcedure("DRP_Pagination", this.Provider);
            sp.Command.AddParameter("tblName", tblName, DbType.AnsiString);
            sp.Command.AddParameter("strGetFields", strGetFields, DbType.AnsiString);
            sp.Command.AddParameter("strOrder", strOrder, DbType.AnsiString);
            sp.Command.AddParameter("strWhere", strWhere, DbType.AnsiString);
            sp.Command.AddParameter("pageIndex", pageIndex, DbType.Int32);
            sp.Command.AddParameter("pageSize", pageSize, DbType.Int32);
            return sp;
        }

        private StoredProcedure DRP_Pagination_Record(string tblName, string strWhere)
        {
            StoredProcedure sp = new StoredProcedure("DRP_Pagination_Record", this.Provider);
            sp.Command.AddParameter("tblName", tblName, DbType.AnsiString);
            sp.Command.AddParameter("strWhere", strWhere, DbType.AnsiString);
            return sp;
        }

        #endregion

        #region 用户权限查询

        /// <summary>
        /// 用户菜单权限查询
        /// </summary> 
        /// <returns></returns>
        public StoredProcedure DRP_UserNavPermission(string userID)
        {
            StoredProcedure sp = new StoredProcedure("DRP_UserPermission", this.Provider);
            sp.Command.AddParameter("userID", userID, DbType.Guid);
            return sp;
        }

        #endregion

        #region << 目的地下有团次数：用于产品预订目的地检索中 >>

        /// <summary>
        /// 目的地下具有团次数：用于产品预订目的地检索中
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable DRP_Pro_GetDestinationTour(string orgID)
        {
            StoredProcedure sp = new StoredProcedure("DRP_Pro_HasTourNum", this.Provider);
            sp.Command.AddParameter("OrgID", orgID, DbType.String);
            return sp.ExecuteDataSet().Tables[0];            
        }
        #endregion
    }
}
