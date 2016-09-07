using DRP.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Xml;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.BF.DataSync
{
    public class OctHelper
    {

        private OctConfig instance;

        public OctHelper()
        {
            instance = new OctConfig();
        }

        /// <summary>
        /// 获取目的地
        /// </summary>
        /// <param name="type">1：周边 2：长线 3：出境</param>
        /// <returns></returns>
        public List<OctAreaEntity> QueryPackageDestinationList()
        {
            var list = GetPackageDestinationList("1");
            list.AddRange(GetPackageDestinationList("2"));
            list.AddRange(GetPackageDestinationList("3"));
            return list;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public List<OctEntity> QueryPackageBuyerStoreUserList()
        {
            var list = GetPackageBuyerStoreUserList();
            return list;
        }



        #region 执行接口方法


        /// <summary>
        /// 获取目的地（私有方法）
        /// </summary>
        /// <param name="type">1：周边 2：长线 3：出境</param>
        /// <returns></returns>
        private List<OctAreaEntity> GetPackageDestinationList(string type)
        {
            var expires = DateTime.Now;
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(type + expires + instance.OctAppKey
, "md5").ToLower();
            var url = string.Concat(instance.OctServiceUrl, "PackageDestinationList");
            string parameters = string.Format("appid={0}&expires={1}&sign={2}&type={3}", instance.OctAppId, expires, sign, type);
            var strXmlData = new HttpHelper().PostRequest(url, parameters);
            var list = new List<OctAreaEntity>();

            if (!string.IsNullOrEmpty(strXmlData))
            {
                var doc = new XmlDocument();
                doc.LoadXml(strXmlData);
                var nodes = doc.SelectNodes("PackageDestinationListReturn/Destinations/Destination");
                foreach (XmlNode node in nodes)
                {
                    var e = new OctAreaEntity();
                    e.AreaID = Guid.Parse(node.GetNodeValue("Guid"));
                    e.PAreaID = Guid.Parse(node.GetNodeValue("ParentGuid"));
                    e.AreaName = node.GetNodeValue("DesName").Replace("<![CDATA[", "").Replace("]]>", "");
                    e.DesType = node.GetNodeValue("DesType").ToInt();

                    list.Add(e);
                }
            }
            return list;
        }



        /// <summary>
        /// 获取用户（私有方法）
        /// </summary>
        /// <returns></returns>
        private List<OctEntity> GetPackageBuyerStoreUserList()
        {
            var expires = DateTime.Now;
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(instance.OctAcct + instance.OctPwd + expires + instance.OctAppKey
, "md5").ToLower();
            var url = string.Concat(instance.OctServiceUrl, "PackageBuyerStoreUserList");
            string parameters = string.Format("appid={0}&expires={1}&sign={2}&userAccount={3}&passWord={4}", instance.OctAppId, expires, sign, instance.OctAcct, instance.OctPwd);
            var strXmlData = new HttpHelper().PostRequest(url, parameters);
            var list = new List<OctEntity>();

            if (!string.IsNullOrEmpty(strXmlData))
            {
                var doc = new XmlDocument();
                doc.LoadXml(strXmlData);
                var nodes = doc.SelectNodes("PackageBuyerStoreUserListReturn/StoreUsers/StoreUser");
                foreach (XmlNode node in nodes)
                {
                    var e = new OctEntity();
                    e.UserID = Guid.Parse(node.GetNodeValue("Guid"));
                    e.UserAccount = node.GetNodeValue("UserAccount").Replace("<![CDATA[", "").Replace("]]>", "");
                    e.Contact = node.GetNodeValue("Contact").Replace("<![CDATA[", "").Replace("]]>", "");

                    list.Add(e);
                }
            }
            return list;
        }


        #endregion
    }
}
