using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Cached;
using DRP.DAL;

namespace DRP.BF.Cache
{
    /// <summary>
    /// 业务层缓存
    /// </summary>
    public class BizCacheHelper
    {
        #region << 机构信息 >>

        private static DRPLocalCache<List<Om_OrgInfo>> _OrgInfoCache;
        /// <summary>
        /// 机构信息集合
        /// 创建人：李金友
        /// 日期：2014-10-21
        /// </summary>
        public static DRPLocalCache<List<Om_OrgInfo>> OrgInfoCache
        {
            get
            {
                if (_OrgInfoCache == null)
                {
                    _OrgInfoCache = new DRPLocalCache<List<Om_OrgInfo>>();
                }
                return _OrgInfoCache;
            }
        }

        #endregion

        #region << 运维推送工具 >>

        private static DRPLocalCache<List<Om_Tool>> _OrgToolsCache;
        /// <summary>
        /// 机构信息集合
        /// 创建人：李金友
        /// 日期：2014-10-21
        /// </summary>
        public static DRPLocalCache<List<Om_Tool>> OrgToolsCache
        {
            get
            {
                if (_OrgToolsCache == null)
                {
                    _OrgToolsCache = new DRPLocalCache<List<Om_Tool>>();
                }
                return _OrgToolsCache;
            }
        }

        #endregion

        #region << 导航组导航菜单 >> 

        private static DRPLocalCache<List<Om_Navigate>> _NavGroupNavigateCache;
        /// <summary>
        /// 按导航组缓存菜单集合
        /// 创建人：李金友
        /// 日期：2014-10-21
        /// </summary>
        public static DRPLocalCache<List<Om_Navigate>> NavGroupNavigateCache
        {
            get
            {
                if (_NavGroupNavigateCache == null)
                {
                    _NavGroupNavigateCache = new DRPLocalCache<List<Om_Navigate>>();
                }
                return _NavGroupNavigateCache;
            }
        }
        #endregion

        #region << 导航组 >>

        private static DRPLocalCache<List<Om_NavGroup>> _NavigateGroupCache;
        /// <summary>
        /// 导航组集合
        /// 创建人：李金友
        /// 日期：2014-10-21
        /// </summary>
        public static DRPLocalCache<List<Om_NavGroup>> NavigateGroupCache
        {
            get
            {
                if (_NavigateGroupCache == null)
                {
                    _NavigateGroupCache = new DRPLocalCache<List<Om_NavGroup>>();
                }
                return _NavigateGroupCache;
            }
        }

        #endregion

        #region << IP地址 >>

        private static DRPLocalCache<List<Sys_IPFilter>> _IpFilterCache;
        /// <summary>
        /// IP地址集合
        /// 创建人：李金友
        /// 日期：2014-10-21
        /// </summary>
        public static DRPLocalCache<List<Sys_IPFilter>> IpFilterCache
        {
            get
            {
                if (_IpFilterCache == null)
                {
                    _IpFilterCache = new DRPLocalCache<List<Sys_IPFilter>>();
                }
                return _IpFilterCache;
            }
        }

        #endregion

        #region << 用户菜单权限【Sys_Permission】 >>

        private static DRPLocalCache<List<Sys_Permission>> _SysPermissionCache;
        /// <summary>
        /// 系统用户菜单权限
        /// 创建人：李金友
        /// 日期：2014-10-21
        /// </summary>
        public static DRPLocalCache<List<Sys_Permission>> SysPermissionCache
        {
            get
            {
                if (_SysPermissionCache == null)
                {
                    _SysPermissionCache = new DRPLocalCache<List<Sys_Permission>>();
                }
                return _SysPermissionCache;
            }
        }

        #endregion

        #region << 目的地【Glo_Destination】 >>

        private static DRPLocalCache<List<Glo_Destination>> _GloDestinationCache;
        /// <summary>
        /// 机构出发地集合
        /// 创建人：李金友
        /// 日期：2014-10-26
        /// </summary>
        public static DRPLocalCache<List<Glo_Destination>> GloDestinaionCache
        {
            get
            {
                if (_GloDestinationCache == null)
                {
                    _GloDestinationCache = new DRPLocalCache<List<Glo_Destination>>();
                }
                return _GloDestinationCache;
            }
        }

        #endregion

        #region << 在线QQ客服【Glo_QQ】 >>

        private static DRPLocalCache<List<Glo_QQ>> _Glo_QQCache;
        /// <summary>
        /// 在线QQ客服
        /// 创建人：李金友
        /// 日期：2014-11-27
        /// </summary>
        public static DRPLocalCache<List<Glo_QQ>> GloQQCache
        {
            get
            {
                if (_Glo_QQCache == null)
                {
                    _Glo_QQCache = new DRPLocalCache<List<Glo_QQ>>();
                }
                return _Glo_QQCache;
            }
        }

        #endregion

        #region << 系统更新日志  >>

        private static DRPLocalCache<List<DAL.Glo_UpdateLog>> _Glo_UpdateLog;

        /// <summary>
        /// 在线系统更新日志客服
        /// 创建人：李金友
        /// 日期：2014-12-15
        /// </summary>
        public static DRPLocalCache<List<Glo_UpdateLog>> UpdateLog
        {
            get
            {
                if (_Glo_UpdateLog == null)
                {
                    _Glo_UpdateLog = new DRPLocalCache<List<DAL.Glo_UpdateLog>>();
                }
                return _Glo_UpdateLog;
            }
        }
        #endregion 
      
    }
}
