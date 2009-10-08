using System;
using FarsiLibrary.Utils;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class Casting : DemoBase
    {
        #region Ctor

        public Casting()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void btnCalc_Click(object sender, EventArgs e)
        {
            lblCastTo.Text = ((PersianDate) DateTime.Now).ToString("G");
            lblCastFrom.Text = ((DateTime) PersianDate.Now).ToString("G");

            lblDTMinValue.Text = ((PersianDate) DateTime.MaxValue).ToString("G");
            lblPDMinValue.Text = ((DateTime) PersianDate.MinValue).ToString("G");
        }

        #endregion
    }
}