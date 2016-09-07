using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRP.DataService.Entity
{
    /// <summary>
    /// BTB推送的订单数据结构
    /// </summary>
    public class BTBOrderEntity
    {
        public Guid ID { get; set; }

        /// <summary>
        /// BTB里的订单序号
        /// </summary>
        public string BTBSerial { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string BTBCode { get; set; }

        public int OrderType{get;set;}

        public Guid AreaID{get;set;}

        public string AreaPath{get;set;}

        public string AreaPathID{get;set;}

        public string RouteName{get;set;}

        public int AdultNum{get;set;}

        public decimal AdultAmt{get;set;}

        public int ChildNum{get;set;}

        public decimal ChildAmt{get;set;}

        public decimal AdjustAmt{get;set;}

        public decimal Receivable{get;set;}

        public DateTime TourDate{get;set;}

        public int TourDays{get;set;}

        public string Comment{get;set;}

        public DateTime CreateDate{get;set;}

        public string Creator{get;set;}

        public Guid CreatorID{get;set;}

        public Guid DeptId{get;set;}

        public string Modifier{get;set;}

        public DateTime ModifyDate{get;set;}

        public int DataStatus{get;set;}

        public string Schedule{get;set;}

        public string Standard{get;set;}

        public string Venue{get;set;}

        public string CollectionTime{get;set;}

        public decimal JAmount{get;set;}

        public decimal SAmount{get;set;}

        public int Period{get;set;}

        public Guid OrderCategory{get;set;}

        public Guid RouteType{get;set;} 

        public bool IsSync{get;set;}

        public DateTime SyncDate{get;set;}

        public string OTAOrderStatus{get;set;}

        public string OTAName{get;set;}

        public List<BTBOrderCustomer> CustomerList { get; set; }

        /// <summary>
        /// OTA结算价
        /// </summary>
        public decimal OrderCost { get; set; }
    }
}
