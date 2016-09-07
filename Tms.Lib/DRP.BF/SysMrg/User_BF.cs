using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DRP.BF.OmMrg;
using DRP.DAL;
using DRP.DAL.DataAccess;
using DRP.Framework.Core;
using System.Transactions;

namespace DRP.BF.SysMrg
{
    /// <summary>
    /// 系统用户管理
    /// </summary>
    public class User_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 系统用户列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Sys_UserInfo> GetSysUser(QueryCriteriaBase qry, out int record)
        {
            var strWhere = "1=1 and OrgID='" + AuthenticationPage.UserInfo.OrgID + "'";
            if (!string.IsNullOrEmpty(qry.Keyword))
                strWhere += string.Format(" and (Name like '%{0}%' or DeptName like '%{0}%')", qry.Keyword);
            return db.GetPaginationList<DAL.Sys_UserInfo>("Sys_UserInfo", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        public DataTable GetOTASysUser(QueryCriteriaBase qry)
        {
            var sql = "select * from V_OTAUserInfo where 1=1 and OrgID='{0}'";
            sql = string.Format(sql, AuthenticationPage.UserInfo.OrgID);
            if (!string.IsNullOrEmpty(qry.Keyword))
                sql += string.Format(" and (Name like '%{0}%' or DeptName like '%{0}%')", qry.Keyword);

            return new SubSonic.Query.CodingHorror(sql).ExecuteDataSet().Tables[0];
        }

        
        /// <summary>
        /// 部门用户
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public List<DAL.Sys_UserInfo> GetSysUser(string deptID)
        {
            return DAL.Sys_UserInfo.Find(x => x.DeptID == deptID && x.OrgID == AuthenticationPage.UserInfo.OrgID).ToList();
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public List<DAL.Sys_UserInfo> GetSysUser()
        {
            return DAL.Sys_UserInfo.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).OrderBy(x => x.DeptID).ToList();
        }

        /// <summary>
        /// 用户的角色列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<DAL.Sys_RoleInfo> GetUserRoles(string userID)
        {
            return new UserInfoDAL().GetUserRoles(userID);
        }

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Sys_UserInfo Get(string keyID)
        {
            return DAL.Sys_UserInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool SaveUser(string keyID, WebControl pnlControl, string pwd)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Sys_UserInfo();
                    entity.OrgID = user.OrgID;
                    entity.CreateUserID = user.UserID;
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                }
                entity = new ReflectHelper<DAL.Sys_UserInfo>().AssignEntity(entity, pnlControl);
                entity.AcctPwd = Security.EncrypByRijndael(pwd);
                entity.DeptName = GetDeptName(entity.DeptID);
                entity.PartDeptName = GetDeptName(entity.PartDeptID);
                entity.ID = keyID;
                entity.Save();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(user, ex, "保存系统用户时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private string GetDeptName(string deptID)
        {
            if (string.IsNullOrEmpty(deptID))
                return "";
            var e = new Dept_BF().Get(deptID);
            return e == null ? "" : e.Name;
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            Sys_UserInfo.Delete(x => x.ID == keyID);
        }

        /// <summary>
        /// 计算用户数量
        /// </summary>
        /// <returns></returns>
        public int CalculateUserQuantity()
        {
            return db.Count<DAL.Sys_UserInfo>(x => x.ID).Where<DAL.Sys_UserInfo>(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).ExecuteScalar<int>();
        }

        private object obj = new object();

        /// <summary>
        /// 登录账号是否存在
        /// </summary>
        /// <param name="userAcct"></param>
        /// <returns></returns>
        public bool ExistAcct(string userAcct, string orgID)
        {
            lock (obj)
            {
                return DAL.Om_UserInfo.Exists(x => x.UserAcct == userAcct && x.OrgID == orgID);
            }
        }

        /// <summary>
        /// 更新登录密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool UpdatePwd(string pwd)
        {
            try
            {
                var user = AuthenticationPage.UserInfo;
                if (user.LoginUserType == UserType.BizUser)
                {
                    var u = Get(user.UserID);
                    u.AcctPwd = Security.EncrypByRijndael(pwd);
                    u.ID = user.UserID;
                    u.Save();
                }
                else
                {
                    var u = new OmUser_BF().Get(user.UserID);
                    u.UserPwd = Security.EncrypByRijndael(pwd);
                    u.ID = user.UserID;
                    u.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "修改密码时发生错误");
                return false;
            }
        }



        #region 用户权限查询

        /// <summary>
        /// 权限权限查询
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable QueryUserNavPermission(string userID)
        {
            return new DRPDB().DRP_UserNavPermission(userID).ExecuteDataSet().Tables[0];
        }

        /// <summary>
        /// 用户数据权限查询
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataPermission QueryUserDataPermission(string userID)
        {
            var p = new PermissionDAL().UserDataPermission(userID);
            return (DataPermission)p;
        }

        /// <summary>
        /// 用户的订单查询权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string QueryUserOrderPermission(string userID)
        {
            var list = new PermissionDAL().UserRouteTypeNamePermission(userID);
            return string.Join("<br/>", list);
        }
        #endregion

        /// <summary>
        /// OTA同步后的数据
        /// </summary>
        /// <param name="OTAID"></param>
        /// <returns></returns>
        public List<OTAUserAccount> GetOTABindUserInfo(string uid)
        {
            var sql = string.Format("select a.ID,a.Name,a.DeptName,a.Mobile,a.Email,b.OTAUName,b.OTAID,c.OTAName,b.ID CID from Sys_UserInfo as a right join OTA_UserInfo as b on a.ID=b.UID left join OTA_Setting as c on b.OTAID=c.ID where a.ID='{0}'", uid);
            return new SubSonic.Query.CodingHorror(sql).ExecuteTypedList<OTAUserAccount>().ToList();
        }

        /// <summary>
        /// OTA同步初始化数据
        /// </summary>
        /// <param name="OtaID"></param>
        /// <returns></returns>
        public bool InitUserData(Guid OtaID)
        {
            bool isok = false;
            try
            {
                var list = GetUserCollection();
                var listGet = new DRP.BF.DataSync.OctHelper().QueryPackageBuyerStoreUserList();
                using (TransactionScope scope = new TransactionScope())
                {
                    DAL.OTA_UserInfo.Delete(x => x.OTAID == OtaID);


                    listGet.ForEach(x =>
                    {
                        var u = DAL.Sys_UserInfo.Find(y => y.Name.Contains(x.Contact)).FirstOrDefault();
                        if (u != null)
                        {
                            var entity = new DAL.OTA_UserInfo();
                            entity.ID = Guid.NewGuid();
                            entity.OTAUID = x.UserID.ToString();
                            entity.OTAUName = x.Contact + "[" + x.UserAccount + "]";
                            entity.UID = Guid.Parse(u.ID);
                            entity.UName = u.Name;
                            entity.CreateDate = DateTime.Now;
                            entity.OTAID = OtaID;
                            entity.Save();
                        }
                    });
                    scope.Complete();
                    isok = true;
                }
            }
            catch (Exception)
            {


            }

            return isok;

        }

        /// <summary>
        /// OTA人员解除绑定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool OTAUserDelete(Guid id)
        {
            bool isok = false;

            try
            {
                DAL.OTA_UserInfo.Delete(x => x.ID == id);
                isok = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return isok;
        }

        public List<DAL.Sys_UserInfo> GetUserCollection()
        {
            return DAL.Sys_UserInfo.All().ToList();
        }

        /// <summary>
        /// 保存OTA同步关系
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="otdid">平台ID</param>
        /// <param name="otauid">OTA用户ID</param>
        /// <param name="name">姓名</param>
        /// <param name="otaName">OTA的用户姓名</param>
        /// <returns></returns>
        public bool OTAUserSave(string uid, string otdid, string otauid, string name, string otaName)
        {
            bool isok = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var __uid = Guid.Parse(uid);
                    var __otdid = Guid.Parse(otdid);
                    DAL.OTA_UserInfo.Delete(x => x.UID == __uid && x.OTAID == __otdid);

                    var entity = new DAL.OTA_UserInfo();
                    entity.ID = Guid.NewGuid();
                    entity.OTAUID = otauid;
                    entity.OTAUName = otaName;
                    entity.OTAID = Guid.Parse(otdid);
                    entity.UID = Guid.Parse(uid);
                    entity.UName = name;
                    entity.CreateDate = DateTime.Now;
                    entity.Save();

                    scope.Complete();
                    isok = true;
                }
            }
            catch (Exception)
            {

            }
            return isok;
        }
    }



    /// <summary>
    /// OTA用户实体
    /// </summary>
    public class OTAUserAccount : Sys_UserInfo
    {
        public string OTAUName { get; set; }
        public Guid OTAID { get; set; }
        public string OTAName { get; set; }
        public Guid CID { get; set; }
    }
}
