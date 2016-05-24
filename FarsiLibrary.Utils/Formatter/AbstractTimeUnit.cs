using FarsiLibrary.Localization;

namespace FarsiLibrary.Utils.Formatter
{
    public abstract class AbstractTimeUnit : ITimeUnit
    {
        public AbstractTimeUnit()
        {
            MaxQuantity = 0;
            MillisPerUnit = 1;
            LoadStringKeys();
        }

        protected FALocalizeManager LocalizeManager
        {
            get { return FALocalizeManager.Instance; }
        }

        public ITimeFormat Format { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public double MaxQuantity { get; set; }
        public double MillisPerUnit { get; set; }

        private void LoadStringKeys()
        {
            var resPrefix = GetResourcePrefix();
            var pattern = LocalizeManager.GetLocalizer().GetFormatterString(resPrefix + "Pattern");
            var futurePrefix = LocalizeManager.GetLocalizer().GetFormatterString(resPrefix + "FuturePrefix");
            var futureSuffix = LocalizeManager.GetLocalizer().GetFormatterString(resPrefix + "FutureSuffix");
            var pastPrefix = LocalizeManager.GetLocalizer().GetFormatterString(resPrefix + "PastPrefix");
            var pastSuffix = LocalizeManager.GetLocalizer().GetFormatterString(resPrefix + "PastSuffix");

            Name = LocalizeManager.GetLocalizer().GetFormatterString(resPrefix + "Name");
            PluralName = LocalizeManager.GetLocalizer().GetFormatterString(resPrefix + "PluralName");

            Format = new BasicTimeFormat().SetPattern(pattern)
                                          .SetFuturePrefix(futurePrefix)
                                          .SetFutureSuffix(futureSuffix)
                                          .SetPastPrefix(pastPrefix)
                                          .SetPastSuffix(pastSuffix);
        }

        protected abstract string GetResourcePrefix();
    }
}