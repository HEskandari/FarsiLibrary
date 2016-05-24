using System;
using System.Globalization;

namespace FarsiLibrary.Localization
{
    /// <summary>
    /// Localizer class to work with internal localized strings.
    /// </summary>
    public class FALocalizeManager
    {
        #region Fields

        private readonly FALocalizer fa = new FALocalizer();
        private readonly ARLocalizer ar = new ARLocalizer();
        private readonly ENLocalizer en = new ENLocalizer();
        private BaseLocalizer customLocalizer;
        private static FALocalizeManager instance;

        #endregion

        #region Ctor

        private FALocalizeManager()
        {
            FarsiCulture = new CultureInfo("fa-IR");
            ArabicCulture = new CultureInfo("ar-SA");
            InvariantCulture = CultureInfo.InvariantCulture;
        }

        #endregion

        #region Events

        /// <summary>
        /// Fired when Localizer has changed.
        /// </summary>
        public event EventHandler LocalizerChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Returns an instance of the localized based on CurrentUICulture of the thread.
        /// </summary>
        /// <returns></returns>
        public BaseLocalizer GetLocalizer()
        {
            return GetLocalizerByCulture(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Returns a localizer instance based on the culture.
        /// </summary>
        internal BaseLocalizer GetLocalizerByCulture(CultureInfo ci)
        {
            if (customLocalizer != null)
                return customLocalizer;
            
            if (ci.Equals(FarsiCulture))
            {
                return fa;
            }
            
            if (ci.Equals(ArabicCulture))
            {
                return ar;
            }
            
            return en;
        }

        #endregion

        #region Props

        /// <summary>
        /// Singleton Instance of FALocalizeManager.
        /// </summary>
        public static FALocalizeManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new FALocalizeManager();

                return instance;
            }
        }

        /// <summary>
        /// Custom culture, when set , is used across all controls.
        /// </summary>
        public CultureInfo CustomCulture
        {
            get;
            set;
        }

        /// <summary>
        /// Farsi Culture
        /// </summary>
        public CultureInfo FarsiCulture
        {
            get;
            private set;
        }

        /// <summary>
        /// Arabic Culture
        /// </summary>
        public CultureInfo ArabicCulture
        {
            get;
            private set;
        }

        /// <summary>
        /// Invariant Culture
        /// </summary>
        public CultureInfo InvariantCulture
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or Sets a new instance of Localizer. If this value is initialized (default is null), Localize Manager class will use the custom class provided, to interpret localized strings.
        /// </summary>
        public BaseLocalizer CustomLocalizer
        {
            get { return customLocalizer; }
            set
            {
                if(customLocalizer == value)
                    return;

                customLocalizer = value;
                OnLocalizerChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Fires the LocalizerChanged event.
        /// </summary>
        /// <param name="e"></param>
        protected void OnLocalizerChanged(EventArgs e)
        {
            if (LocalizerChanged != null)
                LocalizerChanged(null, e);
        }

        internal bool IsCustomArabicCulture
        {
            get { return CustomCulture != null && CustomCulture.Equals(ArabicCulture); }
        }

        internal bool IsCustomFarsiCulture
        {
            get { return CustomCulture != null && CustomCulture.Equals(FarsiCulture); }
        }

        internal bool IsThreadCultureFarsi
        {
            get { return CultureInfo.CurrentUICulture.Equals(FarsiCulture); }
        }

        internal bool IsThreadCultureArabic
        {
            get { return CultureInfo.CurrentUICulture.Equals(ArabicCulture); }
        }

        #endregion
    }
}
