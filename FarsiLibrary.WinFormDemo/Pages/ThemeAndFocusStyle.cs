using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class ThemeAndFocusStyle : DemoBase
    {
        private int currentTheme = (int)ThemeTypes.Office2000;

        public ThemeAndFocusStyle()
        {
            InitializeComponent();
        }

        private void btnChangeTheme_Click(object sender, EventArgs e)
        {
            if (currentTheme > 2)
            {
                currentTheme = 0;
                faMonthView.Theme = ThemeTypes.Office2000;
            }
            else
            {
                currentTheme++;
                faMonthView.Theme = (ThemeTypes)currentTheme;
            }
        }

        private void btnVisualStyles_Click(object sender, EventArgs e)
        {
            if (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled)
            {
                Application.VisualStyleState = VisualStyleState.NoneEnabled;
            }
            else
            {
                Application.VisualStyleState = VisualStyleState.ClientAndNonClientAreasEnabled;
            }
        }

        private void btnToggleBorder_Click(object sender, EventArgs e)
        {
            faMonthView.ShowBorder = !faMonthView.ShowBorder;
        }

        private void btnToggleFocusRect_Click(object sender, EventArgs e)
        {
            faMonthView.ShowFocusRect = !faMonthView.ShowFocusRect;
        }
    }
}