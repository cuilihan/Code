using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;
using DRP.BF.Cache;
using DRP.Cached;
using DRP.DAL;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.Log;
using System.Xml;
using DRP.Message.Core;
using System.Data;

namespace DRP.BF.OmMrg
{

    /// <summary>
    /// 机构信息
    /// </summary>
    public class OrgInfo_BF
    {
        DRPDB db = new DRPDB();

        private const string Om_OrgInfo_Key = "Om_OrgInfo_Key";

        /// <summary>
        /// 机构集合
        /// </summary>
        /// <returns></returns>
        public List<DAL.Om_OrgInfo> GetOrgInfo()
        {
            var list = BizCacheHelper.OrgInfoCache.Get(Om_OrgInfo_Key);
            if (list == null)
            {
                list = Om_OrgInfo.All().OrderBy(x => x.CreateDate).ToList();
                BizCacheHelper.OrgInfoCache.Insert(Om_OrgInfo_Key, list);
            }
            return list;
        }


        /// <summary>
        /// 当前域名对应的机构ID
        /// </summary> 
        /// <returns></returns>
        public static DAL.Om_OrgInfo DomainOrgInfo()
        {
            var hostName = HttpContext.Current.Request.Url.Host;
            var list = new OrgInfo_BF().GetOrgInfo();
            return list.Find(x => x.ProDomain.Equals(hostName));
        }


        /// <summary>
        /// 机构列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<DAL.Om_OrgInfo> GetOrgInfo(QueryCriteriaBase qry, out int record)
        {
            var strWhere = "1=1";
            if (!string.IsNullOrEmpty(qry.Keyword))
                strWhere += string.Format(" and (Name like '%{0}%' or AreaPath like '%{0}%')", qry.Keyword);
            if (qry.DataStatus == -1)
                strWhere += " and DataStatus=-1";
            else
                strWhere += " and DataStatus>-1";
            return db.GetPaginationList<DAL.Om_OrgInfo>("Om_OrgInfo", qry.pageIndex, qry.pageSize, out record, strWhere, qry.SortExpress);
        }

        /// <summary>
        /// 机构详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Om_OrgInfo Get(string keyID)
        {
            return DAL.Om_OrgInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存机构
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool Save(string keyID, WebControl pnlControl, string areaID, string navGroup)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                var entity = Get(keyID);
                if (entity == null)
                {
                    entity = new DAL.Om_OrgInfo();
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserID = user.UserID;
                }
                entity = new ReflectHelper<DAL.Om_OrgInfo>().AssignEntity(entity, pnlControl);
                entity.NavGroup = navGroup;
                entity.ID = keyID;
                entity.AreaID = areaID;
                entity.AreaName = GetAreaName(areaID);
                entity.AreaPath = new OmArea_BF().GetAreaPath(areaID);

                entity.Save();

                BizCacheHelper.OrgInfoCache.Remove(Om_OrgInfo_Key); //机构信息缓存
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存导航组时发生错误");
            }
            return isSuccess;
        }
        /// <summary>
        /// 保存基本信息
        /// </summary>
        public bool SaveInfo(string keyID, string logo)
        {
            bool isSuccess = true;
            try
            {
                //var entity = Get(keyID);
                //entity.logo = logo;
                //entity.Save();

                var sql = string.Format("update Om_OrgInfo set logo='{0}' where id='{1}'", logo, keyID);
                new SubSonic.Query.CodingHorror(sql).Execute();

                BizCacheHelper.OrgInfoCache.Remove(Om_OrgInfo_Key); //机构信息缓存
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存导航组时发生错误");
            }

            return isSuccess;
        }
        /// <summary>
        /// 删除Logo
        /// </summary>
        /// <param name="keyID"></param>
        public bool DeleteLogo(string keyID)
        {
            bool isSuccess = true;
            try
            {
                //var entity = Get(keyID);
                //entity.logo = logo;
                //entity.Save();

                var sql = string.Format("update Om_OrgInfo set logo='' where id='{0}'", keyID);
                new SubSonic.Query.CodingHorror(sql).Execute();

                BizCacheHelper.OrgInfoCache.Remove(Om_OrgInfo_Key); //机构信息缓存
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存导航组时发生错误");
            }

            return isSuccess;
        }

        /// <summary>
        /// 区域名称
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        private string GetAreaName(string areaID)
        {
            var e = new OmArea_BF().Get(areaID);
            return e == null ? "" : e.AreaName;
        }

        /// <summary>
        /// 更新机构二维码
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="qrcodePath"></param>
        /// <returns></returns>
        public bool UpdateQRCode(string orgID, string qrcodePath)
        {
            try
            {
                var e = Get(orgID);
                e.QRCode = qrcodePath;
                e.Save();

                BizCacheHelper.OrgInfoCache.Remove(Om_OrgInfo_Key); //机构信息缓存
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "创建机构二维码时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 删除导航组
        /// </summary>
        /// <param name="keyID"></param>
        public void Delete(string keyID)
        {
            Om_OrgInfo.Delete(x => x.ID == keyID);
            BizCacheHelper.OrgInfoCache.Remove(Om_OrgInfo_Key);
        }

        /// <summary>
        /// 保存子系统的管理员
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="pnlControl"></param>
        /// <returns></returns>
        public bool SaveOrgAdminUser(string orgID, WebControl pnlControl)
        {
            var user = AuthenticationPage.UserInfo;
            bool isSuccess = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var entity = new OmUser_BF().Get(orgID);
                    var org = Get(orgID);//机构实体
                    if (entity == null)
                    {
                        entity = new DAL.Om_UserInfo();
                        entity.OrgID = orgID;
                        entity.CreateName = AuthenticationPage.UserInfo.UserName;
                        entity.CreateDate = DateTime.Now;
                    }

                    entity = new ReflectHelper<DAL.Om_UserInfo>().AssignEntity(entity, pnlControl);
                    entity.UserPwd = Security.EncrypByRijndael(entity.UserPwd);
                    entity.OrgName = org.Name;
                    entity.ID = orgID;
                    entity.Save();
                    //管理员姓名更新至机构主表
                    org.OrgAdmin = entity.UserName;
                    org.ID = orgID;
                    org.Save();

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存机构管理员时发生错误");
            }
            return isSuccess;
        }

        /// <summary>
        /// 是否注册过域名
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool ExistDomain(string domain)
        {
            return DAL.Om_OrgInfo.Exists(x => x.ProDomain == domain);
        }

        #region 收款明细

        /// <summary>
        /// 机构收款明细
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public List<DAL.Om_OrgReceipt> GetReceiptItem(Guid orgID)
        {
            return DAL.Om_OrgReceipt.Find(x => x.OrgID == orgID).OrderByDescending(x => x.ReceiveDate).ToList();
        }

        /// <summary>
        /// 收款明细实体
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Om_OrgReceipt GetReceipt(Guid keyID)
        {
            return DAL.Om_OrgReceipt.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 机构累计付款总金额
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public decimal ReceiptTotalAmt(Guid orgID)
        {
            return db.Sum<DAL.Om_OrgReceipt>(x => x.PaidAmt).Where<DAL.Om_OrgReceipt>(x => x.OrgID == orgID).ExecuteScalar<decimal>();
        }

        /// <summary>
        /// 保存收款
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="orgID"></param>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public bool SaveReceipt(Guid keyID, string orgID, decimal payAmt, int userCount, DateScope grantDate, string receiver, DateTime receiveDate, string comment)
        {
            var isOk = true;
            var user = AuthenticationPage.UserInfo;
            try
            {
                #region 收款明细表
                var e = GetReceipt(keyID);
                if (e == null)
                {
                    e = new DAL.Om_OrgReceipt();
                    e.OrgID = Guid.Parse(orgID);
                }
                e.ID = keyID;
                e.PaidAmt = payAmt;
                e.UserCount = userCount;
                e.sDate = DateTime.Parse(grantDate.sDate);
                e.eDate = DateTime.Parse(grantDate.eDate);
                e.ReceiveDate = receiveDate;
                e.Receiver = receiver;
                e.Comment = comment;
                e.Save();
                #endregion

                #region 更新机构主表

                var org = Get(orgID);
                org.ReceiptAmt = ReceiptTotalAmt(Guid.Parse(orgID));
                org.OpenDate = DateTime.Parse(grantDate.sDate);
                org.ExpiryDate = DateTime.Parse(grantDate.eDate);
                org.MaxUserCount = userCount;
                org.ID = orgID;
                org.Save();

                #endregion
            }
            catch (Exception ex)
            {
                isOk = false;
                BizUtility.ExceptionHandler(user, ex, "机构收款时发生错误");
            }
            return isOk;
        }

        /// <summary>
        /// 删除收款明细
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public bool DeleteReceipt(Guid keyID)
        {
            var isOk = true;
            var user = AuthenticationPage.UserInfo;
            try
            {
                var e = GetReceipt(keyID);
                var amt = e.PaidAmt;
                e.Delete();

                var org = Get(e.OrgID.ToString());
                org.ReceiptAmt -= amt;
                org.ID = e.OrgID.ToString();
                org.Save();
            }
            catch (Exception ex)
            {
                isOk = false;
                BizUtility.ExceptionHandler(user, ex, "机构机构收款明细时发生错误");
            }
            return isOk;
        }

        #endregion

        #region << 营销微站 >>
        /// <summary>
        /// 获取机构信息
        /// </summary>
        /// <param name="eShopID">开通微站唯一标识ID</param>
        /// <returns></returns>
        public DAL.Om_OrgInfo GetOrgInfoByEShopID(string eShopID)
        {
            var list = DAL.Om_OrgInfo.Find(x => x.eShopGuid == eShopID && x.IsOpenEShop == true);
            return list.Count > 0 ? list.First() : null;
        }
        #endregion

        #region 通过接口自助注册
        /// <summary>
        /// 机构自助注册
        /// </summary>
        /// <param name="strData"></param>
        /// <returns>注册成功与否</returns>
        /// <data> 
        public bool RegOrgInfoBySelf(string strData)
        {
            var doc = new XmlDocument();
            doc.LoadXml(strData.ToLower());
            var node = doc.SelectSingleNode("document/data");
            if (node == null) return false;
            var domain = node.GetNodeValue("prodomain");
            if (ExistDomain(domain)) return false;
            var name = node.GetNodeValue("name");
            var proname = node.GetNodeValue("proname");
            if (string.IsNullOrEmpty(proname))
                proname = "旅行社综合业务管理系统";
            var areaID = node.GetNodeValue("areaid");
            var bizType = node.GetNodeValue("orgprop");
            var contact = node.GetNodeValue("contact");
            var mobile = node.GetNodeValue("mobile");

            bool isSuccess = true;
            try
            {
                #region 机构主表

                var entity = new DAL.Om_OrgInfo();
                entity.ID = Guid.NewGuid().ToString();
                entity.CreateUserName = "system";
                entity.CreateDate = DateTime.Now;
                entity.CreateUserID = Guid.Empty.ToString();
                entity.SmsCount = 0;
                entity.SendSmsCount = 0;
                entity.IsOpenEShop = false;
                entity.QRCode = "";
                entity.Name = name;
                entity.ProDomain = domain;
                entity.ProName = proname;
                entity.DataStatus = -1;
                entity.AreaID = areaID;
                entity.AreaName = GetAreaName(areaID);
                entity.AreaPath = new OmArea_BF().GetAreaPath(areaID);
                entity.OpenDate = DateTime.Now;
                entity.ExpiryDate = DateTime.Now.AddMonths(1);//试用期为1个月
                entity.OrgProp = bizType;
                entity.OrgContact = contact;
                entity.ContactPhone = mobile;
                entity.OrgAdmin = "";
                entity.AreaID = areaID;
                entity.AreaName = GetAreaName(areaID);
                entity.AreaPath = new OmArea_BF().GetAreaPath(areaID);
                entity.NavGroup = "简易版";
                entity.NavGroupID = "08154c06-7426-4168-ad4b-d475e054fa32";
                entity.Save();

                #endregion
            }
            catch
            {
                isSuccess = false;
            }
            finally
            {
                if (isSuccess) //短信通知客服
                {
                    #region 短信

                    var wMobile = ConfigHelper.GetAppSettingValue("ServiceMobile");
                    if (!string.IsNullOrEmpty(wMobile))
                    {
                        var content = string.Format("{0}于{1}注册旅管家系统，请及时联系，联系人：{2}，联系电话：{3}",
                            name, DateTime.Now.ToString("yyyy-MM-dd"), contact, mobile);
                        wMobile = wMobile.Replace("，", ",").Replace(" ", ",").Replace("|", ",");

                        var e = new MessageEntity();
                        e.SendUserID = Guid.Empty.ToString();
                        e.SendUserName = "system";
                        e.MsgContent = content;
                        e.OrgID = ConfigHelper.GetAppSettingValue("OmOrgID");
                        e.RecMobile = wMobile;
                        e.IsTemplateSms = false;
                        new NSmsBiz().SendSms(e);
                    }

                    #endregion
                }
            }
            return isSuccess;
        }
        #endregion

        public DataTable QueryOrder_All(QueryCriteriaBase qry)
        {
            var strWhere = "select * from Om_OrgInfo where 1=1";
            if (!string.IsNullOrEmpty(qry.Keyword))
                strWhere += string.Format(" and (Name like '%{0}%' or AreaPath like '%{0}%')", qry.Keyword);
            strWhere += " order by CreateDate desc";
            return new SubSonic.Query.CodingHorror(strWhere).ExecuteDataSet().Tables[0];
        }
    }
}
