using System;
using System.Windows;
using System.Windows.Controls;

namespace FarsiLibrary.WPFDemo.Pages
{
    /// <summary>
    /// Interaction logic for DatePicker.xaml
    /// </summary>
    public partial class DatePicker
    {
        public DatePicker()
        {
            InitializeComponent();
        }

        private void cmbDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDirection.SelectedItem != null)
            {
                ComboBoxItem item = cmbDirection.SelectedItem as ComboBoxItem;
                System.Diagnostics.Debug.Assert(item != null);

                string dir = item.Tag as string;
                if (dir == "LTR")
                {
                    dp.FlowDirection = FlowDirection.LeftToRight;
                }
                else if (dir == "RTL")
                {
                    dp.FlowDirection = FlowDirection.RightToLeft;
                }
            }
        }

        private void SetDateToNull(object sender, RoutedEventArgs e)
        {
            dp.SetNoneDate();
        }

        private void SetDateToValue(object sender, RoutedEventArgs e)
        {
            dp.SelectedDateTime = new DateTime(2008, 2, 25);
        }

        private void SetDateToToday(object sender, RoutedEventArgs e)
        {
            dp.SetTodayDate();
        }
    }
}
