using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.DAL.DataAccess
{
    /// <summary>
    /// 导游领款管理
    /// </summary>
    public class DrawMoneyDAL
    {
        /// <summary>
        /// 更新导游领款状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void UpdateStatus(string id, int status)
        {
            if (id.IndexOf(",") == -1) //单条记录更新
            {
                var sql = "Update Ord_DrawMoney Set DataStatus=@status where ID=@id";
                new SubSonic.Query.CodingHorror(sql, status, id).Execute();
            }
            else //批量更新
            {
                var list = new List<string>();
                foreach (var s in id.Split(','))
                    list.Add(string.Format("'{0}'", s));
                var sql = string.Format("Update Ord_DrawMoney Set DataStatus=@status where ID in ({0})", string.Join(",", list));
                new SubSonic.Query.CodingHorror(sql, status).Execute();
            }
        }
    }
}
