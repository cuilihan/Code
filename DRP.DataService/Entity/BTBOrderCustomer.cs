using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRP.DataService.Entity
{
    /// <summary>
    /// 订单游客
    /// </summary>
    public class BTBOrderCustomer
    {
        public Guid ID{get;set;}

        public Guid OrderID{get;set;}

        public Guid CustomerID{get;set;}

        public string cName{get;set;}

        public string cSex { get; set; }

        public string cPhone{get;set;}

        public int pSort{get;set;}

        public string Company{get;set;}

        public string IDCard{get;set;}

        public string Comment{get;set;} 
    }
}
