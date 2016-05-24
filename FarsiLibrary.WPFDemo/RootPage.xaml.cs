using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using FarsiLibrary.WPFDemo.Localization;

namespace FarsiLibrary.WPFDemo
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class RootPage
    {
        public RootPage()
        {
            InitializeComponent();
        }

        public App MyApplication
        {
            get { return Application.Current as App; }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cmbLanguage.SelectedItem as ComboBoxItem;
            var tag = item?.Tag?.ToString();

            if (tag == null) return;

            var culture = new CultureInfo(tag);

            Thread.CurrentThread.CurrentUICulture = culture;
            //Note: Commented out for Security reasons. When run in
            //Partial trust mode, you can not set CurrentCulture property.
            //Thread.CurrentThread.CurrentCulture = culture;
            LocalizeDictionary.Instance.Culture = culture;

            RefreshCurrentPage();
        }

        private void RefreshCurrentPage()
        {
            var page = ContentFrame?.Content as Page;
            if (page != null)
            {
                page.FlowDirection = LocalizeDictionary.Instance.Culture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
                
                ControlTemplate template = page.Template;
                page.Template = null;
                page.Template = template;
            }
        }
    }
}
