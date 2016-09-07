using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.Framework
{
    /// <summary>
    /// 枚举类定义
    /// </summary>
    public class EnumConst
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public enum OperationAction
        {
            Add = 1, Edit = 2, Delete = 4, Import = 8, Export = 16, DataPrivate = 32, DataPublic = 64, View = 128
        }


        /// <summary>
        /// 遍历枚举类型
        /// </summary>
        /// <param name="EnumType">typeof(枚举类型)</param>      
        /// <returns>字典：枚举项名称-值</returns>
        public static Dictionary<string, string> DictFromEnum(Type EnumType)
        {
            var dict = new Dictionary<string, string>();
            foreach (string str in Enum.GetNames(EnumType))
            {
                dict.Add(str, Enum.Format(EnumType, Enum.Parse(EnumType, str), "d"));
            }
            return dict;
        }

        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetEnumText<T>(T t) where T : Type
        {
            foreach (string str in Enum.GetNames(t))
            {
                var s = str;
            }
            return string.Empty;
        }
    }
}
