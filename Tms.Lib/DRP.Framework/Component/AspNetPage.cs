using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wuqi.Webdiyer;
using System.Web.UI;

namespace DRP.Framework.Component
{ 
    /// <summary>
    /// 扩展aspnetpager组件，应用于DRP项目
    /// </summary>   
    public class AspNetPage:AspNetPager
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.CssClass = "paginator";
            base.FirstPageText = "首页";
            base.LastPageText = "尾页";
            base.CurrentPageButtonClass = "cpb";
            base.NextPageText = "下一页";
            base.PrevPageText = "上一页";
            base.PageIndexBoxType = PageIndexBoxType.TextBox;
            base.ShowPageIndexBox = ShowPageIndexBox.Never;
            base.SubmitButtonText = "GO";
            base.TextAfterPageIndexBox = "页";
            base.TextBeforePageIndexBox = "转到";
            base.AlwaysShow = true;
            base.Render(writer);
        }
    }
}
