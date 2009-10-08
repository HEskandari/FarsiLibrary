using System;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class Cultures : DemoBase
    {
        #region Ctor

        public Cultures()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void btnFarsi_Click(object sender, EventArgs e)
        {
            faMonthView.DefaultCalendar = faMonthView.PersianCalendar;
            faMonthView.DefaultCulture = faMonthView.PersianCulture;
        }

        private void btnArabic_Click(object sender, EventArgs e)
        {
            faMonthView.DefaultCalendar = faMonthView.HijriCalendar;
            faMonthView.DefaultCulture = faMonthView.ArabicCulture;
        }

        private void btnInvariant_Click(object sender, EventArgs e)
        {
            faMonthView.DefaultCalendar = faMonthView.InvariantCalendar;
            faMonthView.DefaultCulture = faMonthView.InvariantCulture;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            faMonthView.DefaultCalendar = null;
            faMonthView.DefaultCulture = null;
        }

        #endregion
    }
}