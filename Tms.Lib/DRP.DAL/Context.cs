


using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using System.Data.Common;
using System.Collections.Generic;

namespace DRP.DAL
{
    public partial class DRPDB : IQuerySurface
    {

        public IDataProvider DataProvider;
        public DbQueryProvider provider;
        
        public static IDataProvider DefaultDataProvider { get; set; }

        public bool TestMode
		{
            get
			{
                return DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public DRPDB() 
        {
            if (DefaultDataProvider == null) {
                DataProvider = ProviderFactory.GetProvider("DRPConnection");
            }
            else {
                DataProvider = DefaultDataProvider;
            }
            Init();
        }

        public DRPDB(string connectionStringName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionStringName);
            Init();
        }

		public DRPDB(string connectionString, string providerName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionString,providerName);
            Init();
        }

		public ITable FindByPrimaryKey(string pkName)
        {
            return DataProvider.Schema.Tables.SingleOrDefault(x => x.PrimaryKey.Name.Equals(pkName, StringComparison.InvariantCultureIgnoreCase));
        }

        public Query<T> GetQuery<T>()
        {
            return new Query<T>(provider);
        }
        
        public ITable FindTable(string tableName)
        {
            return DataProvider.FindTable(tableName);
        }
               
        public IDataProvider Provider
        {
            get { return DataProvider; }
            set {DataProvider=value;}
        }
        
        public DbQueryProvider QueryProvider
        {
            get { return provider; }
        }
        
        BatchQuery _batch = null;
        public void Queue<T>(IQueryable<T> qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void Queue(ISqlQuery qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void ExecuteTransaction(IList<DbCommand> commands)
		{
            if(!TestMode)
			{
                using(var connection = commands[0].Connection)
				{
                   if (connection.State == ConnectionState.Closed)
                        connection.Open();
                   
                   using (var trans = connection.BeginTransaction()) 
				   {
                        foreach (var cmd in commands) 
						{
                            cmd.Transaction = trans;
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    connection.Close();
                }
            }
        }

        public IDataReader ExecuteBatch()
        {
            if (_batch == null)
                throw new InvalidOperationException("There's nothing in the queue");
            if(!TestMode)
                return _batch.ExecuteReader();
            return null;
        }
			
        public Query<Pro_QuotationSetting> Pro_QuotationSettings { get; set; }
        public Query<Sys_CrmPermission> Sys_CrmPermissions { get; set; }
        public Query<Glo_BasicInfo> Glo_BasicInfos { get; set; }
        public Query<Glo_NoticeTrace> Glo_NoticeTraces { get; set; }
        public Query<Glo_Sm> Glo_Sms { get; set; }
        public Query<Glo_Message> Glo_Messages { get; set; }
        public Query<Ord_OrderInvoice> Ord_OrderInvoices { get; set; }
        public Query<Ord_OrderGuide> Ord_OrderGuides { get; set; }
        public Query<Om_Navigate> Om_Navigates { get; set; }
        public Query<Res_BizContact> Res_BizContacts { get; set; }
        public Query<Om_NavGroupRelation> Om_NavGroupRelations { get; set; }
        public Query<Ord_OrderBalanceItem> Ord_OrderBalanceItems { get; set; }
        public Query<Pro_Quotation> Pro_Quotations { get; set; }
        public Query<Ord_OrderBalanceItemDatum> Ord_OrderBalanceItemData { get; set; }
        public Query<Ord_OrderCollection> Ord_OrderCollections { get; set; }
        public Query<Pro_QuotationCostItem> Pro_QuotationCostItems { get; set; }
        public Query<Om_OrgReceipt> Om_OrgReceipts { get; set; }
        public Query<Ord_OrderBalance> Ord_OrderBalances { get; set; }
        public Query<Glo_PushNotice> Glo_PushNotices { get; set; }
        public Query<Sys_UserInfo> Sys_UserInfos { get; set; }
        public Query<Fin_IncomeCheckIn> Fin_IncomeCheckIns { get; set; }
        public Query<Sys_Department> Sys_Departments { get; set; }
        public Query<Ord_OrderFile> Ord_OrderFiles { get; set; }
        public Query<Om_OrgInfo> Om_OrgInfos { get; set; }
        public Query<Fin_PayCheckIn> Fin_PayCheckIns { get; set; }
        public Query<Ord_Budget> Ord_Budgets { get; set; }
        public Query<Res_TravelAgency> Res_TravelAgencies { get; set; }
        public Query<Ord_OrderInvoiceItem> Ord_OrderInvoiceItems { get; set; }
        public Query<Ord_OrderLog> Ord_OrderLogs { get; set; }
        public Query<Sys_RoleInfo> Sys_RoleInfos { get; set; }
        public Query<Sys_RoleMember> Sys_RoleMembers { get; set; }
        public Query<Rpt_BizStatistic> Rpt_BizStatistics { get; set; }
        public Query<Ord_OrderSeat> Ord_OrderSeats { get; set; }
        public Query<Fin_OrderPayable> Fin_OrderPayables { get; set; }
        public Query<Glo_Notice> Glo_Notices { get; set; }
        public Query<Sys_Permission> Sys_Permissions { get; set; }
        public Query<Sys_OrderPermission> Sys_OrderPermissions { get; set; }
        public Query<Sys_Log> Sys_Logs { get; set; }
        public Query<Sys_DataPermission> Sys_DataPermissions { get; set; }
        public Query<Pro_RouteSchedule> Pro_RouteSchedules { get; set; }
        public Query<Res_Guide> Res_Guides { get; set; }
        public Query<Pro_TourInfo> Pro_TourInfos { get; set; }
        public Query<Fin_CollectedItem> Fin_CollectedItems { get; set; }
        public Query<Glo_SerialNo> Glo_SerialNos { get; set; }
        public Query<Pro_TourPrice> Pro_TourPrices { get; set; }
        public Query<Pro_TourSeatLock> Pro_TourSeatLocks { get; set; }
        public Query<Pro_TourVenue> Pro_TourVenues { get; set; }
        public Query<Res_Other> Res_Others { get; set; }
        public Query<Sys_IPFilter> Sys_IPFilters { get; set; }
        public Query<Ord_SupplierInvoice> Ord_SupplierInvoices { get; set; }
        public Query<Ord_OrderCostItem> Ord_OrderCostItems { get; set; }
        public Query<Res_Visa> Res_Visas { get; set; }
        public Query<OTA_AreaSetting> OTA_AreaSettings { get; set; }
        public Query<Crm_Customer> Crm_Customers { get; set; }
        public Query<OTA_Setting> OTA_Settings { get; set; }
        public Query<Ord_OrderPrice> Ord_OrderPrices { get; set; }
        public Query<Sms_Message> Sms_Messages { get; set; }
        public Query<OTA_UserInfo> OTA_UserInfos { get; set; }
        public Query<Res_Hotel> Res_Hotels { get; set; }
        public Query<Sms_Platform> Sms_Platforms { get; set; }
        public Query<Sms_ValidateCode> Sms_ValidateCodes { get; set; }
        public Query<Glo_UpdateLog> Glo_UpdateLogs { get; set; }
        public Query<Res_Motorcade> Res_Motorcades { get; set; }
        public Query<Sn_Complain> Sn_Complains { get; set; }
        public Query<Res_Insurance> Res_Insurances { get; set; }
        public Query<Sn_ComplainResult> Sn_ComplainResults { get; set; }
        public Query<Ord_OrderExtend> Ord_OrderExtends { get; set; }
        public Query<Glo_Departure> Glo_Departures { get; set; }
        public Query<Res_Shopping> Res_Shoppings { get; set; }
        public Query<Sn_NoteInfo> Sn_NoteInfos { get; set; }
        public Query<Crm_VisitTrace> Crm_VisitTraces { get; set; }
        public Query<Sn_Recommend> Sn_Recommends { get; set; }
        public Query<Glo_File> Glo_Files { get; set; }
        public Query<Res_ScenicTicket> Res_ScenicTickets { get; set; }
        public Query<Sn_TravelInfo> Sn_TravelInfos { get; set; }
        public Query<Res_TicketAgency> Res_TicketAgencies { get; set; }
        public Query<Sn_Shared> Sn_Shareds { get; set; }
        public Query<Sn_Order> Sn_Orders { get; set; }
        public Query<User_Setting> User_Settings { get; set; }
        public Query<Crm_Level> Crm_Levels { get; set; }
        public Query<Ord_OrderBalanceSettlement> Ord_OrderBalanceSettlements { get; set; }
        public Query<Om_NavGroup> Om_NavGroups { get; set; }
        public Query<Crm_CustomerCertificate> Crm_CustomerCertificates { get; set; }
        public Query<Om_UserOnLine> Om_UserOnLines { get; set; }
        public Query<Glo_Destination> Glo_Destinations { get; set; }
        public Query<Om_Tool> Om_Tools { get; set; }
        public Query<Ord_TicketOrder> Ord_TicketOrders { get; set; }
        public Query<Glo_QQ> Glo_QQs { get; set; }
        public Query<Pro_Venue> Pro_Venues { get; set; }
        public Query<Ord_OrderCustomer> Ord_OrderCustomers { get; set; }
        public Query<User_Favorite> User_Favorites { get; set; }
        public Query<Om_OrgSetting> Om_OrgSettings { get; set; }
        public Query<Rpt_OrderSheet> Rpt_OrderSheets { get; set; }
        public Query<Pro_RouteInfo> Pro_RouteInfos { get; set; }
        public Query<Sn_BasicInfo> Sn_BasicInfos { get; set; }
        public Query<Sn_AdSlide> Sn_AdSlides { get; set; }
        public Query<Sn_MenuItem> Sn_MenuItems { get; set; }
        public Query<Om_Area> Om_Areas { get; set; }
        public Query<Ord_DrawMoney> Ord_DrawMoneys { get; set; }
        public Query<Sn_Memeber> Sn_Memebers { get; set; }
        public Query<Om_UserInfo> Om_UserInfos { get; set; }
        public Query<Ord_BudgetComment> Ord_BudgetComments { get; set; }
        public Query<Ord_OrderInfo> Ord_OrderInfos { get; set; }

			

        #region ' Aggregates and SubSonic Queries '
        public Select SelectColumns(params string[] columns)
        {
            return new Select(DataProvider, columns);
        }

        public Select Select
        {
            get { return new Select(this.Provider); }
        }

        public Insert Insert
		{
            get { return new Insert(this.Provider); }
        }

        public Update<T> Update<T>() where T:new()
		{
            return new Update<T>(this.Provider);
        }

        public SqlQuery Delete<T>(Expression<Func<T,bool>> column) where T:new()
        {
            LambdaExpression lamda = column;
            SqlQuery result = new Delete<T>(this.Provider);
            result = result.From<T>();
            result.Constraints=lamda.ParseConstraints().ToList();
            return result;
        }

        public SqlQuery Max<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Max)).From(tableName);
        }

        public SqlQuery Min<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Min)).From(tableName);
        }

        public SqlQuery Sum<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Sum)).From(tableName);
        }

        public SqlQuery Avg<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Avg)).From(tableName);
        }

        public SqlQuery Count<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Count)).From(tableName);
        }

        public SqlQuery Variance<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Var)).From(tableName);
        }

        public SqlQuery StandardDeviation<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.StDev)).From(tableName);
        }

        #endregion

        void Init()
        {
            provider = new DbQueryProvider(this.Provider);

            #region ' Query Defs '
            Pro_QuotationSettings = new Query<Pro_QuotationSetting>(provider);
            Sys_CrmPermissions = new Query<Sys_CrmPermission>(provider);
            Glo_BasicInfos = new Query<Glo_BasicInfo>(provider);
            Glo_NoticeTraces = new Query<Glo_NoticeTrace>(provider);
            Glo_Sms = new Query<Glo_Sm>(provider);
            Glo_Messages = new Query<Glo_Message>(provider);
            Ord_OrderInvoices = new Query<Ord_OrderInvoice>(provider);
            Ord_OrderGuides = new Query<Ord_OrderGuide>(provider);
            Om_Navigates = new Query<Om_Navigate>(provider);
            Res_BizContacts = new Query<Res_BizContact>(provider);
            Om_NavGroupRelations = new Query<Om_NavGroupRelation>(provider);
            Ord_OrderBalanceItems = new Query<Ord_OrderBalanceItem>(provider);
            Pro_Quotations = new Query<Pro_Quotation>(provider);
            Ord_OrderBalanceItemData = new Query<Ord_OrderBalanceItemDatum>(provider);
            Ord_OrderCollections = new Query<Ord_OrderCollection>(provider);
            Pro_QuotationCostItems = new Query<Pro_QuotationCostItem>(provider);
            Om_OrgReceipts = new Query<Om_OrgReceipt>(provider);
            Ord_OrderBalances = new Query<Ord_OrderBalance>(provider);
            Glo_PushNotices = new Query<Glo_PushNotice>(provider);
            Sys_UserInfos = new Query<Sys_UserInfo>(provider);
            Fin_IncomeCheckIns = new Query<Fin_IncomeCheckIn>(provider);
            Sys_Departments = new Query<Sys_Department>(provider);
            Ord_OrderFiles = new Query<Ord_OrderFile>(provider);
            Om_OrgInfos = new Query<Om_OrgInfo>(provider);
            Fin_PayCheckIns = new Query<Fin_PayCheckIn>(provider);
            Ord_Budgets = new Query<Ord_Budget>(provider);
            Res_TravelAgencies = new Query<Res_TravelAgency>(provider);
            Ord_OrderInvoiceItems = new Query<Ord_OrderInvoiceItem>(provider);
            Ord_OrderLogs = new Query<Ord_OrderLog>(provider);
            Sys_RoleInfos = new Query<Sys_RoleInfo>(provider);
            Sys_RoleMembers = new Query<Sys_RoleMember>(provider);
            Rpt_BizStatistics = new Query<Rpt_BizStatistic>(provider);
            Ord_OrderSeats = new Query<Ord_OrderSeat>(provider);
            Fin_OrderPayables = new Query<Fin_OrderPayable>(provider);
            Glo_Notices = new Query<Glo_Notice>(provider);
            Sys_Permissions = new Query<Sys_Permission>(provider);
            Sys_OrderPermissions = new Query<Sys_OrderPermission>(provider);
            Sys_Logs = new Query<Sys_Log>(provider);
            Sys_DataPermissions = new Query<Sys_DataPermission>(provider);
            Pro_RouteSchedules = new Query<Pro_RouteSchedule>(provider);
            Res_Guides = new Query<Res_Guide>(provider);
            Pro_TourInfos = new Query<Pro_TourInfo>(provider);
            Fin_CollectedItems = new Query<Fin_CollectedItem>(provider);
            Glo_SerialNos = new Query<Glo_SerialNo>(provider);
            Pro_TourPrices = new Query<Pro_TourPrice>(provider);
            Pro_TourSeatLocks = new Query<Pro_TourSeatLock>(provider);
            Pro_TourVenues = new Query<Pro_TourVenue>(provider);
            Res_Others = new Query<Res_Other>(provider);
            Sys_IPFilters = new Query<Sys_IPFilter>(provider);
            Ord_SupplierInvoices = new Query<Ord_SupplierInvoice>(provider);
            Ord_OrderCostItems = new Query<Ord_OrderCostItem>(provider);
            Res_Visas = new Query<Res_Visa>(provider);
            OTA_AreaSettings = new Query<OTA_AreaSetting>(provider);
            Crm_Customers = new Query<Crm_Customer>(provider);
            OTA_Settings = new Query<OTA_Setting>(provider);
            Ord_OrderPrices = new Query<Ord_OrderPrice>(provider);
            Sms_Messages = new Query<Sms_Message>(provider);
            OTA_UserInfos = new Query<OTA_UserInfo>(provider);
            Res_Hotels = new Query<Res_Hotel>(provider);
            Sms_Platforms = new Query<Sms_Platform>(provider);
            Sms_ValidateCodes = new Query<Sms_ValidateCode>(provider);
            Glo_UpdateLogs = new Query<Glo_UpdateLog>(provider);
            Res_Motorcades = new Query<Res_Motorcade>(provider);
            Sn_Complains = new Query<Sn_Complain>(provider);
            Res_Insurances = new Query<Res_Insurance>(provider);
            Sn_ComplainResults = new Query<Sn_ComplainResult>(provider);
            Ord_OrderExtends = new Query<Ord_OrderExtend>(provider);
            Glo_Departures = new Query<Glo_Departure>(provider);
            Res_Shoppings = new Query<Res_Shopping>(provider);
            Sn_NoteInfos = new Query<Sn_NoteInfo>(provider);
            Crm_VisitTraces = new Query<Crm_VisitTrace>(provider);
            Sn_Recommends = new Query<Sn_Recommend>(provider);
            Glo_Files = new Query<Glo_File>(provider);
            Res_ScenicTickets = new Query<Res_ScenicTicket>(provider);
            Sn_TravelInfos = new Query<Sn_TravelInfo>(provider);
            Res_TicketAgencies = new Query<Res_TicketAgency>(provider);
            Sn_Shareds = new Query<Sn_Shared>(provider);
            Sn_Orders = new Query<Sn_Order>(provider);
            User_Settings = new Query<User_Setting>(provider);
            Crm_Levels = new Query<Crm_Level>(provider);
            Ord_OrderBalanceSettlements = new Query<Ord_OrderBalanceSettlement>(provider);
            Om_NavGroups = new Query<Om_NavGroup>(provider);
            Crm_CustomerCertificates = new Query<Crm_CustomerCertificate>(provider);
            Om_UserOnLines = new Query<Om_UserOnLine>(provider);
            Glo_Destinations = new Query<Glo_Destination>(provider);
            Om_Tools = new Query<Om_Tool>(provider);
            Ord_TicketOrders = new Query<Ord_TicketOrder>(provider);
            Glo_QQs = new Query<Glo_QQ>(provider);
            Pro_Venues = new Query<Pro_Venue>(provider);
            Ord_OrderCustomers = new Query<Ord_OrderCustomer>(provider);
            User_Favorites = new Query<User_Favorite>(provider);
            Om_OrgSettings = new Query<Om_OrgSetting>(provider);
            Rpt_OrderSheets = new Query<Rpt_OrderSheet>(provider);
            Pro_RouteInfos = new Query<Pro_RouteInfo>(provider);
            Sn_BasicInfos = new Query<Sn_BasicInfo>(provider);
            Sn_AdSlides = new Query<Sn_AdSlide>(provider);
            Sn_MenuItems = new Query<Sn_MenuItem>(provider);
            Om_Areas = new Query<Om_Area>(provider);
            Ord_DrawMoneys = new Query<Ord_DrawMoney>(provider);
            Sn_Memebers = new Query<Sn_Memeber>(provider);
            Om_UserInfos = new Query<Om_UserInfo>(provider);
            Ord_BudgetComments = new Query<Ord_BudgetComment>(provider);
            Ord_OrderInfos = new Query<Ord_OrderInfo>(provider);
            #endregion


            #region ' Schemas '
        	if(DataProvider.Schema.Tables.Count == 0)
			{
            	DataProvider.Schema.Tables.Add(new Pro_QuotationSettingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_CrmPermissionTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_BasicInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_NoticeTraceTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_SmsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_MessageTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderInvoiceTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderGuideTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_NavigateTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_BizContactTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_NavGroupRelationTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderBalanceItemTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_QuotationTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderBalanceItemDataTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderCollectionTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_QuotationCostItemTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_OrgReceiptTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderBalanceTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_PushNoticeTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_UserInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Fin_IncomeCheckInTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_DepartmentTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderFileTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_OrgInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Fin_PayCheckInTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_BudgetTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_TravelAgencyTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderInvoiceItemTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderLogTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_RoleInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_RoleMemberTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Rpt_BizStatisticTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderSeatTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Fin_OrderPayableTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_NoticeTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_PermissionTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_OrderPermissionTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_LogTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_DataPermissionTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_RouteScheduleTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_GuideTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_TourInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Fin_CollectedItemTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_SerialNoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_TourPriceTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_TourSeatLockTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_TourVenueTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_OtherTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sys_IPFilterTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_SupplierInvoiceTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderCostItemTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_VisaTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new OTA_AreaSettingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Crm_CustomerTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new OTA_SettingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderPriceTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sms_MessageTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new OTA_UserInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_HotelTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sms_PlatformTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sms_ValidateCodeTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_UpdateLogTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_MotorcadeTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_ComplainTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_InsuranceTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_ComplainResultTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderExtendTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_DepartureTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_ShoppingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_NoteInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Crm_VisitTraceTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_RecommendTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_FilesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_ScenicTicketTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_TravelInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Res_TicketAgencyTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_SharedTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_OrderTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new User_SettingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Crm_LevelTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderBalanceSettlementTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_NavGroupTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Crm_CustomerCertificateTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_UserOnLineTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_DestinationTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_ToolsTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_TicketOrderTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Glo_QQTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_VenueTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderCustomerTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new User_FavoritesTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_OrgSettingTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Rpt_OrderSheetTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Pro_RouteInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_BasicInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_AdSlideTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_MenuItemTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_AreaTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_DrawMoneyTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Sn_MemeberTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Om_UserInfoTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_BudgetCommentTable(DataProvider));
            	DataProvider.Schema.Tables.Add(new Ord_OrderInfoTable(DataProvider));
            }
            #endregion
        }
        

        #region ' Helpers '
            
        internal static DateTime DateTimeNowTruncatedDownToSecond() {
            var now = DateTime.Now;
            return now.AddTicks(-now.Ticks % TimeSpan.TicksPerSecond);
        }

        #endregion

    }
}