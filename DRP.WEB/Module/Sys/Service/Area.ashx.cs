using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.Framework;
using DRP.BF.OTA;

namespace DRP.WEB.Module.Sys.Service
{
    /// <summary>
    /// Area 的摘要说明
    /// </summary>
    public class Area : IHttpHandler
    {
        Area_BF dal = new Area_BF();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 3:
                    context.Response.Write(AreaTreeJson(context));
                    break;
                case 4://接口获取
                    context.Response.Write(OTAAreaTreeJson());
                    break;
                case 5://接口获取数据初始化
                    OTAAreaInit(context);
                    break;
                case 6://绑定目的地
                    OTAAreaSave(context);
                    break;
                case 7://取消绑定目的地
                    OTAAreaDelete(context);
                    break;
            }
        }

        #region 接口的目的地的同步 和绑定


        public void OTAAreaSave(HttpContext context)
        {
            var aid = context.Request["aid"];
            var otdid = context.Request["otdid"];
            var otdaid = context.Request["otdaid"];
            var name = context.Request["name"];
            var otaName = context.Request["otaName"];

            var isok = dal.OTAAreaSave(aid, otdid, otdaid, name, otaName);

            context.Response.Write(isok ? "1" : "0");
        }


        public void OTAAreaDelete(HttpContext context)
        {


            var isok = dal.OTAAreaDelete(Guid.Parse(context.Request["relId"]));

            context.Response.Write(isok ? "1" : "0");
        }

        /// <summary>
        /// OCT目的地初始化同步
        /// </summary>
        public void OTAAreaInit(HttpContext context)
        {

            var isok = dal.InitAreaData(Guid.Parse(context.Request["OTAID"]));

            context.Response.Write(isok ? "1" : "0");
        }




        /// <summary>
        /// OCT接口获取拼装json
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string OTAAreaTreeJson()
        {
            var sb = new List<string>();
            var sID = Guid.NewGuid();
            var lID = Guid.NewGuid();
            var zID = Guid.NewGuid();
            var list = new DRP.BF.DataSync.OctHelper().QueryPackageDestinationList();
            sb.Add("{\"id\":\"" + sID + "\", \"name\":\"" + "周边短线" + "\",\"pId\":\"" + Guid.Empty + "\",\"open\":\"false\"}");
            sb.Add("{\"id\":\"" + lID + "\", \"name\":\"" + "出境旅游" + "\",\"pId\":\"" + Guid.Empty + "\",\"open\":\"false\"}");
            sb.Add("{\"id\":\"" + zID + "\", \"name\":\"" + "国内长线" + "\",\"pId\":\"" + Guid.Empty + "\",\"open\":\"false\"}");
            foreach (var e in list)
            {
                var row = "";
                if (e.PAreaID == Guid.Empty && e.DesType == 1)
                {
                    row = "{\"id\":\"" + e.AreaID + "\", \"name\":\"" + e.AreaName + "\",\"pId\":\"" + sID + "\",\"open\":\"false\"}";
                }
                else if (e.PAreaID == Guid.Empty && e.DesType == 2)
                {
                    row = "{\"id\":\"" + e.AreaID + "\", \"name\":\"" + e.AreaName + "\",\"pId\":\"" + lID + "\",\"open\":\"false\"}";
                }
                else if (e.PAreaID == Guid.Empty && e.DesType == 3)
                {
                    row = "{\"id\":\"" + e.AreaID + "\", \"name\":\"" + e.AreaName + "\",\"pId\":\"" + zID + "\",\"open\":\"false\"}";
                }
                else
                {
                    row = "{\"id\":\"" + e.AreaID + "\", \"name\":\"" + e.AreaName + "\",\"pId\":\"" + e.PAreaID + "\",\"open\":\"false\"}";
                }
                sb.Add(row);
            }
            return "[" + string.Join(",", sb) + "]";
        }


        #endregion

        /// <summary>
        /// 构建AreaTree
        /// </summary>
        /// <returns></returns>
        public string AreaTreeJson(HttpContext context)
        {
            var list = dal.GetOTABindArea(Guid.Parse(context.Request["OTAID"]));
            var s = CreateNode(list, Guid.Empty);
            return "[" + s + "]";
        }

        private string CreateNode(List<OTAArea> list, Guid pid)
        {
            var arr = new List<string>();
            var coll = list.FindAll(x => x.ParentID == pid.ToString());
            foreach (var e in coll)
            {
                if (list.Exists(x => x.ParentID == e.ID)) //子节点
                {
                    var str = "\"ID\":\"{0}\",\"PID\":\"{1}\",\"Name\":\"{2}\",\"OTAareaName\":\"{3}\",\"OTAareaID\":\"{4}\",\"OTAName\":\"{5}\",\"children\":[{6}]";
                    var node = CreateNode(list, Guid.Parse(e.ID));
                    str = string.Format(str, e.ID, e.ParentID, e.Name, e.OTAareaName, e.OTAareaID, e.OTAName, node);
                    str = "{" + str + "}";
                    arr.Add(str);
                }
                else
                {
                    var s = "\"ID\":\"{0}\",\"PID\":\"{1}\",\"Name\":\"{2}\",\"OTAareaName\":\"{3}\",\"OTAareaID\":\"{4}\",\"OTAName\":\"{5}\"";
                    arr.Add("{" + string.Format(s, e.ID, e.ParentID, e.Name, e.OTAareaName, e.OTAareaID, e.OTAName) + "}");
                }
            }
            return string.Join(",", arr);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}