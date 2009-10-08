using System;
using System.Globalization;
using System.Threading;

namespace FarsiLibrary.WebDemo
{
    public partial class Styles : System.Web.UI.Page
    {
        private void SetPersianCulture()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-ir");
        }

        protected override void OnInit(EventArgs e)
        {
            SetPersianCulture();
            base.OnInit(e);
        }
    }
}
