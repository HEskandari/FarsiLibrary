using System.Windows;
using FarsiLibrary.LiveSamples.Localization;

namespace FarsiLibrary.LiveSamples
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Application Startup
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            LocalizeDictionary.ResourcesName = "Resources";
            LocalizeDictionary.AssemblyName = "FarsiLibrary.LiveSamples";

            base.OnStartup(e);
        }
    }
}
