using System;
using System.Windows;
using System.Windows.Controls;

namespace FarsiLibrary.WPFDemo.Pages
{
    /// <summary>
    /// Interaction logic for MonthView.xaml
    /// </summary>
    public partial class MonthView
    {
        public MonthView()
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
                    mv.FlowDirection = FlowDirection.LeftToRight;
                }
                else if (dir == "RTL")
                {
                    mv.FlowDirection = FlowDirection.RightToLeft;
                }
            }
        }

        private void SetDateToValue(object sender, RoutedEventArgs e)
        {
            mv.SelectedDateTime = new DateTime(2008, 2, 25);
        }

        private void SetDateToNull(object sender, RoutedEventArgs e)
        {
            mv.SetNoneDate();
        }

        private void SetDateToToday(object sender, RoutedEventArgs e)
        {
            mv.SetTodayDate();
        }
    }
}
