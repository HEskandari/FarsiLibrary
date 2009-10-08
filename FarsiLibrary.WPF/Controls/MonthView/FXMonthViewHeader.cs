using System.Windows;
using FarsiLibrary.WPF.Base;

namespace FarsiLibrary.WPF.Controls
{
    public class FXMonthViewHeader : TextCell
    {
        /// <summary>
        /// Ctor
        /// </summary>
        static FXMonthViewHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXMonthViewHeader), new FrameworkPropertyMetadata(typeof(FXMonthViewHeader)));
        }
    }
}