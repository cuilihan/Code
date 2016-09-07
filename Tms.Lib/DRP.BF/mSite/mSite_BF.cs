using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml;
using DRP.Framework;
using DRP.Framework.Core;

namespace DRP.BF.mSite
{
    /// <summary>
    /// 微网站设置
    /// </summary>
    public class mSite_BF
    {
        /// <summary>
        /// 微网站基本信息设置
        /// </summary>
        /// <param name="e"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public bool SaveSetting(DAL.Sn_BasicInfo e, string adData)
        {
            var user = AuthenticationPage.UserInfo;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DAL.Sn_BasicInfo.Delete(x => x.OrgID == user.OrgID);
                    var entity = new DAL.Sn_BasicInfo();
                    entity.ID = Guid.NewGuid().ToString();
                    entity.OrgID = user.OrgID;
                    entity.IsShowRoute = e.IsShowRoute;
                    entity.LinkUrl = e.LinkUrl;
                    entity.TravelName = e.TravelName;
                    entity.HotLine = e.HotLine;
                    entity.Logo = e.Logo;
                    entity.AboutUs = e.AboutUs; 
                    entity.CreateUserName = user.UserName;
                    entity.CreateDate = DateTime.Now; 
                    entity.Save();  
                     

                    #region 轮显广告
                    if (!string.IsNullOrEmpty(adData))
                    {
                        DAL.Sn_AdSlide.Delete(x => x.OrgID == user.OrgID);

                        var doc = new XmlDocument();
                        doc.LoadXml(adData);
                        var nodes = doc.SelectNodes("document/data");
                        foreach(XmlNode node in nodes)
                        {
                            var ad = new DAL.Sn_AdSlide();
                            ad.OrgID = user.OrgID;
                            ad.ID = Guid.NewGuid().ToString();
                            ad.LinkUrl = node.GetNodeValue("url");
                            ad.ImgSrc = node.GetNodeValue("imgSrc");
                            ad.CreateDate = DateTime.Now;
                            ad.CreateUserName = user.UserName;
                            ad.Save();
                        }                       
                    }
                    #endregion

                    scope.Complete();

                    return true;
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "设置微网站时发生错误");
                return false;
            }
        }

        /// <summary>
        /// 基本配置信息
        /// </summary>
        /// <returns></returns>
        public DAL.Sn_BasicInfo GetBasicInfo()
        {
            return DAL.Sn_BasicInfo.SingleOrDefault(x => x.OrgID == AuthenticationPage.UserInfo.OrgID);
        }

        /// <summary>
        /// 菜单集合
        /// </summary>
        /// <returns></returns>
        public List<DAL.Sn_MenuItem> GetMenuItem()
        {
            return DAL.Sn_MenuItem.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).OrderBy(x => x.SortIndex).ToList();
        }

        /// <summary>
        /// 广告图片
        /// </summary>
        /// <returns></returns>
        public List<DAL.Sn_AdSlide> GetAdSlide()
        {
            return DAL.Sn_AdSlide.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID).ToList();
        }
    }
}
