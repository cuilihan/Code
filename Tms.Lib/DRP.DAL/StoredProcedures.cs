


  
using System;
using SubSonic;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace DRP.DAL{
	public partial class DRPDB{

        public StoredProcedure DRP_DataBase_Backup(){
            StoredProcedure sp=new StoredProcedure("DRP_DataBase_Backup",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Finance_OrderPayable(){
            StoredProcedure sp=new StoredProcedure("DRP_Finance_OrderPayable",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Internal_ClearData(){
            StoredProcedure sp=new StoredProcedure("DRP_Internal_ClearData",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Order_GetVisitorNum(){
            StoredProcedure sp=new StoredProcedure("DRP_Order_GetVisitorNum",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Order_OrderList(){
            StoredProcedure sp=new StoredProcedure("DRP_Order_OrderList",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Order_PaymentRequestItem(){
            StoredProcedure sp=new StoredProcedure("DRP_Order_PaymentRequestItem",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Pagination(){
            StoredProcedure sp=new StoredProcedure("DRP_Pagination",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Pagination_Record(){
            StoredProcedure sp=new StoredProcedure("DRP_Pagination_Record",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Rpt_DeptStatistic(){
            StoredProcedure sp=new StoredProcedure("DRP_Rpt_DeptStatistic",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Rpt_OrderSource(){
            StoredProcedure sp=new StoredProcedure("DRP_Rpt_OrderSource",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_Rpt_UserStatistic(){
            StoredProcedure sp=new StoredProcedure("DRP_Rpt_UserStatistic",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_TeamOrder_BalanceItem(){
            StoredProcedure sp=new StoredProcedure("DRP_TeamOrder_BalanceItem",this.Provider);
            return sp;
        }
        public StoredProcedure DRP_UserPermission(){
            StoredProcedure sp=new StoredProcedure("DRP_UserPermission",this.Provider);
            return sp;
        }
	
	}
	
}
 