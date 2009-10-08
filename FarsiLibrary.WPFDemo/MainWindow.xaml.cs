using System.Windows;
using System.Windows.Controls;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.WPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ThemeInfo prevTheme;
        private bool isChangingTheme;

        public MainWindow()
        {
            InitializeComponent();
        }

        public App MyApplication
        {
            get { return Application.Current as App; }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(isChangingTheme)
                return;

            try
            {
                isChangingTheme = true;
                ComboBoxItem item = cmbThemes.SelectedValue as ComboBoxItem;
                ThemeInfo theme;

                if (item != null && item.Tag != null)
                {
                    var themeName = (string) item.Tag;
                    theme = MyApplication.AvailableThemes[themeName];
                }
                else
                {
                    //Default Theme
                    if(!MyApplication.AvailableThemes.TryGetValue(ThemeWrapper.CurrentThemeName, out theme))
                    {
                        theme = MyApplication.AvailableThemes[App.Theme_Aero];
                    }
                }

                Application.Current.Resources = theme.SkinTheme;

                if (prevTheme != null && Application.Current.Resources.MergedDictionaries.Contains(prevTheme.SystemTheme))
                    Application.Current.Resources.MergedDictionaries.Remove(prevTheme.SystemTheme);

                if (!Application.Current.Resources.MergedDictionaries.Contains(theme.SystemTheme))
                    Application.Current.Resources.MergedDictionaries.Add(theme.SystemTheme);

                prevTheme = theme;
            }
            finally
            {
                isChangingTheme = false;
            }
        }
    }
}
