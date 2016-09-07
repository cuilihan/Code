using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF
{
    /// <summary>
    /// 流水号生成器
    /// 【单例模式】
    /// </summary>
    public class SerialNo_BF
    {
        private static SerialNo_BF instance;

        private static readonly object _object = new object();

        private SerialNo_BF()
        {
        }

        /// <summary> 
        ///  创建对象实例
        /// （双重加锁 Double-Check Locking） 
        /// </summary> 
        /// <returns></returns> 
        public static SerialNo_BF GetInstance()
        {
            if (instance == null)
            {
                lock (_object) //加锁保证只能有一个线程可以访问
                {
                    if (instance == null)
                    {
                        instance = new SerialNo_BF();
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
        public string CreateSerialNo(string serialType, string orgID)
        {
            var serialNo = 1;
            var e = DAL.Glo_SerialNo.SingleOrDefault(x => x.OrgID == orgID && x.SerialType == serialType.ToUpper());
            if (e != null)
            {
                serialNo = e.SerialNo + 1;
            }
            e = new DAL.Glo_SerialNo();
            e.ID = Guid.NewGuid().ToString();
            e.SerialType = serialType.ToUpper();
            e.SerialNo = serialNo;
            e.OrgID = orgID;
            e.Save();
            return serialType + serialNo.ToString().PadLeft(3, '0');
        }
    }
}
