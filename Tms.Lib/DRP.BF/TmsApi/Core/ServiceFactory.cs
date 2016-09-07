using DRP.BF.TmsApi;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DRP.BF.Core
{
    /// <summary>
    /// 服务实例管理
    /// </summary>
    public class ServiceFactory
    {
        private ServiceFactory()
        { }

        private static Dictionary<string, ITmsServiceProvider> m_ServiceDict = new Dictionary<string, ITmsServiceProvider>();

        /// <summary>
        /// 接口服务配置集合
        /// </summary>
        /// <param name="navGroupID"></param>
        /// <returns></returns>
        private List<TaskEntity> GetTaskCollection()
        {
            var list = new List<TaskEntity>();
            DRPApiConfigurationSection serviceSection = (DRPApiConfigurationSection)ConfigurationManager.GetSection("DRPApiServices");
            if (serviceSection != null && serviceSection.Services != null)
            {
                foreach (DRPApiConfigurationElement ele in serviceSection.Services)
                {
                    var e = new TaskEntity();
                    e.TaskName = ele.Name;
                    e.TaskGuid = ele.TaskGuide;
                    e.Enable = ele.Enabled;
                    e.Type = ele.Type;
                    list.Add(e);
                }
            }
            return list;
        }

        /// <summary>
        /// 加载服务
        /// </summary>
        private void LoadService()
        {
            var list = GetTaskCollection();
            list.ForEach(x =>
            {
                if (x.Enable)
                {
                    Type type = Type.GetType(x.Type);
                    if (type != null)
                    {
                        var obj = (ITmsServiceProvider)Activator.CreateInstance(type);
                        if (obj != null && !m_ServiceDict.ContainsKey(x.TaskGuid))
                            m_ServiceDict.Add(x.TaskGuid, obj);
                    }
                }
            });
        }

        /// <summary>
        /// 实现接口服务实例
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static ITmsServiceProvider GetInstance(string taskGuid)
        {
            if (!m_ServiceDict.ContainsKey(taskGuid))
                new ServiceFactory().LoadService();
            return m_ServiceDict[taskGuid];
        }
    }
}