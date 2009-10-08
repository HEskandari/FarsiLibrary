using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using FarsiLibrary.Utils;

namespace FarsiLibrary.Win.Design
{
    /// <summary>
    /// Type Converter for PersianDate type which handles convertion of various types to PersianDate, mostly in design mode.
    /// </summary>
    internal class PersianDateTypeConverter : TypeConverter
    {
        #region Override

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value != null && value is string)
                return new PersianDate(value.ToString());

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value != null && value is PersianDate)
            {
                if (destinationType == typeof(InstanceDescriptor) && value != null)
                {
                    PersianDate pd = (PersianDate)value;
                    ConstructorInfo ctor = typeof(PersianDate).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int) });
                    object[] args = new object[] { pd.Year, pd.Month, pd.Day };

                    if (ctor != null)
                        return new InstanceDescriptor(ctor, args);
                }
                else if (destinationType == typeof(string))
                {
                    PersianDate pd = (PersianDate)value;
                    return pd.ToString();
                }
                else if(destinationType == typeof(PersianDate))
                {
                    PersianDate pd = (PersianDate)value;
                    return pd;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string) || 
                destinationType == typeof(PersianDate) ||
                destinationType == typeof(InstanceDescriptor))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        #endregion
    }
}
