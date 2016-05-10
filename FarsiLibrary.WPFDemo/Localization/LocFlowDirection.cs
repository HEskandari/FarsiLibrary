using System;
using System.Windows;
using System.Windows.Markup;

namespace FarsiLibrary.WPFDemo.Localization
{
    /// <summary>
    /// <c>LocalizeExtension</c> for <see cref="FlowDirection"/> values
    /// </summary>
    [MarkupExtensionReturnType(typeof(FlowDirection))]
    public class LocFlowDirection : LocalizeExtension<FlowDirection>
    {
        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocFlowDirection()
        {
        }

        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocFlowDirection(string key)
            : base(key)
        {
        }

        /// <summary>
        /// Provides the Value for the first Binding as <see cref="LocFlowDirection"/>
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj = base.ProvideValue(serviceProvider) ?? "LeftToRight";
            if (IsTypeOf(obj.GetType(), typeof(LocalizeExtension<>))) return obj;
            if (obj.GetType().Equals(typeof(string)))
            {
                return FormatOutput(obj);
            }

            throw new NotSupportedException(string.Format("ResourceKey '{0}' returns '{1}' which is not type of FlowDirection", Key, obj.GetType().FullName));
        }

        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        protected override void HandleNewValue()
        {
            var obj = LocalizeDictionary.Instance.GetLocalizedObject<object>(Assembly, Dict, Key, GetForcedCultureOrDefault());
            SetNewValue(FormatOutput(obj));
        }

        /// <summary>
        /// This method is used to modify the passed object into the target format
        /// </summary>
        protected override object FormatOutput(object input)
        {
            if (LocalizeDictionary.Instance.GetIsInDesignMode() && DesignValue != null)
            {
                try
                {
                    return Enum.Parse(typeof(FlowDirection), (string)DesignValue, true);
                }
                catch { return null; }
            }

            return Enum.Parse(typeof(FlowDirection), (string)input, true);
        }
    }
}