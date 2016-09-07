using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using DRP.BF.SysMrg;
using DRP.DAL;
using DRP.Framework.Core;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace DRP.BF.Quotation
{
    /// <summary>
    /// 报价单查询条件
    /// </summary>
    public class QuotationCriteria : QueryCriteriaBase
    {

        /// <summary>
        /// 目的地ID
        /// </summary>
        public string DestinationID { get; set; }

        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteNo { get; set; }

        /// <summary>
        /// 线路名称 
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 行程天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 人均最小区间
        /// </summary>
        public int MinAvgAmount { get; set; }

        /// <summary>
        /// 人均最大区间
        /// </summary>
        public int MaxAvgAmount { get; set; }

        /// <summary>
        /// 团队最小人数
        /// </summary>
        public int MinVisitNum { get; set; }

        /// <summary>
        /// 团队最大人数
        /// </summary>
        public int MaxVisitNum { get; set; }

        /// <summary>
        /// 途经景点
        /// </summary>
        public string ViewSpot { get; set; }
    }

    /// <summary>
    /// 报价模板管理
    /// </summary>
    public class Quotation_BF
    {
        DRPDB db = new DRPDB();

        /// <summary>
        /// 组合客户查询条件
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public string RouteQueryCondition(QuotationCriteria qry)
        {
            var sb = new StringBuilder();
            var user = AuthenticationPage.UserInfo;
            sb.AppendFormat("1=1 and OrgID='{0}'", user.OrgID);

            if (!string.IsNullOrEmpty(qry.RouteNo))
                sb.AppendFormat(" and RouteNo like '%{0}%'", qry.RouteNo);
            if (!string.IsNullOrEmpty(qry.RouteName))
                sb.AppendFormat(" and RouteName like '%{0}%'", qry.RouteName);
            if (!string.IsNullOrEmpty(qry.DestinationID))
                sb.AppendFormat(" and DestinationPath like '%{0}%'", qry.DestinationID);
            if (!string.IsNullOrEmpty(qry.ViewSpot))
                sb.AppendFormat(" and ViewSpot like '%{0}%'",qry.ViewSpot);
            if (qry.Days > 0)
                sb.AppendFormat(" and Days={0}", qry.Days);
            else
            {
                if (qry.Days == -1)
                {
                    sb.Append(" and Days>=12");
                }
            }
            if (qry.MinAvgAmount > 0)
                sb.AppendFormat(" and AvgPrice>={0}", qry.MinAvgAmount);
            if (qry.MaxAvgAmount > 0)
                sb.AppendFormat(" and AvgPrice<={0}", qry.MaxAvgAmount);
            if (qry.MinVisitNum > 0)
                sb.AppendFormat(" and VisitorNum>={0}", qry.MinVisitNum);
            if (qry.MaxVisitNum > 0)
                sb.AppendFormat(" and VisitorNum<={0}", qry.MaxVisitNum);
            return sb.ToString();
        }

        /// <summary>
        /// 线路列表查询
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable QueryData(QuotationCriteria qry, out int record)
        {
            return db.GetPagination("V_Quotation", qry.pageIndex, qry.pageSize, out record, RouteQueryCondition(qry), qry.SortExpress);
        }

        /// <summary>
        /// 报价单详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public DAL.Pro_Quotation Get(string keyID)
        {
            return DAL.Pro_Quotation.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 保存报价单
        /// </summary> 
        public bool Save(DAL.Pro_Quotation entity, List<DAL.Pro_RouteSchedule> list, List<DAL.Pro_QuotationCostItem> listItem)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region << 线路主表 >>
                    var e = Get(entity.ID);
                    var isInsert = e == null;
                    if (e == null)
                    {
                        e = new DAL.Pro_Quotation();
                        e.CreateDate = DateTime.Now;
                        e.CreateUserID = user.UserID;
                        e.CreateUserName = user.UserName;
                        e.OrgID = user.OrgID;
                        e.DeptID = user.DeptID;
                    }
                    e.ID = entity.ID;
                    e.RouteName = entity.RouteName;
                    e.RouteNo = entity.RouteNo;
                    e.Days = entity.Days;
                    e.RouteType = entity.RouteType;
                    e.RouteTypeID = entity.RouteTypeID;
                    e.Destination = entity.Destination;
                    e.DestinationID = entity.DestinationID;
                    e.DestinationPath = entity.RouteTypeID + "," + entity.DestinationPath;
                    e.Stay = entity.Stay;
                    e.Dinner = entity.Dinner;
                    e.VisitorNum = entity.VisitorNum;
                    e.ViewSpot = entity.ViewSpot;
                    e.Feature = entity.Feature;
                    e.SelfItem = entity.SelfItem;
                    e.Notes = entity.Notes;
                    e.Comment = entity.Comment;
                    e.Remark = entity.Remark;
                    e.Cost = entity.Cost;
                    e.AvgPrice = entity.AvgPrice;
                    e.Profit = entity.Profit;
                    e.ChildCost = entity.ChildCost;
                    e.ChildPrice = entity.ChildPrice;
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

                        t.Save();
                    });
                    #endregion

                    #region << 服务标准 >>
                    DAL.Pro_QuotationCostItem.Delete(x => x.QuotationID == e.ID);
                    listItem.ForEach(x =>
                    {
                        var t = new DAL.Pro_QuotationCostItem();
                        t.ID = x.ID;
                        t.QuotationID = e.ID;
                        t.ItemName = x.ItemName;
                        t.ItemRemark = x.ItemRemark;
                        t.ItemPrice = x.ItemPrice;
                        t.ItemNum = x.ItemNum;
                        t.ItemSum = x.ItemSum;
                        t.OrgID = user.OrgID;
                        t.OrderIndex = x.OrderIndex;

                        t.Save();
                    });
                    #endregion

                    e.Save();

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存报价单时发生错误");
                return false;
            }

            return true;
        }

        private void DeletePDF(string routeID)
        {
            try
            {
                var fileName = "/Module/T/Files/" + routeID + ".pdf";
                fileName = HttpContext.Current.Server.MapPath(fileName);
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
            catch { }
        }

        /// <summary>
        /// 删除报价单
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
                    DAL.Pro_QuotationCostItem.Delete(x => x.QuotationID == routeID);
                    DAL.Pro_Quotation.Delete(x => x.ID == routeID);
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

        #region << 服务标准 >>
        /// <summary>
        /// 线路行程列表
        /// </summary>
        /// <param name="routeID"></param>
        /// <returns></returns>
        public List<DAL.Pro_QuotationCostItem> GetQuotationCostItem(string routeID)
        {
            return DAL.Pro_QuotationCostItem.Find(x => x.QuotationID == routeID).OrderBy(x => x.OrderIndex).ToList();
        }
        #endregion

        #region << 报价单转化为PDF >>

        /// <summary>
        /// 创建报价单FPD文件
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool CreateQuotationPDFFile(string keyID, string fileName)
        {
            try
            {

                #region 字体
                BaseFont bfChinese = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(bfChinese, 12, Font.BOLD, new iTextSharp.text.BaseColor(0, 0, 0));
                Font f10 = new Font(bfChinese, 10, Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));
                Font f10_bold = new Font(bfChinese, 10, Font.BOLD, new iTextSharp.text.BaseColor(0, 0, 0));
                Font font14 = new Font(bfChinese, 14, Font.BOLD, new iTextSharp.text.BaseColor(0, 0, 0));
                #endregion

                #region 线路数据
                var entity = Get(keyID);
                #endregion

                #region PDF文档
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
                document.Open();
                #endregion

                #region 线路标题
                var p = new Paragraph(entity.RouteName, font14);
                p.Alignment = 1; //0=Left, 1=Centre, 2=Right
                document.Add(p);
                document.Add(new Paragraph(System.Environment.NewLine));
                #endregion

                #region 线路特色
                if (!string.IsNullOrEmpty(entity.Feature))
                {
                    document.Add(new Paragraph("线路特色", font));
                    var feature = HtmlHeler.NoHTML(entity.Feature).Replace("<br/>", System.Environment.NewLine);
                    document.Add(new Paragraph(feature, f10));
                    document.Add(new Paragraph(System.Environment.NewLine));
                }
                #endregion

                #region 行程安排
                var list = GetRouteSchedule(keyID);
                document.Add(new Paragraph("行程安排", font));
                list.ForEach(x =>
                {
                    document.Add(new Paragraph(string.Format("第{0}天 {1}", x.DayNum, x.Title), f10_bold));
                    var s = "";
                    if (!string.IsNullOrEmpty(x.Schedule))
                        s = x.Schedule.Replace("<br/>", System.Environment.NewLine);
                    document.Add(new Paragraph(s, f10));
                    if (!string.IsNullOrEmpty(x.Dinner))
                        document.Add(new Paragraph(string.Format("用餐： {0}", x.Dinner), f10_bold));
                    if (!string.IsNullOrEmpty(x.Stay))
                        document.Add(new Paragraph(string.Format("住宿： {0}", x.Stay), f10_bold));
                    document.Add(new Paragraph(System.Environment.NewLine));
                });
                document.Add(new Paragraph(System.Environment.NewLine));
                #endregion

                #region 服务标准
                document.Add(new Paragraph("服务标准", font));
                var itemData = GetQuotationCostItem(keyID);
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 20, 80 });

                PdfPCell cell = new PdfPCell(new Phrase("服务项目", font));
                cell.HorizontalAlignment = 1;

                table.AddCell(cell);
                document.Add(new Paragraph(System.Environment.NewLine));
                cell = new PdfPCell(new Phrase("备注说明", font));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                itemData.ForEach(x =>
                {

                    table.AddCell(new Phrase(x.ItemName, f10));
                    table.AddCell(new Phrase(x.ItemRemark, f10));
                });
                document.Add(table);
                document.Add(new Paragraph(System.Environment.NewLine));
                #endregion

                #region 备注说明
                if (!string.IsNullOrEmpty(entity.Notes))
                {
                    document.Add(new Paragraph("特别提醒", font));
                    var str = HtmlHeler.NoHTML(entity.Notes).Replace("<br/>", System.Environment.NewLine);
                    document.Add(new Paragraph(str, f10));
                    document.Add(new Paragraph(System.Environment.NewLine));
                }
                if (!string.IsNullOrEmpty(entity.SelfItem))
                {
                    document.Add(new Paragraph("自费项目说明", font));
                    var str = HtmlHeler.NoHTML(entity.SelfItem).Replace("<br/>", System.Environment.NewLine);
                    document.Add(new Paragraph(str, f10));
                    document.Add(new Paragraph(System.Environment.NewLine));
                }
                if (!string.IsNullOrEmpty(entity.Comment))
                {
                    document.Add(new Paragraph("备注", font));
                    var str = HtmlHeler.NoHTML(entity.Comment).Replace("<br/>", System.Environment.NewLine);
                    document.Add(new Paragraph(str, f10));
                    document.Add(new Paragraph(System.Environment.NewLine));
                }

                #endregion

                #region 综合报价
                document.Add(new Paragraph("团队报价", font));
                document.Add(new Paragraph(string.Format("（按照{0}人独立成团核价，人数增减会导致价格相应变化！住宿若有单男/女，需补足单房差）",
                    entity.VisitorNum), f10));
                document.Add(new Paragraph(" ", font));
                table = new PdfPTable(3);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 40, 30, 30 });
                cell = new PdfPCell(new Phrase("总团费", font));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("成人价", font));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("儿童价", font));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                var totalAmt = (entity.Profit + entity.Cost).ToString();

                cell = new PdfPCell(new Phrase(totalAmt, f10));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(entity.AvgPrice.ToString(), f10));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(entity.ChildPrice.ToString(), f10));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
                cell = new PdfPCell(new Paragraph("儿童报价说明：" + entity.Remark, f10));
                cell.Colspan = 3;
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);
                document.Add(table);
                document.Add(new Paragraph(System.Environment.NewLine));
                #endregion

                #region 页脚
                var user = AuthenticationPage.UserInfo;
                var footer = new QuotationSetting_BF().Get(user.OrgID);
                if (footer != null)
                {
                    var str = footer.Template;
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (user.LoginUserType == UserType.AdminUser)
                        {
                            str = str.Replace("@Dept", user.DeptName).Replace("@UserName", user.UserName).Replace("@UserMobile", user.Mobile).Replace("@UserEmail", "");
                        }
                        else
                        {
                            var u = new User_BF().Get(user.UserID);
                            if (u != null)
                            {
                                str = str.Replace("@Dept", u.DeptName).Replace("@UserName", u.Name).Replace("@UserMobile", u.Mobile).Replace("@UserEmail", u.Email);
                            }
                        }
                        List<IElement> elementList = HTMLWorker.ParseToList(new StringReader(str), null);
                        foreach (var element in elementList)
                        {
                            var ele = element as Paragraph;
                            var a = ele.Content;
                            if (!string.IsNullOrEmpty(a))
                            {
                                var __p = new Paragraph(a, font);
                                __p.Alignment = 2; //0=Left, 1=Centre, 2=Right
                                document.Add(__p);
                            }
                        }
                        document.Add(new Paragraph(System.Environment.NewLine));
                    }
                }
                #endregion

                document.Close();

                return true;
            }
            catch (Exception ex)
            {

                BizUtility.ExceptionHandler(AuthenticationPage.UserInfo, ex, "生成报价单文件时发生错误");
                return false;
            }
        }

        #endregion
    }
}
