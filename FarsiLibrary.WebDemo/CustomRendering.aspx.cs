using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using FarsiLibrary.Utils;
using FarsiLibrary.Web;

namespace FarsiLibrary.WebDemo
{
    public partial class CustomRendering : System.Web.UI.Page
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

        protected void mv_RenderCalendarCell(object sender, RenderCalendarCellArgs e)
        {
            PersianDate pd = e.DayInfo.Date;
            if(pd.Day == 1 && pd.Month == 1)
            {
                var writer = e.Writer;

                writer.Write("<a href='http://en.wikipedia.org/wiki/Nowrooz' ");
                writer.Write(" style='color:red; background:red;'>");
                writer.Write(e.Text);
                writer.Write("</a>");

                e.Handled = true;
            }
        }
    }
}
