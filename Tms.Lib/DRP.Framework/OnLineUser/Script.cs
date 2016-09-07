using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRP.Framework.Core;
using DRP.Framework;

namespace DRP.Framework.OnLineUser
{  
    [ToolboxData("<{0}:Script runat=server></{0}:Script>")]
    public class Script : PlaceHolder
    {
        /// <summary>
        /// 设置js自动刷新的间隔时间，默认为1分钟。
        /// </summary>
        public virtual int RefreshTime
        {
            get {
               var frequency= ConfigHelper.GetAppSettingValue("RefreshFrequency");
               if (string.IsNullOrEmpty(frequency))
                   frequency = "60";
               return frequency.ToInt() * 1000;
            }
        }
        
        /// <summary>
        /// 自定义的Ajax方法
        /// </summary>
        /// <param name="onLineUserCountID"></param>
        /// <param name="refreshUrl"></param>
        /// <returns></returns>
        private string ExecScript(string onLineUserCountID, string refreshUrl)
        {
            var script = @"<script type='text/javascript'>var xmlHttp = false; function SendQuery(url) {try { xmlHttp = new ActiveXObject('Msxml2.XMLHTTP');  } catch (e) { try { xmlHttp = new ActiveXObject('Microsoft.XMLHTTP'); } catch (e) { xmlHttp = false; } } if (!xmlHttp && typeof XMLHttpRequest != 'undefined') { xmlHttp = new XMLHttpRequest(); } var sRand = ''; for (var i = 0; i < 10; i++) { sRand = sRand + (parseInt(Math.random() * 9)).toString(); } xmlHttp.onreadystatechange = GetQuery; xmlHttp.open('GET', url + '?rnd=' + sRand, true);  xmlHttp.send(null); } function GetQuery() { if (xmlHttp.readyState == 4) { var returnValue = xmlHttp.responseText; if (returnValue != '') { document.getElementById('" + onLineUserCountID + "').innerText = returnValue; } } } function " + ClientID + "_PostRefresh() { SendQuery('" + refreshUrl + "'); setTimeout('" + ClientID + "_PostRefresh()', " + RefreshTime + "); } window.addEvent('load', function () { try{ " + ClientID + "_PostRefresh();} catch(e){} });</script>";
            return script;
        }

        /// <summary>
        /// 应用mootools的ajax刷新在线用户
        /// </summary>
        /// <returns></returns>
        private string ExecScriptInMootools(string onLineUserCountID, string refreshUrl)
        {
            return string.Format("<script type='text/javascript'>RefreshOnlineUser('{0}','{1}','{2}')", refreshUrl, onLineUserCountID, RefreshTime)+"</script>";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var status=ConfigHelper.GetAppSettingValue("OnLineStatus"); //是否启用在线用户功能 
            var onLineUserCountID = ConfigHelper.GetAppSettingValue("OnLineUserCountID"); //显示在线用户数的控件ID
            var refreshUrl = ResolveUrl("~/") + ConfigHelper.GetAppSettingValue("RefreshUrl"); //刷新URL
            var isApplyToMooltools = ConfigHelper.GetAppSettingValue("IsApplyToMooltools"); //启用Ajax的方法
            if (string.IsNullOrEmpty(status)) return;
            if (status.ToLower().Equals("true"))
            {
                if (isApplyToMooltools.ToLower().Equals("true"))
                    writer.Write(ExecScriptInMootools(onLineUserCountID, refreshUrl));
                else
                    writer.Write(ExecScript(onLineUserCountID,refreshUrl));
                base.Render(writer);
            }
        }
    }
}
