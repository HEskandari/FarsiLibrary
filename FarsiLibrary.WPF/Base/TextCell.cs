using System.Windows;
using System.Windows.Controls;

namespace FarsiLibrary.WPF.Base
{
    public abstract class TextCell : ContentControl
    {
        #region DependencyProperty

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(TextCell), new UIPropertyMetadata(false));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextCell), new UIPropertyMetadata(string.Empty));
        public static readonly DependencyProperty CellCornerRadiusProperty = DependencyProperty.Register("CellCornerRadius", typeof(CornerRadius), typeof(TextCell), new UIPropertyMetadata(null));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(TextCell), new UIPropertyMetadata(null));

        #endregion
 
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        static TextCell()
        {
            FocusableProperty.OverrideMetadata(typeof(TextCell), new FrameworkPropertyMetadata(false));
            Border.CornerRadiusProperty.AddOwner(typeof(TextCell));
        }

        #endregion

        #region Props

        /// <summary>
        /// IsSelected
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// CornerRadius
        /// </summary>
        public CornerRadius CellCornerRadius
        {
            get { return (CornerRadius)GetValue(CellCornerRadiusProperty); }
            set { SetValue(CellCornerRadiusProperty, value); }
        }

        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// TextAlignment
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        #endregion
    }
}