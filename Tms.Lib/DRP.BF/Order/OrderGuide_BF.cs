using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml;
using DRP.Framework;
using DRP.Framework.Core;
using DRP.DAL;

namespace DRP.BF.Order
{
    /// <summary>
    /// 团队订单导游管理
    /// </summary>
    public class OrderGuide_BF
    {
        /// <summary>
        /// 安排导游
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="orderID"></param>
        /// <param name="ordType"></param>
        /// <returns></returns>
        public bool Save(string xmlData, string orderID)
        {
            if (string.IsNullOrEmpty(xmlData)) return false;
            var isRec = true;
            var user = AuthenticationPage.UserInfo;
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlData);

                using (TransactionScope scope = new TransactionScope())
                {
                    DAL.Ord_OrderGuide.Delete(x => x.OrderID == orderID && x.OrgID == user.OrgID);
                    var nodes = doc.SelectNodes("document/data");
                    foreach (XmlNode node in nodes)
                    {
                        var guideID = node.GetNodeValue("GuideID");
                        var guideName = node.GetNodeValue("GuideName");
                        var mobile = node.GetNodeValue("GuideMobile");
                        var pwd = node.GetNodeValue("GuidePwd");
                        var en = DAL.Res_Guide.SingleOrDefault(x => x.ID == guideID);

                        if (en == null)
                        {
                            en = new DAL.Res_Guide();
                            en.ID = Guid.NewGuid().ToString();
                            guideID = en.ID;
                            en.DepartureID = Guid.Empty.ToString();
                            en.DepartureName = "";
                            en.GuideLevel = "初级";
                            en.Spell = EcanConvertToCh.GetFirstChar(guideName);
                            en.IsLeaderCard = false;
                            en.IsIDCard = false;
                            en.Language = "普通话";
                            en.TradeNum = 0;
                            en.TradeAmt = 0;
                            en.TradeAdultNum = 0;
                            en.TradeChildNum = 0;
                            en.OrderIndex = 0;
                            en.CreateUserName = user.UserName;
                            en.CreateUserID = user.UserID;
                            en.CreateDate = DateTime.Now;
                            en.OrgID = user.OrgID;
                            en.DeptID = user.DeptID;
                            en.IsEnable = true;
                        }
                        en.Name = guideName;
                        en.Mobile = mobile;

                        en.Save();

                        if (string.IsNullOrEmpty(guideName) && string.IsNullOrEmpty(mobile) && string.IsNullOrEmpty(pwd))
                        {
                            continue;
                        }
                        else
                        {
                            var e = new DAL.Ord_OrderGuide();
                            e.ID = Guid.NewGuid().ToString();
                            e.OrderID = orderID;
                            e.GuideName = guideName;
                            e.GuideID = guideID;
                            e.Mobile = mobile;
                            e.AcctPwd = pwd;
                            e.OrgID = user.OrgID;
                            e.CreateDate = DateTime.Now;
                            e.Save();
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                BizUtility.ExceptionHandler(user, ex, "安排导游时发生错误");
                isRec = false;
            }
            return isRec;
        }

        /// <summary>
        /// 团队订单安排的导游列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<DAL.Ord_OrderGuide> GetOrderGuide(string orderID)
        {
            return DAL.Ord_OrderGuide.Find(x => x.OrgID == AuthenticationPage.UserInfo.OrgID && x.OrderID == orderID).ToList();
        }

        public DAL.Ord_OrderGuide Get(string keyID)
        {
            return DAL.Ord_OrderGuide.SingleOrDefault(x => x.ID == keyID);
        }

        /// <summary>
        /// 导游报账登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pwd"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public bool GuideLogin(string mobile, string pwd, string orgID)
        {
            mobile = HtmlHeler.NoHTML(mobile);
            pwd = HtmlHeler.NoHTML(pwd);
            return DAL.Ord_OrderGuide.Exists(x => x.Mobile == mobile && x.AcctPwd == pwd && x.OrgID == orgID);
        }
    }
}
