using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRP.BF.Core
{
    /// <summary>
    /// 接口服务数据接口
    /// </summary>
    public interface ITmsServiceProvider
    {
        string GetData(string taskGuid,string dataType, string xmlData);

        string SetData(string taskGuid, string dataType, string xmlData);

        string TransferData(string taskGuid, string dataType, string xmlData); 
    }

}