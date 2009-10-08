using System;
using FarsiLibrary.Win.Enums;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;

namespace FarsiLibrary.Win
{
    public class FAThemeManager
    {
        #region Fields

        private static ThemeTypes theme;
        public static event EventHandler ManagerThemeChanged;
        private static bool useGlobalThemes;
        
        #endregion

        #region Ctor

        private FAThemeManager()
        {
        }

        #endregion

        #region Props

        /// <summary>
        /// Determines if the current global theme is set.
        /// </summary>
        public static bool UseGlobalThemes
        {
            get { return useGlobalThemes; }
        }

        /// <summary>
        /// Currently selected theme.
        /// </summary>
        public static ThemeTypes Theme
        {
            get
            {
                if (UseThemes == false)
                    theme = ThemeTypes.Office2000;

                return theme;
            }
            set
            {
                theme = !UseThemes ? ThemeTypes.Office2000 : value;
                useGlobalThemes = true;
                OnManagerThemeChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Checks if the control can paint itself using styles. Styles are only available on WindowsXP or 
        /// greater, and should be enabled by the developer, using <see cref="Application.RenderWithVisualStyles">RenderWithVisualStyles</see> property of <see cref="Application">Application</see> class and
        /// by the system administrator by enabling windows theming.
        /// </summary>
        public static bool UseThemes
        {
            get
            {
                return VisualStyleInformation.IsSupportedByOS &&
                       VisualStyleInformation.IsEnabledByUser && 
                       Application.RenderWithVisualStyles;
            }
        }

        #endregion

        #region Methods

        protected internal static void OnManagerThemeChanged(EventArgs e)
        {
            if (ManagerThemeChanged != null)
                ManagerThemeChanged(null, e);
        }

        #endregion
    }
}
