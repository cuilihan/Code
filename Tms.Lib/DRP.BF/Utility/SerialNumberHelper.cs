using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF
{
    /// <summary>
    /// 流水号创建器
    /// <remarks>单例模式保证实例唯一，防止流水号重复</remarks>
    /// </summary>
    public class SerialNumberHelper
    {
        private static SerialNumberHelper instance;

        private static readonly object _object = new object();

        private SerialNumberHelper()
        {
        }

        /// <summary> 
        ///  创建对象实例
        /// （双重加锁 Double-Check Locking） 
        /// </summary> 
        /// <returns></returns> 
        public static SerialNumberHelper GetInstance()
        {
            if (instance == null)
            {
                lock (_object) //加锁保证只能有一个线程可以访问
                {
                    if (instance == null)
                    {
                        instance = new SerialNumberHelper();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 创建流水号
        /// </summary>
        /// <param name="serialType"></param>
        /// <param name="orgID"></param>
        /// <returns>SerialNo+三位流水号如CX20141021-1023-001</returns>
        private string CreateSerialNo(string serialType, string orgID)
        {
            var e = DAL.Glo_SerialNo.SingleOrDefault(x => x.OrgID == orgID && x.SerialType == serialType.ToUpper());
            if (e == null)
            {
                e = new DAL.Glo_SerialNo();
                e.ID = Guid.NewGuid().ToString();
                e.SerialType = serialType.ToUpper();
                e.OrgID = orgID;
                e.SerialNo = 1;
            }
            else
            {
                e.SerialNo = e.SerialNo + 1;
            }
            e.Save();

            return serialType + e.SerialNo.ToString().PadLeft(3, '0');
        }


        /// <summary>
        /// 创建团次号
        /// [线路类型][出团日期]-[回团日期]-[三位流水]
        /// 如：DX20141012-1017-001
        /// </summary>
        /// <returns></returns>
        public string CreateTourNo(DAL.Pro_RouteInfo route, DAL.Pro_TourInfo tour)
        {
            var pre = "";
            if (route.RouteType.Contains("短线"))
                pre = "DX";
            if (route.RouteType.Contains("长线"))
                pre = "CX";
            if (route.RouteType.Contains("出境"))
                pre = "CJ";
            pre += tour.TourDate.ToString("yyyyMMdd") + "-" + tour.ReturnDate.ToString("MMdd");
            return CreateSerialNo(pre, AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 创建订单编号
        /// [线路类型][出团日期]-[回团日期]-[三位流水]
        /// 如：DX20141012-1017-001
        /// </summary>
        /// <returns></returns>
        public string CreateOrderNo(string routeType, DateTime tourDate, int tourDays)
        {
            var pre = "";
            if (!string.IsNullOrEmpty(routeType))
            {
                if (routeType.Contains("短线"))
                    pre = "DX";
                if (routeType.Contains("长线"))
                    pre = "CX";
                if (routeType.Contains("出境"))
                    pre = "CJ";
            }
            else
                pre = "DX";
            pre += tourDate.ToString("yyyyMMdd") + "-" + tourDate.AddDays(tourDays).ToString("MMdd");
            return CreateSerialNo(pre, AuthenticationPage.UserInfo.OrgID);
        }
    }
}
