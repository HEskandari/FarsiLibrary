using System;
using System.Windows.Markup;

namespace FarsiLibrary.WPFDemo.Localization
{
    /// <summary>
    /// <c>LocalizeExtension</c> for string objects
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class LocText : LocalizeExtension<string>
    {

        #region Enums
        /// <summary>
        /// This enumeration is used to determine the type 
        /// of the return value of <see cref="GetAppendText"/>
        /// </summary>
        protected enum AppendType
        {
            /// <summary>
            /// The return value is used as prefix
            /// </summary>
            Prefix,
            /// <summary>
            /// The return value is used as suffix
            /// </summary>
            Suffix
        }
        #endregion

        #region Members
        /// <summary>
        /// Holds the local prefix value
        /// </summary>
        private string m_Prefix;

        /// <summary>
        /// Holds the local suffix value
        /// </summary>
        private string m_Suffix;

        /// <summary>
        /// Holds the local format segment array
        /// </summary>
        private string[] m_FormatSegments;
        #endregion

        #region Properties
        /// <summary>
        /// Defines a prefix for the localized text
        /// </summary>
        public string Prefix
        {
            get { return m_Prefix; }
            set
            {
                m_Prefix = value;
                // reset the value of the target property
                HandleNewValue();
            }
        }

        /// <summary>
        /// Defines a suffix for the localized text
        /// </summary>
        public string Suffix
        {
            get { return m_Suffix; }
            set
            {
                m_Suffix = value;
                // reset the value of the target property
                HandleNewValue();
            }
        }

        #region FormatSegments
        /// <summary>
        /// Gets or sets the format segment 1.
        /// This will be used to replace format place holders from the localized text.
        /// </summary>
        /// <value>The format segment 1.</value>
        public string FormatSegment1
        {
            get { return m_FormatSegments[0]; }
            set
            {
                m_FormatSegments[0] = value;
                HandleNewValue();
            }
        }

        /// <summary>
        /// Gets or sets the format segment 2.
        /// This will be used to replace format place holders from the localized text.
        /// </summary>
        /// <value>The format segment 2.</value>
        public string FormatSegment2
        {
            get { return m_FormatSegments[1]; }
            set
            {
                m_FormatSegments[1] = value;
                HandleNewValue();
            }
        }

        /// <summary>
        /// Gets or sets the format segment 3.
        /// This will be used to replace format place holders from the localized text.
        /// </summary>
        /// <value>The format segment 3.</value>
        public string FormatSegment3
        {
            get { return m_FormatSegments[2]; }
            set
            {
                m_FormatSegments[2] = value;
                HandleNewValue();
            }
        }

        /// <summary>
        /// Gets or sets the format segment 4.
        /// This will be used to replace format place holders from the localized text.
        /// </summary>
        /// <value>The format segment 4.</value>
        public string FormatSegment4
        {
            get { return m_FormatSegments[3]; }
            set
            {
                m_FormatSegments[3] = value;
                HandleNewValue();
            }
        }

        /// <summary>
        /// Gets or sets the format segment 5.
        /// This will be used to replace format place holders from the localized text.
        /// </summary>
        /// <value>The format segment 5.</value>
        public string FormatSegment5
        {
            get { return m_FormatSegments[4]; }
            set
            {
                m_FormatSegments[4] = value;
                HandleNewValue();
            }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocText()
        {
            InitializeLocText();
        }
        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocText(string key)
            : base(key)
        {
            InitializeLocText();
        }
        #endregion

        #region InitializeLocText
        /// <summary>
        /// Initializes the <see cref="LocText"/> extension.
        /// </summary>
        private void InitializeLocText()
        {
            m_FormatSegments = new string[5];
            m_FormatSegments.Initialize();
        }
        #endregion

        #region GetAppendText
        /// <summary>
        /// Returns the prefix or suffix text, depending on the supplied <see cref="AppendType"/>.
        /// If the prefix or suffix is null, it will be returned a string.empty.
        /// </summary>
        /// <param name="at">The <see cref="AppendType"/> defines the format of the return value</param>
        /// <returns>Returns the formated prefix or suffix</returns>
        private string GetAppendText(AppendType at)
        {
            // define a return value
            string retVal = string.Empty;

            // if it should be a prefix, the format will be [PREFIX]
            if (at == AppendType.Prefix && !string.IsNullOrEmpty(m_Prefix))
            {
                retVal = m_Prefix ?? string.Empty;
            }
                // if it should be a suffix, the format will be [SUFFIX]
            else if (at == AppendType.Suffix && !string.IsNullOrEmpty(m_Suffix))
            {
                retVal = m_Suffix ?? string.Empty;
            }

            // return the formated prefix or suffix
            return retVal;
        }
        #endregion

        #region FormatText
        /// <summary>
        /// This method formats the localized text.
        /// If the passed target text is null, string.empty will be returned.
        /// </summary>
        /// <param name="target">The text to format.</param>
        /// <returns>Returns the formated text or string.empty, if the target text was null.</returns>
        protected virtual string FormatText(string target)
        {
            return target ?? string.Empty;
        }
        #endregion

        #region ProvideValue
        /// <summary>
        /// Provides the Value for the first Binding as <see cref="System.String"/>
        /// </summary>
        /// <param name="serviceProvider">
        /// The <see cref="System.Windows.Markup.IProvideValueTarget"/> provided from the <see cref="MarkupExtension"/>
        /// </param>
        /// <returns>The founded item from the .resx directory or null if not founded</returns>
        /// <exception cref="System.InvalidOperationException">
        /// thrown if <paramref name="serviceProvider"/> is not type of <see cref="System.Windows.Markup.IProvideValueTarget"/>
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// thrown if the founded object is not type of <see cref="System.String"/>
        /// </exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj = base.ProvideValue(serviceProvider);
            if (obj == null)
                return LocalizeDictionary.Instance.GetIsInDesignMode() ? GetDesignValue() : null;

            if (IsTypeOf(obj.GetType(), typeof(LocalizeExtension<>))) return obj;
            if (obj.GetType().Equals(typeof(string)))
            {
                return FormatOutput(obj);
            }

            throw new NotSupportedException(
                string.Format("ResourceKey '{0}' returns '{1}' which is not type of System.String",
                              Key, obj.GetType().FullName));
        }

        private object GetDesignValue()
        {
            if (DesignValue != null)
            {
                return DesignValue;
            }

            return Key;
        }

        #endregion

        #region override HandleNewValue
        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        protected override void HandleNewValue()
        {
            SetNewValue(FormatOutput(null));
        }
        #endregion

        #region FormatOutput
        /// <summary>
        /// This method returns the finished formatted text
        /// </summary>
        /// <param name="input">If the passed string not null, it will be used, otherwise a fresh localized text will be loaded.</param>
        /// <returns>Returns the finished formatted text in format [PREFIX]LocalizedText[SUFFIX]</returns>
        protected override object FormatOutput(object input)
        {
            if (LocalizeDictionary.Instance.GetIsInDesignMode() && DesignValue != null)
            {
                input = DesignValue;
            }
            else
            {
                // load a fresh localized text, if the passed string is null
                input = input ?? LocalizeDictionary.Instance.GetLocalizedObject<object>(Assembly, Dict, Key, GetForcedCultureOrDefault());
            }

            // get the main text as string xor string.empty
            string textMain = input as string ?? string.Empty;

            try
            {
                // add some format segments, in case that the main text contains format place holders like {0}
                textMain = string.Format(LocalizeDictionary.Instance.SpecificCulture,
                                         textMain,
                                         m_FormatSegments[0] ?? string.Empty,
                                         m_FormatSegments[1] ?? string.Empty,
                                         m_FormatSegments[2] ?? string.Empty,
                                         m_FormatSegments[3] ?? string.Empty,
                                         m_FormatSegments[4] ?? string.Empty);
            }
            catch (FormatException)
            {
                // if a format exception was thrown, change the text to an error string
                textMain = "TextFormatError: Max 5 Format PlaceHolders! {0} to {4}";
            }

            // get the prefix
            string textPrefix = GetAppendText(AppendType.Prefix);

            // get the suffix
            string textSuffix = GetAppendText(AppendType.Suffix);

            // format the text with prefix and suffix to [PREFIX]LocalizedText[SUFFIX]
            input = FormatText(textPrefix + textMain + textSuffix);

            // return the finished formatted text
            return input;
        }
        #endregion
    }
}