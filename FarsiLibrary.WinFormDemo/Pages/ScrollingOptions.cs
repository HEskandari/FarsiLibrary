using System;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class ScrollingOptions : DemoBase
    {
        #region Ctor

        public ScrollingOptions()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void rbDays_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDays.Checked)
            {
                faMonthView1.ScrollOption = ScrollOptionTypes.Day;
                faMonthView1.Focus();
            }
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth.Checked)
            {
                faMonthView1.ScrollOption = ScrollOptionTypes.Month;
                faMonthView1.Focus();
            }
        }

        private void rbYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rbYear.Checked)
            {
                faMonthView1.ScrollOption = ScrollOptionTypes.Year;
                faMonthView1.Focus();
            }
        }

        #endregion
    }
}