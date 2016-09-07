using DRP.BF.eShop;
using DRP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DRP.eShop.Module.My
{
    public partial class Photo : mPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            var photo = base.CurrentUser.Photo;
            var src = string.IsNullOrEmpty(photo) ? "/Files/Sys/Default.jpg" : photo;
            lblPhoto.Text = string.Format("<img id='imgPhoto' src='{0}' />", src);
            lblMalePhoto.Text = BindPhoto("male");
            lblFemalePhoto.Text = BindPhoto("female");
            lblOtherPhoto.Text = BindPhoto("other");
        }

        private string BindPhoto(string folder)
        {
            var path = Server.MapPath("/Files/Photo/") + folder;
            var files = new FileManager().GetFileItems(path);
            var sb = new StringBuilder();
            files.ForEach(x =>
            {
                sb.AppendFormat("<img src='/Files/Photo/{0}/{1}' />", folder, x.Name);
            });
            return sb.ToString();
        }

        protected override string LocationName
        {
            get
            {
                return "更换头像";
            }
        }
    }
}