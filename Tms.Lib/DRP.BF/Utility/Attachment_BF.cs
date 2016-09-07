using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.Utility
{
    public class Attachment_BF
    {
        public bool Save(DAL.Glo_File entity, out string creator)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                creator = AuthenticationPage.UserInfo.UserName;
                entity.OrgID = AuthenticationPage.UserInfo.OrgID;
                entity.DeptID = AuthenticationPage.UserInfo.DeptID;
                entity.CreateUserID = AuthenticationPage.UserInfo.UserID;
                entity.CreateUserName = creator;
                entity.Save();
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "上传附件时发生错误");
                creator = "";
                return false;
            }
        }

        public bool Save(DAL.Glo_File entity)
        {
            var user = AuthenticationPage.UserInfo;
            try
            {
                var e = GetFile(entity.ID);
                if (e == null) e = new DAL.Glo_File();
                e.ID = entity.ID;
                e.FileType = entity.FileType;
                e.FileName = entity.FileName;
                e.FileSize = entity.FileSize;
                e.CreateUserName = entity.CreateUserName;
                e.CreateDate = entity.CreateDate;
                e.FilePath = entity.FilePath;

                e.Save();
                return true;
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "上传附件时发生错误");
                return false;
            }
        }

        public void Delete(string keyID)
        {
            DAL.Glo_File.Delete(x => x.ID == keyID);
        }

        /// <summary>
        /// 订单附件
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Glo_File> GetAttachment(Guid orderID)
        {
            var sql = "select b.* from dbo.Ord_OrderFile a inner join Glo_Files b on a.FileD=b.ID where a.OrderID='{0}' order by CreateDate asc";
            sql = string.Format(sql, orderID);
            return new SubSonic.Query.CodingHorror(sql).ExecuteTypedList<DAL.Glo_File>();
        }


        public DAL.Glo_File GetFile(string keyID)
        {
            return DAL.Glo_File.SingleOrDefault(x => x.ID == keyID);
        }
    }
}
