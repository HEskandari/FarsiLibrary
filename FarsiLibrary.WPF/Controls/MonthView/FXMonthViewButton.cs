using System.Windows;
using System.Windows.Controls;

namespace FarsiLibrary.WPF.Controls
{
    public class FXMonthViewButton : Button
    {
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        static FXMonthViewButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXMonthViewButton), new FrameworkPropertyMetadata(typeof(FXMonthViewButton)));
        }

        #endregion
    }
}