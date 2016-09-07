using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.DAL.Model
{
    public class V_Ord_CheckAccountEntity
    {
        public string ID { get; set; }

        public string OrderID{get;set;}

        public string OrderName{get;set;}

        public DateTime TourDate{get;set;}

        public string OrderNo{get;set;}

        public string Mobile{get;set;}

        public string AcctPwd{get;set;}

        public string OrgID{get;set;}

        public string GuideName{get;set;}
        
        public int OrderType { get; set; }


        /// <summary>
        /// 报账单ID
        /// </summary>
        public string OrderBalanceID { get; set; }

        /// <summary>
        /// 0：未报账  1：已报账
        /// </summary>
        public int IsOver { get; set; }
    }
}
