using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DRP.BF.Glo;
using DRP.BF.Order;
using DRP.BF.PageBase;
using DRP.Framework;
using DRP.BF.SysMrg;

namespace DRP.WEB.Module.Order.Service
{
    /// <summary>
    /// 订单通用方法
    /// </summary>
    public class OrderUtiltity : HandlerBase
    {
        protected override void DRPProcessRequest(HttpContext context)
        {
            var action = context.Request["action"].ToInt();
            switch (action)
            {
                case 1://线路类型
                    BindRouteType(context);
                    break;
                case 2: //按线路类型查询目的地(combotree)
                    BindDestination(context);
                    break;
                case 3://删除订单成本项目（Ord_OrderCostItem)
                    DeleteOrderCostItem(context);
                    break;
                case 4://部门
                    BindDept(context);
                    break;
                case 5://创建部门人员
                    BindEmployee(context);
                    break;
            }
        }

        /// <summary>
        /// 线路类型
        /// </summary>
        /// <param name="context"></param>
        private void BindRouteType(HttpContext context)
        {
            var list = new BasicInfo_BF().GetBasicInfo(BasicType.Pro_RouteType);
            var coll = new List<string>();
            list.ForEach(x =>
            {
                var json = string.Format("\"id\":\"{0}\",\"text\":\"{1}\"", x.ID, x.Name);
                coll.Add("{" + json + "}");
            });
            var str = "[" + string.Join(",", coll) + "]";
            context.Response.Write(str);
        }


        /// <summary>
        /// 目的地
        /// </summary>
        /// <param name="context"></param>
        private void BindDestination(HttpContext context)
        {
            var routeTypeID = context.Request["routeTypeID"];
            var json = new Destination_BF().GetDestinationComboTree(routeTypeID);
            context.Response.Write(json);
        }

        /// <summary>
        /// 删除订单成本
        /// </summary>
        /// <param name="context"></param>
        private void DeleteOrderCostItem(HttpContext context)
        {
            var itemID = context.Request["costItemID"];
            new OrderUtility().DeleteOrderCostItem(itemID);
            context.Response.Write(1);
        }

        /// <summary>
        /// 允许匿名访问
        /// </summary>
        protected override string NavigateID
        {
            get
            {
                return "anonymous";
            }
        }

        /// <summary>
        /// 创建部门树
        /// </summary>
        /// <param name="context"></param>
        #region 创建部门树
        private void BindDept(HttpContext context) 
        {
            var list = new Dept_BF().GetDepartment();
            var s = CreateTreeNode(list, Guid.Empty.ToString());
            context.Response.Write("[" + s + "]");
        }

        private string CreateTreeNode(List<DAL.Sys_Department> list, string pId)
        {
            var arr = new List<string>();
            var coll = list.FindAll(x => x.ParentID == pId);
            var T = "\"id\":\"{0}\",\"parentID\":\"{1}\",\"text\":\"{2}\",\"DataStatus\":\"{3}\"";
            foreach (var e in coll)
            {
                if (list.Exists(x => x.ParentID == e.ID)) //子节点
                {
                    var str = T + ",\"children\":[{4}]";
                    var node = CreateTreeNode(list, e.ID);
                    str = string.Format(str, e.ID, e.ParentID, e.Name, e.DataStatus, node);
                    str = "{" + str + "}";
                    arr.Add(str);
                }
                else
                {
                    arr.Add("{" + string.Format(T, e.ID, e.ParentID, e.Name, e.DataStatus) + "}");
                }
            }
            return string.Join(",", arr);
        }
        #endregion
        /// <summary>
        /// 创建部门人员
        /// </summary>
        /// <param name="context"></param>
        private void BindEmployee(HttpContext context)
        {
            var DeptID = context.Request["DeptID"];
            var list = new User_BF().GetSysUser(DeptID);
            var coll = new List<string>();
            list.ForEach(x =>
            {
                var json = string.Format("\"id\":\"{0}\",\"text\":\"{1}\"", x.ID, x.Name);
                coll.Add("{" + json + "}");
            });
            var str = "[" + string.Join(",", coll) + "]";
            context.Response.Write(str);
        }
    }
}