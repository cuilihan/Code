
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DRP.DataService
{
    /// <summary>
    /// 文件日志
    /// 设置为单例模式，防止多线程操作日志
    /// </summary>
    internal class FileLog
    {
        private FileLog()
        { }

        private static volatile FileLog instance = null;

        private static object lockHelper = new object();

        /// <summary>
        /// 日志的实例
        /// </summary>
        internal static FileLog Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new FileLog();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 写入文件日志记录
        /// </summary>
        /// <param name="log"></param>
        internal void Write(string log)
        {
            try
            {

                var path = AppDomain.CurrentDomain.BaseDirectory + @"Log\" + DateTime.Now.ToString("yyyyMM") + "\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = path + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.Write("操作时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + System.Environment.NewLine);
                    sw.Write("日志内容：" + log + System.Environment.NewLine);
                    sw.Write(System.Environment.NewLine);
                    sw.Flush();
                }
            }
            catch { }
        }
    }
}
