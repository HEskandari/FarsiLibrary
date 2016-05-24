using System;

namespace FarsiLibrary.Localization
{
    public abstract class BaseLocalizer
    {
        #region Abstract Methods

        public abstract string GetLocalizedString(StringID id);

        public string GetFormatterString(string enumKey)
        {
            var key = (FormatterStringID)Enum.Parse(typeof(FormatterStringID), enumKey);
            return GetFormatterString(key);
        }

        public abstract string GetFormatterString(FormatterStringID stringID);

        #endregion
    }
}
