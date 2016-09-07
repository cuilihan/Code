using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRP.Log;

namespace DRP.BF
{
    /// <summary>
    /// 类型层通用类
    /// </summary>
    public class BizUtility
    {
        /// <summary>
        /// 写日志 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        public static void WriteLog(UserInfo user, string message)
        {
            var log=new LogEntity();
            log.UserID=user.UserID;
            log.UserName=user.UserName;
            log.OrgID=user.OrgID;
            log.Message=message; 
            LogHelper.Info(log); 
        }

        /// <summary>
        /// 异常处理类(目前只写日志)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ex"></param>
        public static void ExceptionHandler(UserInfo user, Exception ex, string message = "")
        {
            var log = new LogEntity();
            log.UserID = user.UserID;
            log.UserName = user.UserName;
            log.OrgID = user.OrgID;
            log.Message = message;
            log.ex = ex;
            LogHelper.Error(log);  
        }
    }
}
