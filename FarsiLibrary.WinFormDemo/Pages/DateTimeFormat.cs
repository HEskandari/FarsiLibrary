using System;
using System.Globalization;
using FarsiLibrary.Utils;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DateTimeFormat : DemoBase
    {
        public DateTimeFormat()
        {
            InitializeComponent();
        }

        private void frm21_Load(object sender, EventArgs e)
        {
            var culture = new PersianCultureInfo();
            var calendar = culture.Calendar;
            

            lblDateTimeToString.Text = DateTime.Now.Date.ToString("d");
            lblMinDateTime.Text = calendar.MinSupportedDateTime.Date.ToString("d", CultureInfo.InvariantCulture);
            lblMaxDateTime.Text = calendar.MaxSupportedDateTime.Date.ToString("d", CultureInfo.InvariantCulture);
            lblDayNames.Text = GetNames(culture.DateTimeFormat.AbbreviatedDayNames);
            lblMonthNames.Text = GetNames(culture.DateTimeFormat.MonthNames);

        }

        private string GetNames(string[] list)
        {
            return string.Join(";", list);
        }
    }
}