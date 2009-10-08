using System;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class MultiViewControl : DemoBase
    {
        private int currentTheme = (int)ThemeTypes.Office2000;

        public MultiViewControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.faMultiView1.View = ViewType.Month;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.faMultiView1.View = ViewType.Day;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentTheme > 2)
            {
                currentTheme = 0;
                faMultiView1.Theme = ThemeTypes.Office2000;
            }
            else
            {
                currentTheme++;
                faMultiView1.Theme = (ThemeTypes)currentTheme;
            }
        }

        private void faMultiView1_SelectedDateTimeChanged(object sender, EventArgs e)
        {
            if(faMultiView1.SelectedDateTime.HasValue)
            {
                lblSelectedDate.Text = faMultiView1.SelectedDateTime.Value.ToString();
            }
            else
            {
                lblSelectedDate.Text = "[Empty]";
            }
        }
    }
}