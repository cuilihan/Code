using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.DataSync
{
    public class OctConfig
    {
        /// <summary>
        /// 接口账号
        /// </summary>
        private static string _OctAcct { get; set; }

        /// <summary>
        /// 接口密码
        /// </summary>
        private static string _OctPwd { get; set; }


        /// <summary>
        /// 接口AppId
        /// </summary>
        private static string _OctAppId { get; set; }


        /// <summary>
        /// 接口Appkey
        /// </summary>
        private static string _OctAppKey { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        private static string _OctServiceUrl { get; set; }

        private static DAL.OTA_Setting OCTOTA()
        {
            return DAL.OTA_Setting.SingleOrDefault(x => x.OTA == "Octopus");
        }

        /// <summary>
        /// 接口账号
        /// </summary>
        public string OctAcct
        {
            get
            {
                if (string.IsNullOrEmpty(_OctAcct))
                {
                    _OctAcct = OCTOTA().AcctID;
                }
                return _OctAcct;
            }
        }


        /// <summary>
        /// 接口密码
        /// </summary>
        public string OctPwd
        {
            get
            {
                if (string.IsNullOrEmpty(_OctPwd))
                {
                    _OctPwd = OCTOTA().AcctPwd;
                }
                return _OctPwd;
            }
        }

        /// <summary>
        /// 接口AppID
        /// </summary>
        public string OctAppId
        {
            get
            {
                if (string.IsNullOrEmpty(_OctAppId))
                {
                    _OctAppId = OCTOTA().AppId;
                }
                return _OctAppId;
            }
        }

        /// <summary>
        /// 接口OctAppKey
        /// </summary>
        public string OctAppKey
        {
            get
            {
                if (string.IsNullOrEmpty(_OctAppKey))
                {
                    _OctAppKey = OCTOTA().AppKey;
                }
                return _OctAppKey;
            }
        }

        /// <summary>
        /// 接口OctAppKey
        /// </summary>
        public string OctServiceUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_OctServiceUrl))
                {
                    _OctServiceUrl = OCTOTA().OTAServiceUrl;
                }
                return _OctServiceUrl;
            }
        }
    }
}
