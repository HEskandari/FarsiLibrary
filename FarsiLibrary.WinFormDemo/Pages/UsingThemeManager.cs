using System;
using FarsiLibrary.Win;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class UsingThemeManager : DemoBase
    {
        public UsingThemeManager()
        {
            InitializeComponent();
        }

        private void faButton2_Click(object sender, EventArgs e)
        {
            FAThemeManager.Theme = ThemeTypes.WindowsXP;
        }

        private void faButton1_Click(object sender, EventArgs e)
        {
            FAThemeManager.Theme = ThemeTypes.Office2000;
        }

        private void faButton3_Click(object sender, EventArgs e)
        {
            FAThemeManager.Theme = ThemeTypes.Office2003;
        }
    }
}