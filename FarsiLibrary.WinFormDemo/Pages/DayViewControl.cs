using System;
using System.Drawing;
using FarsiLibrary.Win.Events;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DayViewControl : DemoBase
    {
        public DayViewControl()
        {
            InitializeComponent();
        }

        private void faMonthView1_SelectedDateTimeChanged(object sender, EventArgs e)
        {
            this.faDayView1.SelectedDateTime = faMonthView1.SelectedDateTime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            faMonthView1.Enabled = !faMonthView1.Enabled;
            faDayView1.Enabled = !faDayView1.Enabled;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            faMonthView1.ShowEmptyButton = !faMonthView1.ShowEmptyButton;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            faMonthView1.ShowTodayButton = !faMonthView1.ShowTodayButton;
        }

        private void frm22_Load(object sender, EventArgs e)
        {
            faMonthView1.SelectedDateTime = faDayView1.SelectedDateTime = DateTime.Now.Date;
        }

        private void faDayView1_Draw(object sender, CustomDrawEventArgs e)
        {
            var dayNumber = faDayView1.DefaultCalendar.GetDayOfYear(faDayView1.ViewDateTime);
            if(dayNumber == 1)
            {
                using(var fmt = new StringFormat())
                {
                    fmt.Alignment = StringAlignment.Center;
                    fmt.LineAlignment = StringAlignment.Center;

                    e.Graphics.FillRectangle(Brushes.Red, e.Rectangle);
                    e.Graphics.DrawString("Happy New Year", faDayView1.DayFont, Brushes.Black, e.Rectangle, fmt);
                }
                e.Handled = true;
            }
        }
    }
}