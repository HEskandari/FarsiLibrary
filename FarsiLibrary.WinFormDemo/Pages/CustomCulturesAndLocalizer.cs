using System;
using System.Globalization;
using FarsiLibrary.Localization;
using FarsiLibrary.WinFormDemo.Data;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class CustomCultureAndLocalizer : DemoBase
    {
        #region Ctor

        public CustomCultureAndLocalizer()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void btnCustom_Click(object sender, EventArgs e)
        {
            FALocalizeManager.Instance.CustomCulture = new CultureInfo("es-ES");
            FALocalizeManager.Instance.CustomLocalizer = new ESLocalizer();

            faDatePicker.SelectedDateTime = DateTime.Now;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            FALocalizeManager.Instance.CustomCulture = null;
            FALocalizeManager.Instance.CustomLocalizer = null;

            faDatePicker.SelectedDateTime = DateTime.Now;
        }

        #endregion
    }
}