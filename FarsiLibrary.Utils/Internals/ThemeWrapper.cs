using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FarsiLibrary.Utils.Internals
{
    /// <summary>
    /// A wrapper around Win32 Theming. Return which theme is
    /// currently active.
    /// </summary>
    public static class ThemeWrapper
    {
        #region Fields

        private static string _themeName;
        
        #endregion

        #region Native

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern bool IsThemeActive();

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        private static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int dwMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);

        #endregion

        #region Ctor

        static ThemeWrapper()
        {
            CreateThemeInfo();
        }

        #endregion

        #region Props

        public static string CurrentThemeName
        {
            get
            {
                if (string.IsNullOrEmpty(ThemeColor))
                    return ThemeName;

                return string.Format("{0}.{1}", ThemeName, ThemeColor);
            }
        }

        public static bool IsActive
        {
            get; private set;
        }

        private static string ThemeColor
        {
            get; set;
        }

        private static string ThemeName
        {
            get
            {
                if (_themeName == string.Empty)
                    return "classic";
                
                return Path.GetFileNameWithoutExtension(_themeName);
            }
            set { _themeName = value; }
        }

        #endregion

        #region Methods

        private static void EnsureThemeName()
        {
            var sbTheme = new StringBuilder(260);
            var sbColor = new StringBuilder(260);

            if (GetCurrentThemeName(sbTheme, sbTheme.Capacity, sbColor, sbColor.Capacity, null, 0) == 0)
            {
                ThemeName = sbTheme.ToString().ToLower();
                ThemeColor = sbColor.ToString().ToLower();
            }
            else
            {
                ThemeName = ThemeColor = string.Empty;
            }
        }
        
        private static void CreateThemeInfo()
        {
            IsActive = IsThemeActive();
            EnsureThemeName();
        }

        #endregion
    }
}
