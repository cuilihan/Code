using DRP.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DRP.BF.OmMrg
{
    /// <summary>
    /// 运营模板报表
    /// </summary>
    public class OmRpt_BF
    {
        /// <summary>
        /// 机构开通数量统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public List<DAL.Om_OrgInfo> QuantityRpt(string sDate)
        {
            return new OrgInfo_BF().GetOrgInfo().FindAll(x => x.CreateDate.ToString("yyyy-MM-dd").Equals(sDate));
        }

        /// <summary>
        /// 机构月度开通情况统计
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public DataTable OrgOpenQuantityStatistic(int y)
        {
            return new OmRptDAL().OrgOpenQuantityStatistic(y);
        }


        /// <summary>
        /// 机构的订单量统计
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public DataTable OrgOrderQuantityStatistic(string orgName,string sDate,string eDate)
        {
            return new OmRptDAL().OrgOrderQuantityStatistic(orgName,sDate,eDate);
        }

        /// <summary>
        /// 订单的应收款与游客人数统计
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable OrderReceivableStatistic(string sDate, string eDate)
        {
            return new OmRptDAL().OrderReceivableStatistic(sDate, eDate);
        }


        /// <summary>
        /// 针对机构收取服务费用统计 
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable OrgReceiptAmoutStatistic(int year)
        {
            return new OmRptDAL().OrgReceiptAmoutStatistic(year);
        }
    }
}
