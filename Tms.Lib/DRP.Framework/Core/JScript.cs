using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace DRP.Framework.Core
{
    /// <summary>
    /// 执行JS方法
    /// </summary>
    public class JScript
    {
        public static void ScriptStartUp(string scriptContent)
        {
            Page p = (Page)HttpContext.Current.Handler;
          //  string script = scriptContent.Replace("\r", @"\r").Replace("\n", @"\n");
            p.ClientScript.RegisterStartupScript(p.GetType().BaseType, Guid.NewGuid().ToString(), scriptContent, true);
        }

        public static void Alert(string msg)
        {
            ScriptStartUp("Alert('" + msg + "');");
        }
        public static void Error(string msg)
        {
            ScriptStartUp("Error('" + msg + "');");
        }
        public static void Question(string msg)
        {
            ScriptStartUp("Question('" + msg + "');");
        }
        public static void Warning(string msg)
        {
            ScriptStartUp("Warning('" + msg + "');");
        }
        
        /// <summary>
        /// 右下角消息提醒 3秒自动关闭
        /// </summary>
        /// <param name="msg"></param>
        public static void Notice(string msg)
        {
            ScriptStartUp(string.Format("Notice('{0}');", msg));
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="winID"></param>
        public static void CloseDialogWin(string msg)
        {
            ScriptStartUp(string.Format("closeWindow('{0}');", msg));
        }
         

        /// <summary>
        /// 关闭Tab窗口
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="winID"></param>
        public static void CloseTabPanel(string msg, string tabTitle)
        {
            ScriptStartUp(string.Format("closeTab('{0}','{1}');", msg, tabTitle));
        }
    }
}
