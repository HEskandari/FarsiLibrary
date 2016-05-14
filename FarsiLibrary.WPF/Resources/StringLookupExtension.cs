using System;
using System.ComponentModel;
using System.Windows.Markup;
using FarsiLibrary.Localization;

namespace FarsiLibrary.WPF.Resources
{
    [MarkupExtensionReturnType(typeof(string))]
    public class StringLookupExtension : MarkupExtension
    {
        public string Key
        {
            get; set;
        }

        /// <summary>
        /// Returns localizer string for the specified key
        /// </summary>
        public override object ProvideValue(IServiceProvider provider)
        {
            StringID selectedID;
            if (!Enum.TryParse(Key, true, out selectedID))
                throw new InvalidEnumArgumentException("Key", 0, typeof(StringID));

            return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(selectedID);
        }
    }
}
