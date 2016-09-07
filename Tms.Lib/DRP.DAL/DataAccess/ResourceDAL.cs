using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 资源（供应商）管理
    /// </summary>
    public class ResourceDAL
    {
        /// <summary>
        /// 查询供应商(只查询有效的供应商）
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable QueryResource(string tblName, string orgID)
        {
            var sql = string.Format("Select ID,Name,Spell From {0} where 1=1 and OrgID=@OrgID and IsEnable=@IsEnable Order by Spell", tblName);
            return new SubSonic.Query.CodingHorror(sql, orgID, true).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 查询供应商(只查询有效的供应商）
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public List<T> QueryResource<T>(string tblName, string orgID) where T : new()
        {
            var sql = string.Format("Select ID,Name,Spell From {0} where 1=1 and OrgID=@OrgID and IsEnable=@IsEnable Order by Spell", tblName);
            return new SubSonic.Query.CodingHorror(sql, orgID, true).ExecuteTypedList<T>();
        }

        /// <summary>
        ///更新供应商的合作情况
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="keyID"></param>
        /// <param name="costAmt"></param>
        /// <param name="adultNum"></param>
        /// <param name="childNum"></param>
        public void UpdateResourceTradeInfo(string tblName, string keyID)
        {
            var sql = "SELECT ISNULL(SUM(AdultNum),0) AdultNum,ISNULL(SUM(ChildNum),0) ChildNum,COUNT(ID) TradeNum,ISNULL(SUM(OrderCost),0) CostAmt FROM [V_Res_TradeOrder] WHERE SupplierID=@SupplierID";
            using (IDataReader reader = new SubSonic.Query.CodingHorror(sql, keyID).ExecuteReader())
            {
                if (reader.Read())
                {
                    var adultNum = Convert.ToInt16(reader["AdultNum"].ToString());
                    var childNum = Convert.ToInt16(reader["ChildNum"].ToString());
                    var tradeNum = Convert.ToInt16(reader["TradeNum"].ToString());
                    var costAmt = Convert.ToDecimal(reader["CostAmt"].ToString());
                    sql = "Update {0} Set TradeNum='{1}',TradeAmt='{2}',TradeAdultNum='{3}',TradeChildNum='{4}' where 1=1 and ID='{5}'";
                    sql = string.Format(sql, tblName, tradeNum, costAmt, adultNum, childNum, keyID);
                    new SubSonic.Query.CodingHorror(sql).Execute();
                }
            }
        }

    }
}
