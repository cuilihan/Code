using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Framework;
using DRP.DAL;

namespace DRP.BF.Glo
{
    /// <summary>
    /// 参数类型
    /// </summary>
    public enum BasicType
    {
        /// <summary>
        /// 导游等级定义
        /// </summary>
        Res_GuideGrade = 1,
        /// <summary>
        /// 酒店星级定义
        /// </summary>
        Res_HotelStar = 2,
        /// <summary>
        /// 客户类型
        /// </summary>
        Crm_CustomerType = 3,
        /// <summary>
        /// 线路类型
        /// </summary>
        Pro_RouteType = 4,
        /// <summary>
        /// 销售跟踪类型
        /// </summary>
        Crm_SalesTraceType=5,
        /// <summary>
        /// 收款类型
        /// </summary>
        Fin_CollectedType=6,
        /// <summary>
        /// 订单来源
        /// </summary>
        Ord_OrderSource=7,
        /// <summary>
        /// 开票项目设置
        /// </summary>
        Fin_InvoiceItem=8,
        /// <summary>
        /// 发票领取方式
        /// </summary>
        Fin_InvoiceFetchType=9,
        /// <summary>
        /// 车队规模
        /// </summary>
        Res_MotorcadeScale=10,
        /// <summary>
        /// 报价模板-住宿标准
        /// </summary>
        Pro_QuotationStay=11,
        /// <summary>
        /// 报价模板-用餐标准
        /// </summary>
        Pro_QuotationDinner=12,
        /// <summary>
        /// 导游领款方式（预算）
        /// </summary>
        Ord_DrawMoneyMethod=13,
        /// <summary>
        /// 单项业务订单类型
        /// </summary>
        Ord_SingleBizType=14,
        /// <summary>
        /// 非订单收入类型
        /// </summary>
        CheckIn_IncomeSign=15,
        /// <summary>
        /// 非订单支出类型
        /// </summary>
        CheckIn_PayableSign=16,
         /// <summary>
        /// 客户证件类型
        /// </summary>
        Crm_CredentialType = 17,
         /// <summary>
        /// 线路来源
        /// </summary>
        Pro_RouteSource = 18
    }

    /// <summary>
    /// 基本参数定义
    /// </summary>
    public class BasicInfo_BF
    {
        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="basicType"></param>
        /// <returns></returns>
        public List<Glo_BasicInfo> GetBasicInfo(BasicType basicType)
        {
            return DAL.Glo_BasicInfo.Find(x => x.BasicType == (int)basicType
                && x.OrgID == AuthenticationPage.UserInfo.OrgID)
                .OrderBy(x => x.OrderIndex).ToList();
        }

        /// <summary>
        /// 参数详情
        /// </summary>
        /// <param name="keyID"></param>
        /// <returns></returns>
        public Glo_BasicInfo Get(string keyID)
        {
            return DAL.Glo_BasicInfo.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 参数保存
        /// </summary>
        /// <param name="basicType"></param>
        /// <param name="paraName"></param>
        /// <param name="orderIndex"></param>
        /// <returns></returns>
        public bool Save(string keyID, BasicType basicType, string paraName, int orderIndex)
        {
             var user = AuthenticationPage.UserInfo;
            try
            {               
                var e = Get(keyID);
                if (e == null)
                {
                    e = new Glo_BasicInfo();
                    e.CreateDate = DateTime.Now;
                    e.CreateUserID = user.UserID;
                    e.CreateUserName = user.UserName;
                    e.DeptID = user.DeptID;
                    e.OrgID = user.OrgID;
                }
                e.Name = paraName;
                e.BasicType = (int)basicType;
                e.OrderIndex = orderIndex;
                e.ID = keyID;
                e.Save();

                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "保存参数时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public bool Delete(List<string> listID)
        {
           return new Glo_BasicInfo().MultiDelete(listID);
        }
    }
}
