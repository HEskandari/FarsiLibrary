using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace FarsiLibrary.WPFDemo.Localization
{
    /// <summary>
    /// <c>LocalizeExtension</c> for brush objects as string (uses <see cref="TypeConverter"/>)
    /// </summary>
    [MarkupExtensionReturnType(typeof(System.Windows.Media.Brush))]
    public class LocBrush : LocalizeExtension<System.Windows.Media.Brush>
    {
        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocBrush()
        {
        }

        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        public LocBrush(string key) : base(key)
        {
        }

        /// <summary>
        /// Provides the Value for the first Binding as <see cref="System.Windows.Media.Brush"/>
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

            throw new NotSupportedException(string.Format("ResourceKey '{0}' returns '{1}' which is not type of System.Drawing.Bitmap", Key, obj.GetType().FullName));
        }

        /// <summary>
        /// see <c>LocalizeExtension</c>
        /// </summary>
        protected override void HandleNewValue()
        {
            var obj = LocalizeDictionary.Instance.GetLocalizedObject<object>(Assembly, Dict, Key, GetForcedCultureOrDefault());
            SetNewValue(new System.Windows.Media.BrushConverter().ConvertFromString((string)obj));
        }

        /// <summary>
        /// This method is used to modify the passed object into the target format
        /// </summary>
        /// <param name="input">The object that will be modified</param>
        /// <returns>Returns the modified object</returns>
        protected override object FormatOutput(object input)
        {
            if (LocalizeDictionary.Instance.GetIsInDesignMode() && DesignValue != null)
            {
                try
                {
                    return new System.Windows.Media.BrushConverter().ConvertFromString((string)DesignValue);
                }
                catch
                {
                    return null;
                }
            }

            return new System.Windows.Media.BrushConverter().ConvertFromString((string)input);
        }
    }
}