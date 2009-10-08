using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.WPFDemo
{
    /// <summary>
    /// Interaction logic for Language.xaml
    /// </summary>
    public partial class Language
    {
        public Language()
        {
            InitializeComponent();
        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            if (cmbLanguage.SelectedItem != null)
            {
                ComboBoxItem item = cmbLanguage.SelectedItem as ComboBoxItem;
                System.Diagnostics.Debug.Assert(item != null);

                string dir = item.Content as string;
                if (dir == "English")
                {
                    SetCulture(new CultureInfo("en-US"));
                }
                else if (dir == "Persian")
                {
                    SetCulture(new PersianCultureInfo());
                }
                else if (dir == "Arabic")
                {
                    SetCulture(CultureHelper.ArabicCulture);
                }
            }

            MainWindow window = new MainWindow();
            Application.Current.MainWindow = window;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
            Close();
        }

        private static void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
