using System;
using System.Windows.Markup;
using FarsiLibrary.Resources;

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
            StringID selectedID = (StringID) Enum.Parse(typeof (StringID), Key, true);
            return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(selectedID);
        }
    }
}
