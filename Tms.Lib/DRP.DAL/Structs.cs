


using System;
using SubSonic.Schema;
using System.Collections.Generic;
using SubSonic.DataProviders;
using System.Data;

namespace DRP.DAL {
	
        /// <summary>
        /// Table: Pro_QuotationSetting
        /// Primary Key: ID
        /// </summary>

        public class Pro_QuotationSettingTable: DatabaseTable {
            
            public Pro_QuotationSettingTable(IDataProvider provider):base("Pro_QuotationSetting",provider){
                ClassName = "Pro_QuotationSetting";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Template", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn Template{
                get{
                    return this.GetColumn("Template");
                }
            }
				
   			public static string TemplateColumn{
			      get{
        			return "Template";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_CrmPermission
        /// Primary Key: ID
        /// </summary>

        public class Sys_CrmPermissionTable: DatabaseTable {
            
            public Sys_CrmPermissionTable(IDataProvider provider):base("Sys_CrmPermission",provider){
                ClassName = "Sys_CrmPermission";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RoleID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BtnPermission", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RoleID{
                get{
                    return this.GetColumn("RoleID");
                }
            }
				
   			public static string RoleIDColumn{
			      get{
        			return "RoleID";
      			}
		    }
            
            public IColumn BtnPermission{
                get{
                    return this.GetColumn("BtnPermission");
                }
            }
				
   			public static string BtnPermissionColumn{
			      get{
        			return "BtnPermission";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_BasicInfo
        /// Primary Key: ID
        /// </summary>

        public class Glo_BasicInfoTable: DatabaseTable {
            
            public Glo_BasicInfoTable(IDataProvider provider):base("Glo_BasicInfo",provider){
                ClassName = "Glo_BasicInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BasicType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn BasicType{
                get{
                    return this.GetColumn("BasicType");
                }
            }
				
   			public static string BasicTypeColumn{
			      get{
        			return "BasicType";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_NoticeTrace
        /// Primary Key: ID
        /// </summary>

        public class Glo_NoticeTraceTable: DatabaseTable {
            
            public Glo_NoticeTraceTable(IDataProvider provider):base("Glo_NoticeTrace",provider){
                ClassName = "Glo_NoticeTrace";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("NoticeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn NoticeID{
                get{
                    return this.GetColumn("NoticeID");
                }
            }
				
   			public static string NoticeIDColumn{
			      get{
        			return "NoticeID";
      			}
		    }
            
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn UserName{
                get{
                    return this.GetColumn("UserName");
                }
            }
				
   			public static string UserNameColumn{
			      get{
        			return "UserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_Sms
        /// Primary Key: ID
        /// </summary>

        public class Glo_SmsTable: DatabaseTable {
            
            public Glo_SmsTable(IDataProvider provider):base("Glo_Sms",provider){
                ClassName = "Glo_Sm";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SendUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SendUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RecMobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("MsgContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn SendUserID{
                get{
                    return this.GetColumn("SendUserID");
                }
            }
				
   			public static string SendUserIDColumn{
			      get{
        			return "SendUserID";
      			}
		    }
            
            public IColumn SendUserName{
                get{
                    return this.GetColumn("SendUserName");
                }
            }
				
   			public static string SendUserNameColumn{
			      get{
        			return "SendUserName";
      			}
		    }
            
            public IColumn RecMobile{
                get{
                    return this.GetColumn("RecMobile");
                }
            }
				
   			public static string RecMobileColumn{
			      get{
        			return "RecMobile";
      			}
		    }
            
            public IColumn MsgContent{
                get{
                    return this.GetColumn("MsgContent");
                }
            }
				
   			public static string MsgContentColumn{
			      get{
        			return "MsgContent";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_Message
        /// Primary Key: ID
        /// </summary>

        public class Glo_MessageTable: DatabaseTable {
            
            public Glo_MessageTable(IDataProvider provider):base("Glo_Message",provider){
                ClassName = "Glo_Message";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RecUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RecUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SendUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SendUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("MsgTitle", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("MsgContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("URL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("Target", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RecUserID{
                get{
                    return this.GetColumn("RecUserID");
                }
            }
				
   			public static string RecUserIDColumn{
			      get{
        			return "RecUserID";
      			}
		    }
            
            public IColumn RecUserName{
                get{
                    return this.GetColumn("RecUserName");
                }
            }
				
   			public static string RecUserNameColumn{
			      get{
        			return "RecUserName";
      			}
		    }
            
            public IColumn SendUserID{
                get{
                    return this.GetColumn("SendUserID");
                }
            }
				
   			public static string SendUserIDColumn{
			      get{
        			return "SendUserID";
      			}
		    }
            
            public IColumn SendUserName{
                get{
                    return this.GetColumn("SendUserName");
                }
            }
				
   			public static string SendUserNameColumn{
			      get{
        			return "SendUserName";
      			}
		    }
            
            public IColumn MsgTitle{
                get{
                    return this.GetColumn("MsgTitle");
                }
            }
				
   			public static string MsgTitleColumn{
			      get{
        			return "MsgTitle";
      			}
		    }
            
            public IColumn MsgContent{
                get{
                    return this.GetColumn("MsgContent");
                }
            }
				
   			public static string MsgContentColumn{
			      get{
        			return "MsgContent";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn URL{
                get{
                    return this.GetColumn("URL");
                }
            }
				
   			public static string URLColumn{
			      get{
        			return "URL";
      			}
		    }
            
            public IColumn Target{
                get{
                    return this.GetColumn("Target");
                }
            }
				
   			public static string TargetColumn{
			      get{
        			return "Target";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderInvoice
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderInvoiceTable: DatabaseTable {
            
            public Ord_OrderInvoiceTable(IDataProvider provider):base("Ord_OrderInvoice",provider){
                ClassName = "Ord_OrderInvoice";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("InvoiceName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("InvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("InvoiceItem", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("FetchType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("InvoiceUnit", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("IsOverAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("InvoiceStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("InvoiceNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("InvoiceDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Auditor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AuditDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AuditorID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AuditRemark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn InvoiceName{
                get{
                    return this.GetColumn("InvoiceName");
                }
            }
				
   			public static string InvoiceNameColumn{
			      get{
        			return "InvoiceName";
      			}
		    }
            
            public IColumn InvoiceAmt{
                get{
                    return this.GetColumn("InvoiceAmt");
                }
            }
				
   			public static string InvoiceAmtColumn{
			      get{
        			return "InvoiceAmt";
      			}
		    }
            
            public IColumn InvoiceItem{
                get{
                    return this.GetColumn("InvoiceItem");
                }
            }
				
   			public static string InvoiceItemColumn{
			      get{
        			return "InvoiceItem";
      			}
		    }
            
            public IColumn FetchType{
                get{
                    return this.GetColumn("FetchType");
                }
            }
				
   			public static string FetchTypeColumn{
			      get{
        			return "FetchType";
      			}
		    }
            
            public IColumn InvoiceUnit{
                get{
                    return this.GetColumn("InvoiceUnit");
                }
            }
				
   			public static string InvoiceUnitColumn{
			      get{
        			return "InvoiceUnit";
      			}
		    }
            
            public IColumn IsOverAmt{
                get{
                    return this.GetColumn("IsOverAmt");
                }
            }
				
   			public static string IsOverAmtColumn{
			      get{
        			return "IsOverAmt";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn InvoiceStatus{
                get{
                    return this.GetColumn("InvoiceStatus");
                }
            }
				
   			public static string InvoiceStatusColumn{
			      get{
        			return "InvoiceStatus";
      			}
		    }
            
            public IColumn OrderNum{
                get{
                    return this.GetColumn("OrderNum");
                }
            }
				
   			public static string OrderNumColumn{
			      get{
        			return "OrderNum";
      			}
		    }
            
            public IColumn OrderName{
                get{
                    return this.GetColumn("OrderName");
                }
            }
				
   			public static string OrderNameColumn{
			      get{
        			return "OrderName";
      			}
		    }
            
            public IColumn InvoiceNo{
                get{
                    return this.GetColumn("InvoiceNo");
                }
            }
				
   			public static string InvoiceNoColumn{
			      get{
        			return "InvoiceNo";
      			}
		    }
            
            public IColumn InvoiceDate{
                get{
                    return this.GetColumn("InvoiceDate");
                }
            }
				
   			public static string InvoiceDateColumn{
			      get{
        			return "InvoiceDate";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn Auditor{
                get{
                    return this.GetColumn("Auditor");
                }
            }
				
   			public static string AuditorColumn{
			      get{
        			return "Auditor";
      			}
		    }
            
            public IColumn AuditDate{
                get{
                    return this.GetColumn("AuditDate");
                }
            }
				
   			public static string AuditDateColumn{
			      get{
        			return "AuditDate";
      			}
		    }
            
            public IColumn AuditorID{
                get{
                    return this.GetColumn("AuditorID");
                }
            }
				
   			public static string AuditorIDColumn{
			      get{
        			return "AuditorID";
      			}
		    }
            
            public IColumn AuditRemark{
                get{
                    return this.GetColumn("AuditRemark");
                }
            }
				
   			public static string AuditRemarkColumn{
			      get{
        			return "AuditRemark";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderGuide
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderGuideTable: DatabaseTable {
            
            public Ord_OrderGuideTable(IDataProvider provider):base("Ord_OrderGuide",provider){
                ClassName = "Ord_OrderGuide";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("GuideID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("GuideName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AcctPwd", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsOver", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderBalanceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn GuideID{
                get{
                    return this.GetColumn("GuideID");
                }
            }
				
   			public static string GuideIDColumn{
			      get{
        			return "GuideID";
      			}
		    }
            
            public IColumn GuideName{
                get{
                    return this.GetColumn("GuideName");
                }
            }
				
   			public static string GuideNameColumn{
			      get{
        			return "GuideName";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn AcctPwd{
                get{
                    return this.GetColumn("AcctPwd");
                }
            }
				
   			public static string AcctPwdColumn{
			      get{
        			return "AcctPwd";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn IsOver{
                get{
                    return this.GetColumn("IsOver");
                }
            }
				
   			public static string IsOverColumn{
			      get{
        			return "IsOver";
      			}
		    }
            
            public IColumn OrderBalanceID{
                get{
                    return this.GetColumn("OrderBalanceID");
                }
            }
				
   			public static string OrderBalanceIDColumn{
			      get{
        			return "OrderBalanceID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_Navigate
        /// Primary Key: ID
        /// </summary>

        public class Om_NavigateTable: DatabaseTable {
            
            public Om_NavigateTable(IDataProvider provider):base("Om_Navigate",provider){
                ClassName = "Om_Navigate";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PageID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ParentID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("NavName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("NavUrl", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("NavCls", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("NavIcon", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsVisual", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn PageID{
                get{
                    return this.GetColumn("PageID");
                }
            }
				
   			public static string PageIDColumn{
			      get{
        			return "PageID";
      			}
		    }
            
            public IColumn ParentID{
                get{
                    return this.GetColumn("ParentID");
                }
            }
				
   			public static string ParentIDColumn{
			      get{
        			return "ParentID";
      			}
		    }
            
            public IColumn NavName{
                get{
                    return this.GetColumn("NavName");
                }
            }
				
   			public static string NavNameColumn{
			      get{
        			return "NavName";
      			}
		    }
            
            public IColumn NavUrl{
                get{
                    return this.GetColumn("NavUrl");
                }
            }
				
   			public static string NavUrlColumn{
			      get{
        			return "NavUrl";
      			}
		    }
            
            public IColumn NavCls{
                get{
                    return this.GetColumn("NavCls");
                }
            }
				
   			public static string NavClsColumn{
			      get{
        			return "NavCls";
      			}
		    }
            
            public IColumn NavIcon{
                get{
                    return this.GetColumn("NavIcon");
                }
            }
				
   			public static string NavIconColumn{
			      get{
        			return "NavIcon";
      			}
		    }
            
            public IColumn IsVisual{
                get{
                    return this.GetColumn("IsVisual");
                }
            }
				
   			public static string IsVisualColumn{
			      get{
        			return "IsVisual";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_BizContact
        /// Primary Key: ID
        /// </summary>

        public class Res_BizContactTable: DatabaseTable {
            
            public Res_BizContactTable(IDataProvider provider):base("Res_BizContact",provider){
                ClassName = "Res_BizContact";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("FKID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Fax", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn FKID{
                get{
                    return this.GetColumn("FKID");
                }
            }
				
   			public static string FKIDColumn{
			      get{
        			return "FKID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn Fax{
                get{
                    return this.GetColumn("Fax");
                }
            }
				
   			public static string FaxColumn{
			      get{
        			return "Fax";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_NavGroupRelation
        /// Primary Key: ID
        /// </summary>

        public class Om_NavGroupRelationTable: DatabaseTable {
            
            public Om_NavGroupRelationTable(IDataProvider provider):base("Om_NavGroupRelation",provider){
                ClassName = "Om_NavGroupRelation";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("GroupID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("NavID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn GroupID{
                get{
                    return this.GetColumn("GroupID");
                }
            }
				
   			public static string GroupIDColumn{
			      get{
        			return "GroupID";
      			}
		    }
            
            public IColumn NavID{
                get{
                    return this.GetColumn("NavID");
                }
            }
				
   			public static string NavIDColumn{
			      get{
        			return "NavID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderBalanceItem
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderBalanceItemTable: DatabaseTable {
            
            public Ord_OrderBalanceItemTable(IDataProvider provider):base("Ord_OrderBalanceItem",provider){
                ClassName = "Ord_OrderBalanceItem";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderBalanceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("pSort", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderBalanceID{
                get{
                    return this.GetColumn("OrderBalanceID");
                }
            }
				
   			public static string OrderBalanceIDColumn{
			      get{
        			return "OrderBalanceID";
      			}
		    }
            
            public IColumn ItemName{
                get{
                    return this.GetColumn("ItemName");
                }
            }
				
   			public static string ItemNameColumn{
			      get{
        			return "ItemName";
      			}
		    }
            
            public IColumn ItemType{
                get{
                    return this.GetColumn("ItemType");
                }
            }
				
   			public static string ItemTypeColumn{
			      get{
        			return "ItemType";
      			}
		    }
            
            public IColumn pSort{
                get{
                    return this.GetColumn("pSort");
                }
            }
				
   			public static string pSortColumn{
			      get{
        			return "pSort";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_Quotation
        /// Primary Key: ID
        /// </summary>

        public class Pro_QuotationTable: DatabaseTable {
            
            public Pro_QuotationTable(IDataProvider provider):base("Pro_Quotation",provider){
                ClassName = "Pro_Quotation";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("RouteNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Destination", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationPath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("VisitorNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Days", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Stay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("Dinner", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("ViewSpot", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Feature", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("SelfItem", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Notes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Cost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AvgPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Profit", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ChildPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ChildCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RouteName{
                get{
                    return this.GetColumn("RouteName");
                }
            }
				
   			public static string RouteNameColumn{
			      get{
        			return "RouteName";
      			}
		    }
            
            public IColumn RouteNo{
                get{
                    return this.GetColumn("RouteNo");
                }
            }
				
   			public static string RouteNoColumn{
			      get{
        			return "RouteNo";
      			}
		    }
            
            public IColumn RouteType{
                get{
                    return this.GetColumn("RouteType");
                }
            }
				
   			public static string RouteTypeColumn{
			      get{
        			return "RouteType";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn Destination{
                get{
                    return this.GetColumn("Destination");
                }
            }
				
   			public static string DestinationColumn{
			      get{
        			return "Destination";
      			}
		    }
            
            public IColumn DestinationID{
                get{
                    return this.GetColumn("DestinationID");
                }
            }
				
   			public static string DestinationIDColumn{
			      get{
        			return "DestinationID";
      			}
		    }
            
            public IColumn DestinationPath{
                get{
                    return this.GetColumn("DestinationPath");
                }
            }
				
   			public static string DestinationPathColumn{
			      get{
        			return "DestinationPath";
      			}
		    }
            
            public IColumn VisitorNum{
                get{
                    return this.GetColumn("VisitorNum");
                }
            }
				
   			public static string VisitorNumColumn{
			      get{
        			return "VisitorNum";
      			}
		    }
            
            public IColumn Days{
                get{
                    return this.GetColumn("Days");
                }
            }
				
   			public static string DaysColumn{
			      get{
        			return "Days";
      			}
		    }
            
            public IColumn Stay{
                get{
                    return this.GetColumn("Stay");
                }
            }
				
   			public static string StayColumn{
			      get{
        			return "Stay";
      			}
		    }
            
            public IColumn Dinner{
                get{
                    return this.GetColumn("Dinner");
                }
            }
				
   			public static string DinnerColumn{
			      get{
        			return "Dinner";
      			}
		    }
            
            public IColumn ViewSpot{
                get{
                    return this.GetColumn("ViewSpot");
                }
            }
				
   			public static string ViewSpotColumn{
			      get{
        			return "ViewSpot";
      			}
		    }
            
            public IColumn Feature{
                get{
                    return this.GetColumn("Feature");
                }
            }
				
   			public static string FeatureColumn{
			      get{
        			return "Feature";
      			}
		    }
            
            public IColumn SelfItem{
                get{
                    return this.GetColumn("SelfItem");
                }
            }
				
   			public static string SelfItemColumn{
			      get{
        			return "SelfItem";
      			}
		    }
            
            public IColumn Notes{
                get{
                    return this.GetColumn("Notes");
                }
            }
				
   			public static string NotesColumn{
			      get{
        			return "Notes";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn Cost{
                get{
                    return this.GetColumn("Cost");
                }
            }
				
   			public static string CostColumn{
			      get{
        			return "Cost";
      			}
		    }
            
            public IColumn AvgPrice{
                get{
                    return this.GetColumn("AvgPrice");
                }
            }
				
   			public static string AvgPriceColumn{
			      get{
        			return "AvgPrice";
      			}
		    }
            
            public IColumn Profit{
                get{
                    return this.GetColumn("Profit");
                }
            }
				
   			public static string ProfitColumn{
			      get{
        			return "Profit";
      			}
		    }
            
            public IColumn ChildPrice{
                get{
                    return this.GetColumn("ChildPrice");
                }
            }
				
   			public static string ChildPriceColumn{
			      get{
        			return "ChildPrice";
      			}
		    }
            
            public IColumn ChildCost{
                get{
                    return this.GetColumn("ChildCost");
                }
            }
				
   			public static string ChildCostColumn{
			      get{
        			return "ChildCost";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderBalanceItemData
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderBalanceItemDataTable: DatabaseTable {
            
            public Ord_OrderBalanceItemDataTable(IDataProvider provider):base("Ord_OrderBalanceItemData",provider){
                ClassName = "Ord_OrderBalanceItemDatum";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderBalanceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("ItemPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ItemNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ItemAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsSign", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsInvoice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("pSort", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderBalanceID{
                get{
                    return this.GetColumn("OrderBalanceID");
                }
            }
				
   			public static string OrderBalanceIDColumn{
			      get{
        			return "OrderBalanceID";
      			}
		    }
            
            public IColumn ItemID{
                get{
                    return this.GetColumn("ItemID");
                }
            }
				
   			public static string ItemIDColumn{
			      get{
        			return "ItemID";
      			}
		    }
            
            public IColumn ItemName{
                get{
                    return this.GetColumn("ItemName");
                }
            }
				
   			public static string ItemNameColumn{
			      get{
        			return "ItemName";
      			}
		    }
            
            public IColumn ItemPrice{
                get{
                    return this.GetColumn("ItemPrice");
                }
            }
				
   			public static string ItemPriceColumn{
			      get{
        			return "ItemPrice";
      			}
		    }
            
            public IColumn ItemNum{
                get{
                    return this.GetColumn("ItemNum");
                }
            }
				
   			public static string ItemNumColumn{
			      get{
        			return "ItemNum";
      			}
		    }
            
            public IColumn ItemAmt{
                get{
                    return this.GetColumn("ItemAmt");
                }
            }
				
   			public static string ItemAmtColumn{
			      get{
        			return "ItemAmt";
      			}
		    }
            
            public IColumn IsSign{
                get{
                    return this.GetColumn("IsSign");
                }
            }
				
   			public static string IsSignColumn{
			      get{
        			return "IsSign";
      			}
		    }
            
            public IColumn IsInvoice{
                get{
                    return this.GetColumn("IsInvoice");
                }
            }
				
   			public static string IsInvoiceColumn{
			      get{
        			return "IsInvoice";
      			}
		    }
            
            public IColumn pSort{
                get{
                    return this.GetColumn("pSort");
                }
            }
				
   			public static string pSortColumn{
			      get{
        			return "pSort";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderCollection
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderCollectionTable: DatabaseTable {
            
            public Ord_OrderCollectionTable(IDataProvider provider):base("Ord_OrderCollection",provider){
                ClassName = "Ord_OrderCollection";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CollectAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CollectType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CollectDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CollectBill", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("ClaimID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SrcBank", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SrcName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CollectStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Auditor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AuditorID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AuditDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn CollectAmt{
                get{
                    return this.GetColumn("CollectAmt");
                }
            }
				
   			public static string CollectAmtColumn{
			      get{
        			return "CollectAmt";
      			}
		    }
            
            public IColumn CollectType{
                get{
                    return this.GetColumn("CollectType");
                }
            }
				
   			public static string CollectTypeColumn{
			      get{
        			return "CollectType";
      			}
		    }
            
            public IColumn CollectDate{
                get{
                    return this.GetColumn("CollectDate");
                }
            }
				
   			public static string CollectDateColumn{
			      get{
        			return "CollectDate";
      			}
		    }
            
            public IColumn CollectBill{
                get{
                    return this.GetColumn("CollectBill");
                }
            }
				
   			public static string CollectBillColumn{
			      get{
        			return "CollectBill";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn ClaimID{
                get{
                    return this.GetColumn("ClaimID");
                }
            }
				
   			public static string ClaimIDColumn{
			      get{
        			return "ClaimID";
      			}
		    }
            
            public IColumn SrcBank{
                get{
                    return this.GetColumn("SrcBank");
                }
            }
				
   			public static string SrcBankColumn{
			      get{
        			return "SrcBank";
      			}
		    }
            
            public IColumn SrcName{
                get{
                    return this.GetColumn("SrcName");
                }
            }
				
   			public static string SrcNameColumn{
			      get{
        			return "SrcName";
      			}
		    }
            
            public IColumn CollectStatus{
                get{
                    return this.GetColumn("CollectStatus");
                }
            }
				
   			public static string CollectStatusColumn{
			      get{
        			return "CollectStatus";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn Auditor{
                get{
                    return this.GetColumn("Auditor");
                }
            }
				
   			public static string AuditorColumn{
			      get{
        			return "Auditor";
      			}
		    }
            
            public IColumn AuditorID{
                get{
                    return this.GetColumn("AuditorID");
                }
            }
				
   			public static string AuditorIDColumn{
			      get{
        			return "AuditorID";
      			}
		    }
            
            public IColumn AuditDate{
                get{
                    return this.GetColumn("AuditDate");
                }
            }
				
   			public static string AuditDateColumn{
			      get{
        			return "AuditDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_QuotationCostItem
        /// Primary Key: ID
        /// </summary>

        public class Pro_QuotationCostItemTable: DatabaseTable {
            
            public Pro_QuotationCostItemTable(IDataProvider provider):base("Pro_QuotationCostItem",provider){
                ClassName = "Pro_QuotationCostItem";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QuotationID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("ItemRemark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("ItemPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ItemNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ItemSum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn QuotationID{
                get{
                    return this.GetColumn("QuotationID");
                }
            }
				
   			public static string QuotationIDColumn{
			      get{
        			return "QuotationID";
      			}
		    }
            
            public IColumn ItemName{
                get{
                    return this.GetColumn("ItemName");
                }
            }
				
   			public static string ItemNameColumn{
			      get{
        			return "ItemName";
      			}
		    }
            
            public IColumn ItemRemark{
                get{
                    return this.GetColumn("ItemRemark");
                }
            }
				
   			public static string ItemRemarkColumn{
			      get{
        			return "ItemRemark";
      			}
		    }
            
            public IColumn ItemPrice{
                get{
                    return this.GetColumn("ItemPrice");
                }
            }
				
   			public static string ItemPriceColumn{
			      get{
        			return "ItemPrice";
      			}
		    }
            
            public IColumn ItemNum{
                get{
                    return this.GetColumn("ItemNum");
                }
            }
				
   			public static string ItemNumColumn{
			      get{
        			return "ItemNum";
      			}
		    }
            
            public IColumn ItemSum{
                get{
                    return this.GetColumn("ItemSum");
                }
            }
				
   			public static string ItemSumColumn{
			      get{
        			return "ItemSum";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_OrgReceipt
        /// Primary Key: ID
        /// </summary>

        public class Om_OrgReceiptTable: DatabaseTable {
            
            public Om_OrgReceiptTable(IDataProvider provider):base("Om_OrgReceipt",provider){
                ClassName = "Om_OrgReceipt";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PaidAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("sDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("eDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Receiver", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ReceiveDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("UserCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn PaidAmt{
                get{
                    return this.GetColumn("PaidAmt");
                }
            }
				
   			public static string PaidAmtColumn{
			      get{
        			return "PaidAmt";
      			}
		    }
            
            public IColumn sDate{
                get{
                    return this.GetColumn("sDate");
                }
            }
				
   			public static string sDateColumn{
			      get{
        			return "sDate";
      			}
		    }
            
            public IColumn eDate{
                get{
                    return this.GetColumn("eDate");
                }
            }
				
   			public static string eDateColumn{
			      get{
        			return "eDate";
      			}
		    }
            
            public IColumn Receiver{
                get{
                    return this.GetColumn("Receiver");
                }
            }
				
   			public static string ReceiverColumn{
			      get{
        			return "Receiver";
      			}
		    }
            
            public IColumn ReceiveDate{
                get{
                    return this.GetColumn("ReceiveDate");
                }
            }
				
   			public static string ReceiveDateColumn{
			      get{
        			return "ReceiveDate";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn UserCount{
                get{
                    return this.GetColumn("UserCount");
                }
            }
				
   			public static string UserCountColumn{
			      get{
        			return "UserCount";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderBalance
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderBalanceTable: DatabaseTable {
            
            public Ord_OrderBalanceTable(IDataProvider provider):base("Ord_OrderBalance",provider){
                ClassName = "Ord_OrderBalance";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderGuideID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("GuideName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("GuideMobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("YLTK", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("YLTK_Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("XSTK", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("XSTK_Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("YHZZ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("YHZZ_Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("QTSR", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("QTSR_Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn OrderGuideID{
                get{
                    return this.GetColumn("OrderGuideID");
                }
            }
				
   			public static string OrderGuideIDColumn{
			      get{
        			return "OrderGuideID";
      			}
		    }
            
            public IColumn OrderType{
                get{
                    return this.GetColumn("OrderType");
                }
            }
				
   			public static string OrderTypeColumn{
			      get{
        			return "OrderType";
      			}
		    }
            
            public IColumn AdultNum{
                get{
                    return this.GetColumn("AdultNum");
                }
            }
				
   			public static string AdultNumColumn{
			      get{
        			return "AdultNum";
      			}
		    }
            
            public IColumn ChildNum{
                get{
                    return this.GetColumn("ChildNum");
                }
            }
				
   			public static string ChildNumColumn{
			      get{
        			return "ChildNum";
      			}
		    }
            
            public IColumn GuideName{
                get{
                    return this.GetColumn("GuideName");
                }
            }
				
   			public static string GuideNameColumn{
			      get{
        			return "GuideName";
      			}
		    }
            
            public IColumn GuideMobile{
                get{
                    return this.GetColumn("GuideMobile");
                }
            }
				
   			public static string GuideMobileColumn{
			      get{
        			return "GuideMobile";
      			}
		    }
            
            public IColumn YLTK{
                get{
                    return this.GetColumn("YLTK");
                }
            }
				
   			public static string YLTKColumn{
			      get{
        			return "YLTK";
      			}
		    }
            
            public IColumn YLTK_Remark{
                get{
                    return this.GetColumn("YLTK_Remark");
                }
            }
				
   			public static string YLTK_RemarkColumn{
			      get{
        			return "YLTK_Remark";
      			}
		    }
            
            public IColumn XSTK{
                get{
                    return this.GetColumn("XSTK");
                }
            }
				
   			public static string XSTKColumn{
			      get{
        			return "XSTK";
      			}
		    }
            
            public IColumn XSTK_Remark{
                get{
                    return this.GetColumn("XSTK_Remark");
                }
            }
				
   			public static string XSTK_RemarkColumn{
			      get{
        			return "XSTK_Remark";
      			}
		    }
            
            public IColumn YHZZ{
                get{
                    return this.GetColumn("YHZZ");
                }
            }
				
   			public static string YHZZColumn{
			      get{
        			return "YHZZ";
      			}
		    }
            
            public IColumn YHZZ_Remark{
                get{
                    return this.GetColumn("YHZZ_Remark");
                }
            }
				
   			public static string YHZZ_RemarkColumn{
			      get{
        			return "YHZZ_Remark";
      			}
		    }
            
            public IColumn QTSR{
                get{
                    return this.GetColumn("QTSR");
                }
            }
				
   			public static string QTSRColumn{
			      get{
        			return "QTSR";
      			}
		    }
            
            public IColumn QTSR_Remark{
                get{
                    return this.GetColumn("QTSR_Remark");
                }
            }
				
   			public static string QTSR_RemarkColumn{
			      get{
        			return "QTSR_Remark";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_PushNotice
        /// Primary Key: ID
        /// </summary>

        public class Glo_PushNoticeTable: DatabaseTable {
            
            public Glo_PushNoticeTable(IDataProvider provider):base("Glo_PushNotice",provider){
                ClassName = "Glo_PushNotice";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("nContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("LinkUrl", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("sDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("eDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Creator", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn nContent{
                get{
                    return this.GetColumn("nContent");
                }
            }
				
   			public static string nContentColumn{
			      get{
        			return "nContent";
      			}
		    }
            
            public IColumn LinkUrl{
                get{
                    return this.GetColumn("LinkUrl");
                }
            }
				
   			public static string LinkUrlColumn{
			      get{
        			return "LinkUrl";
      			}
		    }
            
            public IColumn sDate{
                get{
                    return this.GetColumn("sDate");
                }
            }
				
   			public static string sDateColumn{
			      get{
        			return "sDate";
      			}
		    }
            
            public IColumn eDate{
                get{
                    return this.GetColumn("eDate");
                }
            }
				
   			public static string eDateColumn{
			      get{
        			return "eDate";
      			}
		    }
            
            public IColumn Creator{
                get{
                    return this.GetColumn("Creator");
                }
            }
				
   			public static string CreatorColumn{
			      get{
        			return "Creator";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_UserInfo
        /// Primary Key: ID
        /// </summary>

        public class Sys_UserInfoTable: DatabaseTable {
            
            public Sys_UserInfoTable(IDataProvider provider):base("Sys_UserInfo",provider){
                ClassName = "Sys_UserInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IDNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PartDeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PartDeptName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20
                });

                Columns.Add(new DatabaseColumn("Email", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("AcctID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AcctPwd", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn IDNo{
                get{
                    return this.GetColumn("IDNo");
                }
            }
				
   			public static string IDNoColumn{
			      get{
        			return "IDNo";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn DeptName{
                get{
                    return this.GetColumn("DeptName");
                }
            }
				
   			public static string DeptNameColumn{
			      get{
        			return "DeptName";
      			}
		    }
            
            public IColumn PartDeptID{
                get{
                    return this.GetColumn("PartDeptID");
                }
            }
				
   			public static string PartDeptIDColumn{
			      get{
        			return "PartDeptID";
      			}
		    }
            
            public IColumn PartDeptName{
                get{
                    return this.GetColumn("PartDeptName");
                }
            }
				
   			public static string PartDeptNameColumn{
			      get{
        			return "PartDeptName";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Email{
                get{
                    return this.GetColumn("Email");
                }
            }
				
   			public static string EmailColumn{
			      get{
        			return "Email";
      			}
		    }
            
            public IColumn AcctID{
                get{
                    return this.GetColumn("AcctID");
                }
            }
				
   			public static string AcctIDColumn{
			      get{
        			return "AcctID";
      			}
		    }
            
            public IColumn AcctPwd{
                get{
                    return this.GetColumn("AcctPwd");
                }
            }
				
   			public static string AcctPwdColumn{
			      get{
        			return "AcctPwd";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Fin_IncomeCheckIn
        /// Primary Key: ID
        /// </summary>

        public class Fin_IncomeCheckInTable: DatabaseTable {
            
            public Fin_IncomeCheckInTable(IDataProvider provider):base("Fin_IncomeCheckIn",provider){
                ClassName = "Fin_IncomeCheckIn";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IncomeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IncomeDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IncomeMethod", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IncomeType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IncomeTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Operator", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IncomeSource", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn IncomeAmt{
                get{
                    return this.GetColumn("IncomeAmt");
                }
            }
				
   			public static string IncomeAmtColumn{
			      get{
        			return "IncomeAmt";
      			}
		    }
            
            public IColumn IncomeDate{
                get{
                    return this.GetColumn("IncomeDate");
                }
            }
				
   			public static string IncomeDateColumn{
			      get{
        			return "IncomeDate";
      			}
		    }
            
            public IColumn IncomeMethod{
                get{
                    return this.GetColumn("IncomeMethod");
                }
            }
				
   			public static string IncomeMethodColumn{
			      get{
        			return "IncomeMethod";
      			}
		    }
            
            public IColumn IncomeType{
                get{
                    return this.GetColumn("IncomeType");
                }
            }
				
   			public static string IncomeTypeColumn{
			      get{
        			return "IncomeType";
      			}
		    }
            
            public IColumn IncomeTypeID{
                get{
                    return this.GetColumn("IncomeTypeID");
                }
            }
				
   			public static string IncomeTypeIDColumn{
			      get{
        			return "IncomeTypeID";
      			}
		    }
            
            public IColumn Operator{
                get{
                    return this.GetColumn("Operator");
                }
            }
				
   			public static string OperatorColumn{
			      get{
        			return "Operator";
      			}
		    }
            
            public IColumn DeptName{
                get{
                    return this.GetColumn("DeptName");
                }
            }
				
   			public static string DeptNameColumn{
			      get{
        			return "DeptName";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn IncomeSource{
                get{
                    return this.GetColumn("IncomeSource");
                }
            }
				
   			public static string IncomeSourceColumn{
			      get{
        			return "IncomeSource";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_Department
        /// Primary Key: ID
        /// </summary>

        public class Sys_DepartmentTable: DatabaseTable {
            
            public Sys_DepartmentTable(IDataProvider provider):base("Sys_Department",provider){
                ClassName = "Sys_Department";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ParentID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn ParentID{
                get{
                    return this.GetColumn("ParentID");
                }
            }
				
   			public static string ParentIDColumn{
			      get{
        			return "ParentID";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderFile
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderFileTable: DatabaseTable {
            
            public Ord_OrderFileTable(IDataProvider provider):base("Ord_OrderFile",provider){
                ClassName = "Ord_OrderFile";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("FilleD", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Text", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn FilleD{
                get{
                    return this.GetColumn("FilleD");
                }
            }
				
   			public static string FilleDColumn{
			      get{
        			return "FilleD";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn Text{
                get{
                    return this.GetColumn("Text");
                }
            }
				
   			public static string TextColumn{
			      get{
        			return "Text";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_OrgInfo
        /// Primary Key: ID
        /// </summary>

        public class Om_OrgInfoTable: DatabaseTable {
            
            public Om_OrgInfoTable(IDataProvider provider):base("Om_OrgInfo",provider){
                ClassName = "Om_OrgInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("ProDomain", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("ProName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AreaName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AreaID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AreaPath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("OpenDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ExpiryDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgProp", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("OrgContact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ContactPhone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("OrgAdmin", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgInfo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("NavGroup", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("NavGroupID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QRCode", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("eShopGuid", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsOpenEShop", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SmsCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SendSmsCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Brand", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ReceiptAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("MaxUserCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("logo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn ProDomain{
                get{
                    return this.GetColumn("ProDomain");
                }
            }
				
   			public static string ProDomainColumn{
			      get{
        			return "ProDomain";
      			}
		    }
            
            public IColumn ProName{
                get{
                    return this.GetColumn("ProName");
                }
            }
				
   			public static string ProNameColumn{
			      get{
        			return "ProName";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn AreaName{
                get{
                    return this.GetColumn("AreaName");
                }
            }
				
   			public static string AreaNameColumn{
			      get{
        			return "AreaName";
      			}
		    }
            
            public IColumn AreaID{
                get{
                    return this.GetColumn("AreaID");
                }
            }
				
   			public static string AreaIDColumn{
			      get{
        			return "AreaID";
      			}
		    }
            
            public IColumn AreaPath{
                get{
                    return this.GetColumn("AreaPath");
                }
            }
				
   			public static string AreaPathColumn{
			      get{
        			return "AreaPath";
      			}
		    }
            
            public IColumn OpenDate{
                get{
                    return this.GetColumn("OpenDate");
                }
            }
				
   			public static string OpenDateColumn{
			      get{
        			return "OpenDate";
      			}
		    }
            
            public IColumn ExpiryDate{
                get{
                    return this.GetColumn("ExpiryDate");
                }
            }
				
   			public static string ExpiryDateColumn{
			      get{
        			return "ExpiryDate";
      			}
		    }
            
            public IColumn OrgProp{
                get{
                    return this.GetColumn("OrgProp");
                }
            }
				
   			public static string OrgPropColumn{
			      get{
        			return "OrgProp";
      			}
		    }
            
            public IColumn OrgContact{
                get{
                    return this.GetColumn("OrgContact");
                }
            }
				
   			public static string OrgContactColumn{
			      get{
        			return "OrgContact";
      			}
		    }
            
            public IColumn ContactPhone{
                get{
                    return this.GetColumn("ContactPhone");
                }
            }
				
   			public static string ContactPhoneColumn{
			      get{
        			return "ContactPhone";
      			}
		    }
            
            public IColumn OrgAdmin{
                get{
                    return this.GetColumn("OrgAdmin");
                }
            }
				
   			public static string OrgAdminColumn{
			      get{
        			return "OrgAdmin";
      			}
		    }
            
            public IColumn OrgInfo{
                get{
                    return this.GetColumn("OrgInfo");
                }
            }
				
   			public static string OrgInfoColumn{
			      get{
        			return "OrgInfo";
      			}
		    }
            
            public IColumn NavGroup{
                get{
                    return this.GetColumn("NavGroup");
                }
            }
				
   			public static string NavGroupColumn{
			      get{
        			return "NavGroup";
      			}
		    }
            
            public IColumn NavGroupID{
                get{
                    return this.GetColumn("NavGroupID");
                }
            }
				
   			public static string NavGroupIDColumn{
			      get{
        			return "NavGroupID";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn QRCode{
                get{
                    return this.GetColumn("QRCode");
                }
            }
				
   			public static string QRCodeColumn{
			      get{
        			return "QRCode";
      			}
		    }
            
            public IColumn eShopGuid{
                get{
                    return this.GetColumn("eShopGuid");
                }
            }
				
   			public static string eShopGuidColumn{
			      get{
        			return "eShopGuid";
      			}
		    }
            
            public IColumn IsOpenEShop{
                get{
                    return this.GetColumn("IsOpenEShop");
                }
            }
				
   			public static string IsOpenEShopColumn{
			      get{
        			return "IsOpenEShop";
      			}
		    }
            
            public IColumn SmsCount{
                get{
                    return this.GetColumn("SmsCount");
                }
            }
				
   			public static string SmsCountColumn{
			      get{
        			return "SmsCount";
      			}
		    }
            
            public IColumn SendSmsCount{
                get{
                    return this.GetColumn("SendSmsCount");
                }
            }
				
   			public static string SendSmsCountColumn{
			      get{
        			return "SendSmsCount";
      			}
		    }
            
            public IColumn Brand{
                get{
                    return this.GetColumn("Brand");
                }
            }
				
   			public static string BrandColumn{
			      get{
        			return "Brand";
      			}
		    }
            
            public IColumn ReceiptAmt{
                get{
                    return this.GetColumn("ReceiptAmt");
                }
            }
				
   			public static string ReceiptAmtColumn{
			      get{
        			return "ReceiptAmt";
      			}
		    }
            
            public IColumn MaxUserCount{
                get{
                    return this.GetColumn("MaxUserCount");
                }
            }
				
   			public static string MaxUserCountColumn{
			      get{
        			return "MaxUserCount";
      			}
		    }
            
            public IColumn logo{
                get{
                    return this.GetColumn("logo");
                }
            }
				
   			public static string logoColumn{
			      get{
        			return "logo";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Fin_PayCheckIn
        /// Primary Key: ID
        /// </summary>

        public class Fin_PayCheckInTable: DatabaseTable {
            
            public Fin_PayCheckInTable(IDataProvider provider):base("Fin_PayCheckIn",provider){
                ClassName = "Fin_PayCheckIn";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PayType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PayTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PayMethod", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PayAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PayDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Operator", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn PayType{
                get{
                    return this.GetColumn("PayType");
                }
            }
				
   			public static string PayTypeColumn{
			      get{
        			return "PayType";
      			}
		    }
            
            public IColumn PayTypeID{
                get{
                    return this.GetColumn("PayTypeID");
                }
            }
				
   			public static string PayTypeIDColumn{
			      get{
        			return "PayTypeID";
      			}
		    }
            
            public IColumn PayMethod{
                get{
                    return this.GetColumn("PayMethod");
                }
            }
				
   			public static string PayMethodColumn{
			      get{
        			return "PayMethod";
      			}
		    }
            
            public IColumn PayAmt{
                get{
                    return this.GetColumn("PayAmt");
                }
            }
				
   			public static string PayAmtColumn{
			      get{
        			return "PayAmt";
      			}
		    }
            
            public IColumn PayDate{
                get{
                    return this.GetColumn("PayDate");
                }
            }
				
   			public static string PayDateColumn{
			      get{
        			return "PayDate";
      			}
		    }
            
            public IColumn Operator{
                get{
                    return this.GetColumn("Operator");
                }
            }
				
   			public static string OperatorColumn{
			      get{
        			return "Operator";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn DeptName{
                get{
                    return this.GetColumn("DeptName");
                }
            }
				
   			public static string DeptNameColumn{
			      get{
        			return "DeptName";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_Budget
        /// Primary Key: ID
        /// </summary>

        public class Ord_BudgetTable: DatabaseTable {
            
            public Ord_BudgetTable(IDataProvider provider):base("Ord_Budget",provider){
                ClassName = "Ord_Budget";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderProfit", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ProfitRate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn AdultNum{
                get{
                    return this.GetColumn("AdultNum");
                }
            }
				
   			public static string AdultNumColumn{
			      get{
        			return "AdultNum";
      			}
		    }
            
            public IColumn ChildNum{
                get{
                    return this.GetColumn("ChildNum");
                }
            }
				
   			public static string ChildNumColumn{
			      get{
        			return "ChildNum";
      			}
		    }
            
            public IColumn OrderAmt{
                get{
                    return this.GetColumn("OrderAmt");
                }
            }
				
   			public static string OrderAmtColumn{
			      get{
        			return "OrderAmt";
      			}
		    }
            
            public IColumn OrderCost{
                get{
                    return this.GetColumn("OrderCost");
                }
            }
				
   			public static string OrderCostColumn{
			      get{
        			return "OrderCost";
      			}
		    }
            
            public IColumn OrderProfit{
                get{
                    return this.GetColumn("OrderProfit");
                }
            }
				
   			public static string OrderProfitColumn{
			      get{
        			return "OrderProfit";
      			}
		    }
            
            public IColumn ProfitRate{
                get{
                    return this.GetColumn("ProfitRate");
                }
            }
				
   			public static string ProfitRateColumn{
			      get{
        			return "ProfitRate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_TravelAgency
        /// Primary Key: ID
        /// </summary>

        public class Res_TravelAgencyTable: DatabaseTable {
            
            public Res_TravelAgencyTable(IDataProvider provider):base("Res_TravelAgency",provider){
                ClassName = "Res_TravelAgency";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiStringFixedLength,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("Brand", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Fax", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BizType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("Destination", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationPath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("RouteType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BankName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("BankAcct", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn Brand{
                get{
                    return this.GetColumn("Brand");
                }
            }
				
   			public static string BrandColumn{
			      get{
        			return "Brand";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn Fax{
                get{
                    return this.GetColumn("Fax");
                }
            }
				
   			public static string FaxColumn{
			      get{
        			return "Fax";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn BizType{
                get{
                    return this.GetColumn("BizType");
                }
            }
				
   			public static string BizTypeColumn{
			      get{
        			return "BizType";
      			}
		    }
            
            public IColumn Destination{
                get{
                    return this.GetColumn("Destination");
                }
            }
				
   			public static string DestinationColumn{
			      get{
        			return "Destination";
      			}
		    }
            
            public IColumn DestinationID{
                get{
                    return this.GetColumn("DestinationID");
                }
            }
				
   			public static string DestinationIDColumn{
			      get{
        			return "DestinationID";
      			}
		    }
            
            public IColumn DestinationPath{
                get{
                    return this.GetColumn("DestinationPath");
                }
            }
				
   			public static string DestinationPathColumn{
			      get{
        			return "DestinationPath";
      			}
		    }
            
            public IColumn RouteType{
                get{
                    return this.GetColumn("RouteType");
                }
            }
				
   			public static string RouteTypeColumn{
			      get{
        			return "RouteType";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn BankName{
                get{
                    return this.GetColumn("BankName");
                }
            }
				
   			public static string BankNameColumn{
			      get{
        			return "BankName";
      			}
		    }
            
            public IColumn BankAcct{
                get{
                    return this.GetColumn("BankAcct");
                }
            }
				
   			public static string BankAcctColumn{
			      get{
        			return "BankAcct";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderInvoiceItem
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderInvoiceItemTable: DatabaseTable {
            
            public Ord_OrderInvoiceItemTable(IDataProvider provider):base("Ord_OrderInvoiceItem",provider){
                ClassName = "Ord_OrderInvoiceItem";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("InvoiceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("InvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn InvoiceID{
                get{
                    return this.GetColumn("InvoiceID");
                }
            }
				
   			public static string InvoiceIDColumn{
			      get{
        			return "InvoiceID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn InvoiceAmt{
                get{
                    return this.GetColumn("InvoiceAmt");
                }
            }
				
   			public static string InvoiceAmtColumn{
			      get{
        			return "InvoiceAmt";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderLog
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderLogTable: DatabaseTable {
            
            public Ord_OrderLogTable(IDataProvider provider):base("Ord_OrderLog",provider){
                ClassName = "Ord_OrderLog";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("OpIP", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OpBrowser", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn OpIP{
                get{
                    return this.GetColumn("OpIP");
                }
            }
				
   			public static string OpIPColumn{
			      get{
        			return "OpIP";
      			}
		    }
            
            public IColumn OpBrowser{
                get{
                    return this.GetColumn("OpBrowser");
                }
            }
				
   			public static string OpBrowserColumn{
			      get{
        			return "OpBrowser";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_RoleInfo
        /// Primary Key: ID
        /// </summary>

        public class Sys_RoleInfoTable: DatabaseTable {
            
            public Sys_RoleInfoTable(IDataProvider provider):base("Sys_RoleInfo",provider){
                ClassName = "Sys_RoleInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RoleName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RoleMember", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2500
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RoleName{
                get{
                    return this.GetColumn("RoleName");
                }
            }
				
   			public static string RoleNameColumn{
			      get{
        			return "RoleName";
      			}
		    }
            
            public IColumn RoleMember{
                get{
                    return this.GetColumn("RoleMember");
                }
            }
				
   			public static string RoleMemberColumn{
			      get{
        			return "RoleMember";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_RoleMember
        /// Primary Key: ID
        /// </summary>

        public class Sys_RoleMemberTable: DatabaseTable {
            
            public Sys_RoleMemberTable(IDataProvider provider):base("Sys_RoleMember",provider){
                ClassName = "Sys_RoleMember";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RoleID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RoleID{
                get{
                    return this.GetColumn("RoleID");
                }
            }
				
   			public static string RoleIDColumn{
			      get{
        			return "RoleID";
      			}
		    }
            
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn UserName{
                get{
                    return this.GetColumn("UserName");
                }
            }
				
   			public static string UserNameColumn{
			      get{
        			return "UserName";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Rpt_BizStatistic
        /// Primary Key: ID
        /// </summary>

        public class Rpt_BizStatisticTable: DatabaseTable {
            
            public Rpt_BizStatisticTable(IDataProvider provider):base("Rpt_BizStatistic",provider){
                ClassName = "Rpt_BizStatistic";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CustomerNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CustomerNumDay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderNumDay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("VisitorNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("VisitorNumDay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrdeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderAmtDay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderCostDay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn CustomerNum{
                get{
                    return this.GetColumn("CustomerNum");
                }
            }
				
   			public static string CustomerNumColumn{
			      get{
        			return "CustomerNum";
      			}
		    }
            
            public IColumn CustomerNumDay{
                get{
                    return this.GetColumn("CustomerNumDay");
                }
            }
				
   			public static string CustomerNumDayColumn{
			      get{
        			return "CustomerNumDay";
      			}
		    }
            
            public IColumn OrderNum{
                get{
                    return this.GetColumn("OrderNum");
                }
            }
				
   			public static string OrderNumColumn{
			      get{
        			return "OrderNum";
      			}
		    }
            
            public IColumn OrderNumDay{
                get{
                    return this.GetColumn("OrderNumDay");
                }
            }
				
   			public static string OrderNumDayColumn{
			      get{
        			return "OrderNumDay";
      			}
		    }
            
            public IColumn VisitorNum{
                get{
                    return this.GetColumn("VisitorNum");
                }
            }
				
   			public static string VisitorNumColumn{
			      get{
        			return "VisitorNum";
      			}
		    }
            
            public IColumn VisitorNumDay{
                get{
                    return this.GetColumn("VisitorNumDay");
                }
            }
				
   			public static string VisitorNumDayColumn{
			      get{
        			return "VisitorNumDay";
      			}
		    }
            
            public IColumn OrdeAmt{
                get{
                    return this.GetColumn("OrdeAmt");
                }
            }
				
   			public static string OrdeAmtColumn{
			      get{
        			return "OrdeAmt";
      			}
		    }
            
            public IColumn OrderAmtDay{
                get{
                    return this.GetColumn("OrderAmtDay");
                }
            }
				
   			public static string OrderAmtDayColumn{
			      get{
        			return "OrderAmtDay";
      			}
		    }
            
            public IColumn OrderCost{
                get{
                    return this.GetColumn("OrderCost");
                }
            }
				
   			public static string OrderCostColumn{
			      get{
        			return "OrderCost";
      			}
		    }
            
            public IColumn OrderCostDay{
                get{
                    return this.GetColumn("OrderCostDay");
                }
            }
				
   			public static string OrderCostDayColumn{
			      get{
        			return "OrderCostDay";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrgName{
                get{
                    return this.GetColumn("OrgName");
                }
            }
				
   			public static string OrgNameColumn{
			      get{
        			return "OrgName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderSeat
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderSeatTable: DatabaseTable {
            
            public Ord_OrderSeatTable(IDataProvider provider):base("Ord_OrderSeat",provider){
                ClassName = "Ord_OrderSeat";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TourID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SeatNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn TourID{
                get{
                    return this.GetColumn("TourID");
                }
            }
				
   			public static string TourIDColumn{
			      get{
        			return "TourID";
      			}
		    }
            
            public IColumn SeatNum{
                get{
                    return this.GetColumn("SeatNum");
                }
            }
				
   			public static string SeatNumColumn{
			      get{
        			return "SeatNum";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Fin_OrderPayable
        /// Primary Key: ID
        /// </summary>

        public class Fin_OrderPayableTable: DatabaseTable {
            
            public Fin_OrderPayableTable(IDataProvider provider):base("Fin_OrderPayable",provider){
                ClassName = "Fin_OrderPayable";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderCostItemID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SupplierID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SupplierName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Amount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PayDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PayType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderCostItemID{
                get{
                    return this.GetColumn("OrderCostItemID");
                }
            }
				
   			public static string OrderCostItemIDColumn{
			      get{
        			return "OrderCostItemID";
      			}
		    }
            
            public IColumn SupplierID{
                get{
                    return this.GetColumn("SupplierID");
                }
            }
				
   			public static string SupplierIDColumn{
			      get{
        			return "SupplierID";
      			}
		    }
            
            public IColumn SupplierName{
                get{
                    return this.GetColumn("SupplierName");
                }
            }
				
   			public static string SupplierNameColumn{
			      get{
        			return "SupplierName";
      			}
		    }
            
            public IColumn Amount{
                get{
                    return this.GetColumn("Amount");
                }
            }
				
   			public static string AmountColumn{
			      get{
        			return "Amount";
      			}
		    }
            
            public IColumn PayDate{
                get{
                    return this.GetColumn("PayDate");
                }
            }
				
   			public static string PayDateColumn{
			      get{
        			return "PayDate";
      			}
		    }
            
            public IColumn PayType{
                get{
                    return this.GetColumn("PayType");
                }
            }
				
   			public static string PayTypeColumn{
			      get{
        			return "PayType";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_Notice
        /// Primary Key: ID
        /// </summary>

        public class Glo_NoticeTable: DatabaseTable {
            
            public Glo_NoticeTable(IDataProvider provider):base("Glo_Notice",provider){
                ClassName = "Glo_Notice";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Subject", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("nContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("DeptName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("IsAll", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Subject{
                get{
                    return this.GetColumn("Subject");
                }
            }
				
   			public static string SubjectColumn{
			      get{
        			return "Subject";
      			}
		    }
            
            public IColumn nContent{
                get{
                    return this.GetColumn("nContent");
                }
            }
				
   			public static string nContentColumn{
			      get{
        			return "nContent";
      			}
		    }
            
            public IColumn DeptName{
                get{
                    return this.GetColumn("DeptName");
                }
            }
				
   			public static string DeptNameColumn{
			      get{
        			return "DeptName";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn IsAll{
                get{
                    return this.GetColumn("IsAll");
                }
            }
				
   			public static string IsAllColumn{
			      get{
        			return "IsAll";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_Permission
        /// Primary Key: ID
        /// </summary>

        public class Sys_PermissionTable: DatabaseTable {
            
            public Sys_PermissionTable(IDataProvider provider):base("Sys_Permission",provider){
                ClassName = "Sys_Permission";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RoleID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("NavID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Permission", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RoleID{
                get{
                    return this.GetColumn("RoleID");
                }
            }
				
   			public static string RoleIDColumn{
			      get{
        			return "RoleID";
      			}
		    }
            
            public IColumn NavID{
                get{
                    return this.GetColumn("NavID");
                }
            }
				
   			public static string NavIDColumn{
			      get{
        			return "NavID";
      			}
		    }
            
            public IColumn Permission{
                get{
                    return this.GetColumn("Permission");
                }
            }
				
   			public static string PermissionColumn{
			      get{
        			return "Permission";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_OrderPermission
        /// Primary Key: ID
        /// </summary>

        public class Sys_OrderPermissionTable: DatabaseTable {
            
            public Sys_OrderPermissionTable(IDataProvider provider):base("Sys_OrderPermission",provider){
                ClassName = "Sys_OrderPermission";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RoleID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RoleID{
                get{
                    return this.GetColumn("RoleID");
                }
            }
				
   			public static string RoleIDColumn{
			      get{
        			return "RoleID";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_Log
        /// Primary Key: ID
        /// </summary>

        public class Sys_LogTable: DatabaseTable {
            
            public Sys_LogTable(IDataProvider provider):base("Sys_Log",provider){
                ClassName = "Sys_Log";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("LogDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Message", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2147483647
                });

                Columns.Add(new DatabaseColumn("Exception", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2147483647
                });

                Columns.Add(new DatabaseColumn("LogType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("LogAction", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IP", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Browser", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Platform", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Creator", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreatorID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn LogDate{
                get{
                    return this.GetColumn("LogDate");
                }
            }
				
   			public static string LogDateColumn{
			      get{
        			return "LogDate";
      			}
		    }
            
            public IColumn Message{
                get{
                    return this.GetColumn("Message");
                }
            }
				
   			public static string MessageColumn{
			      get{
        			return "Message";
      			}
		    }
            
            public IColumn Exception{
                get{
                    return this.GetColumn("Exception");
                }
            }
				
   			public static string ExceptionColumn{
			      get{
        			return "Exception";
      			}
		    }
            
            public IColumn LogType{
                get{
                    return this.GetColumn("LogType");
                }
            }
				
   			public static string LogTypeColumn{
			      get{
        			return "LogType";
      			}
		    }
            
            public IColumn LogAction{
                get{
                    return this.GetColumn("LogAction");
                }
            }
				
   			public static string LogActionColumn{
			      get{
        			return "LogAction";
      			}
		    }
            
            public IColumn IP{
                get{
                    return this.GetColumn("IP");
                }
            }
				
   			public static string IPColumn{
			      get{
        			return "IP";
      			}
		    }
            
            public IColumn Browser{
                get{
                    return this.GetColumn("Browser");
                }
            }
				
   			public static string BrowserColumn{
			      get{
        			return "Browser";
      			}
		    }
            
            public IColumn Platform{
                get{
                    return this.GetColumn("Platform");
                }
            }
				
   			public static string PlatformColumn{
			      get{
        			return "Platform";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn Creator{
                get{
                    return this.GetColumn("Creator");
                }
            }
				
   			public static string CreatorColumn{
			      get{
        			return "Creator";
      			}
		    }
            
            public IColumn CreatorID{
                get{
                    return this.GetColumn("CreatorID");
                }
            }
				
   			public static string CreatorIDColumn{
			      get{
        			return "CreatorID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_DataPermission
        /// Primary Key: ID
        /// </summary>

        public class Sys_DataPermissionTable: DatabaseTable {
            
            public Sys_DataPermissionTable(IDataProvider provider):base("Sys_DataPermission",provider){
                ClassName = "Sys_DataPermission";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RoleID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Permission", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RoleID{
                get{
                    return this.GetColumn("RoleID");
                }
            }
				
   			public static string RoleIDColumn{
			      get{
        			return "RoleID";
      			}
		    }
            
            public IColumn Permission{
                get{
                    return this.GetColumn("Permission");
                }
            }
				
   			public static string PermissionColumn{
			      get{
        			return "Permission";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_RouteSchedule
        /// Primary Key: ID
        /// </summary>

        public class Pro_RouteScheduleTable: DatabaseTable {
            
            public Pro_RouteScheduleTable(IDataProvider provider):base("Pro_RouteSchedule",provider){
                ClassName = "Pro_RouteSchedule";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DayNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Schedule", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });

                Columns.Add(new DatabaseColumn("Dinner", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("Stay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Traffic", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RouteID{
                get{
                    return this.GetColumn("RouteID");
                }
            }
				
   			public static string RouteIDColumn{
			      get{
        			return "RouteID";
      			}
		    }
            
            public IColumn DayNum{
                get{
                    return this.GetColumn("DayNum");
                }
            }
				
   			public static string DayNumColumn{
			      get{
        			return "DayNum";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn Schedule{
                get{
                    return this.GetColumn("Schedule");
                }
            }
				
   			public static string ScheduleColumn{
			      get{
        			return "Schedule";
      			}
		    }
            
            public IColumn Dinner{
                get{
                    return this.GetColumn("Dinner");
                }
            }
				
   			public static string DinnerColumn{
			      get{
        			return "Dinner";
      			}
		    }
            
            public IColumn Stay{
                get{
                    return this.GetColumn("Stay");
                }
            }
				
   			public static string StayColumn{
			      get{
        			return "Stay";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn Traffic{
                get{
                    return this.GetColumn("Traffic");
                }
            }
				
   			public static string TrafficColumn{
			      get{
        			return "Traffic";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_Guide
        /// Primary Key: ID
        /// </summary>

        public class Res_GuideTable: DatabaseTable {
            
            public Res_GuideTable(IDataProvider provider):base("Res_Guide",provider){
                ClassName = "Res_Guide";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DepartureID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DepartureName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Sex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2
                });

                Columns.Add(new DatabaseColumn("GuideLevel", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("IsIDCard", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IDCardNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsLeaderCard", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Language", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Skill", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("BankName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("BankAcct", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("IDNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn DepartureID{
                get{
                    return this.GetColumn("DepartureID");
                }
            }
				
   			public static string DepartureIDColumn{
			      get{
        			return "DepartureID";
      			}
		    }
            
            public IColumn DepartureName{
                get{
                    return this.GetColumn("DepartureName");
                }
            }
				
   			public static string DepartureNameColumn{
			      get{
        			return "DepartureName";
      			}
		    }
            
            public IColumn Sex{
                get{
                    return this.GetColumn("Sex");
                }
            }
				
   			public static string SexColumn{
			      get{
        			return "Sex";
      			}
		    }
            
            public IColumn GuideLevel{
                get{
                    return this.GetColumn("GuideLevel");
                }
            }
				
   			public static string GuideLevelColumn{
			      get{
        			return "GuideLevel";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn IsIDCard{
                get{
                    return this.GetColumn("IsIDCard");
                }
            }
				
   			public static string IsIDCardColumn{
			      get{
        			return "IsIDCard";
      			}
		    }
            
            public IColumn IDCardNum{
                get{
                    return this.GetColumn("IDCardNum");
                }
            }
				
   			public static string IDCardNumColumn{
			      get{
        			return "IDCardNum";
      			}
		    }
            
            public IColumn IsLeaderCard{
                get{
                    return this.GetColumn("IsLeaderCard");
                }
            }
				
   			public static string IsLeaderCardColumn{
			      get{
        			return "IsLeaderCard";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Language{
                get{
                    return this.GetColumn("Language");
                }
            }
				
   			public static string LanguageColumn{
			      get{
        			return "Language";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn Skill{
                get{
                    return this.GetColumn("Skill");
                }
            }
				
   			public static string SkillColumn{
			      get{
        			return "Skill";
      			}
		    }
            
            public IColumn BankName{
                get{
                    return this.GetColumn("BankName");
                }
            }
				
   			public static string BankNameColumn{
			      get{
        			return "BankName";
      			}
		    }
            
            public IColumn BankAcct{
                get{
                    return this.GetColumn("BankAcct");
                }
            }
				
   			public static string BankAcctColumn{
			      get{
        			return "BankAcct";
      			}
		    }
            
            public IColumn IDNum{
                get{
                    return this.GetColumn("IDNum");
                }
            }
				
   			public static string IDNumColumn{
			      get{
        			return "IDNum";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_TourInfo
        /// Primary Key: ID
        /// </summary>

        public class Pro_TourInfoTable: DatabaseTable {
            
            public Pro_TourInfoTable(IDataProvider provider):base("Pro_TourInfo",provider){
                ClassName = "Pro_TourInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TourStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TourName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("TourNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SeatNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PlanNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ClusterNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TourDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TourDays", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ExpiryDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ReturnDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DefaultPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("NoticePath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UpdateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RouteID{
                get{
                    return this.GetColumn("RouteID");
                }
            }
				
   			public static string RouteIDColumn{
			      get{
        			return "RouteID";
      			}
		    }
            
            public IColumn TourStatus{
                get{
                    return this.GetColumn("TourStatus");
                }
            }
				
   			public static string TourStatusColumn{
			      get{
        			return "TourStatus";
      			}
		    }
            
            public IColumn TourName{
                get{
                    return this.GetColumn("TourName");
                }
            }
				
   			public static string TourNameColumn{
			      get{
        			return "TourName";
      			}
		    }
            
            public IColumn TourNo{
                get{
                    return this.GetColumn("TourNo");
                }
            }
				
   			public static string TourNoColumn{
			      get{
        			return "TourNo";
      			}
		    }
            
            public IColumn SeatNum{
                get{
                    return this.GetColumn("SeatNum");
                }
            }
				
   			public static string SeatNumColumn{
			      get{
        			return "SeatNum";
      			}
		    }
            
            public IColumn PlanNum{
                get{
                    return this.GetColumn("PlanNum");
                }
            }
				
   			public static string PlanNumColumn{
			      get{
        			return "PlanNum";
      			}
		    }
            
            public IColumn ClusterNum{
                get{
                    return this.GetColumn("ClusterNum");
                }
            }
				
   			public static string ClusterNumColumn{
			      get{
        			return "ClusterNum";
      			}
		    }
            
            public IColumn TourDate{
                get{
                    return this.GetColumn("TourDate");
                }
            }
				
   			public static string TourDateColumn{
			      get{
        			return "TourDate";
      			}
		    }
            
            public IColumn TourDays{
                get{
                    return this.GetColumn("TourDays");
                }
            }
				
   			public static string TourDaysColumn{
			      get{
        			return "TourDays";
      			}
		    }
            
            public IColumn ExpiryDate{
                get{
                    return this.GetColumn("ExpiryDate");
                }
            }
				
   			public static string ExpiryDateColumn{
			      get{
        			return "ExpiryDate";
      			}
		    }
            
            public IColumn ReturnDate{
                get{
                    return this.GetColumn("ReturnDate");
                }
            }
				
   			public static string ReturnDateColumn{
			      get{
        			return "ReturnDate";
      			}
		    }
            
            public IColumn DefaultPrice{
                get{
                    return this.GetColumn("DefaultPrice");
                }
            }
				
   			public static string DefaultPriceColumn{
			      get{
        			return "DefaultPrice";
      			}
		    }
            
            public IColumn NoticePath{
                get{
                    return this.GetColumn("NoticePath");
                }
            }
				
   			public static string NoticePathColumn{
			      get{
        			return "NoticePath";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn UpdateUserID{
                get{
                    return this.GetColumn("UpdateUserID");
                }
            }
				
   			public static string UpdateUserIDColumn{
			      get{
        			return "UpdateUserID";
      			}
		    }
            
            public IColumn UpdateUserName{
                get{
                    return this.GetColumn("UpdateUserName");
                }
            }
				
   			public static string UpdateUserNameColumn{
			      get{
        			return "UpdateUserName";
      			}
		    }
            
            public IColumn UpdateDate{
                get{
                    return this.GetColumn("UpdateDate");
                }
            }
				
   			public static string UpdateDateColumn{
			      get{
        			return "UpdateDate";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Fin_CollectedItem
        /// Primary Key: ID
        /// </summary>

        public class Fin_CollectedItemTable: DatabaseTable {
            
            public Fin_CollectedItemTable(IDataProvider provider):base("Fin_CollectedItem",provider){
                ClassName = "Fin_CollectedItem";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BankName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("TradeDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Summary", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("IncomeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FromBank", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("FromAcct", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2000
                });

                Columns.Add(new DatabaseColumn("OrderType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2000
                });

                Columns.Add(new DatabaseColumn("BillNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ClaimUser", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("Creator", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreatorID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BatchCollected", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn BankName{
                get{
                    return this.GetColumn("BankName");
                }
            }
				
   			public static string BankNameColumn{
			      get{
        			return "BankName";
      			}
		    }
            
            public IColumn TradeDate{
                get{
                    return this.GetColumn("TradeDate");
                }
            }
				
   			public static string TradeDateColumn{
			      get{
        			return "TradeDate";
      			}
		    }
            
            public IColumn TradeTime{
                get{
                    return this.GetColumn("TradeTime");
                }
            }
				
   			public static string TradeTimeColumn{
			      get{
        			return "TradeTime";
      			}
		    }
            
            public IColumn Summary{
                get{
                    return this.GetColumn("Summary");
                }
            }
				
   			public static string SummaryColumn{
			      get{
        			return "Summary";
      			}
		    }
            
            public IColumn IncomeAmt{
                get{
                    return this.GetColumn("IncomeAmt");
                }
            }
				
   			public static string IncomeAmtColumn{
			      get{
        			return "IncomeAmt";
      			}
		    }
            
            public IColumn FromBank{
                get{
                    return this.GetColumn("FromBank");
                }
            }
				
   			public static string FromBankColumn{
			      get{
        			return "FromBank";
      			}
		    }
            
            public IColumn FromAcct{
                get{
                    return this.GetColumn("FromAcct");
                }
            }
				
   			public static string FromAcctColumn{
			      get{
        			return "FromAcct";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn OrderNo{
                get{
                    return this.GetColumn("OrderNo");
                }
            }
				
   			public static string OrderNoColumn{
			      get{
        			return "OrderNo";
      			}
		    }
            
            public IColumn OrderType{
                get{
                    return this.GetColumn("OrderType");
                }
            }
				
   			public static string OrderTypeColumn{
			      get{
        			return "OrderType";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn BillNo{
                get{
                    return this.GetColumn("BillNo");
                }
            }
				
   			public static string BillNoColumn{
			      get{
        			return "BillNo";
      			}
		    }
            
            public IColumn ClaimUser{
                get{
                    return this.GetColumn("ClaimUser");
                }
            }
				
   			public static string ClaimUserColumn{
			      get{
        			return "ClaimUser";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn Creator{
                get{
                    return this.GetColumn("Creator");
                }
            }
				
   			public static string CreatorColumn{
			      get{
        			return "Creator";
      			}
		    }
            
            public IColumn CreatorID{
                get{
                    return this.GetColumn("CreatorID");
                }
            }
				
   			public static string CreatorIDColumn{
			      get{
        			return "CreatorID";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn BatchCollected{
                get{
                    return this.GetColumn("BatchCollected");
                }
            }
				
   			public static string BatchCollectedColumn{
			      get{
        			return "BatchCollected";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_SerialNo
        /// Primary Key: ID
        /// </summary>

        public class Glo_SerialNoTable: DatabaseTable {
            
            public Glo_SerialNoTable(IDataProvider provider):base("Glo_SerialNo",provider){
                ClassName = "Glo_SerialNo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SerialType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SerialNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn SerialType{
                get{
                    return this.GetColumn("SerialType");
                }
            }
				
   			public static string SerialTypeColumn{
			      get{
        			return "SerialType";
      			}
		    }
            
            public IColumn SerialNo{
                get{
                    return this.GetColumn("SerialNo");
                }
            }
				
   			public static string SerialNoColumn{
			      get{
        			return "SerialNo";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_TourPrice
        /// Primary Key: ID
        /// </summary>

        public class Pro_TourPriceTable: DatabaseTable {
            
            public Pro_TourPriceTable(IDataProvider provider):base("Pro_TourPrice",provider){
                ClassName = "Pro_TourPrice";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TourID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SalePrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Rebate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RoomRate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsSeat", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsChild", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsDefault", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UpdateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn TourID{
                get{
                    return this.GetColumn("TourID");
                }
            }
				
   			public static string TourIDColumn{
			      get{
        			return "TourID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn SalePrice{
                get{
                    return this.GetColumn("SalePrice");
                }
            }
				
   			public static string SalePriceColumn{
			      get{
        			return "SalePrice";
      			}
		    }
            
            public IColumn Rebate{
                get{
                    return this.GetColumn("Rebate");
                }
            }
				
   			public static string RebateColumn{
			      get{
        			return "Rebate";
      			}
		    }
            
            public IColumn RoomRate{
                get{
                    return this.GetColumn("RoomRate");
                }
            }
				
   			public static string RoomRateColumn{
			      get{
        			return "RoomRate";
      			}
		    }
            
            public IColumn IsSeat{
                get{
                    return this.GetColumn("IsSeat");
                }
            }
				
   			public static string IsSeatColumn{
			      get{
        			return "IsSeat";
      			}
		    }
            
            public IColumn IsChild{
                get{
                    return this.GetColumn("IsChild");
                }
            }
				
   			public static string IsChildColumn{
			      get{
        			return "IsChild";
      			}
		    }
            
            public IColumn IsDefault{
                get{
                    return this.GetColumn("IsDefault");
                }
            }
				
   			public static string IsDefaultColumn{
			      get{
        			return "IsDefault";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn UpdateUserID{
                get{
                    return this.GetColumn("UpdateUserID");
                }
            }
				
   			public static string UpdateUserIDColumn{
			      get{
        			return "UpdateUserID";
      			}
		    }
            
            public IColumn UpdateUserName{
                get{
                    return this.GetColumn("UpdateUserName");
                }
            }
				
   			public static string UpdateUserNameColumn{
			      get{
        			return "UpdateUserName";
      			}
		    }
            
            public IColumn UpdateDate{
                get{
                    return this.GetColumn("UpdateDate");
                }
            }
				
   			public static string UpdateDateColumn{
			      get{
        			return "UpdateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_TourSeatLock
        /// Primary Key: ID
        /// </summary>

        public class Pro_TourSeatLockTable: DatabaseTable {
            
            public Pro_TourSeatLockTable(IDataProvider provider):base("Pro_TourSeatLock",provider){
                ClassName = "Pro_TourSeatLock";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TourID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SeatNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UpdateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn TourID{
                get{
                    return this.GetColumn("TourID");
                }
            }
				
   			public static string TourIDColumn{
			      get{
        			return "TourID";
      			}
		    }
            
            public IColumn SeatNo{
                get{
                    return this.GetColumn("SeatNo");
                }
            }
				
   			public static string SeatNoColumn{
			      get{
        			return "SeatNo";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn UpdateUserID{
                get{
                    return this.GetColumn("UpdateUserID");
                }
            }
				
   			public static string UpdateUserIDColumn{
			      get{
        			return "UpdateUserID";
      			}
		    }
            
            public IColumn UpdateUserName{
                get{
                    return this.GetColumn("UpdateUserName");
                }
            }
				
   			public static string UpdateUserNameColumn{
			      get{
        			return "UpdateUserName";
      			}
		    }
            
            public IColumn UpdateDate{
                get{
                    return this.GetColumn("UpdateDate");
                }
            }
				
   			public static string UpdateDateColumn{
			      get{
        			return "UpdateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_TourVenue
        /// Primary Key: ID
        /// </summary>

        public class Pro_TourVenueTable: DatabaseTable {
            
            public Pro_TourVenueTable(IDataProvider provider):base("Pro_TourVenue",provider){
                ClassName = "Pro_TourVenue";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TourID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("MeetTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PickAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SendAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DepartureID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Departure", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UpdateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn TourID{
                get{
                    return this.GetColumn("TourID");
                }
            }
				
   			public static string TourIDColumn{
			      get{
        			return "TourID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn MeetTime{
                get{
                    return this.GetColumn("MeetTime");
                }
            }
				
   			public static string MeetTimeColumn{
			      get{
        			return "MeetTime";
      			}
		    }
            
            public IColumn PickAmt{
                get{
                    return this.GetColumn("PickAmt");
                }
            }
				
   			public static string PickAmtColumn{
			      get{
        			return "PickAmt";
      			}
		    }
            
            public IColumn SendAmt{
                get{
                    return this.GetColumn("SendAmt");
                }
            }
				
   			public static string SendAmtColumn{
			      get{
        			return "SendAmt";
      			}
		    }
            
            public IColumn DepartureID{
                get{
                    return this.GetColumn("DepartureID");
                }
            }
				
   			public static string DepartureIDColumn{
			      get{
        			return "DepartureID";
      			}
		    }
            
            public IColumn Departure{
                get{
                    return this.GetColumn("Departure");
                }
            }
				
   			public static string DepartureColumn{
			      get{
        			return "Departure";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn UpdateUserID{
                get{
                    return this.GetColumn("UpdateUserID");
                }
            }
				
   			public static string UpdateUserIDColumn{
			      get{
        			return "UpdateUserID";
      			}
		    }
            
            public IColumn UpdateUserName{
                get{
                    return this.GetColumn("UpdateUserName");
                }
            }
				
   			public static string UpdateUserNameColumn{
			      get{
        			return "UpdateUserName";
      			}
		    }
            
            public IColumn UpdateDate{
                get{
                    return this.GetColumn("UpdateDate");
                }
            }
				
   			public static string UpdateDateColumn{
			      get{
        			return "UpdateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_Other
        /// Primary Key: ID
        /// </summary>

        public class Res_OtherTable: DatabaseTable {
            
            public Res_OtherTable(IDataProvider provider):base("Res_Other",provider){
                ClassName = "Res_Other";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("TicketType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn TicketType{
                get{
                    return this.GetColumn("TicketType");
                }
            }
				
   			public static string TicketTypeColumn{
			      get{
        			return "TicketType";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sys_IPFilter
        /// Primary Key: ID
        /// </summary>

        public class Sys_IPFilterTable: DatabaseTable {
            
            public Sys_IPFilterTable(IDataProvider provider):base("Sys_IPFilter",provider){
                ClassName = "Sys_IPFilter";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("StartIP", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("EndIP", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("xType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Creator", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreatorID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn StartIP{
                get{
                    return this.GetColumn("StartIP");
                }
            }
				
   			public static string StartIPColumn{
			      get{
        			return "StartIP";
      			}
		    }
            
            public IColumn EndIP{
                get{
                    return this.GetColumn("EndIP");
                }
            }
				
   			public static string EndIPColumn{
			      get{
        			return "EndIP";
      			}
		    }
            
            public IColumn xType{
                get{
                    return this.GetColumn("xType");
                }
            }
				
   			public static string xTypeColumn{
			      get{
        			return "xType";
      			}
		    }
            
            public IColumn Creator{
                get{
                    return this.GetColumn("Creator");
                }
            }
				
   			public static string CreatorColumn{
			      get{
        			return "Creator";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreatorID{
                get{
                    return this.GetColumn("CreatorID");
                }
            }
				
   			public static string CreatorIDColumn{
			      get{
        			return "CreatorID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_SupplierInvoice
        /// Primary Key: ID
        /// </summary>

        public class Ord_SupplierInvoiceTable: DatabaseTable {
            
            public Ord_SupplierInvoiceTable(IDataProvider provider):base("Ord_SupplierInvoice",provider){
                ClassName = "Ord_SupplierInvoice";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("InvoiceNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("InvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2500
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn InvoiceNo{
                get{
                    return this.GetColumn("InvoiceNo");
                }
            }
				
   			public static string InvoiceNoColumn{
			      get{
        			return "InvoiceNo";
      			}
		    }
            
            public IColumn InvoiceAmt{
                get{
                    return this.GetColumn("InvoiceAmt");
                }
            }
				
   			public static string InvoiceAmtColumn{
			      get{
        			return "InvoiceAmt";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderCostItem
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderCostItemTable: DatabaseTable {
            
            public Ord_OrderCostItemTable(IDataProvider provider):base("Ord_OrderCostItem",provider){
                ClassName = "Ord_OrderCostItem";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ItemName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SupplierID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Supplier", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("CostAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn ItemType{
                get{
                    return this.GetColumn("ItemType");
                }
            }
				
   			public static string ItemTypeColumn{
			      get{
        			return "ItemType";
      			}
		    }
            
            public IColumn ItemName{
                get{
                    return this.GetColumn("ItemName");
                }
            }
				
   			public static string ItemNameColumn{
			      get{
        			return "ItemName";
      			}
		    }
            
            public IColumn SupplierID{
                get{
                    return this.GetColumn("SupplierID");
                }
            }
				
   			public static string SupplierIDColumn{
			      get{
        			return "SupplierID";
      			}
		    }
            
            public IColumn Supplier{
                get{
                    return this.GetColumn("Supplier");
                }
            }
				
   			public static string SupplierColumn{
			      get{
        			return "Supplier";
      			}
		    }
            
            public IColumn CostAmt{
                get{
                    return this.GetColumn("CostAmt");
                }
            }
				
   			public static string CostAmtColumn{
			      get{
        			return "CostAmt";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_Visa
        /// Primary Key: ID
        /// </summary>

        public class Res_VisaTable: DatabaseTable {
            
            public Res_VisaTable(IDataProvider provider):base("Res_Visa",provider){
                ClassName = "Res_Visa";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Wechat", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BizType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("BankInfo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Wechat{
                get{
                    return this.GetColumn("Wechat");
                }
            }
				
   			public static string WechatColumn{
			      get{
        			return "Wechat";
      			}
		    }
            
            public IColumn BizType{
                get{
                    return this.GetColumn("BizType");
                }
            }
				
   			public static string BizTypeColumn{
			      get{
        			return "BizType";
      			}
		    }
            
            public IColumn BankInfo{
                get{
                    return this.GetColumn("BankInfo");
                }
            }
				
   			public static string BankInfoColumn{
			      get{
        			return "BankInfo";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: OTA_AreaSetting
        /// Primary Key: ID
        /// </summary>

        public class OTA_AreaSettingTable: DatabaseTable {
            
            public OTA_AreaSettingTable(IDataProvider provider):base("OTA_AreaSetting",provider){
                ClassName = "OTA_AreaSetting";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OTAID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OTAareaID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OTAareaName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OTAareaType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AreaID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AreaName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OTAID{
                get{
                    return this.GetColumn("OTAID");
                }
            }
				
   			public static string OTAIDColumn{
			      get{
        			return "OTAID";
      			}
		    }
            
            public IColumn OTAareaID{
                get{
                    return this.GetColumn("OTAareaID");
                }
            }
				
   			public static string OTAareaIDColumn{
			      get{
        			return "OTAareaID";
      			}
		    }
            
            public IColumn OTAareaName{
                get{
                    return this.GetColumn("OTAareaName");
                }
            }
				
   			public static string OTAareaNameColumn{
			      get{
        			return "OTAareaName";
      			}
		    }
            
            public IColumn OTAareaType{
                get{
                    return this.GetColumn("OTAareaType");
                }
            }
				
   			public static string OTAareaTypeColumn{
			      get{
        			return "OTAareaType";
      			}
		    }
            
            public IColumn AreaID{
                get{
                    return this.GetColumn("AreaID");
                }
            }
				
   			public static string AreaIDColumn{
			      get{
        			return "AreaID";
      			}
		    }
            
            public IColumn AreaName{
                get{
                    return this.GetColumn("AreaName");
                }
            }
				
   			public static string AreaNameColumn{
			      get{
        			return "AreaName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Crm_Customer
        /// Primary Key: ID
        /// </summary>

        public class Crm_CustomerTable: DatabaseTable {
            
            public Crm_CustomerTable(IDataProvider provider):base("Crm_Customer",provider){
                ClassName = "Crm_Customer";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Company", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("Sex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IDNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Fax", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("CustomerType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CommunicateNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20
                });

                Columns.Add(new DatabaseColumn("CustomerCertificate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2000
                });

                Columns.Add(new DatabaseColumn("EngName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BirthDay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Company{
                get{
                    return this.GetColumn("Company");
                }
            }
				
   			public static string CompanyColumn{
			      get{
        			return "Company";
      			}
		    }
            
            public IColumn Sex{
                get{
                    return this.GetColumn("Sex");
                }
            }
				
   			public static string SexColumn{
			      get{
        			return "Sex";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn IDNum{
                get{
                    return this.GetColumn("IDNum");
                }
            }
				
   			public static string IDNumColumn{
			      get{
        			return "IDNum";
      			}
		    }
            
            public IColumn Fax{
                get{
                    return this.GetColumn("Fax");
                }
            }
				
   			public static string FaxColumn{
			      get{
        			return "Fax";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn CustomerType{
                get{
                    return this.GetColumn("CustomerType");
                }
            }
				
   			public static string CustomerTypeColumn{
			      get{
        			return "CustomerType";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn CommunicateNum{
                get{
                    return this.GetColumn("CommunicateNum");
                }
            }
				
   			public static string CommunicateNumColumn{
			      get{
        			return "CommunicateNum";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn CustomerCertificate{
                get{
                    return this.GetColumn("CustomerCertificate");
                }
            }
				
   			public static string CustomerCertificateColumn{
			      get{
        			return "CustomerCertificate";
      			}
		    }
            
            public IColumn EngName{
                get{
                    return this.GetColumn("EngName");
                }
            }
				
   			public static string EngNameColumn{
			      get{
        			return "EngName";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn BirthDay{
                get{
                    return this.GetColumn("BirthDay");
                }
            }
				
   			public static string BirthDayColumn{
			      get{
        			return "BirthDay";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: OTA_Setting
        /// Primary Key: ID
        /// </summary>

        public class OTA_SettingTable: DatabaseTable {
            
            public OTA_SettingTable(IDataProvider provider):base("OTA_Setting",provider){
                ClassName = "OTA_Setting";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OTAID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OTAName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("AcctID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AcctPwd", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AppId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AppKey", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OTA", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SyncType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Creator", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreatorID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OTAServiceUrl", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OTAID{
                get{
                    return this.GetColumn("OTAID");
                }
            }
				
   			public static string OTAIDColumn{
			      get{
        			return "OTAID";
      			}
		    }
            
            public IColumn OTAName{
                get{
                    return this.GetColumn("OTAName");
                }
            }
				
   			public static string OTANameColumn{
			      get{
        			return "OTAName";
      			}
		    }
            
            public IColumn AcctID{
                get{
                    return this.GetColumn("AcctID");
                }
            }
				
   			public static string AcctIDColumn{
			      get{
        			return "AcctID";
      			}
		    }
            
            public IColumn AcctPwd{
                get{
                    return this.GetColumn("AcctPwd");
                }
            }
				
   			public static string AcctPwdColumn{
			      get{
        			return "AcctPwd";
      			}
		    }
            
            public IColumn AppId{
                get{
                    return this.GetColumn("AppId");
                }
            }
				
   			public static string AppIdColumn{
			      get{
        			return "AppId";
      			}
		    }
            
            public IColumn AppKey{
                get{
                    return this.GetColumn("AppKey");
                }
            }
				
   			public static string AppKeyColumn{
			      get{
        			return "AppKey";
      			}
		    }
            
            public IColumn OTA{
                get{
                    return this.GetColumn("OTA");
                }
            }
				
   			public static string OTAColumn{
			      get{
        			return "OTA";
      			}
		    }
            
            public IColumn SyncType{
                get{
                    return this.GetColumn("SyncType");
                }
            }
				
   			public static string SyncTypeColumn{
			      get{
        			return "SyncType";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn Creator{
                get{
                    return this.GetColumn("Creator");
                }
            }
				
   			public static string CreatorColumn{
			      get{
        			return "Creator";
      			}
		    }
            
            public IColumn CreatorID{
                get{
                    return this.GetColumn("CreatorID");
                }
            }
				
   			public static string CreatorIDColumn{
			      get{
        			return "CreatorID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OTAServiceUrl{
                get{
                    return this.GetColumn("OTAServiceUrl");
                }
            }
				
   			public static string OTAServiceUrlColumn{
			      get{
        			return "OTAServiceUrl";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderPrice
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderPriceTable: DatabaseTable {
            
            public Ord_OrderPriceTable(IDataProvider provider):base("Ord_OrderPrice",provider){
                ClassName = "Ord_OrderPrice";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SalePrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Rebate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("VisitorNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RoomRate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsSeat", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsChild", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("InsuranceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("InsuranceCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TourPriceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn SalePrice{
                get{
                    return this.GetColumn("SalePrice");
                }
            }
				
   			public static string SalePriceColumn{
			      get{
        			return "SalePrice";
      			}
		    }
            
            public IColumn Rebate{
                get{
                    return this.GetColumn("Rebate");
                }
            }
				
   			public static string RebateColumn{
			      get{
        			return "Rebate";
      			}
		    }
            
            public IColumn VisitorNum{
                get{
                    return this.GetColumn("VisitorNum");
                }
            }
				
   			public static string VisitorNumColumn{
			      get{
        			return "VisitorNum";
      			}
		    }
            
            public IColumn RoomRate{
                get{
                    return this.GetColumn("RoomRate");
                }
            }
				
   			public static string RoomRateColumn{
			      get{
        			return "RoomRate";
      			}
		    }
            
            public IColumn IsSeat{
                get{
                    return this.GetColumn("IsSeat");
                }
            }
				
   			public static string IsSeatColumn{
			      get{
        			return "IsSeat";
      			}
		    }
            
            public IColumn IsChild{
                get{
                    return this.GetColumn("IsChild");
                }
            }
				
   			public static string IsChildColumn{
			      get{
        			return "IsChild";
      			}
		    }
            
            public IColumn InsuranceAmt{
                get{
                    return this.GetColumn("InsuranceAmt");
                }
            }
				
   			public static string InsuranceAmtColumn{
			      get{
        			return "InsuranceAmt";
      			}
		    }
            
            public IColumn InsuranceCost{
                get{
                    return this.GetColumn("InsuranceCost");
                }
            }
				
   			public static string InsuranceCostColumn{
			      get{
        			return "InsuranceCost";
      			}
		    }
            
            public IColumn TourPriceID{
                get{
                    return this.GetColumn("TourPriceID");
                }
            }
				
   			public static string TourPriceIDColumn{
			      get{
        			return "TourPriceID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sms_Message
        /// Primary Key: ID
        /// </summary>

        public class Sms_MessageTable: DatabaseTable {
            
            public Sms_MessageTable(IDataProvider provider):base("Sms_Message",provider){
                ClassName = "Sms_Message";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SendUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SendUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RecMobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("MsgContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn SendUserID{
                get{
                    return this.GetColumn("SendUserID");
                }
            }
				
   			public static string SendUserIDColumn{
			      get{
        			return "SendUserID";
      			}
		    }
            
            public IColumn SendUserName{
                get{
                    return this.GetColumn("SendUserName");
                }
            }
				
   			public static string SendUserNameColumn{
			      get{
        			return "SendUserName";
      			}
		    }
            
            public IColumn RecMobile{
                get{
                    return this.GetColumn("RecMobile");
                }
            }
				
   			public static string RecMobileColumn{
			      get{
        			return "RecMobile";
      			}
		    }
            
            public IColumn MsgContent{
                get{
                    return this.GetColumn("MsgContent");
                }
            }
				
   			public static string MsgContentColumn{
			      get{
        			return "MsgContent";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: OTA_UserInfo
        /// Primary Key: ID
        /// </summary>

        public class OTA_UserInfoTable: DatabaseTable {
            
            public OTA_UserInfoTable(IDataProvider provider):base("OTA_UserInfo",provider){
                ClassName = "OTA_UserInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OTAID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OTAUID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OTAUName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Guid,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OTAID{
                get{
                    return this.GetColumn("OTAID");
                }
            }
				
   			public static string OTAIDColumn{
			      get{
        			return "OTAID";
      			}
		    }
            
            public IColumn OTAUID{
                get{
                    return this.GetColumn("OTAUID");
                }
            }
				
   			public static string OTAUIDColumn{
			      get{
        			return "OTAUID";
      			}
		    }
            
            public IColumn OTAUName{
                get{
                    return this.GetColumn("OTAUName");
                }
            }
				
   			public static string OTAUNameColumn{
			      get{
        			return "OTAUName";
      			}
		    }
            
            public IColumn UID{
                get{
                    return this.GetColumn("UID");
                }
            }
				
   			public static string UIDColumn{
			      get{
        			return "UID";
      			}
		    }
            
            public IColumn UName{
                get{
                    return this.GetColumn("UName");
                }
            }
				
   			public static string UNameColumn{
			      get{
        			return "UName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_Hotel
        /// Primary Key: ID
        /// </summary>

        public class Res_HotelTable: DatabaseTable {
            
            public Res_HotelTable(IDataProvider provider):base("Res_Hotel",provider){
                ClassName = "Res_Hotel";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("StarLv", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Fax", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Price", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("RouteType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Destination", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationPath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("DestinationID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn StarLv{
                get{
                    return this.GetColumn("StarLv");
                }
            }
				
   			public static string StarLvColumn{
			      get{
        			return "StarLv";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn Fax{
                get{
                    return this.GetColumn("Fax");
                }
            }
				
   			public static string FaxColumn{
			      get{
        			return "Fax";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn Price{
                get{
                    return this.GetColumn("Price");
                }
            }
				
   			public static string PriceColumn{
			      get{
        			return "Price";
      			}
		    }
            
            public IColumn RouteType{
                get{
                    return this.GetColumn("RouteType");
                }
            }
				
   			public static string RouteTypeColumn{
			      get{
        			return "RouteType";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn Destination{
                get{
                    return this.GetColumn("Destination");
                }
            }
				
   			public static string DestinationColumn{
			      get{
        			return "Destination";
      			}
		    }
            
            public IColumn DestinationPath{
                get{
                    return this.GetColumn("DestinationPath");
                }
            }
				
   			public static string DestinationPathColumn{
			      get{
        			return "DestinationPath";
      			}
		    }
            
            public IColumn DestinationID{
                get{
                    return this.GetColumn("DestinationID");
                }
            }
				
   			public static string DestinationIDColumn{
			      get{
        			return "DestinationID";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sms_Platform
        /// Primary Key: ID
        /// </summary>

        public class Sms_PlatformTable: DatabaseTable {
            
            public Sms_PlatformTable(IDataProvider provider):base("Sms_Platform",provider){
                ClassName = "Sms_Platform";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SmsCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UnitPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Amount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 550
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn SmsCount{
                get{
                    return this.GetColumn("SmsCount");
                }
            }
				
   			public static string SmsCountColumn{
			      get{
        			return "SmsCount";
      			}
		    }
            
            public IColumn UnitPrice{
                get{
                    return this.GetColumn("UnitPrice");
                }
            }
				
   			public static string UnitPriceColumn{
			      get{
        			return "UnitPrice";
      			}
		    }
            
            public IColumn Amount{
                get{
                    return this.GetColumn("Amount");
                }
            }
				
   			public static string AmountColumn{
			      get{
        			return "Amount";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sms_ValidateCode
        /// Primary Key: ID
        /// </summary>

        public class Sms_ValidateCodeTable: DatabaseTable {
            
            public Sms_ValidateCodeTable(IDataProvider provider):base("Sms_ValidateCode",provider){
                ClassName = "Sms_ValidateCode";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Code", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Code{
                get{
                    return this.GetColumn("Code");
                }
            }
				
   			public static string CodeColumn{
			      get{
        			return "Code";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_UpdateLog
        /// Primary Key: ID
        /// </summary>

        public class Glo_UpdateLogTable: DatabaseTable {
            
            public Glo_UpdateLogTable(IDataProvider provider):base("Glo_UpdateLog",provider){
                ClassName = "Glo_UpdateLog";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Summary", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("xType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Summary{
                get{
                    return this.GetColumn("Summary");
                }
            }
				
   			public static string SummaryColumn{
			      get{
        			return "Summary";
      			}
		    }
            
            public IColumn xType{
                get{
                    return this.GetColumn("xType");
                }
            }
				
   			public static string xTypeColumn{
			      get{
        			return "xType";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_Motorcade
        /// Primary Key: ID
        /// </summary>

        public class Res_MotorcadeTable: DatabaseTable {
            
            public Res_MotorcadeTable(IDataProvider provider):base("Res_Motorcade",provider){
                ClassName = "Res_Motorcade";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("Scale", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Fax", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Departure", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DepartureID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn Scale{
                get{
                    return this.GetColumn("Scale");
                }
            }
				
   			public static string ScaleColumn{
			      get{
        			return "Scale";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn Fax{
                get{
                    return this.GetColumn("Fax");
                }
            }
				
   			public static string FaxColumn{
			      get{
        			return "Fax";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn Departure{
                get{
                    return this.GetColumn("Departure");
                }
            }
				
   			public static string DepartureColumn{
			      get{
        			return "Departure";
      			}
		    }
            
            public IColumn DepartureID{
                get{
                    return this.GetColumn("DepartureID");
                }
            }
				
   			public static string DepartureIDColumn{
			      get{
        			return "DepartureID";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_Complain
        /// Primary Key: 
        /// </summary>

        public class Sn_ComplainTable: DatabaseTable {
            
            public Sn_ComplainTable(IDataProvider provider):base("Sn_Complain",provider){
                ClassName = "Sn_Complain";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("xType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("nContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2500
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn xType{
                get{
                    return this.GetColumn("xType");
                }
            }
				
   			public static string xTypeColumn{
			      get{
        			return "xType";
      			}
		    }
            
            public IColumn nContent{
                get{
                    return this.GetColumn("nContent");
                }
            }
				
   			public static string nContentColumn{
			      get{
        			return "nContent";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_Insurance
        /// Primary Key: ID
        /// </summary>

        public class Res_InsuranceTable: DatabaseTable {
            
            public Res_InsuranceTable(IDataProvider provider):base("Res_Insurance",provider){
                ClassName = "Res_Insurance";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_ComplainResult
        /// Primary Key: ID
        /// </summary>

        public class Sn_ComplainResultTable: DatabaseTable {
            
            public Sn_ComplainResultTable(IDataProvider provider):base("Sn_ComplainResult",provider){
                ClassName = "Sn_ComplainResult";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ComplainID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Summary", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn ComplainID{
                get{
                    return this.GetColumn("ComplainID");
                }
            }
				
   			public static string ComplainIDColumn{
			      get{
        			return "ComplainID";
      			}
		    }
            
            public IColumn Summary{
                get{
                    return this.GetColumn("Summary");
                }
            }
				
   			public static string SummaryColumn{
			      get{
        			return "Summary";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderExtend
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderExtendTable: DatabaseTable {
            
            public Ord_OrderExtendTable(IDataProvider provider):base("Ord_OrderExtend",provider){
                ClassName = "Ord_OrderExtend";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderProfit", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ProfitRate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DrawMoneyAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PaidAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderInvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CostInvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BudgetStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsCloseCollected", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsCheckAccount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn AdultNum{
                get{
                    return this.GetColumn("AdultNum");
                }
            }
				
   			public static string AdultNumColumn{
			      get{
        			return "AdultNum";
      			}
		    }
            
            public IColumn ChildNum{
                get{
                    return this.GetColumn("ChildNum");
                }
            }
				
   			public static string ChildNumColumn{
			      get{
        			return "ChildNum";
      			}
		    }
            
            public IColumn OrderAmt{
                get{
                    return this.GetColumn("OrderAmt");
                }
            }
				
   			public static string OrderAmtColumn{
			      get{
        			return "OrderAmt";
      			}
		    }
            
            public IColumn OrderCost{
                get{
                    return this.GetColumn("OrderCost");
                }
            }
				
   			public static string OrderCostColumn{
			      get{
        			return "OrderCost";
      			}
		    }
            
            public IColumn OrderProfit{
                get{
                    return this.GetColumn("OrderProfit");
                }
            }
				
   			public static string OrderProfitColumn{
			      get{
        			return "OrderProfit";
      			}
		    }
            
            public IColumn ProfitRate{
                get{
                    return this.GetColumn("ProfitRate");
                }
            }
				
   			public static string ProfitRateColumn{
			      get{
        			return "ProfitRate";
      			}
		    }
            
            public IColumn DrawMoneyAmt{
                get{
                    return this.GetColumn("DrawMoneyAmt");
                }
            }
				
   			public static string DrawMoneyAmtColumn{
			      get{
        			return "DrawMoneyAmt";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn PaidAmt{
                get{
                    return this.GetColumn("PaidAmt");
                }
            }
				
   			public static string PaidAmtColumn{
			      get{
        			return "PaidAmt";
      			}
		    }
            
            public IColumn OrderInvoiceAmt{
                get{
                    return this.GetColumn("OrderInvoiceAmt");
                }
            }
				
   			public static string OrderInvoiceAmtColumn{
			      get{
        			return "OrderInvoiceAmt";
      			}
		    }
            
            public IColumn CostInvoiceAmt{
                get{
                    return this.GetColumn("CostInvoiceAmt");
                }
            }
				
   			public static string CostInvoiceAmtColumn{
			      get{
        			return "CostInvoiceAmt";
      			}
		    }
            
            public IColumn OrderStatus{
                get{
                    return this.GetColumn("OrderStatus");
                }
            }
				
   			public static string OrderStatusColumn{
			      get{
        			return "OrderStatus";
      			}
		    }
            
            public IColumn BudgetStatus{
                get{
                    return this.GetColumn("BudgetStatus");
                }
            }
				
   			public static string BudgetStatusColumn{
			      get{
        			return "BudgetStatus";
      			}
		    }
            
            public IColumn IsCloseCollected{
                get{
                    return this.GetColumn("IsCloseCollected");
                }
            }
				
   			public static string IsCloseCollectedColumn{
			      get{
        			return "IsCloseCollected";
      			}
		    }
            
            public IColumn IsCheckAccount{
                get{
                    return this.GetColumn("IsCheckAccount");
                }
            }
				
   			public static string IsCheckAccountColumn{
			      get{
        			return "IsCheckAccount";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_Departure
        /// Primary Key: ID
        /// </summary>

        public class Glo_DepartureTable: DatabaseTable {
            
            public Glo_DepartureTable(IDataProvider provider):base("Glo_Departure",provider){
                ClassName = "Glo_Departure";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_Shopping
        /// Primary Key: ID
        /// </summary>

        public class Res_ShoppingTable: DatabaseTable {
            
            public Res_ShoppingTable(IDataProvider provider):base("Res_Shopping",provider){
                ClassName = "Res_Shopping";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("RouteType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Destination", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationPath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("DestinationID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn RouteType{
                get{
                    return this.GetColumn("RouteType");
                }
            }
				
   			public static string RouteTypeColumn{
			      get{
        			return "RouteType";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn Destination{
                get{
                    return this.GetColumn("Destination");
                }
            }
				
   			public static string DestinationColumn{
			      get{
        			return "Destination";
      			}
		    }
            
            public IColumn DestinationPath{
                get{
                    return this.GetColumn("DestinationPath");
                }
            }
				
   			public static string DestinationPathColumn{
			      get{
        			return "DestinationPath";
      			}
		    }
            
            public IColumn DestinationID{
                get{
                    return this.GetColumn("DestinationID");
                }
            }
				
   			public static string DestinationIDColumn{
			      get{
        			return "DestinationID";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_NoteInfo
        /// Primary Key: ID
        /// </summary>

        public class Sn_NoteInfoTable: DatabaseTable {
            
            public Sn_NoteInfoTable(IDataProvider provider):base("Sn_NoteInfo",provider){
                ClassName = "Sn_NoteInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Subject", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("nContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn Subject{
                get{
                    return this.GetColumn("Subject");
                }
            }
				
   			public static string SubjectColumn{
			      get{
        			return "Subject";
      			}
		    }
            
            public IColumn nContent{
                get{
                    return this.GetColumn("nContent");
                }
            }
				
   			public static string nContentColumn{
			      get{
        			return "nContent";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Crm_VisitTrace
        /// Primary Key: ID
        /// </summary>

        public class Crm_VisitTraceTable: DatabaseTable {
            
            public Crm_VisitTraceTable(IDataProvider provider):base("Crm_VisitTrace",provider){
                ClassName = "Crm_VisitTrace";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CustomerID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("ItemType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TradeDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 350
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn CustomerID{
                get{
                    return this.GetColumn("CustomerID");
                }
            }
				
   			public static string CustomerIDColumn{
			      get{
        			return "CustomerID";
      			}
		    }
            
            public IColumn ItemName{
                get{
                    return this.GetColumn("ItemName");
                }
            }
				
   			public static string ItemNameColumn{
			      get{
        			return "ItemName";
      			}
		    }
            
            public IColumn ItemType{
                get{
                    return this.GetColumn("ItemType");
                }
            }
				
   			public static string ItemTypeColumn{
			      get{
        			return "ItemType";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn TradeDate{
                get{
                    return this.GetColumn("TradeDate");
                }
            }
				
   			public static string TradeDateColumn{
			      get{
        			return "TradeDate";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_Recommend
        /// Primary Key: ID
        /// </summary>

        public class Sn_RecommendTable: DatabaseTable {
            
            public Sn_RecommendTable(IDataProvider provider):base("Sn_Recommend",provider){
                ClassName = "Sn_Recommend";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("nContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn nContent{
                get{
                    return this.GetColumn("nContent");
                }
            }
				
   			public static string nContentColumn{
			      get{
        			return "nContent";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_Files
        /// Primary Key: ID
        /// </summary>

        public class Glo_FilesTable: DatabaseTable {
            
            public Glo_FilesTable(IDataProvider provider):base("Glo_Files",provider){
                ClassName = "Glo_File";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("FileName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("FileSize", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("FilePath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("FileType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn FileName{
                get{
                    return this.GetColumn("FileName");
                }
            }
				
   			public static string FileNameColumn{
			      get{
        			return "FileName";
      			}
		    }
            
            public IColumn FileSize{
                get{
                    return this.GetColumn("FileSize");
                }
            }
				
   			public static string FileSizeColumn{
			      get{
        			return "FileSize";
      			}
		    }
            
            public IColumn FilePath{
                get{
                    return this.GetColumn("FilePath");
                }
            }
				
   			public static string FilePathColumn{
			      get{
        			return "FilePath";
      			}
		    }
            
            public IColumn FileType{
                get{
                    return this.GetColumn("FileType");
                }
            }
				
   			public static string FileTypeColumn{
			      get{
        			return "FileType";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_ScenicTicket
        /// Primary Key: ID
        /// </summary>

        public class Res_ScenicTicketTable: DatabaseTable {
            
            public Res_ScenicTicketTable(IDataProvider provider):base("Res_ScenicTicket",provider){
                ClassName = "Res_ScenicTicket";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Fax", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("NormalPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("CooperatePrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("TeamPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("RouteType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Destination", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationPath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000
                });

                Columns.Add(new DatabaseColumn("DestinationID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn Fax{
                get{
                    return this.GetColumn("Fax");
                }
            }
				
   			public static string FaxColumn{
			      get{
        			return "Fax";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn NormalPrice{
                get{
                    return this.GetColumn("NormalPrice");
                }
            }
				
   			public static string NormalPriceColumn{
			      get{
        			return "NormalPrice";
      			}
		    }
            
            public IColumn CooperatePrice{
                get{
                    return this.GetColumn("CooperatePrice");
                }
            }
				
   			public static string CooperatePriceColumn{
			      get{
        			return "CooperatePrice";
      			}
		    }
            
            public IColumn TeamPrice{
                get{
                    return this.GetColumn("TeamPrice");
                }
            }
				
   			public static string TeamPriceColumn{
			      get{
        			return "TeamPrice";
      			}
		    }
            
            public IColumn RouteType{
                get{
                    return this.GetColumn("RouteType");
                }
            }
				
   			public static string RouteTypeColumn{
			      get{
        			return "RouteType";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn Destination{
                get{
                    return this.GetColumn("Destination");
                }
            }
				
   			public static string DestinationColumn{
			      get{
        			return "Destination";
      			}
		    }
            
            public IColumn DestinationPath{
                get{
                    return this.GetColumn("DestinationPath");
                }
            }
				
   			public static string DestinationPathColumn{
			      get{
        			return "DestinationPath";
      			}
		    }
            
            public IColumn DestinationID{
                get{
                    return this.GetColumn("DestinationID");
                }
            }
				
   			public static string DestinationIDColumn{
			      get{
        			return "DestinationID";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_TravelInfo
        /// Primary Key: ID
        /// </summary>

        public class Sn_TravelInfoTable: DatabaseTable {
            
            public Sn_TravelInfoTable(IDataProvider provider):base("Sn_TravelInfo",provider){
                ClassName = "Sn_TravelInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Schedule", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("GuideName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("GuideMobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("MeetPlace", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("MeetTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PlateNumber", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Driver", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DriverMobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("EmergencyUser", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("EmergencyMobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn Schedule{
                get{
                    return this.GetColumn("Schedule");
                }
            }
				
   			public static string ScheduleColumn{
			      get{
        			return "Schedule";
      			}
		    }
            
            public IColumn GuideName{
                get{
                    return this.GetColumn("GuideName");
                }
            }
				
   			public static string GuideNameColumn{
			      get{
        			return "GuideName";
      			}
		    }
            
            public IColumn GuideMobile{
                get{
                    return this.GetColumn("GuideMobile");
                }
            }
				
   			public static string GuideMobileColumn{
			      get{
        			return "GuideMobile";
      			}
		    }
            
            public IColumn MeetPlace{
                get{
                    return this.GetColumn("MeetPlace");
                }
            }
				
   			public static string MeetPlaceColumn{
			      get{
        			return "MeetPlace";
      			}
		    }
            
            public IColumn MeetTime{
                get{
                    return this.GetColumn("MeetTime");
                }
            }
				
   			public static string MeetTimeColumn{
			      get{
        			return "MeetTime";
      			}
		    }
            
            public IColumn PlateNumber{
                get{
                    return this.GetColumn("PlateNumber");
                }
            }
				
   			public static string PlateNumberColumn{
			      get{
        			return "PlateNumber";
      			}
		    }
            
            public IColumn Driver{
                get{
                    return this.GetColumn("Driver");
                }
            }
				
   			public static string DriverColumn{
			      get{
        			return "Driver";
      			}
		    }
            
            public IColumn DriverMobile{
                get{
                    return this.GetColumn("DriverMobile");
                }
            }
				
   			public static string DriverMobileColumn{
			      get{
        			return "DriverMobile";
      			}
		    }
            
            public IColumn EmergencyUser{
                get{
                    return this.GetColumn("EmergencyUser");
                }
            }
				
   			public static string EmergencyUserColumn{
			      get{
        			return "EmergencyUser";
      			}
		    }
            
            public IColumn EmergencyMobile{
                get{
                    return this.GetColumn("EmergencyMobile");
                }
            }
				
   			public static string EmergencyMobileColumn{
			      get{
        			return "EmergencyMobile";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Res_TicketAgency
        /// Primary Key: ID
        /// </summary>

        public class Res_TicketAgencyTable: DatabaseTable {
            
            public Res_TicketAgencyTable(IDataProvider provider):base("Res_TicketAgency",provider){
                ClassName = "Res_TicketAgency";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Spell", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10
                });

                Columns.Add(new DatabaseColumn("TicketType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Phone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Addr", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("TradeNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeAdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TradeChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Spell{
                get{
                    return this.GetColumn("Spell");
                }
            }
				
   			public static string SpellColumn{
			      get{
        			return "Spell";
      			}
		    }
            
            public IColumn TicketType{
                get{
                    return this.GetColumn("TicketType");
                }
            }
				
   			public static string TicketTypeColumn{
			      get{
        			return "TicketType";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn Mail{
                get{
                    return this.GetColumn("Mail");
                }
            }
				
   			public static string MailColumn{
			      get{
        			return "Mail";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Phone{
                get{
                    return this.GetColumn("Phone");
                }
            }
				
   			public static string PhoneColumn{
			      get{
        			return "Phone";
      			}
		    }
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
                }
            }
				
   			public static string IsEnableColumn{
			      get{
        			return "IsEnable";
      			}
		    }
            
            public IColumn Addr{
                get{
                    return this.GetColumn("Addr");
                }
            }
				
   			public static string AddrColumn{
			      get{
        			return "Addr";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn TradeNum{
                get{
                    return this.GetColumn("TradeNum");
                }
            }
				
   			public static string TradeNumColumn{
			      get{
        			return "TradeNum";
      			}
		    }
            
            public IColumn TradeAmt{
                get{
                    return this.GetColumn("TradeAmt");
                }
            }
				
   			public static string TradeAmtColumn{
			      get{
        			return "TradeAmt";
      			}
		    }
            
            public IColumn TradeAdultNum{
                get{
                    return this.GetColumn("TradeAdultNum");
                }
            }
				
   			public static string TradeAdultNumColumn{
			      get{
        			return "TradeAdultNum";
      			}
		    }
            
            public IColumn TradeChildNum{
                get{
                    return this.GetColumn("TradeChildNum");
                }
            }
				
   			public static string TradeChildNumColumn{
			      get{
        			return "TradeChildNum";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_Shared
        /// Primary Key: ID
        /// </summary>

        public class Sn_SharedTable: DatabaseTable {
            
            public Sn_SharedTable(IDataProvider provider):base("Sn_Shared",provider){
                ClassName = "Sn_Shared";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserMobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Subject", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("nContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn UserName{
                get{
                    return this.GetColumn("UserName");
                }
            }
				
   			public static string UserNameColumn{
			      get{
        			return "UserName";
      			}
		    }
            
            public IColumn UserMobile{
                get{
                    return this.GetColumn("UserMobile");
                }
            }
				
   			public static string UserMobileColumn{
			      get{
        			return "UserMobile";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Subject{
                get{
                    return this.GetColumn("Subject");
                }
            }
				
   			public static string SubjectColumn{
			      get{
        			return "Subject";
      			}
		    }
            
            public IColumn nContent{
                get{
                    return this.GetColumn("nContent");
                }
            }
				
   			public static string nContentColumn{
			      get{
        			return "nContent";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_Order
        /// Primary Key: ID
        /// </summary>

        public class Sn_OrderTable: DatabaseTable {
            
            public Sn_OrderTable(IDataProvider provider):base("Sn_Order",provider){
                ClassName = "Sn_Order";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TourDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RouteName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("VisitorNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn TourDate{
                get{
                    return this.GetColumn("TourDate");
                }
            }
				
   			public static string TourDateColumn{
			      get{
        			return "TourDate";
      			}
		    }
            
            public IColumn RouteName{
                get{
                    return this.GetColumn("RouteName");
                }
            }
				
   			public static string RouteNameColumn{
			      get{
        			return "RouteName";
      			}
		    }
            
            public IColumn VisitorNum{
                get{
                    return this.GetColumn("VisitorNum");
                }
            }
				
   			public static string VisitorNumColumn{
			      get{
        			return "VisitorNum";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: User_Setting
        /// Primary Key: ID
        /// </summary>

        public class User_SettingTable: DatabaseTable {
            
            public User_SettingTable(IDataProvider provider):base("User_Setting",provider){
                ClassName = "User_Setting";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("xType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn xType{
                get{
                    return this.GetColumn("xType");
                }
            }
				
   			public static string xTypeColumn{
			      get{
        			return "xType";
      			}
		    }
            
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Crm_Level
        /// Primary Key: ID
        /// </summary>

        public class Crm_LevelTable: DatabaseTable {
            
            public Crm_LevelTable(IDataProvider provider):base("Crm_Level",provider){
                ClassName = "Crm_Level";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("MinAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("MaxAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn MinAmt{
                get{
                    return this.GetColumn("MinAmt");
                }
            }
				
   			public static string MinAmtColumn{
			      get{
        			return "MinAmt";
      			}
		    }
            
            public IColumn MaxAmt{
                get{
                    return this.GetColumn("MaxAmt");
                }
            }
				
   			public static string MaxAmtColumn{
			      get{
        			return "MaxAmt";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderBalanceSettlement
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderBalanceSettlementTable: DatabaseTable {
            
            public Ord_OrderBalanceSettlementTable(IDataProvider provider):base("Ord_OrderBalanceSettlement",provider){
                ClassName = "Ord_OrderBalanceSettlement";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderBalanceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("GuideName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("GuideMobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BankAcct", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("BankName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DrawMoneyAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BalanceIncome", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BalanceCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SettlementAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SettlementType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Auditor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AuditDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn OrderType{
                get{
                    return this.GetColumn("OrderType");
                }
            }
				
   			public static string OrderTypeColumn{
			      get{
        			return "OrderType";
      			}
		    }
            
            public IColumn OrderBalanceID{
                get{
                    return this.GetColumn("OrderBalanceID");
                }
            }
				
   			public static string OrderBalanceIDColumn{
			      get{
        			return "OrderBalanceID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn GuideName{
                get{
                    return this.GetColumn("GuideName");
                }
            }
				
   			public static string GuideNameColumn{
			      get{
        			return "GuideName";
      			}
		    }
            
            public IColumn GuideMobile{
                get{
                    return this.GetColumn("GuideMobile");
                }
            }
				
   			public static string GuideMobileColumn{
			      get{
        			return "GuideMobile";
      			}
		    }
            
            public IColumn BankAcct{
                get{
                    return this.GetColumn("BankAcct");
                }
            }
				
   			public static string BankAcctColumn{
			      get{
        			return "BankAcct";
      			}
		    }
            
            public IColumn BankName{
                get{
                    return this.GetColumn("BankName");
                }
            }
				
   			public static string BankNameColumn{
			      get{
        			return "BankName";
      			}
		    }
            
            public IColumn DrawMoneyAmt{
                get{
                    return this.GetColumn("DrawMoneyAmt");
                }
            }
				
   			public static string DrawMoneyAmtColumn{
			      get{
        			return "DrawMoneyAmt";
      			}
		    }
            
            public IColumn BalanceIncome{
                get{
                    return this.GetColumn("BalanceIncome");
                }
            }
				
   			public static string BalanceIncomeColumn{
			      get{
        			return "BalanceIncome";
      			}
		    }
            
            public IColumn BalanceCost{
                get{
                    return this.GetColumn("BalanceCost");
                }
            }
				
   			public static string BalanceCostColumn{
			      get{
        			return "BalanceCost";
      			}
		    }
            
            public IColumn SettlementAmt{
                get{
                    return this.GetColumn("SettlementAmt");
                }
            }
				
   			public static string SettlementAmtColumn{
			      get{
        			return "SettlementAmt";
      			}
		    }
            
            public IColumn SettlementType{
                get{
                    return this.GetColumn("SettlementType");
                }
            }
				
   			public static string SettlementTypeColumn{
			      get{
        			return "SettlementType";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn Auditor{
                get{
                    return this.GetColumn("Auditor");
                }
            }
				
   			public static string AuditorColumn{
			      get{
        			return "Auditor";
      			}
		    }
            
            public IColumn AuditDate{
                get{
                    return this.GetColumn("AuditDate");
                }
            }
				
   			public static string AuditDateColumn{
			      get{
        			return "AuditDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_NavGroup
        /// Primary Key: ID
        /// </summary>

        public class Om_NavGroupTable: DatabaseTable {
            
            public Om_NavGroupTable(IDataProvider provider):base("Om_NavGroup",provider){
                ClassName = "Om_NavGroup";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("NavGroup", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn NavGroup{
                get{
                    return this.GetColumn("NavGroup");
                }
            }
				
   			public static string NavGroupColumn{
			      get{
        			return "NavGroup";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Crm_CustomerCertificate
        /// Primary Key: ID
        /// </summary>

        public class Crm_CustomerCertificateTable: DatabaseTable {
            
            public Crm_CustomerCertificateTable(IDataProvider provider):base("Crm_CustomerCertificate",provider){
                ClassName = "Crm_CustomerCertificate";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CustomerID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ItemVal", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SortIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn CustomerID{
                get{
                    return this.GetColumn("CustomerID");
                }
            }
				
   			public static string CustomerIDColumn{
			      get{
        			return "CustomerID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn ItemType{
                get{
                    return this.GetColumn("ItemType");
                }
            }
				
   			public static string ItemTypeColumn{
			      get{
        			return "ItemType";
      			}
		    }
            
            public IColumn ItemVal{
                get{
                    return this.GetColumn("ItemVal");
                }
            }
				
   			public static string ItemValColumn{
			      get{
        			return "ItemVal";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn SortIndex{
                get{
                    return this.GetColumn("SortIndex");
                }
            }
				
   			public static string SortIndexColumn{
			      get{
        			return "SortIndex";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_UserOnLine
        /// Primary Key: ID
        /// </summary>

        public class Om_UserOnLineTable: DatabaseTable {
            
            public Om_UserOnLineTable(IDataProvider provider):base("Om_UserOnLine",provider){
                ClassName = "Om_UserOnLine";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrgName{
                get{
                    return this.GetColumn("OrgName");
                }
            }
				
   			public static string OrgNameColumn{
			      get{
        			return "OrgName";
      			}
		    }
            
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn UserName{
                get{
                    return this.GetColumn("UserName");
                }
            }
				
   			public static string UserNameColumn{
			      get{
        			return "UserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_Destination
        /// Primary Key: ID
        /// </summary>

        public class Glo_DestinationTable: DatabaseTable {
            
            public Glo_DestinationTable(IDataProvider provider):base("Glo_Destination",provider){
                ClassName = "Glo_Destination";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ParentID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn ParentID{
                get{
                    return this.GetColumn("ParentID");
                }
            }
				
   			public static string ParentIDColumn{
			      get{
        			return "ParentID";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_Tools
        /// Primary Key: ID
        /// </summary>

        public class Om_ToolsTable: DatabaseTable {
            
            public Om_ToolsTable(IDataProvider provider):base("Om_Tools",provider){
                ClassName = "Om_Tool";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("URL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("Target", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IconCls", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn URL{
                get{
                    return this.GetColumn("URL");
                }
            }
				
   			public static string URLColumn{
			      get{
        			return "URL";
      			}
		    }
            
            public IColumn Target{
                get{
                    return this.GetColumn("Target");
                }
            }
				
   			public static string TargetColumn{
			      get{
        			return "Target";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn IconCls{
                get{
                    return this.GetColumn("IconCls");
                }
            }
				
   			public static string IconClsColumn{
			      get{
        			return "IconCls";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_TicketOrder
        /// Primary Key: ID
        /// </summary>

        public class Ord_TicketOrderTable: DatabaseTable {
            
            public Ord_TicketOrderTable(IDataProvider provider):base("Ord_TicketOrder",provider){
                ClassName = "Ord_TicketOrder";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("OrderNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PNR", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ToFlightLeg", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ToFlightInfo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("ToAirport", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("ToAirLine", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("ToCabin", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ToTicketPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FromFlightLeg", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("FromFlightInfo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("FromAirport", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("FromAirLine", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("FromCabin", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("FromTicketPrice", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Contact", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ContactPhone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("TicketType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TourDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ReturnDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("SupplierName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("CollectedAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ToConfirmCollectedAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PaidAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderInvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CostInvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderCostID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SupplierID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Participant", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ParticipantID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PartDeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Company", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderName{
                get{
                    return this.GetColumn("OrderName");
                }
            }
				
   			public static string OrderNameColumn{
			      get{
        			return "OrderName";
      			}
		    }
            
            public IColumn OrderNo{
                get{
                    return this.GetColumn("OrderNo");
                }
            }
				
   			public static string OrderNoColumn{
			      get{
        			return "OrderNo";
      			}
		    }
            
            public IColumn PNR{
                get{
                    return this.GetColumn("PNR");
                }
            }
				
   			public static string PNRColumn{
			      get{
        			return "PNR";
      			}
		    }
            
            public IColumn ToFlightLeg{
                get{
                    return this.GetColumn("ToFlightLeg");
                }
            }
				
   			public static string ToFlightLegColumn{
			      get{
        			return "ToFlightLeg";
      			}
		    }
            
            public IColumn ToFlightInfo{
                get{
                    return this.GetColumn("ToFlightInfo");
                }
            }
				
   			public static string ToFlightInfoColumn{
			      get{
        			return "ToFlightInfo";
      			}
		    }
            
            public IColumn ToAirport{
                get{
                    return this.GetColumn("ToAirport");
                }
            }
				
   			public static string ToAirportColumn{
			      get{
        			return "ToAirport";
      			}
		    }
            
            public IColumn ToAirLine{
                get{
                    return this.GetColumn("ToAirLine");
                }
            }
				
   			public static string ToAirLineColumn{
			      get{
        			return "ToAirLine";
      			}
		    }
            
            public IColumn ToCabin{
                get{
                    return this.GetColumn("ToCabin");
                }
            }
				
   			public static string ToCabinColumn{
			      get{
        			return "ToCabin";
      			}
		    }
            
            public IColumn ToTicketPrice{
                get{
                    return this.GetColumn("ToTicketPrice");
                }
            }
				
   			public static string ToTicketPriceColumn{
			      get{
        			return "ToTicketPrice";
      			}
		    }
            
            public IColumn FromFlightLeg{
                get{
                    return this.GetColumn("FromFlightLeg");
                }
            }
				
   			public static string FromFlightLegColumn{
			      get{
        			return "FromFlightLeg";
      			}
		    }
            
            public IColumn FromFlightInfo{
                get{
                    return this.GetColumn("FromFlightInfo");
                }
            }
				
   			public static string FromFlightInfoColumn{
			      get{
        			return "FromFlightInfo";
      			}
		    }
            
            public IColumn FromAirport{
                get{
                    return this.GetColumn("FromAirport");
                }
            }
				
   			public static string FromAirportColumn{
			      get{
        			return "FromAirport";
      			}
		    }
            
            public IColumn FromAirLine{
                get{
                    return this.GetColumn("FromAirLine");
                }
            }
				
   			public static string FromAirLineColumn{
			      get{
        			return "FromAirLine";
      			}
		    }
            
            public IColumn FromCabin{
                get{
                    return this.GetColumn("FromCabin");
                }
            }
				
   			public static string FromCabinColumn{
			      get{
        			return "FromCabin";
      			}
		    }
            
            public IColumn FromTicketPrice{
                get{
                    return this.GetColumn("FromTicketPrice");
                }
            }
				
   			public static string FromTicketPriceColumn{
			      get{
        			return "FromTicketPrice";
      			}
		    }
            
            public IColumn Contact{
                get{
                    return this.GetColumn("Contact");
                }
            }
				
   			public static string ContactColumn{
			      get{
        			return "Contact";
      			}
		    }
            
            public IColumn ContactPhone{
                get{
                    return this.GetColumn("ContactPhone");
                }
            }
				
   			public static string ContactPhoneColumn{
			      get{
        			return "ContactPhone";
      			}
		    }
            
            public IColumn TicketType{
                get{
                    return this.GetColumn("TicketType");
                }
            }
				
   			public static string TicketTypeColumn{
			      get{
        			return "TicketType";
      			}
		    }
            
            public IColumn AdultNum{
                get{
                    return this.GetColumn("AdultNum");
                }
            }
				
   			public static string AdultNumColumn{
			      get{
        			return "AdultNum";
      			}
		    }
            
            public IColumn ChildNum{
                get{
                    return this.GetColumn("ChildNum");
                }
            }
				
   			public static string ChildNumColumn{
			      get{
        			return "ChildNum";
      			}
		    }
            
            public IColumn OrderAmt{
                get{
                    return this.GetColumn("OrderAmt");
                }
            }
				
   			public static string OrderAmtColumn{
			      get{
        			return "OrderAmt";
      			}
		    }
            
            public IColumn OrderCost{
                get{
                    return this.GetColumn("OrderCost");
                }
            }
				
   			public static string OrderCostColumn{
			      get{
        			return "OrderCost";
      			}
		    }
            
            public IColumn TourDate{
                get{
                    return this.GetColumn("TourDate");
                }
            }
				
   			public static string TourDateColumn{
			      get{
        			return "TourDate";
      			}
		    }
            
            public IColumn ReturnDate{
                get{
                    return this.GetColumn("ReturnDate");
                }
            }
				
   			public static string ReturnDateColumn{
			      get{
        			return "ReturnDate";
      			}
		    }
            
            public IColumn OrderStatus{
                get{
                    return this.GetColumn("OrderStatus");
                }
            }
				
   			public static string OrderStatusColumn{
			      get{
        			return "OrderStatus";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn SupplierName{
                get{
                    return this.GetColumn("SupplierName");
                }
            }
				
   			public static string SupplierNameColumn{
			      get{
        			return "SupplierName";
      			}
		    }
            
            public IColumn CollectedAmt{
                get{
                    return this.GetColumn("CollectedAmt");
                }
            }
				
   			public static string CollectedAmtColumn{
			      get{
        			return "CollectedAmt";
      			}
		    }
            
            public IColumn ToConfirmCollectedAmt{
                get{
                    return this.GetColumn("ToConfirmCollectedAmt");
                }
            }
				
   			public static string ToConfirmCollectedAmtColumn{
			      get{
        			return "ToConfirmCollectedAmt";
      			}
		    }
            
            public IColumn PaidAmt{
                get{
                    return this.GetColumn("PaidAmt");
                }
            }
				
   			public static string PaidAmtColumn{
			      get{
        			return "PaidAmt";
      			}
		    }
            
            public IColumn OrderInvoiceAmt{
                get{
                    return this.GetColumn("OrderInvoiceAmt");
                }
            }
				
   			public static string OrderInvoiceAmtColumn{
			      get{
        			return "OrderInvoiceAmt";
      			}
		    }
            
            public IColumn CostInvoiceAmt{
                get{
                    return this.GetColumn("CostInvoiceAmt");
                }
            }
				
   			public static string CostInvoiceAmtColumn{
			      get{
        			return "CostInvoiceAmt";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn UpdateUserID{
                get{
                    return this.GetColumn("UpdateUserID");
                }
            }
				
   			public static string UpdateUserIDColumn{
			      get{
        			return "UpdateUserID";
      			}
		    }
            
            public IColumn UpdateUserName{
                get{
                    return this.GetColumn("UpdateUserName");
                }
            }
				
   			public static string UpdateUserNameColumn{
			      get{
        			return "UpdateUserName";
      			}
		    }
            
            public IColumn UpdateDate{
                get{
                    return this.GetColumn("UpdateDate");
                }
            }
				
   			public static string UpdateDateColumn{
			      get{
        			return "UpdateDate";
      			}
		    }
            
            public IColumn OrderCostID{
                get{
                    return this.GetColumn("OrderCostID");
                }
            }
				
   			public static string OrderCostIDColumn{
			      get{
        			return "OrderCostID";
      			}
		    }
            
            public IColumn SupplierID{
                get{
                    return this.GetColumn("SupplierID");
                }
            }
				
   			public static string SupplierIDColumn{
			      get{
        			return "SupplierID";
      			}
		    }
            
            public IColumn Participant{
                get{
                    return this.GetColumn("Participant");
                }
            }
				
   			public static string ParticipantColumn{
			      get{
        			return "Participant";
      			}
		    }
            
            public IColumn DeptName{
                get{
                    return this.GetColumn("DeptName");
                }
            }
				
   			public static string DeptNameColumn{
			      get{
        			return "DeptName";
      			}
		    }
            
            public IColumn ParticipantID{
                get{
                    return this.GetColumn("ParticipantID");
                }
            }
				
   			public static string ParticipantIDColumn{
			      get{
        			return "ParticipantID";
      			}
		    }
            
            public IColumn PartDeptID{
                get{
                    return this.GetColumn("PartDeptID");
                }
            }
				
   			public static string PartDeptIDColumn{
			      get{
        			return "PartDeptID";
      			}
		    }
            
            public IColumn Company{
                get{
                    return this.GetColumn("Company");
                }
            }
				
   			public static string CompanyColumn{
			      get{
        			return "Company";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Glo_QQ
        /// Primary Key: ID
        /// </summary>

        public class Glo_QQTable: DatabaseTable {
            
            public Glo_QQTable(IDataProvider provider):base("Glo_QQ",provider){
                ClassName = "Glo_QQ";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("QQ", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn QQ{
                get{
                    return this.GetColumn("QQ");
                }
            }
				
   			public static string QQColumn{
			      get{
        			return "QQ";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_Venue
        /// Primary Key: ID
        /// </summary>

        public class Pro_VenueTable: DatabaseTable {
            
            public Pro_VenueTable(IDataProvider provider):base("Pro_Venue",provider){
                ClassName = "Pro_Venue";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DepartureID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Departure", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PickAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SendAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("RouteType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("MeetTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn DepartureID{
                get{
                    return this.GetColumn("DepartureID");
                }
            }
				
   			public static string DepartureIDColumn{
			      get{
        			return "DepartureID";
      			}
		    }
            
            public IColumn Departure{
                get{
                    return this.GetColumn("Departure");
                }
            }
				
   			public static string DepartureColumn{
			      get{
        			return "Departure";
      			}
		    }
            
            public IColumn PickAmt{
                get{
                    return this.GetColumn("PickAmt");
                }
            }
				
   			public static string PickAmtColumn{
			      get{
        			return "PickAmt";
      			}
		    }
            
            public IColumn SendAmt{
                get{
                    return this.GetColumn("SendAmt");
                }
            }
				
   			public static string SendAmtColumn{
			      get{
        			return "SendAmt";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn RouteType{
                get{
                    return this.GetColumn("RouteType");
                }
            }
				
   			public static string RouteTypeColumn{
			      get{
        			return "RouteType";
      			}
		    }
            
            public IColumn MeetTime{
                get{
                    return this.GetColumn("MeetTime");
                }
            }
				
   			public static string MeetTimeColumn{
			      get{
        			return "MeetTime";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderCustomer
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderCustomerTable: DatabaseTable {
            
            public Ord_OrderCustomerTable(IDataProvider provider):base("Ord_OrderCustomer",provider){
                ClassName = "Ord_OrderCustomer";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CustomerID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Sex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IDType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IDNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200
                });

                Columns.Add(new DatabaseColumn("Company", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("IsLeader", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn CustomerID{
                get{
                    return this.GetColumn("CustomerID");
                }
            }
				
   			public static string CustomerIDColumn{
			      get{
        			return "CustomerID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Sex{
                get{
                    return this.GetColumn("Sex");
                }
            }
				
   			public static string SexColumn{
			      get{
        			return "Sex";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn IDType{
                get{
                    return this.GetColumn("IDType");
                }
            }
				
   			public static string IDTypeColumn{
			      get{
        			return "IDType";
      			}
		    }
            
            public IColumn IDNo{
                get{
                    return this.GetColumn("IDNo");
                }
            }
				
   			public static string IDNoColumn{
			      get{
        			return "IDNo";
      			}
		    }
            
            public IColumn Company{
                get{
                    return this.GetColumn("Company");
                }
            }
				
   			public static string CompanyColumn{
			      get{
        			return "Company";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn IsLeader{
                get{
                    return this.GetColumn("IsLeader");
                }
            }
				
   			public static string IsLeaderColumn{
			      get{
        			return "IsLeader";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: User_Favorites
        /// Primary Key: ID
        /// </summary>

        public class User_FavoritesTable: DatabaseTable {
            
            public User_FavoritesTable(IDataProvider provider):base("User_Favorites",provider){
                ClassName = "User_Favorite";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("URL", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn URL{
                get{
                    return this.GetColumn("URL");
                }
            }
				
   			public static string URLColumn{
			      get{
        			return "URL";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_OrgSetting
        /// Primary Key: ID
        /// </summary>

        public class Om_OrgSettingTable: DatabaseTable {
            
            public Om_OrgSettingTable(IDataProvider provider):base("Om_OrgSetting",provider){
                ClassName = "Om_OrgSetting";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("xType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("xVal", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("effectiveData", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn xType{
                get{
                    return this.GetColumn("xType");
                }
            }
				
   			public static string xTypeColumn{
			      get{
        			return "xType";
      			}
		    }
            
            public IColumn xVal{
                get{
                    return this.GetColumn("xVal");
                }
            }
				
   			public static string xValColumn{
			      get{
        			return "xVal";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn effectiveData{
                get{
                    return this.GetColumn("effectiveData");
                }
            }
				
   			public static string effectiveDataColumn{
			      get{
        			return "effectiveData";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Rpt_OrderSheet
        /// Primary Key: ID
        /// </summary>

        public class Rpt_OrderSheetTable: DatabaseTable {
            
            public Rpt_OrderSheetTable(IDataProvider provider):base("Rpt_OrderSheet",provider){
                ClassName = "Rpt_OrderSheet";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("TourDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ReturnDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SupplierName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CustomerName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CollectedAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UnCollectedAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Profit", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderInvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PaidAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UnPaidAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Participant", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ParticipantID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderCreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderNo{
                get{
                    return this.GetColumn("OrderNo");
                }
            }
				
   			public static string OrderNoColumn{
			      get{
        			return "OrderNo";
      			}
		    }
            
            public IColumn OrderName{
                get{
                    return this.GetColumn("OrderName");
                }
            }
				
   			public static string OrderNameColumn{
			      get{
        			return "OrderName";
      			}
		    }
            
            public IColumn TourDate{
                get{
                    return this.GetColumn("TourDate");
                }
            }
				
   			public static string TourDateColumn{
			      get{
        			return "TourDate";
      			}
		    }
            
            public IColumn ReturnDate{
                get{
                    return this.GetColumn("ReturnDate");
                }
            }
				
   			public static string ReturnDateColumn{
			      get{
        			return "ReturnDate";
      			}
		    }
            
            public IColumn OrderType{
                get{
                    return this.GetColumn("OrderType");
                }
            }
				
   			public static string OrderTypeColumn{
			      get{
        			return "OrderType";
      			}
		    }
            
            public IColumn SupplierName{
                get{
                    return this.GetColumn("SupplierName");
                }
            }
				
   			public static string SupplierNameColumn{
			      get{
        			return "SupplierName";
      			}
		    }
            
            public IColumn CustomerName{
                get{
                    return this.GetColumn("CustomerName");
                }
            }
				
   			public static string CustomerNameColumn{
			      get{
        			return "CustomerName";
      			}
		    }
            
            public IColumn AdultNum{
                get{
                    return this.GetColumn("AdultNum");
                }
            }
				
   			public static string AdultNumColumn{
			      get{
        			return "AdultNum";
      			}
		    }
            
            public IColumn ChildNum{
                get{
                    return this.GetColumn("ChildNum");
                }
            }
				
   			public static string ChildNumColumn{
			      get{
        			return "ChildNum";
      			}
		    }
            
            public IColumn OrderAmt{
                get{
                    return this.GetColumn("OrderAmt");
                }
            }
				
   			public static string OrderAmtColumn{
			      get{
        			return "OrderAmt";
      			}
		    }
            
            public IColumn CollectedAmt{
                get{
                    return this.GetColumn("CollectedAmt");
                }
            }
				
   			public static string CollectedAmtColumn{
			      get{
        			return "CollectedAmt";
      			}
		    }
            
            public IColumn UnCollectedAmt{
                get{
                    return this.GetColumn("UnCollectedAmt");
                }
            }
				
   			public static string UnCollectedAmtColumn{
			      get{
        			return "UnCollectedAmt";
      			}
		    }
            
            public IColumn OrderCost{
                get{
                    return this.GetColumn("OrderCost");
                }
            }
				
   			public static string OrderCostColumn{
			      get{
        			return "OrderCost";
      			}
		    }
            
            public IColumn Profit{
                get{
                    return this.GetColumn("Profit");
                }
            }
				
   			public static string ProfitColumn{
			      get{
        			return "Profit";
      			}
		    }
            
            public IColumn OrderInvoiceAmt{
                get{
                    return this.GetColumn("OrderInvoiceAmt");
                }
            }
				
   			public static string OrderInvoiceAmtColumn{
			      get{
        			return "OrderInvoiceAmt";
      			}
		    }
            
            public IColumn PaidAmt{
                get{
                    return this.GetColumn("PaidAmt");
                }
            }
				
   			public static string PaidAmtColumn{
			      get{
        			return "PaidAmt";
      			}
		    }
            
            public IColumn UnPaidAmt{
                get{
                    return this.GetColumn("UnPaidAmt");
                }
            }
				
   			public static string UnPaidAmtColumn{
			      get{
        			return "UnPaidAmt";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn DeptName{
                get{
                    return this.GetColumn("DeptName");
                }
            }
				
   			public static string DeptNameColumn{
			      get{
        			return "DeptName";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrderStatus{
                get{
                    return this.GetColumn("OrderStatus");
                }
            }
				
   			public static string OrderStatusColumn{
			      get{
        			return "OrderStatus";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn Participant{
                get{
                    return this.GetColumn("Participant");
                }
            }
				
   			public static string ParticipantColumn{
			      get{
        			return "Participant";
      			}
		    }
            
            public IColumn ParticipantID{
                get{
                    return this.GetColumn("ParticipantID");
                }
            }
				
   			public static string ParticipantIDColumn{
			      get{
        			return "ParticipantID";
      			}
		    }
            
            public IColumn OrderCreateDate{
                get{
                    return this.GetColumn("OrderCreateDate");
                }
            }
				
   			public static string OrderCreateDateColumn{
			      get{
        			return "OrderCreateDate";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Pro_RouteInfo
        /// Primary Key: ID
        /// </summary>

        public class Pro_RouteInfoTable: DatabaseTable {
            
            public Pro_RouteInfoTable(IDataProvider provider):base("Pro_RouteInfo",provider){
                ClassName = "Pro_RouteInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("ScheduleDays", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RouteType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Destination", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationPath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Feature", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });

                Columns.Add(new DatabaseColumn("PriceInclude", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });

                Columns.Add(new DatabaseColumn("PriceNonIncude", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });

                Columns.Add(new DatabaseColumn("SelfItem", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });

                Columns.Add(new DatabaseColumn("Remind", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });

                Columns.Add(new DatabaseColumn("Shopping", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UpdateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RouteSource", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteSourceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn RouteNo{
                get{
                    return this.GetColumn("RouteNo");
                }
            }
				
   			public static string RouteNoColumn{
			      get{
        			return "RouteNo";
      			}
		    }
            
            public IColumn RouteName{
                get{
                    return this.GetColumn("RouteName");
                }
            }
				
   			public static string RouteNameColumn{
			      get{
        			return "RouteName";
      			}
		    }
            
            public IColumn ScheduleDays{
                get{
                    return this.GetColumn("ScheduleDays");
                }
            }
				
   			public static string ScheduleDaysColumn{
			      get{
        			return "ScheduleDays";
      			}
		    }
            
            public IColumn RouteType{
                get{
                    return this.GetColumn("RouteType");
                }
            }
				
   			public static string RouteTypeColumn{
			      get{
        			return "RouteType";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn Destination{
                get{
                    return this.GetColumn("Destination");
                }
            }
				
   			public static string DestinationColumn{
			      get{
        			return "Destination";
      			}
		    }
            
            public IColumn DestinationID{
                get{
                    return this.GetColumn("DestinationID");
                }
            }
				
   			public static string DestinationIDColumn{
			      get{
        			return "DestinationID";
      			}
		    }
            
            public IColumn DestinationPath{
                get{
                    return this.GetColumn("DestinationPath");
                }
            }
				
   			public static string DestinationPathColumn{
			      get{
        			return "DestinationPath";
      			}
		    }
            
            public IColumn Feature{
                get{
                    return this.GetColumn("Feature");
                }
            }
				
   			public static string FeatureColumn{
			      get{
        			return "Feature";
      			}
		    }
            
            public IColumn PriceInclude{
                get{
                    return this.GetColumn("PriceInclude");
                }
            }
				
   			public static string PriceIncludeColumn{
			      get{
        			return "PriceInclude";
      			}
		    }
            
            public IColumn PriceNonIncude{
                get{
                    return this.GetColumn("PriceNonIncude");
                }
            }
				
   			public static string PriceNonIncudeColumn{
			      get{
        			return "PriceNonIncude";
      			}
		    }
            
            public IColumn SelfItem{
                get{
                    return this.GetColumn("SelfItem");
                }
            }
				
   			public static string SelfItemColumn{
			      get{
        			return "SelfItem";
      			}
		    }
            
            public IColumn Remind{
                get{
                    return this.GetColumn("Remind");
                }
            }
				
   			public static string RemindColumn{
			      get{
        			return "Remind";
      			}
		    }
            
            public IColumn Shopping{
                get{
                    return this.GetColumn("Shopping");
                }
            }
				
   			public static string ShoppingColumn{
			      get{
        			return "Shopping";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn UpdateUserID{
                get{
                    return this.GetColumn("UpdateUserID");
                }
            }
				
   			public static string UpdateUserIDColumn{
			      get{
        			return "UpdateUserID";
      			}
		    }
            
            public IColumn UpdateUserName{
                get{
                    return this.GetColumn("UpdateUserName");
                }
            }
				
   			public static string UpdateUserNameColumn{
			      get{
        			return "UpdateUserName";
      			}
		    }
            
            public IColumn UpdateDate{
                get{
                    return this.GetColumn("UpdateDate");
                }
            }
				
   			public static string UpdateDateColumn{
			      get{
        			return "UpdateDate";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
            public IColumn RouteSource{
                get{
                    return this.GetColumn("RouteSource");
                }
            }
				
   			public static string RouteSourceColumn{
			      get{
        			return "RouteSource";
      			}
		    }
            
            public IColumn RouteSourceID{
                get{
                    return this.GetColumn("RouteSourceID");
                }
            }
				
   			public static string RouteSourceIDColumn{
			      get{
        			return "RouteSourceID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_BasicInfo
        /// Primary Key: ID
        /// </summary>

        public class Sn_BasicInfoTable: DatabaseTable {
            
            public Sn_BasicInfoTable(IDataProvider provider):base("Sn_BasicInfo",provider){
                ClassName = "Sn_BasicInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsShowRoute", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("LinkUrl", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("TravelName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("HotLine", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Logo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("AboutUs", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn IsShowRoute{
                get{
                    return this.GetColumn("IsShowRoute");
                }
            }
				
   			public static string IsShowRouteColumn{
			      get{
        			return "IsShowRoute";
      			}
		    }
            
            public IColumn LinkUrl{
                get{
                    return this.GetColumn("LinkUrl");
                }
            }
				
   			public static string LinkUrlColumn{
			      get{
        			return "LinkUrl";
      			}
		    }
            
            public IColumn TravelName{
                get{
                    return this.GetColumn("TravelName");
                }
            }
				
   			public static string TravelNameColumn{
			      get{
        			return "TravelName";
      			}
		    }
            
            public IColumn HotLine{
                get{
                    return this.GetColumn("HotLine");
                }
            }
				
   			public static string HotLineColumn{
			      get{
        			return "HotLine";
      			}
		    }
            
            public IColumn Logo{
                get{
                    return this.GetColumn("Logo");
                }
            }
				
   			public static string LogoColumn{
			      get{
        			return "Logo";
      			}
		    }
            
            public IColumn AboutUs{
                get{
                    return this.GetColumn("AboutUs");
                }
            }
				
   			public static string AboutUsColumn{
			      get{
        			return "AboutUs";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_AdSlide
        /// Primary Key: ID
        /// </summary>

        public class Sn_AdSlideTable: DatabaseTable {
            
            public Sn_AdSlideTable(IDataProvider provider):base("Sn_AdSlide",provider){
                ClassName = "Sn_AdSlide";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ImgSrc", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("Summary", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250
                });

                Columns.Add(new DatabaseColumn("LinkUrl", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn ImgSrc{
                get{
                    return this.GetColumn("ImgSrc");
                }
            }
				
   			public static string ImgSrcColumn{
			      get{
        			return "ImgSrc";
      			}
		    }
            
            public IColumn Summary{
                get{
                    return this.GetColumn("Summary");
                }
            }
				
   			public static string SummaryColumn{
			      get{
        			return "Summary";
      			}
		    }
            
            public IColumn LinkUrl{
                get{
                    return this.GetColumn("LinkUrl");
                }
            }
				
   			public static string LinkUrlColumn{
			      get{
        			return "LinkUrl";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_MenuItem
        /// Primary Key: ID
        /// </summary>

        public class Sn_MenuItemTable: DatabaseTable {
            
            public Sn_MenuItemTable(IDataProvider provider):base("Sn_MenuItem",provider){
                ClassName = "Sn_MenuItem";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("MenuName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("MenuUrl", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("SortIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CtrlChecked", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn MenuName{
                get{
                    return this.GetColumn("MenuName");
                }
            }
				
   			public static string MenuNameColumn{
			      get{
        			return "MenuName";
      			}
		    }
            
            public IColumn MenuUrl{
                get{
                    return this.GetColumn("MenuUrl");
                }
            }
				
   			public static string MenuUrlColumn{
			      get{
        			return "MenuUrl";
      			}
		    }
            
            public IColumn SortIndex{
                get{
                    return this.GetColumn("SortIndex");
                }
            }
				
   			public static string SortIndexColumn{
			      get{
        			return "SortIndex";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CtrlChecked{
                get{
                    return this.GetColumn("CtrlChecked");
                }
            }
				
   			public static string CtrlCheckedColumn{
			      get{
        			return "CtrlChecked";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_Area
        /// Primary Key: ID
        /// </summary>

        public class Om_AreaTable: DatabaseTable {
            
            public Om_AreaTable(IDataProvider provider):base("Om_Area",provider){
                ClassName = "Om_Area";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ParentID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AreaName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderIndex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn ParentID{
                get{
                    return this.GetColumn("ParentID");
                }
            }
				
   			public static string ParentIDColumn{
			      get{
        			return "ParentID";
      			}
		    }
            
            public IColumn AreaName{
                get{
                    return this.GetColumn("AreaName");
                }
            }
				
   			public static string AreaNameColumn{
			      get{
        			return "AreaName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn OrderIndex{
                get{
                    return this.GetColumn("OrderIndex");
                }
            }
				
   			public static string OrderIndexColumn{
			      get{
        			return "OrderIndex";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_DrawMoney
        /// Primary Key: ID
        /// </summary>

        public class Ord_DrawMoneyTable: DatabaseTable {
            
            public Ord_DrawMoneyTable(IDataProvider provider):base("Ord_DrawMoney",provider){
                ClassName = "Ord_DrawMoney";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Amount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Method", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderID{
                get{
                    return this.GetColumn("OrderID");
                }
            }
				
   			public static string OrderIDColumn{
			      get{
        			return "OrderID";
      			}
		    }
            
            public IColumn Amount{
                get{
                    return this.GetColumn("Amount");
                }
            }
				
   			public static string AmountColumn{
			      get{
        			return "Amount";
      			}
		    }
            
            public IColumn Method{
                get{
                    return this.GetColumn("Method");
                }
            }
				
   			public static string MethodColumn{
			      get{
        			return "Method";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Sn_Memeber
        /// Primary Key: ID
        /// </summary>

        public class Sn_MemeberTable: DatabaseTable {
            
            public Sn_MemeberTable(IDataProvider provider):base("Sn_Memeber",provider){
                ClassName = "Sn_Memeber";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserAcct", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserPwd", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("NickName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AppTokenID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Photo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 150
                });

                Columns.Add(new DatabaseColumn("IDNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Integral", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("LastActiveDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn UserAcct{
                get{
                    return this.GetColumn("UserAcct");
                }
            }
				
   			public static string UserAcctColumn{
			      get{
        			return "UserAcct";
      			}
		    }
            
            public IColumn UserPwd{
                get{
                    return this.GetColumn("UserPwd");
                }
            }
				
   			public static string UserPwdColumn{
			      get{
        			return "UserPwd";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn UserName{
                get{
                    return this.GetColumn("UserName");
                }
            }
				
   			public static string UserNameColumn{
			      get{
        			return "UserName";
      			}
		    }
            
            public IColumn NickName{
                get{
                    return this.GetColumn("NickName");
                }
            }
				
   			public static string NickNameColumn{
			      get{
        			return "NickName";
      			}
		    }
            
            public IColumn AppTokenID{
                get{
                    return this.GetColumn("AppTokenID");
                }
            }
				
   			public static string AppTokenIDColumn{
			      get{
        			return "AppTokenID";
      			}
		    }
            
            public IColumn Photo{
                get{
                    return this.GetColumn("Photo");
                }
            }
				
   			public static string PhotoColumn{
			      get{
        			return "Photo";
      			}
		    }
            
            public IColumn IDNo{
                get{
                    return this.GetColumn("IDNo");
                }
            }
				
   			public static string IDNoColumn{
			      get{
        			return "IDNo";
      			}
		    }
            
            public IColumn Integral{
                get{
                    return this.GetColumn("Integral");
                }
            }
				
   			public static string IntegralColumn{
			      get{
        			return "Integral";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn LastActiveDate{
                get{
                    return this.GetColumn("LastActiveDate");
                }
            }
				
   			public static string LastActiveDateColumn{
			      get{
        			return "LastActiveDate";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Om_UserInfo
        /// Primary Key: ID
        /// </summary>

        public class Om_UserInfoTable: DatabaseTable {
            
            public Om_UserInfoTable(IDataProvider provider):base("Om_UserInfo",provider){
                ClassName = "Om_UserInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserAcct", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UserPwd", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DataStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn OrgName{
                get{
                    return this.GetColumn("OrgName");
                }
            }
				
   			public static string OrgNameColumn{
			      get{
        			return "OrgName";
      			}
		    }
            
            public IColumn UserName{
                get{
                    return this.GetColumn("UserName");
                }
            }
				
   			public static string UserNameColumn{
			      get{
        			return "UserName";
      			}
		    }
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
   			public static string MobileColumn{
			      get{
        			return "Mobile";
      			}
		    }
            
            public IColumn UserAcct{
                get{
                    return this.GetColumn("UserAcct");
                }
            }
				
   			public static string UserAcctColumn{
			      get{
        			return "UserAcct";
      			}
		    }
            
            public IColumn UserPwd{
                get{
                    return this.GetColumn("UserPwd");
                }
            }
				
   			public static string UserPwdColumn{
			      get{
        			return "UserPwd";
      			}
		    }
            
            public IColumn DataStatus{
                get{
                    return this.GetColumn("DataStatus");
                }
            }
				
   			public static string DataStatusColumn{
			      get{
        			return "DataStatus";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn CreateName{
                get{
                    return this.GetColumn("CreateName");
                }
            }
				
   			public static string CreateNameColumn{
			      get{
        			return "CreateName";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_BudgetComment
        /// Primary Key: ID
        /// </summary>

        public class Ord_BudgetCommentTable: DatabaseTable {
            
            public Ord_BudgetCommentTable(IDataProvider provider):base("Ord_BudgetComment",provider){
                ClassName = "Ord_BudgetComment";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderBudgetID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Comment", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderBudgetID{
                get{
                    return this.GetColumn("OrderBudgetID");
                }
            }
				
   			public static string OrderBudgetIDColumn{
			      get{
        			return "OrderBudgetID";
      			}
		    }
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
            
            public IColumn Comment{
                get{
                    return this.GetColumn("Comment");
                }
            }
				
   			public static string CommentColumn{
			      get{
        			return "Comment";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
                    
        }
        
        /// <summary>
        /// Table: Ord_OrderInfo
        /// Primary Key: ID
        /// </summary>

        public class Ord_OrderInfoTable: DatabaseTable {
            
            public Ord_OrderInfoTable(IDataProvider provider):base("Ord_OrderInfo",provider){
                ClassName = "Ord_OrderInfo";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrderType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RouteTypeID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("RouteTypeName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DestinationPath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("OrderName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("OrderNo", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("Departure", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DepartureID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("AdultNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ChildNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AdjustAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderCost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TourDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TourDays", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ReturnDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BudgetStatus", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Remark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 400
                });

                Columns.Add(new DatabaseColumn("Schedule", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("VenueName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PickAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SendAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CollectTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsCloseCollected", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TourID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SourceID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SourceName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SeatNum", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CustomerID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CustomerName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("SupplierName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("CollectedAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ToConfirmCollectedAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PaidAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("OrderInvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CostInvoiceAmt", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("OrgID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("UpdateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsCheckAccount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("AuditRemark", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 500
                });

                Columns.Add(new DatabaseColumn("Participant", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("DeptName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("ParticipantID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PartDeptID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });
                    
                
                
            }

            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
            
            public IColumn OrderType{
                get{
                    return this.GetColumn("OrderType");
                }
            }
				
   			public static string OrderTypeColumn{
			      get{
        			return "OrderType";
      			}
		    }
            
            public IColumn RouteTypeID{
                get{
                    return this.GetColumn("RouteTypeID");
                }
            }
				
   			public static string RouteTypeIDColumn{
			      get{
        			return "RouteTypeID";
      			}
		    }
            
            public IColumn RouteTypeName{
                get{
                    return this.GetColumn("RouteTypeName");
                }
            }
				
   			public static string RouteTypeNameColumn{
			      get{
        			return "RouteTypeName";
      			}
		    }
            
            public IColumn DestinationID{
                get{
                    return this.GetColumn("DestinationID");
                }
            }
				
   			public static string DestinationIDColumn{
			      get{
        			return "DestinationID";
      			}
		    }
            
            public IColumn DestinationName{
                get{
                    return this.GetColumn("DestinationName");
                }
            }
				
   			public static string DestinationNameColumn{
			      get{
        			return "DestinationName";
      			}
		    }
            
            public IColumn DestinationPath{
                get{
                    return this.GetColumn("DestinationPath");
                }
            }
				
   			public static string DestinationPathColumn{
			      get{
        			return "DestinationPath";
      			}
		    }
            
            public IColumn OrderName{
                get{
                    return this.GetColumn("OrderName");
                }
            }
				
   			public static string OrderNameColumn{
			      get{
        			return "OrderName";
      			}
		    }
            
            public IColumn OrderNo{
                get{
                    return this.GetColumn("OrderNo");
                }
            }
				
   			public static string OrderNoColumn{
			      get{
        			return "OrderNo";
      			}
		    }
            
            public IColumn Departure{
                get{
                    return this.GetColumn("Departure");
                }
            }
				
   			public static string DepartureColumn{
			      get{
        			return "Departure";
      			}
		    }
            
            public IColumn DepartureID{
                get{
                    return this.GetColumn("DepartureID");
                }
            }
				
   			public static string DepartureIDColumn{
			      get{
        			return "DepartureID";
      			}
		    }
            
            public IColumn AdultNum{
                get{
                    return this.GetColumn("AdultNum");
                }
            }
				
   			public static string AdultNumColumn{
			      get{
        			return "AdultNum";
      			}
		    }
            
            public IColumn ChildNum{
                get{
                    return this.GetColumn("ChildNum");
                }
            }
				
   			public static string ChildNumColumn{
			      get{
        			return "ChildNum";
      			}
		    }
            
            public IColumn AdjustAmt{
                get{
                    return this.GetColumn("AdjustAmt");
                }
            }
				
   			public static string AdjustAmtColumn{
			      get{
        			return "AdjustAmt";
      			}
		    }
            
            public IColumn OrderAmt{
                get{
                    return this.GetColumn("OrderAmt");
                }
            }
				
   			public static string OrderAmtColumn{
			      get{
        			return "OrderAmt";
      			}
		    }
            
            public IColumn OrderCost{
                get{
                    return this.GetColumn("OrderCost");
                }
            }
				
   			public static string OrderCostColumn{
			      get{
        			return "OrderCost";
      			}
		    }
            
            public IColumn TourDate{
                get{
                    return this.GetColumn("TourDate");
                }
            }
				
   			public static string TourDateColumn{
			      get{
        			return "TourDate";
      			}
		    }
            
            public IColumn TourDays{
                get{
                    return this.GetColumn("TourDays");
                }
            }
				
   			public static string TourDaysColumn{
			      get{
        			return "TourDays";
      			}
		    }
            
            public IColumn ReturnDate{
                get{
                    return this.GetColumn("ReturnDate");
                }
            }
				
   			public static string ReturnDateColumn{
			      get{
        			return "ReturnDate";
      			}
		    }
            
            public IColumn OrderStatus{
                get{
                    return this.GetColumn("OrderStatus");
                }
            }
				
   			public static string OrderStatusColumn{
			      get{
        			return "OrderStatus";
      			}
		    }
            
            public IColumn BudgetStatus{
                get{
                    return this.GetColumn("BudgetStatus");
                }
            }
				
   			public static string BudgetStatusColumn{
			      get{
        			return "BudgetStatus";
      			}
		    }
            
            public IColumn Remark{
                get{
                    return this.GetColumn("Remark");
                }
            }
				
   			public static string RemarkColumn{
			      get{
        			return "Remark";
      			}
		    }
            
            public IColumn Schedule{
                get{
                    return this.GetColumn("Schedule");
                }
            }
				
   			public static string ScheduleColumn{
			      get{
        			return "Schedule";
      			}
		    }
            
            public IColumn VenueName{
                get{
                    return this.GetColumn("VenueName");
                }
            }
				
   			public static string VenueNameColumn{
			      get{
        			return "VenueName";
      			}
		    }
            
            public IColumn PickAmt{
                get{
                    return this.GetColumn("PickAmt");
                }
            }
				
   			public static string PickAmtColumn{
			      get{
        			return "PickAmt";
      			}
		    }
            
            public IColumn SendAmt{
                get{
                    return this.GetColumn("SendAmt");
                }
            }
				
   			public static string SendAmtColumn{
			      get{
        			return "SendAmt";
      			}
		    }
            
            public IColumn CollectTime{
                get{
                    return this.GetColumn("CollectTime");
                }
            }
				
   			public static string CollectTimeColumn{
			      get{
        			return "CollectTime";
      			}
		    }
            
            public IColumn IsCloseCollected{
                get{
                    return this.GetColumn("IsCloseCollected");
                }
            }
				
   			public static string IsCloseCollectedColumn{
			      get{
        			return "IsCloseCollected";
      			}
		    }
            
            public IColumn TourID{
                get{
                    return this.GetColumn("TourID");
                }
            }
				
   			public static string TourIDColumn{
			      get{
        			return "TourID";
      			}
		    }
            
            public IColumn SourceID{
                get{
                    return this.GetColumn("SourceID");
                }
            }
				
   			public static string SourceIDColumn{
			      get{
        			return "SourceID";
      			}
		    }
            
            public IColumn SourceName{
                get{
                    return this.GetColumn("SourceName");
                }
            }
				
   			public static string SourceNameColumn{
			      get{
        			return "SourceName";
      			}
		    }
            
            public IColumn SeatNum{
                get{
                    return this.GetColumn("SeatNum");
                }
            }
				
   			public static string SeatNumColumn{
			      get{
        			return "SeatNum";
      			}
		    }
            
            public IColumn CustomerID{
                get{
                    return this.GetColumn("CustomerID");
                }
            }
				
   			public static string CustomerIDColumn{
			      get{
        			return "CustomerID";
      			}
		    }
            
            public IColumn CustomerName{
                get{
                    return this.GetColumn("CustomerName");
                }
            }
				
   			public static string CustomerNameColumn{
			      get{
        			return "CustomerName";
      			}
		    }
            
            public IColumn SupplierName{
                get{
                    return this.GetColumn("SupplierName");
                }
            }
				
   			public static string SupplierNameColumn{
			      get{
        			return "SupplierName";
      			}
		    }
            
            public IColumn CollectedAmt{
                get{
                    return this.GetColumn("CollectedAmt");
                }
            }
				
   			public static string CollectedAmtColumn{
			      get{
        			return "CollectedAmt";
      			}
		    }
            
            public IColumn ToConfirmCollectedAmt{
                get{
                    return this.GetColumn("ToConfirmCollectedAmt");
                }
            }
				
   			public static string ToConfirmCollectedAmtColumn{
			      get{
        			return "ToConfirmCollectedAmt";
      			}
		    }
            
            public IColumn PaidAmt{
                get{
                    return this.GetColumn("PaidAmt");
                }
            }
				
   			public static string PaidAmtColumn{
			      get{
        			return "PaidAmt";
      			}
		    }
            
            public IColumn OrderInvoiceAmt{
                get{
                    return this.GetColumn("OrderInvoiceAmt");
                }
            }
				
   			public static string OrderInvoiceAmtColumn{
			      get{
        			return "OrderInvoiceAmt";
      			}
		    }
            
            public IColumn CostInvoiceAmt{
                get{
                    return this.GetColumn("CostInvoiceAmt");
                }
            }
				
   			public static string CostInvoiceAmtColumn{
			      get{
        			return "CostInvoiceAmt";
      			}
		    }
            
            public IColumn CreateUserID{
                get{
                    return this.GetColumn("CreateUserID");
                }
            }
				
   			public static string CreateUserIDColumn{
			      get{
        			return "CreateUserID";
      			}
		    }
            
            public IColumn CreateUserName{
                get{
                    return this.GetColumn("CreateUserName");
                }
            }
				
   			public static string CreateUserNameColumn{
			      get{
        			return "CreateUserName";
      			}
		    }
            
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
            
            public IColumn DeptID{
                get{
                    return this.GetColumn("DeptID");
                }
            }
				
   			public static string DeptIDColumn{
			      get{
        			return "DeptID";
      			}
		    }
            
            public IColumn OrgID{
                get{
                    return this.GetColumn("OrgID");
                }
            }
				
   			public static string OrgIDColumn{
			      get{
        			return "OrgID";
      			}
		    }
            
            public IColumn UpdateUserID{
                get{
                    return this.GetColumn("UpdateUserID");
                }
            }
				
   			public static string UpdateUserIDColumn{
			      get{
        			return "UpdateUserID";
      			}
		    }
            
            public IColumn UpdateUserName{
                get{
                    return this.GetColumn("UpdateUserName");
                }
            }
				
   			public static string UpdateUserNameColumn{
			      get{
        			return "UpdateUserName";
      			}
		    }
            
            public IColumn UpdateDate{
                get{
                    return this.GetColumn("UpdateDate");
                }
            }
				
   			public static string UpdateDateColumn{
			      get{
        			return "UpdateDate";
      			}
		    }
            
            public IColumn IsCheckAccount{
                get{
                    return this.GetColumn("IsCheckAccount");
                }
            }
				
   			public static string IsCheckAccountColumn{
			      get{
        			return "IsCheckAccount";
      			}
		    }
            
            public IColumn AuditRemark{
                get{
                    return this.GetColumn("AuditRemark");
                }
            }
				
   			public static string AuditRemarkColumn{
			      get{
        			return "AuditRemark";
      			}
		    }
            
            public IColumn Participant{
                get{
                    return this.GetColumn("Participant");
                }
            }
				
   			public static string ParticipantColumn{
			      get{
        			return "Participant";
      			}
		    }
            
            public IColumn DeptName{
                get{
                    return this.GetColumn("DeptName");
                }
            }
				
   			public static string DeptNameColumn{
			      get{
        			return "DeptName";
      			}
		    }
            
            public IColumn ParticipantID{
                get{
                    return this.GetColumn("ParticipantID");
                }
            }
				
   			public static string ParticipantIDColumn{
			      get{
        			return "ParticipantID";
      			}
		    }
            
            public IColumn PartDeptID{
                get{
                    return this.GetColumn("PartDeptID");
                }
            }
				
   			public static string PartDeptIDColumn{
			      get{
        			return "PartDeptID";
      			}
		    }
            
                    
        }
        
}