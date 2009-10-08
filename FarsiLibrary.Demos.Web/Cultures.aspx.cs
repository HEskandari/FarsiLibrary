using System;
using System.Globalization;
using System.Threading;
using FarsiLibrary.Utils;

namespace FarsiLibrary.WebDemo
{
    public partial class Cultures : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCurrentDate();

            if(!Page.IsPostBack)
                SetThreadCulture();
        }

        private void SetCurrentDate()
        {
            if(mv.SelectedDate != DateTime.MinValue)
            {
                currentDate.Text = mv.SelectedDate.ToString();
                currentPersianDate.Text = toFarsi.Convert(((PersianDate)mv.SelectedDate).ToWritten());
            }
            else
            {
                currentDate.Text = currentPersianDate.Text = "[Empty]";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            SetThreadCulture();
            base.OnInit(e);
        }

        protected void mv_SelectionChanged(object sender, EventArgs e)
        {
            SetCurrentDate();
        }

        protected void SetThreadCulture(object sender, EventArgs e)
        {
            if(sender == this.culturePersian)
            {
                CurrentCulture = new CultureInfo("fa-ir");
            }
            else if(sender == this.cultureArabic)
            {
                CurrentCulture = new CultureInfo("ar-sa");
            }
            else
            {
                CurrentCulture = CultureInfo.InvariantCulture;
            }

            SetThreadCulture();
        }

        private void SetThreadCulture()
        {
            if (CurrentCulture != null)
                Thread.CurrentThread.CurrentUICulture = CurrentCulture;
        }

        public CultureInfo CurrentCulture
        {
            get { return Session["CurrentCulture"] as CultureInfo; }
            set { Session["CurrentCulture"] = value; }
        }
    }
}
