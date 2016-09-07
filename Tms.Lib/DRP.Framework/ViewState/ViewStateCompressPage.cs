using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using DRP.Framework.Core;
using System.Text.RegularExpressions;
using System.Web; 

namespace DRP.Framework.ViewState
{
    /// <summary>
    /// 压缩ViewState
    /// </summary>
    public class ViewStateCompressPage : System.Web.UI.Page
    {
        private const string HiddenFieldName = "__DRPSTATE";

        private const string HiddenFieldFlag = "__DRPSTATEFLAG";

        /// <summary>
        /// 当ViewState大于1096时才压缩
        /// </summary>
        /// <param name="state"></param>
        protected override void SavePageStateToPersistenceMedium(object state)
        {
            LosFormatter mFormat = new LosFormatter();
            using (StringWriter mWriter = new StringWriter())
            {
                mFormat.Serialize(mWriter, state);
                var mViewStateStr = mWriter.ToString();
                var stateFlag = false;
                if (mViewStateStr.Length > 1096)
                {
                    byte[] pBytes = System.Convert.FromBase64String(mViewStateStr);
                    pBytes = Zip.Compress(pBytes);
                    mViewStateStr = System.Convert.ToBase64String(pBytes);
                    stateFlag = true;
                }
                ClientScript.RegisterHiddenField(HiddenFieldName, mViewStateStr);
                ClientScript.RegisterHiddenField(HiddenFieldFlag, stateFlag.ToString().ToLower());//注册压缩的标志
            }
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            var vState = this.Request.Form.Get(HiddenFieldName);
            var flag = this.Request.Form.Get(HiddenFieldFlag);
            byte[] pBytes = System.Convert.FromBase64String(vState);
            if (flag.Equals("true"))
            { 
                pBytes = Zip.DeCompress(pBytes);
            }
            LosFormatter mFormat = new LosFormatter();
            return mFormat.Deserialize(System.Convert.ToBase64String(pBytes));
        }

        protected override void OnPreLoad(EventArgs e)
        { 
            base.OnPreLoad(e);
        } 
    }
}
