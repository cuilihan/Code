using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web;
using System.Threading;

namespace DRP.Framework.Core
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class DownFile
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="_fileName">目标文件名，保存时显示名</param>
        /// <param name="_fullPath">源文件路径，绝对路径</param>
        /// <param name="_speed"></param>
        /// <returns></returns>
        public static bool DownLoad(string _fileName, string _fullPath, long _speed = 1024000)
        {
            try
            { 
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    HttpContext.Current.Response.AddHeader("Accept-Ranges", "bytes");
                    HttpContext.Current.Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;
                    double pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor(1000 * pack / _speed) + 1;
                    if (HttpContext.Current.Request.Headers["Range"] != null)
                    {
                        HttpContext.Current.Response.StatusCode = 206;
                        string[] range = HttpContext.Current.Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    HttpContext.Current.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    HttpContext.Current.Response.AddHeader("Connection", "Keep-Alive");
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", 
                        "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (HttpContext.Current.Response.IsClientConnected)
                        {
                            HttpContext.Current.Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                            Thread.Sleep(sleep);
                        }
                        else
                            i = maxCount;
                    }                   
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
