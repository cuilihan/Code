using DRP.BF.OmMrg;
using DRP.BF.TmsApi;
using DRP.Framework.Core;
using DRP.Message.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using DRP.Framework;
using DRP.BF.GloMrg;
using DRP.BF.Core; 

namespace DRP.Api.cgi_bin
{
    /// <summary>
    /// TMS对外开发的数据服务接口
    /// </summary>
    [WebService(Namespace = "http://tms.360ly.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class TmsOpenData : TmsServiceBase
    {
        /// <summary>
        /// 单项读取数据
        /// </summary>
        /// <param name="taskGuid"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetData(string taskGuid, string xmlData)
        {
            var isValid = false;
            var strVal = base.AccessAuthority(taskGuid, xmlData, out isValid);
            if (!isValid)
                return strVal;
            else 
                return ServiceFactory.GetInstance(taskGuid).GetData(taskGuid, strVal, xmlData);
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="taskGuid"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        [WebMethod]
        public string SetData(string taskGuid, string xmlData)
        {
            var isValid = false;
            var strVal = base.AccessAuthority(taskGuid, xmlData, out isValid);
            if (!isValid)
                return strVal;
            else
                return ServiceFactory.GetInstance(taskGuid).SetData(taskGuid, strVal, xmlData); 
        }

        /// <summary>
        /// 数据交换，适用于综合查询
        /// </summary>
        /// <param name="taskGuid"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        [WebMethod]
        public string TransferData(string taskGuid, string xmlData)
        {
            var isValid = false;
            var strVal = base.AccessAuthority(taskGuid, xmlData, out isValid);
            if (!isValid)
                return strVal;
            else
                return ServiceFactory.GetInstance(taskGuid).TransferData(taskGuid, strVal, xmlData);
        } 
    }
}
