using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using FarsiLibrary.LiveSamples.Localization;
using System.Windows.Data;

namespace FarsiLibrary.LiveSamples
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class RootPage : Page
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
            if(item == null)
                return;

            var tag = item.Tag.ToString();
            if(tag == null)
                return;

            var culture = new CultureInfo(tag);

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            LocalizeDictionary.Instance.Culture = culture;

            RefreshCurrentPage();
        }

        private void RefreshCurrentPage()
        {
            if(ContentFrame == null)
                return;

            var page = ContentFrame.Content as Page;
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
