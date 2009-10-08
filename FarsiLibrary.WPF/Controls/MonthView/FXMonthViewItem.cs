using System.Windows;
using System.Windows.Controls;

namespace FarsiLibrary.WPF.Controls
{
    public class FXMonthViewItem : ListBoxItem
    {
        static FXMonthViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXMonthViewItem), new FrameworkPropertyMetadata(typeof(FXMonthViewItem)));
        }

        /// <summary>
        /// Override right-click item selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
        }
    }
}