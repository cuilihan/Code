using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;

namespace DRP.Framework.Core
{
    public class UpLoadFile
    {
        /// <summary>
        /// 上传文件，返回空值表示上传失败
        /// </summary>
        /// <param name="fileUpLoad"><see cref="FileUpload"/></param>
        /// <param name="sSavePath">上传文件存放的目录，绝对路径</param>
        /// <param name="filterExtendName">过滤上传的文件后缀名</param>
        /// <returns></returns>
        public static string UpLoad(FileUpload fileUpLoad, string sSavePath, string filterExtendName = ".exe|.bat|.js|.ubb|.htm|.html|.reg|.sql")
        {
            HttpPostedFile myFile = fileUpLoad.PostedFile;
            if (fileUpLoad.PostedFile == null)
                return "";
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
                return "";
            //文件过滤
            string extendName = Path.GetExtension(myFile.FileName).ToLower();
            if (filterExtendName.Contains(extendName))
                return "";

            byte[] myData = new Byte[nFileLen];
            myFile.InputStream.Read(myData, 0, nFileLen);
            
            string sFilename = Path.GetFileName(myFile.FileName);
            int file_append = 0;
            //检查当前文件夹下是否有同名文件,有则在文件名+1 
            while (File.Exists(sSavePath + sFilename))
            {
                file_append++;
                sFilename = Path.GetFileNameWithoutExtension(myFile.FileName)
                    + file_append.ToString() + extendName;
            }
            FileStream newFile = new FileStream(sSavePath + sFilename, FileMode.Create, FileAccess.Write);
            newFile.Write(myData, 0, myData.Length);
            newFile.Close();
            return sFilename;
        }


        /// <summary>
        /// 上传文件，返回空值表示上传失败
        /// </summary>
        /// <param name="fileUpLoad"><see cref="FileUpload"/></param>
        /// <param name="sSavePath">上传文件存放的目录，绝对路径</param>
        /// <param name="filterExtendName">过滤上传的文件后缀名</param>
        /// <returns></returns>
        public static string UpLoad(HttpPostedFile myFile, string sSavePath, string filterExtendName = ".exe|.bat|.js|.ubb|.htm|.html|.reg|.sql")
        {
            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
                return "";
            //文件过滤
            string extendName = Path.GetExtension(myFile.FileName).ToLower();
            if (filterExtendName.Contains(extendName))
                return "";

            byte[] myData = new Byte[nFileLen];
            myFile.InputStream.Read(myData, 0, nFileLen);

            string sFilename = Path.GetFileName(myFile.FileName);
            int file_append = 0;
            //检查当前文件夹下是否有同名文件,有则在文件名+1 
            while (File.Exists(sSavePath + sFilename))
            {
                file_append++;
                sFilename = Path.GetFileNameWithoutExtension(myFile.FileName)
                    + file_append.ToString() + extendName;
            }
            FileStream newFile = new FileStream(sSavePath + sFilename, FileMode.Create, FileAccess.Write);
            newFile.Write(myData, 0, myData.Length);
            newFile.Close();
            return sFilename;
        }
    }
}
