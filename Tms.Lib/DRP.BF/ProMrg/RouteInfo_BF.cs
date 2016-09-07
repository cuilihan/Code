using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using DRP.DAL;
using Novacode;
using System.IO;
using DRP.Framework.Core;
using System.Web;

namespace DRP.BF.ProMrg
{
    /// <summary>
    /// 线路查询条件
    /// </summary>
    public class RouteCriteria : QueryCriteriaBase
    {
        /// <summary>
        /// 线路类型
        /// </summary>
        public string RouteType { get; set; }

        /// <summary>
        /// 编号编号
        /// </summary>
        public string RouteNo { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
    }

    /// <summary>
    /// 线路管理
    /// </summary>
    public class RouteInfo_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 组合客户查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string RouteQueryCondition(RouteCriteria qry)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);
            #region 用户权限
            switch (user.UserDataPermission)
            {
                case SysMrg.DataPermission.Dept:
                    sb.AppendFormat(" and DeptID='{0}'", user.DeptID);
                    break;
                case SysMrg.DataPermission.Private:
                    sb.AppendFormat(" and CreateUserID='{0}'", user.UserID);
                    break;
            }
            #endregion
            if (!string.IsNullOrEmpty(qry.RouteType))
                sb.AppendFormat(" and RouteTypeID= '{0}'", qry.RouteType);
            if (!string.IsNullOrEmpty(qry.RouteNo))
                sb.AppendFormat(" and RouteNo like '%{0}%'", qry.RouteNo);
            if (!string.IsNullOrEmpty(qry.RouteName))
                sb.AppendFormat(" and RouteName like '%{0}%'", qry.RouteName);
            return sb.ToString();
        }

        /// <summary>
        /// 线路列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryData(RouteCriteria qry, out int record)
        {
            return db.GetPagination("V_RouteInfo", qry.pageIndex, qry.pageSize, out record, RouteQueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 线路详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Pro_RouteInfo Get(string keyID)
        {
            return DAL.Pro_RouteInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存线路
        /// </summary> 
        public bool Save(DAL.Pro_RouteInfo entity, List<DAL.Pro_RouteSchedule> list)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region << 线路主表 >>
                    var e = Get(entity.ID);
                    if (e == null)
                    {
                        e = new DAL.Pro_RouteInfo();
                        e.CreateDate = DateTime.Now;
                        e.CreateUserID = user.UserID;
                        e.CreateUserName = user.UserName;
                        e.OrgID = user.OrgID;
                        e.DeptID = user.DeptID;
                        e.UpdateDate = DateTime.Now;
                        e.UpdateUserID = user.UserID;
                        e.UpdateUserName = user.UserName;
                        e.OrderIndex = 0;
                    }
                    else
                    {
                        e.UpdateDate = DateTime.Now;
                        e.UpdateUserID = user.UserID;
                        e.UpdateUserName = user.UserName;
                    }
                    e.ID = entity.ID;
                    e.RouteName = entity.RouteName;
                    e.RouteNo = entity.RouteNo;
                    e.ScheduleDays = entity.ScheduleDays;
                    e.RouteType = entity.RouteType;
                    e.RouteTypeID = entity.RouteTypeID;
                    e.Destination = entity.Destination;
                    e.DestinationID = entity.DestinationID;
                    e.DestinationPath = entity.DestinationPath;
                    e.Feature = entity.Feature;
                    e.PriceInclude = entity.PriceInclude;
                    e.PriceNonIncude = entity.PriceNonIncude;
                    e.SelfItem = entity.SelfItem;
                    e.Remind = entity.Remind;
                    e.Shopping = entity.Shopping;
                    e.Comment = entity.Comment;
                    e.RouteSource = entity.RouteSource;
                    e.RouteSourceID =entity.RouteSourceID;

                    #endregion

                    #region << 线路行程 >>
                    DAL.Pro_RouteSchedule.Delete(x => x.RouteID == e.ID);
                    list.ForEach(x =>
                    {
                        var t = new DAL.Pro_RouteSchedule();
                        t.ID = x.ID;
                        t.RouteID = e.ID;
                        t.DayNum = x.DayNum;
                        t.Title = x.Title;
                        t.Schedule = x.Schedule;
                        t.Dinner = x.Dinner;
                        t.Stay = x.Stay;
                        t.CreateDate = DateTime.Now;
                        t.CreateUserID = user.UserID;
                        t.CreateUserName = user.UserName;
                        t.Traffic = x.Traffic;

                        t.Save();
                    });
                    #endregion

                    e.Save();

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存线路时发生错误");
                return false;
            }
            finally
            {
                DeleteRouteFile(entity.ID);
            }

            return true;
        }

        /// <summary>
        /// 删除线路
        /// </summary>
        /// <param name="routeID"></param>
        /// <returns></returns>
        public bool Delete(string routeID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DAL.Pro_RouteSchedule.Delete(x => x.RouteID == routeID);
                    DAL.Pro_RouteInfo.Delete(x => x.ID == routeID);
                    scope.Complete();

                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "保存线路时发生错误");
                return false;
            }
        }

        #region << 线路行程 >>
        /// <summary>
        /// 线路行程列表
        /// </summary>
        /// <param name="routeID"></param>
        /// <returns></returns>
        public List<DAL.Pro_RouteSchedule> GetRouteSchedule(string routeID)
        {
            return DAL.Pro_RouteSchedule.Find(x => x.RouteID == routeID).OrderBy(x => x.DayNum).ToList();
        }
        #endregion

        #region << 行程单导出Word >>

        /// <summary>
        /// 线路行程单导出Word
        /// </summary>
        /// <param name="vPath"></param>
        /// <param name="routeID"></param>
        public void ToWord(string vPath, string routeID)
        {
            var route = Get(routeID);
            var fileName = vPath + route.ID + ".docx";
            var downFileName = route.RouteName + "行程单.docx";
            if (!File.Exists(fileName))
                CreateWord(fileName, route);
            DownFile.DownLoad(downFileName, fileName);
        }

        /// <summary>
        /// 行程单导出Word
        /// </summary>
        /// <param name="vPath"></param>
        /// <param name="routeID"></param>
        public void CreateWord(string fileName, DAL.Pro_RouteInfo route)
        {
            using (DocX doc = DocX.Create(fileName))
            {
                var p = doc.InsertParagraph();
                p.Append("【" + route.Destination + "】" + route.RouteName + "行程单").Bold().FontSize(16).Alignment = Alignment.center;
                p = doc.InsertParagraph();
                SetText(p, "特别提醒", route.Remind);
                SetText(p, "行程特色", route.Feature);

                #region << 行程 >>
                p = doc.InsertParagraph("行程安排：").Bold();
                var list = new RouteInfo_BF().GetRouteSchedule(route.ID);
                var dt = doc.InsertTable(list.Count, 1);
                dt.Design = TableDesign.TableNormal;
                dt.AutoFit = AutoFit.Window;
                for (var i = 0; i < list.Count; i++)
                {
                    var e = list[i];
                    var cell = dt.Rows[i].Cells[0];
                    Border bor = new Border();
                    bor.Tcbs = Novacode.BorderStyle.Tcbs_single;

                    //填充表格颜色及绘制边框
                    cell.Paragraphs[0].Append(string.Format("第{0}天：", (i + 1)) + e.Title).Bold();
                    cell.SetBorder(TableCellBorderType.Left, bor);
                    cell.SetBorder(TableCellBorderType.Right, bor);
                    cell.SetBorder(TableCellBorderType.Top, bor);
                    cell.SetBorder(TableCellBorderType.Bottom, bor);
                    cell.Paragraphs[0].Append(System.Environment.NewLine);
                    cell.Paragraphs[0].Append(e.Schedule);
                    cell.Paragraphs[0].Append(System.Environment.NewLine);
                    if (!string.IsNullOrEmpty(e.Stay))
                    {
                        cell.Paragraphs[0].Append("住宿:").Bold();
                        cell.Paragraphs[0].Append(e.Stay);
                    }
                    if (!string.IsNullOrEmpty(e.Dinner))
                    {
                        cell.Paragraphs[0].Append(System.Environment.NewLine);
                        cell.Paragraphs[0].Append("用餐:").Bold();
                        cell.Paragraphs[0].Append(e.Dinner);
                    }
                }
                #endregion

                p = doc.InsertParagraph();
                p.Append(System.Environment.NewLine);
                p.Append(System.Environment.NewLine);
                SetText(p, "费用包含", route.PriceInclude);
                SetText(p, "费用不含", route.PriceNonIncude);
                SetText(p, "自费项目", route.SelfItem);
                SetText(p, "购物说明", route.Shopping);
                SetText(p, "备注", route.Comment);
                doc.Save();
            }
        }

        private void SetText(Paragraph p, string subject, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                p.Append(subject + "：").Bold();
                p.Append(System.Environment.NewLine);
                var s = HtmlHeler.NoHTML(text);
                p.Append(s);
                p.Append(System.Environment.NewLine);
                p.Append(System.Environment.NewLine);
            }
        }

        /// <summary>
        /// 删除行程单文件
        /// </summary>
        /// <param name="routeID"></param>
        private void DeleteRouteFile(string routeID)
        {
            try
            {
                var fileName = HttpContext.Current.Server.MapPath("/Module/Prol/RouteFile/") + routeID + ".docx";
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
            catch { }
        }
        #endregion
    }
}
