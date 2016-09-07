using System;
using DRP.Framework.Core;
using DRP.Framework;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web;


namespace DRP.BF
{
    public partial class Pagebase : AuthenticationPage
    {
        #region 清空浏览器缓存
        private bool __isClearPageCache = false;
        /// <summary>
        /// 是否清空客户端的浏览器缓存，默认非清空
        /// </summary>
        protected virtual bool IsCleaPageCache
        {
            get { return __isClearPageCache; }
        }
        /// <summary>
        /// 清空客户端的浏览器缓存
        /// </summary>
        private void ClearPageCache()
        {
            Response.Buffer = true;
            Response.Expires = 0;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";
        }
        #endregion

        #region 参数属性

        /// <summary>
        /// 传递参数ID
        /// </summary>
        protected string KeyID
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["ID"]))
                    return Guid.NewGuid().ToString();
                else
                    return Request.QueryString["ID"];
            }
        }

        /// <summary>
        /// 传递参数
        /// </summary>
        protected int KeyIntID
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["ID"]))
                    return -1;
                else
                    return Request.QueryString["ID"].ToInt();
            }
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        protected EnumConst.OperationAction Mode
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["action"]))
                    return DRP.Framework.EnumConst.OperationAction.View;
                else
                {
                    return (EnumConst.OperationAction)Request.QueryString["action"].ToInt();
                }
            }
        }

        #endregion

        #region 显示明细数据
        /// <summary>
        /// 载入数据
        /// </summary>
        protected virtual void LoadData<T>(T entity, System.Web.UI.Control pnlContainer) where T : class
        {
            if (entity != null && pnlContainer != null)
            {
                var e = new ReflectHelper<T>();
                e.BindDataToControl(entity, pnlContainer);
            }
        }
        #endregion

        #region 微站不加载JS与CSS

        private bool __IsWechat = false;

        /// <summary>
        /// 是否是微站
        /// </summary>
        protected virtual bool IsWechat
        {
            get
            {
                return __IsWechat;
            }
        }

        #endregion
         
        #region 加载Js与Css
        /// <summary>
        /// 加载Js与Css
        /// </summary>
        private void LoadMedia()
        {
            if (IsWechat) return;

            var theme = CookieHelper.GetCookieValue("DRP_TMS_THEMES_" + UserInfo.UserID);
            var path = "default";
            var cssName = "layoutui-blue.css";
            if (!string.IsNullOrEmpty(theme))
            {
                switch (theme.ToUpper())
                {
                    case "GRAY":
                        path = "bootstrap";
                        cssName = "layoutui-gray.css";
                        break;
                    case "BLUE":
                        path = "default";
                        cssName = "layoutui-blue.css";
                        break;
                    default:
                        path = "default";
                        cssName = "layoutui-blue.css";
                        break;
                }
            }
            var domain = ConfigHelper.GetAppSettingValue("MediumDomain");
            if (string.IsNullOrEmpty(domain)) domain = "/";
            LoadCssFile(1, string.Format("{0}UI/themes/default/{1}", domain, cssName));
            LoadCssFile(2, string.Format("{0}UI/themes/{1}/easyui.css", domain, path));
            LoadCssFile(3, string.Format("{0}UI/themes/icon.css", domain));

            LoadJsFile(4, string.Format("{0}Scripts/jquery-1.7.1.min.js", domain));
            LoadJsFile(5, string.Format("{0}UI/jquery.easyui.min.js", domain));
            LoadJsFile(6, string.Format("{0}UI/locale/easyui-lang-zh_CN.js", domain));
            LoadJsFile(7, string.Format("{0}UI/plugins/jquery.extendvalid.js", domain));
            LoadJsFile(8, string.Format("{0}Scripts/drp.core.js", domain));
        }

        private void LoadCssFile(int index, string cssFilePath)
        {
            HtmlGenericControl child = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            child.Attributes.Add("href", cssFilePath);
            child.Attributes.Add("rel", "stylesheet");
            child.Attributes.Add("type", "text/css");
            Page handler = (Page)HttpContext.Current.Handler;
            handler.Header.Controls.AddAt(index, child);
        }

        private void LoadJsFile(int index, string jsFilePath)
        {
            HtmlGenericControl child = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            child.Attributes.Add("src", jsFilePath);
            child.Attributes.Add("type", "text/javascript");
            Page handler = (Page)HttpContext.Current.Handler;
            handler.Header.Controls.AddAt(index, child);
        }

        #endregion
  
        protected override void OnPreLoad(EventArgs e)
        {
            LoadMedia(); 
            base.OnPreLoad(e);
        }

        protected override void OnPreInit(EventArgs e)
        {
            if (IsCleaPageCache)
                ClearPageCache();
            base.OnPreInit(e);
        }

        /// <summary>
        /// 清空页面控件的值　
        /// </summary>
        /// <param name="ctrl"></param>
        protected virtual void ClearControl(System.Web.UI.Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c.HasControls())
                {
                    ClearControl(c);
                }
                else
                {
                    if (c.GetType() == typeof(TextBox))
                        ((TextBox)c).Text = "";
                    if (c.GetType() == typeof(DropDownList))
                        ((DropDownList)c).SelectedIndex = 0;
                }
            }
        }
    }
}
