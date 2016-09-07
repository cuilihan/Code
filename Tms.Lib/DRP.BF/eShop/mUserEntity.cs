using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.BF.eShop
{
    /// <summary>
    /// 微网用户实体
    /// </summary>
    [Serializable]
    public class mUserEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserAcct { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrgID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; }
         
        /// <summary>
        /// 用户微信AppID(保留字段)
        /// </summary>
        public string WechatAppID { get; set; }
    }
}
