using System;
using System.Globalization;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DateConversion : DemoBase
    {
        #region Ctor

        public DateConversion()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void btnToday_Click(object sender, EventArgs e)
        {
            lblTodayGregorian.Text = DateTime.Now.ToString("d", CultureHelper.NeutralCulture);
            lblTodayPersian.Text = DateTime.Now.ToPersianDate().ToString("d");
            lblTodayPersianDate.Text = PersianDate.Now.ToWritten();
            lblPersianDateCtor.Text = new PersianDate(DateTime.Now).ToString("G");
            lblDirectCast.Text = ((PersianDate) DateTime.Now).ToWritten();
        }

        #endregion
    }
}