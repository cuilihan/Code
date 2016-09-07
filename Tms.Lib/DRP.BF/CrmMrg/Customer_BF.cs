using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.UI.WebControls;
using DRP.DAL;
using DRP.Framework.Core;
using DRP.DAL.DataAccess;
using DRP.BF.CrmMrg;

namespace DRP.BF.Crm
{
    /// <summary>
    /// 客户查询条件
    /// </summary>
    public class CustomCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 创建部门ID
        /// </summary>
        public string DeptID { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreatorID { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public string CustomerType { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company { get; set; }
    }


    /// <summary>
    /// 客户生日查询类型
    /// </summary>
    public enum BirthDayType
    {
        /// <summary>
        /// 本月生日
        /// </summary>
        CurrentMonth,
        /// <summary>
        /// 下月生日
        /// </summary>
        NextMonth,
        /// <summary>
        /// 本日生日
        /// </summary>
        Today
    }

    /// <summary>
    /// 客户管理
    /// </summary>
    public class Customer_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 组合客户查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string CustomerQueryCondition(CustomCriteria qry)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            #region 用户权限
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    sb.AppendFormat(" and DeptID='{0}'", user.DeptID);
                    break;
                case SysMrg.DataPermission.Private:
                    sb.AppendFormat(" and CreateUserID='{0}'", user.UserID);
                    break;
            }
            #endregion
            if (!string.IsNullOrEmpty(qry.Name))
                sb.AppendFormat(" and Name like '%{0}%'", qry.Name);
            if (!string.IsNullOrEmpty(qry.Mobile))
                sb.AppendFormat(" and Mobile like '%{0}%'", qry.Mobile);
            if (!string.IsNullOrEmpty(qry.DeptID))
                sb.AppendFormat(" and DeptID='{0}'", qry.DeptID);
            if (!string.IsNullOrEmpty(qry.CreatorID))
                sb.AppendFormat(" and CreateUserID='{0}'", qry.CreatorID);
            if (!string.IsNullOrEmpty(qry.CustomerType))
                sb.AppendFormat(" and CustomerType like '%{0}%'", qry.CustomerType);
            if (!string.IsNullOrEmpty(qry.Company))
                sb.AppendFormat(" and Company like '%{0}%'", qry.Company);
            if (qry.QueryDateScope != null)
            {
                if (!string.IsNullOrEmpty(qry.QueryDateScope.sDate))
                    sb.AppendFormat(" and CreateDate>='{0}'", qry.QueryDateScope.sDate);
                if (!string.IsNullOrEmpty(qry.QueryDateScope.eDate))
                    sb.AppendFormat(" and CreateDate<'{0}'", Convert.ToDateTime(qry.QueryDateScope.eDate).AddDays(1).ToString("yyyy-MM-dd"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 客户列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Crm_Customer> QueryData(CustomCriteria qry, out int record)
        {
            return db.GetPaginationList<DAL.Crm_Customer>("Crm_Customer", qry.pageIndex, qry.pageSize, out record, CustomerQueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 客户即将生日提醒查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Crm_Customer> QueryCustomerBirth(CustomCriteria qry, BirthDayType birthday, out int record)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            if (birthday == BirthDayType.CurrentMonth)
                sb.AppendFormat(" and MONTH(dbo.f_getbirth(idnum,BirthDay))=MONTH(GETDATE())");
            else if (birthday == BirthDayType.NextMonth)
            {
                sb.AppendFormat(" and MONTH(dbo.f_getbirth(idnum,BirthDay))=MONTH(DATEADD(MONTH,1, GETDATE()))");
            }
            else
            {
                sb.AppendFormat(" and DAY(dbo.f_getbirth(idnum,BirthDay))=DAY(GETDATE()) and MONTH(dbo.f_getbirth(idnum,BirthDay))=MONTH(GETDATE())");
            }


            #region 用户权限
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    sb.AppendFormat(" and DeptID='{0}'", user.DeptID);
                    break;
                case SysMrg.DataPermission.Private:
                    sb.AppendFormat(" and CreateUserID='{0}'", user.UserID);
                    break;
            }
            #endregion

            return db.GetPaginationList<DAL.Crm_Customer>("Crm_Customer", qry.pageIndex, qry.pageSize, out record, sb.ToString(), qry.SortExpress);
        }


        /// <summary>
        /// 客户证件类型
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<DAL.Crm_CustomerCertificate> GetCertificate(string customerID)
        {
            return DAL.Crm_CustomerCertificate.Find(x => x.CustomerID == customerID).OrderBy(x => x.SortIndex).ToList();
        }

        /// <summary>
        /// 查询客户的证件详情
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public DAL.Crm_CustomerCertificate FindCustomerCertificate(string customerID, string itemType)
        {
            var list = DAL.Crm_CustomerCertificate.Find(x => x.ItemType == itemType && x.CustomerID == customerID);
            return list.Count > 0 ? list.First() : null;
        }

        /// <summary>
        /// 查询符合条件的所有数据，用于导出Excel
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private DataTable QueryAllData(CustomCriteria qry)
        {
            return new CustomerDAL().QueryAllData(CustomerQueryCondition(qry));
        }

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Crm_Customer Get(string keyID)
        {
            return DAL.Crm_Customer.SingleOrDefault(x => x.ID == keyID);
        }


        /// <summary>
        /// 查询客户资料
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public DAL.Crm_Customer GetCustomerByMobile(string mobile)
        {
            return DAL.Crm_Customer.SingleOrDefault(x => x.Mobile == mobile);
        }

        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <returns></returns>
        public bool Save(Crm_Customer e, List<DAL.Crm_VisitTrace> listTrace, List<DAL.Crm_CustomerCertificate> listCertificate)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region 销售线索子表
                    listTrace.ForEach(x =>
                    {
                        var traceID = string.IsNullOrEmpty(x.ID) ? Guid.NewGuid().ToString() : x.ID;
                        DAL.Crm_VisitTrace t = null;
                        if (!string.IsNullOrEmpty(x.ID))
                        {
                            t = DAL.Crm_VisitTrace.SingleOrDefault(b => b.ID == traceID);
                        }

                        if (t == null)
                        {
                            t = new Crm_VisitTrace();
                            t.ID = traceID;
                            t.OrgID = user.OrgID;
                            t.CreateDate = DateTime.Now;
                            t.CreateUserID = user.UserID;
                            t.CreateUserName = user.UserName;
                            t.CustomerID = e.ID;
                        }
                        t.ItemName = x.ItemName;
                        t.ItemType = x.ItemType;
                        t.Contact = x.Contact;
                        t.TradeDate = x.TradeDate;
                        t.Comment = x.Comment;
                        t.Save();
                    });
                    #endregion

                    var list = new List<string>();

                    #region 证件类型
                    DAL.Crm_CustomerCertificate.Delete(x => x.CustomerID == e.ID);
                    var idx = 1;
                    listCertificate.ForEach(x =>
                    {
                        var item = new DAL.Crm_CustomerCertificate();
                        item.ID = Guid.NewGuid().ToString();
                        item.CustomerID = e.ID;
                        item.OrgID = user.OrgID;
                        item.CreateDate = DateTime.Now;
                        item.ItemType = x.ItemType;
                        item.ItemVal = x.ItemVal;
                        item.SortIndex = idx++;
                        item.Save();
                        if (!string.IsNullOrEmpty(x.ItemType))
                        {
                            list.Add(x.ItemType);
                        }
                    });
                    #endregion

                    #region 客户主表
                    var entity = Get(e.ID);
                    if (entity == null)
                    {
                        entity = new DAL.Crm_Customer();
                        entity.ID = e.ID;
                        entity.OrgID = user.OrgID;
                        entity.DeptID = user.DeptID;
                        entity.CreateUserID = user.UserID;
                        entity.CreateUserName = user.UserName;
                        entity.CreateDate = DateTime.Now;
                        entity.TradeNum = 0;
                        entity.TradeAmt = 0;
                        entity.CommunicateNum = 0;
                    }

                    entity.CommunicateNum = listTrace.Count;
                    entity.Name = e.Name;
                    entity.EngName = e.EngName;
                    entity.Sex = e.Sex;
                    entity.Mobile = e.Mobile;
                    entity.Phone = e.Phone;
                    entity.IDNum = e.IDNum;
                    entity.Fax = e.Fax;
                    entity.Mail = e.Mail;
                    entity.QQ = e.QQ;
                    entity.Addr = e.Addr;
                    entity.Remark = e.Remark;
                    entity.Company = e.Company;
                    entity.CustomerType = e.CustomerType;
                    entity.BirthDay = e.BirthDay;
                    var str = string.Join(",", list);
                    entity.CustomerCertificate = str;//客户相关的证件及号码
                    entity.Save();
                    #endregion

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "保存客户时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="keyID"></param>
        public bool Delete(List<string> listIDs)
        {
            return new DAL.Crm_Customer().MultiDelete(listIDs);
        }

        /// <summary>
        /// 客户手机号是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool ExistMobile(string mobile, string orgID)
        {
            return DAL.Crm_Customer.Exists(x => x.Mobile.Equals(mobile) && x.OrgID == orgID);
        }

        /// <summary>
        /// 导入客户资料
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>-1:导入失败  >0：成功导入客户记录数</returns>
        public int Import(string fileName)
        {
            var iCount = 0;
            var user = AuthenticationPage.UserInfo;
            try
            {
                var keyArr = new string[] { "姓名", "性别", "手机号", "传真", "QQ", "公司名称", "身份证号", "备注", "客户类型", "办公电话", "地址" };
                var dt = new ExcelHelper().GetExcelDataAsTable(fileName);
                foreach (DataRow row in dt.Rows)
                {
                    var mobile = row[keyArr[2]].ToString().Replace("\t", "");
                    var name = row[keyArr[0]].ToString().Replace("\t", "");
                    if (string.IsNullOrEmpty(mobile))
                        continue;
                    if (mobile.Length != 11)
                        continue;
                    if (ExistMobile(mobile, user.OrgID))
                        continue;
                    var e = new DAL.Crm_Customer();
                    e.ID = Guid.NewGuid().ToString();
                    e.Name = name;
                    e.Company = row[keyArr[5]].ToString();
                    e.Sex = row[keyArr[1]].ToString();
                    e.Mobile = mobile;
                    e.IDNum = row[keyArr[6]].ToString();
                    e.Fax = row[keyArr[3]].ToString();
                    e.QQ = row[keyArr[4]].ToString();
                    e.CustomerType = row[keyArr[8]].ToString();
                    e.Remark = row[keyArr[7]].ToString();
                    e.Phone = row[keyArr[9]].ToString();
                    e.Addr = row[keyArr[10]].ToString();
                    e.TradeAmt = e.TradeNum = e.CommunicateNum = 0;
                    e.CreateDate = DateTime.Now;
                    e.CreateUserID = user.UserID;
                    e.CreateUserName = user.UserName;
                    e.OrgID = user.OrgID;
                    e.DeptID = user.DeptID;
                    e.Save();
                    iCount++;
                }
            }
            catch (Exception ex)
            {
                iCount = -1;
                BizUtility.ExceptionHandler(user, ex, "导入客户资料时发生错误");
            }
            finally //删除导入的临时文件
            {
                try
                {
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                }
                catch { }
            }
            return iCount;
        }

        /// <summary>
        /// 导出客户资料
        /// </summary>
        /// <param name="qry">查询条件</param>
        public bool Export(CustomCriteria qry)
        {
            try
            {
                var dt = QueryAllData(qry);
                var fileName = "客户资料";
                var list = new List<ExcelCellFormat>();
                list.Add(new ExcelCellFormat(15, "Name", "客户姓名"));
                list.Add(new ExcelCellFormat(20, "Company", "公司名称"));
                list.Add(new ExcelCellFormat(5, "Sex", "性别"));
                list.Add(new ExcelCellFormat(15, "Mobile", "手机号码"));
                list.Add(new ExcelCellFormat(20, "IDNum", "身份证号"));
                list.Add(new ExcelCellFormat(15, "CustomerType", "客户类型"));
                list.Add(new ExcelCellFormat(30, "Remark", "备注"));
                list.Add(new ExcelCellFormat(20, "Addr", "联系地址"));
                list.Add(new ExcelCellFormat(10, "TradeNum", "消费次数"));
                list.Add(new ExcelCellFormat(10, "TradeAmt", "消费金额"));
                list.Add(new ExcelCellFormat(10, "CommunicateNum", "沟通次数"));
                list.Add(new ExcelCellFormat(15, "CreateUserName", "创建人"));
                new ExcelHelper().ExportToExcel(fileName, dt, list);

                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "导出客户资料时发生错误");
                return false;
            }
        }

        #region << 客户订单查询 >>
        /// <summary>
        /// 客户订单查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable TradeOrder(QueryCriteriaBase qry, string sDate, string eDate, string customerID, out int record)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}' and CustomerID='{1}'", user.OrgID, customerID);
            if (!string.IsNullOrEmpty(qry.Keyword))
                sb.AppendFormat(" and OrderName like '%{0}%'", qry.Keyword);
            if (!string.IsNullOrEmpty(sDate))
                sb.AppendFormat(" and TourDate>='{0}'", sDate);
            if (!string.IsNullOrEmpty(eDate))
                sb.AppendFormat(" and TourDate<'{0}'", Convert.ToDateTime(eDate).AddDays(1).ToString("yyyy-MM-dd"));
            return db.GetPagination("V_Crm_TradeOrder", qry.pageIndex, qry.pageSize, out record, sb.ToString(), qry.SortExpress);
        }

        #endregion

        /// <summary>
        /// 查询客户资料：用于选择客户，默认只显示自己的客户，查询时可以查询别人的客户
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryMyCusomterData(CustomCriteria qry, out int record)
        {
            var strWhere = QueryMyCusomterDataCondition(qry);
            return db.GetPagination("Crm_Customer", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        private string QueryMyCusomterDataCondition(CustomCriteria qry)
        {
            var user = AuthenticationPage.UserInfo;
            var strWhere = "1=1 and Mobile<>'' and OrgID='" + user.OrgID + "' ";
            if (string.IsNullOrEmpty(qry.Name) && string.IsNullOrEmpty(qry.Mobile)) //只显示自己的数据
            {
                strWhere += string.Format(" and CreateUserID='{0}'", user.UserID);
            }
            else
            {
                if (!string.IsNullOrEmpty(qry.Name))
                    strWhere += string.Format(" and Name like '%{0}%'", qry.Name);
                if (!string.IsNullOrEmpty(qry.Mobile))
                    strWhere += string.Format(" and Mobile like '%{0}%'", qry.Mobile);
            }
            return strWhere;
        }
    }
}
