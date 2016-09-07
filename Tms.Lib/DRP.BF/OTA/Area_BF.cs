using DRP.BF.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRP.BF.OTA
{
    public class OTAArea : DAL.Glo_Destination
    {

        public string OTAareaName { get; set; }
        public Guid OTAareaID { get; set; }
        public string OTAName { get; set; }
    }

    public class Area_BF
    {
        private const string DestinationCacheKey = "Glo_Destination_Key";

        public List<DAL.Glo_Destination> GetArea()
        {
            var user = AuthenticationPage.UserInfo;
            var key = DestinationCacheKey + "_" + user.OrgID;
            var list = BizCacheHelper.GloDestinaionCache.Get(key);
            if (list == null)
            {
                list = DAL.Glo_Destination.Find(x => x.OrgID == user.OrgID).ToList();
                BizCacheHelper.GloDestinaionCache.Insert(key, list);
            }
            return list;
        }

        public List<DAL.Glo_Destination> GetArea(Guid pID)
        {
            var user = AuthenticationPage.UserInfo;
            var key = DestinationCacheKey + "_" + user.OrgID;
            var list = BizCacheHelper.GloDestinaionCache.Get(key);
            if (list == null)
            {
                list = DAL.Glo_Destination.Find(x => x.OrgID == user.OrgID && x.ParentID == pID.ToString()).ToList();
                BizCacheHelper.GloDestinaionCache.Insert(key, list);
            }
            return list;
        }

        public List<DAL.Glo_Destination> GetArea(Guid pID, string str)
        {
            var user = AuthenticationPage.UserInfo;
            var key = DestinationCacheKey + "_" + user.OrgID;
            var list = BizCacheHelper.GloDestinaionCache.Get(key);
            if (list == null)
            {
                list = DAL.Glo_Destination.Find(x => x.OrgID == user.OrgID && x.ParentID == pID.ToString() && x.Name.Equals(str)).ToList();
                BizCacheHelper.GloDestinaionCache.Insert(key, list);
            }
            return list;
        }

        /// <summary>
        /// OTA同步后的数据
        /// </summary>
        /// <param name="OTAID"></param>
        /// <returns></returns>
        public List<OTAArea> GetOTABindArea(Guid OTAID)
        {
            var user = AuthenticationPage.UserInfo;
            var sql = string.Format("select a.id,a.ParentID,a.Name,b.OTAareaName,b.OTAareaID,c.OTAName,a.OrgID from Glo_Destination as a left join OTA_AreaSetting as b on a.ID=b.AreaID left join OTA_Setting as c on b.OTAID=c.ID where a.OrgID='{0}'", user.OrgID);
            return new SubSonic.Query.CodingHorror(sql).ExecuteTypedList<OTAArea>().ToList();
        }



        public DAL.Glo_Destination GetDetail(Guid keyID)
        {
            return DAL.Glo_Destination.SingleOrDefault(x => x.ID == keyID.ToString());
        }

        public bool Save(Guid keyID, Guid pID, string pName, int sort)
        {
            try
            {

                var e = GetDetail(keyID);
                if (e == null)
                {
                    e = new DAL.Glo_Destination();
                }
                e.ParentID = pID.ToString();
                e.Name = pName;
                e.ID = keyID.ToString();
                e.Save();
                return true;
            }
            catch (Exception ex)
            {
                //SysLog_BF.Write("区域管理", "区域资料更新时发生错误", ex);
                return false;
            }
        }



        /// <summary>
        /// OTA初始化目的地
        /// </summary>
        /// <param name="OtaID"></param>
        /// <returns></returns>
        public bool InitAreaData(Guid OtaID)
        {
            bool isok = false;
            try
            {
                var user = AuthenticationPage.UserInfo;
                var list = GetArea();
                var listGet = new DRP.BF.DataSync.OctHelper().QueryPackageDestinationList();

                DAL.OTA_AreaSetting.Delete(x => x.OTAID == OtaID);


                listGet.ForEach(x =>
                {
                    var area = DAL.Glo_Destination.Find(y => y.Name==x.AreaName).FirstOrDefault();
                    if (area != null)
                    {
                        var entity = new DAL.OTA_AreaSetting();
                        entity.ID = Guid.NewGuid();
                        entity.OTAareaID = x.AreaID.ToString();
                        entity.OTAareaName = x.AreaName;
                        entity.AreaID = Guid.Parse(area.ID);
                        entity.AreaName = area.Name;
                        entity.CreateDate = DateTime.Now;
                        entity.OTAID = OtaID;
                        entity.OrgID = Guid.Parse(user.OrgID);
                        entity.Save();
                    }
                });

                isok = true;

            }
            catch (Exception)
            {


            }

            return isok;

        }


        /// <summary>
        /// OTA目的地绑定
        /// </summary>
        /// <param name="aid">目的地ID 本地</param>
        /// <param name="otdid">OTAID</param>
        /// <param name="otdaid">OTA的目的地ID</param>
        /// <param name="name">目的地</param>
        /// <param name="otaName">OTA目的地</param>
        /// <returns></returns>
        public bool OTAAreaSave(string aid, string otdid, string otdaid, string name, string otaName)
        {
            bool isok = false;
            try
            {
                var entity = new DAL.OTA_AreaSetting();
                entity.ID = Guid.NewGuid();
                entity.OTAareaID = otdaid;
                entity.OTAareaName = otaName;
                entity.OTAID = Guid.Parse(otdid);
                entity.AreaID = Guid.Parse(aid);
                entity.AreaName = name;
                entity.CreateDate = DateTime.Now;
                entity.OTAareaType = 0;
                entity.Save();
                isok = true;
            }
            catch (Exception)
            {

                throw;
            }
            return isok;
        }

        /// <summary>
        /// OTA目的地解除绑定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool OTAAreaDelete(Guid id)
        {
            bool isok = false;

            try
            {
                DAL.OTA_AreaSetting.Delete(x => x.AreaID == id);
                isok = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return isok;
        }
    }
}
