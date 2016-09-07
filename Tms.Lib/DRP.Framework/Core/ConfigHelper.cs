using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DRP.Framework.Core
{
    public class ConfigHelper
    {
        /// <summary>
        /// 获取WebConfig中AppSetting的配置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
