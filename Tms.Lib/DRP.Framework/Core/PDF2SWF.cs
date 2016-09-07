using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace DRP.Framework.Core
{
    /// <summary>
    /// PDF转Flash
    /// 依赖文件两个文件包：
    /// 1、语言包 C:\xpdf (在语言包的配置文件中配置为c:\xpdf，当前文件包须放在c:\xpdf)
    ///           语言包配置文件：C:\xpdf\xpdf-chinese-simplified下的add-to-xpdfrc注意相关路径配置
    /// 2、PDF转换工具 D:\Program Files (x86)\SWFTools (安装文件)
    /// </summary>
    public class PDF2SWF
    {
        #region tools
        //根目录       
        private static string ROOT_PATH = AppDomain.CurrentDomain.BaseDirectory;
        private static string PDFTOOLSPATH = ConfigHelper.GetAppSettingValue("PDFToolsPath");

        //pdf转swf        
        private static string PDF2SWF_PATH = PDFTOOLSPATH + @"\pdf2swf.exe";
        //合并swf        
        private static string SWFCOMBINE_PATH = PDFTOOLSPATH + @"\swfcombine.exe";
        //导航        
        private static string SWFVIEWER_PATH = PDFTOOLSPATH + @"\rfxview.swf";
        //临时swf
        private static string SWFTEMP_PATH = HttpContext.Current.Server.MapPath(DRPResolveUrl.ResolveUrl("~/Files/SWF/temp.swf"));

        //语言包路径       
        private static string XPDF_LANG_PATH = ConfigHelper.GetAppSettingValue("PDFLangPath");

        #endregion

        ///<summary>        
        ///swf格式文件播放      
        ///</summary>        
        ///<param name="url"></param>   
        ///<param name="width"></param>       
        ///<param name="height"></param>    
        ///<returns></returns>       
        public static string AddSwf(string url, int height = 850)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" width=\"100%\" height=\"" + height + "px\"");
            sb.Append(" codebase=\"http://active.macromedia.com/flash5/cabs/swflash.cab#version=8,0,0,0\">");
            sb.Append("<PARAM NAME='Movie' VALUE='" + url + "' />");
            sb.Append("<PARAM NAME='Play' VALUE='true' />");
            sb.Append("<PARAM NAME='Loop' VALUE='true' />");
            sb.Append("<PARAM NAME='Quality' VALUE='High' />");
            sb.Append("<param name=\"wmode\" value=\"transparent\">");
            sb.Append("<PARAM NAME='FLASHVARS' VALUE='zoomtype=3' />");
            sb.Append("<embed src='" + url + "' width='100%' height='" + height + "px' play='true' wmode=\"transparent\" align=\"\" loop='true' quality='high' pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' flashvars='zoomtype=3'>");
            sb.Append("</embed>");
            sb.Append("</object>");
            return sb.ToString();
        }

        ///<summary>      
        /// 传入PDF的文件路径，以及输出文件的位置，执行pdf2swf的命令        
        ///</summary>    
        ///<param name="strPDFPath"></param>       
        ///<param name="strSwfPath"></param>     
        public static bool DoPDF2Swf(string strPDFPath, string strSwfPath)
        {
            bool isSuccess = false;
            //如果PDF不存在           
            if (!File.Exists(strPDFPath))
            {
                return false;
            }
            #region 清理之前的记录
            if (File.Exists(strSwfPath))
            {
                File.Delete(strSwfPath);         //已经存在,删除     
            }
            if (File.Exists(SWFTEMP_PATH))
            {
                File.Delete(SWFTEMP_PATH);
            }
            #endregion

            //将pdf文档转成temp.swf文件        
            string strCommand = String.Format("{0} -s languagedir={3} {1} -o {2}", PDF2SWF_PATH, strPDFPath, SWFTEMP_PATH, XPDF_LANG_PATH);
            //  string strCommand = String.Format("{0} {1} -o {2}",PDF2SWF_PATH, strPDFPath, SWFTEMP_PATH);

            RunShell(strCommand);

            //第一步转档失败，则返回          
            if (!File.Exists(SWFTEMP_PATH))
            {
                return false;
            }

            //将temp.swf加入到rfxview.swf加入翻页的导航         
            strCommand = String.Format("{0} {1} viewport={2} -o {3}", SWFCOMBINE_PATH, SWFVIEWER_PATH, SWFTEMP_PATH, strSwfPath);

            RunShell(strCommand);
            if (File.Exists(strSwfPath))
            {
                isSuccess = true;
            }
            return isSuccess;
        }


        ///<summary>
        /// 运行命令       
        /// </summary>       
        /// ///<param name="strShellCommand">命令字符串</param>     
        /// ///<returns>命令运行时间</returns>   
        private static void RunShell(string strShellCommand)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.Arguments = String.Format(@"/c {0}", strShellCommand);
            cmd.Start();
            cmd.WaitForExit();
            cmd.Close();
        }
    }
}
