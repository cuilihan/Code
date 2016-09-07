using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DRP.BF.Cache;
using DRP.BF.OmMrg;
using DRP.BF.SysMrg;
using DRP.Framework.Core;

namespace DRP.BF
{
    /// <summary>
    /// 权限控制
    /// </summary>
    public partial class Permisstion_BF
    {
        #region << 登录页面权限控制 >>

        /// <summary>
        /// 跳转错误页面
        /// </summary>
        /// <param name="msg"></param>
        public static void UrlReturn(string msg = "")
        {
            var url = "/Error.aspx";
            if (!string.IsNullOrEmpty(msg))
                url += "?msg=" + Security.Base64Encrypt(msg);
            var s = "window.parent.window.location.href ='{0}'";
            s = string.Format(s, url);
            JScript.ScriptStartUp(s);
        }

        /// <summary>
        /// 访问权限
        /// </summary>
        /// <param name="navigateID"></param>
        public void AccessAuthority(string navigateID)
        {
            if (navigateID.ToUpper().Equals("WECHAT")) return;

            CheckDomain();//域名验证，防止跨域访问
            if (!HttpContext.Current.Request.IsAuthenticated)
                throw new Exception("用户未登录");
            if (string.IsNullOrEmpty(navigateID))
                throw new Exception("权限拒绝：无访问权限");

           // ClientIPFilter();//IP地址过滤

            #region 非法访问：如直接输入网址等
            //匿名的不限制，有些公用页面只要是登录用户就可以访问的，如通知公告
            if (!navigateID.ToLower().Equals("anonymous"))
            {
                //管理员不限制，原因是管理员需要具有所有的权限才能给其他人分配权限
                if (AuthenticationPage.UserInfo.LoginUserType == UserType.BizUser)
                    PagePermissionFilter(navigateID);//页面权限
            }
            #endregion
        }

        /// <summary>
        /// 域名验证
        /// </summary>
        /// <returns>机构ID</returns>
        public string CheckDomain()
        {
            var hostName = HttpContext.Current.Request.Url.Host;
            var e = OrgInfo_BF.DomainOrgInfo();
            if (e == null) throw new Exception(string.Format("当前网址({0})不存在", hostName));
            if (e.DataStatus == 0) throw new Exception("您的业务系统被禁用，请联系运营公司：" + ConfigHelper.GetAppSettingValue("OrgName"));
            if (e.OpenDate > DateTime.Today) throw new Exception("您的业务系统尚未启用，启用日期：" + e.OpenDate.ToString("yyyy-MM-dd"));
            if (e.ExpiryDate < DateTime.Today) throw new Exception("您的业务系统已失效，请尽快联系运营公司：" + ConfigHelper.GetAppSettingValue("OrgName"));
            return e.ID;
        }

        /// <summary>
        /// 客户端IP地址过滤
        /// </summary>
        private void ClientIPFilter()
        {
            var strEnabled = ConfigHelper.GetAppSettingValue("IsEnabledIPFilter");
            if (string.IsNullOrEmpty(strEnabled)) return;
            var isEnabled = true;
            if (!bool.TryParse(strEnabled, out isEnabled)) return;
            if (!isEnabled) return;

            var ip = IP.GetClientIp();
            if (string.IsNullOrEmpty(ip)) throw new Exception("未能获取到客户端IP地址");
            if (ip.Equals("::1") || ip.Equals("127.0.0.1")) return;
            var ipNum = IP.IPToInt(ip);
            var ipItems = new IPFilter_BF().GetOrgIPItem();
            if (ExistIP(ipNum, ipItems.FindAll(x => x.xType == 0)) && !ExistIP(ipNum, ipItems.FindAll(x => x.xType == 1)))
                throw new Exception("您的访问IP(" + ip + ")已被列入黑名单，请联系管理员");
        }

        /// <summary>
        /// IP地址是否存在IP设置的范围内
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool ExistIP(int ip, List<DAL.Sys_IPFilter> list)
        {
            return list.Exists(x => (string.IsNullOrEmpty(x.StartIP) ? 0 : IP.IPToInt(x.StartIP)) <= ip &&
                ip <= (string.IsNullOrEmpty(x.EndIP) ? int.MaxValue : IP.IPToInt(x.EndIP)));
        }

        /// <summary>
        /// 页面权限
        /// </summary>
        /// <param name="navigateID"></param>
        private void PagePermissionFilter(string navigateID)
        {
            var list = new Navigate_BF().GetLoginUserNavigate();
            if (!list.Exists(x => x.PageID == navigateID))
                throw new Exception("权限拒绝：非法访问");
        }
        #endregion

        #region << 登录功能权限过滤 >>

        private const string SysPermissionKey = "Sys_Permision_Key";

        /// <summary>
        /// 按机构[OrgID]缓存菜单权限
        /// </summary>
        /// <returns></returns>
        internal List<DAL.Sys_Permission> GetSysPermission(string orgID)
        {
            var key = SysPermissionKey + "_" + orgID;
            var list = BizCacheHelper.SysPermissionCache.Get(key);
            if (list == null)
            {
                list = DAL.Sys_Permission.Find(x => x.OrgID == orgID).ToList();
                BizCacheHelper.SysPermissionCache.Insert(key, list);
            }
            return list;
        }

        /// <summary>
        /// 当前登录用户所属的机构[OrgID]缓存菜单权限
        /// </summary>
        /// <returns></returns>
        internal List<DAL.Sys_Permission> GetSysPermission()
        {
            var user = AuthenticationPage.UserInfo;
            return GetSysPermission(user.OrgID);
        }

        /// <summary>
        /// 当前登录用户导航菜单权限过滤
        /// </summary>
        /// <param name="list"></param>
        /// <param name="roleID"></param>
        public List<DAL.Om_Navigate> NavigatePermissionFilter(List<DAL.Om_Navigate> list, string roleID)
        {
            var listNavPermission = GetSysPermission().FindAll(x => x.RoleID == roleID);
            var data = new List<DAL.Om_Navigate>();
            foreach (var e in listNavPermission)
            {
                var entity = list.Find(x => x.ID == e.NavID);
                if (entity != null) data.Add(entity);
            }
            return data;
        }
        #endregion
    }
}
