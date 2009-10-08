using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using FarsiLibrary.Utils;
using FarsiLibrary.Win.Events;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DayCustomDrawing : DemoBase
    {
        #region Ctor

        public DayCustomDrawing()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            faMonthView.DefaultCalendar = faMonthView.PersianCalendar;
            faMonthView.DefaultCulture = faMonthView.PersianCulture;

            faMonthView.SelectedDateTime = PersianDate.Now;
        }

        private void faMonthView_DrawCurrentDay(object sender, CustomDrawDayEventArgs e)
        {
            //Remember to set IsHandled = true if you're doing the drawings, unless
            //the control will overpaint on your job.
            if(e.Year == 1384 && e.Month == 4 && e.Day == 8)
            {
                using(SolidBrush br1 = new SolidBrush(Color.Wheat))
                using(LinearGradientBrush br2 = new LinearGradientBrush(e.Rectangle, Color.DeepSkyBlue, Color.DarkSlateBlue, 45, true))
                using(StringFormat fmt = new StringFormat())
                using(Font font = new Font("Tahoma", 8, FontStyle.Bold))
                {
                    string dayNo = toFarsi.Convert(e.Day.ToString());
                    
                    fmt.Alignment = StringAlignment.Center;
                    fmt.LineAlignment = StringAlignment.Center;
                    e.Graphics.FillRectangle(br2, e.Rectangle);
                    e.Graphics.DrawString(dayNo, font, br1, e.Rectangle, fmt);
                    
                    if(e.IsToday)
                        faMonthView.Painter.DrawFocusRect(e.Graphics, e.Rectangle);
                }

                e.Handled = true;
            }
            else if(e.Month == 1 && e.Day == 1)
            {
                using(SolidBrush br1 = new SolidBrush(Color.Black))
                using(SolidBrush br2 = new SolidBrush(Color.DarkSeaGreen))
                using(StringFormat fmt = new StringFormat())
                using(Pen p = new Pen(Color.DarkGreen))
                using(Font font = new Font("Tahoma", 9, FontStyle.Italic))
                {
                    string dayNo = toFarsi.Convert(e.Day.ToString());
                    
                    fmt.Alignment = StringAlignment.Center;
                    fmt.LineAlignment = StringAlignment.Center;
                    
                    e.Graphics.FillRectangle(br2, e.Rectangle);
                    e.Graphics.DrawString(dayNo, font, br1, e.Rectangle, fmt);
                    e.Graphics.DrawRectangle(p, e.Rectangle);
                }

                e.Handled = true;
            }
            else if(e.Date.DayOfWeek == DayOfWeek.Friday)
            {
                using(Pen p = new Pen(Color.Red))
                using(SolidBrush br = new SolidBrush(Color.Red))
                using(Font font = new Font("Tahoma", 9, FontStyle.Bold))
                using (StringFormat fmt = new StringFormat())
                {
                    string dayNo = toFarsi.Convert(e.Day.ToString());

                    fmt.Alignment = StringAlignment.Center;
                    fmt.LineAlignment = StringAlignment.Center;

                    e.Graphics.DrawString(dayNo, font, br, e.Rectangle, fmt);

                    e.Handled = true;
                }                
            }
        }

        #endregion
    }
}