using System;
using System.Collections.Generic;
using System.Windows;

namespace FarsiLibrary.WPFDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Theme Names

        public const string Theme_Generic = "generic";
        public const string Theme_Aero = "aero.normalcolor";

        #endregion

        #region Fields

        private readonly Dictionary<string, ThemeInfo> _themeList;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public App()
        {
            _themeList = new Dictionary<string, ThemeInfo>();
        }

        #endregion

        #region Props

        public Dictionary<string, ThemeInfo> AvailableThemes
        {
            get { return _themeList; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Application Startup
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            _themeList.Add(Theme_Aero, GetTheme(Theme_Aero));

            base.OnStartup(e);
        }

        private ResourceDictionary GetSystemTheme(string name)
        {
            Uri uri = null;

            switch (name)
            {
                case Theme_Aero:
                    uri = new Uri("/PresentationFramework.Aero;component/themes/Aero.NormalColor.xaml", UriKind.Relative);
                    break;

            }

            if (uri != null)
                return LoadComponent(uri) as ResourceDictionary;

            return null;
        }

        private ThemeInfo GetTheme(string themeName)
        {
            Uri uri = new Uri(string.Format(@"FarsiLibrary.WPF;;;component/themes/{0}.xaml", themeName), UriKind.Relative);
            ResourceDictionary skinTheme = LoadComponent(uri) as ResourceDictionary;
            ResourceDictionary systemTheme = GetSystemTheme(themeName);

            return new ThemeInfo(themeName, skinTheme, systemTheme);
        }

        #endregion
    }

    #region ThemeInfo

    public class ThemeInfo
    {
        public ResourceDictionary SkinTheme;
        public ResourceDictionary SystemTheme;
        public string Name;

        public ThemeInfo(string name, ResourceDictionary skinTheme, ResourceDictionary systemTheme)
        {
            SkinTheme = skinTheme;
            SystemTheme = systemTheme;
            Name = name;
        }
    }

    #endregion
}
