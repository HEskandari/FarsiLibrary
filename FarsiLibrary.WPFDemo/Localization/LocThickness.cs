using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace FarsiLibrary.WPFDemo.Localization
{
    /// <summary>
    /// <c>LocalizeExtension</c> for Thickness values
    /// </summary>
    [MarkupExtensionReturnType(typeof(Thickness))]
    public class LocThickness : LocalizeExtension<Thickness>
    {
        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocThickness()
        {
        }

        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocThickness(string key) : base(key)
        {
        }

        /// <summary>
        /// Provides the Value for the first Binding as Thickness
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj = base.ProvideValue(serviceProvider);
            if (obj == null) return null;
            if (IsTypeOf(obj.GetType(), typeof(LocalizeExtension<>))) return obj;
            if (obj.GetType().Equals(typeof(string)))
            {
                return FormatOutput(obj);
            }

            throw new NotSupportedException(string.Format("ResourceKey '{0}' returns '{1}' which is not type of double", Key, obj.GetType().FullName));
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
            MethodInfo method = typeof(ThicknessConverter).GetMethod("FromString", BindingFlags.Static | BindingFlags.NonPublic);

            if (LocalizeDictionary.Instance.GetIsInDesignMode() && DesignValue != null)
            {
                try
                {
                    return (Thickness)method.Invoke(null, new[] { DesignValue, new CultureInfo("en-US") });
                }
                catch
                {
                    return null;
                }
            }

            return (Thickness)method.Invoke(null, new[] { input, new CultureInfo("en-US") });
        }
    }
}