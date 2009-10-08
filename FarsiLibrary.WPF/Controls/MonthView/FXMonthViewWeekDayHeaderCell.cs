using System.Windows;
using FarsiLibrary.WPF.Base;

namespace FarsiLibrary.WPF.Controls
{
    public class FXMonthViewWeekDayHeaderCell : TextCell
    {
        #region Dependency Property

        public static readonly DependencyProperty DayNoProperty = DependencyProperty.Register("DayNo", typeof(int), typeof(FXMonthViewWeekDayHeaderCell));
        
        #endregion

        #region Ctor

        static FXMonthViewWeekDayHeaderCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXMonthViewWeekDayHeaderCell), new FrameworkPropertyMetadata(typeof(FXMonthViewWeekDayHeaderCell)));
        }

        #endregion

        #region Props

        /// <summary>
        /// DayNo
        /// </summary>
        public int DayNo
        {
            get { return (int)GetValue(DayNoProperty); }
            set { SetValue(DayNoProperty, value); }
        }

        #endregion
    }
}