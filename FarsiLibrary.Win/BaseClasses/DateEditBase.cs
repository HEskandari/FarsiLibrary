using System.ComponentModel;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.BaseClasses
{
    /// <summary>
    /// DateEditBase is the base class for all date picker controls, which provider formatting information.
    /// </summary>
    [ToolboxItem(false)]
    public class DateEditBase : TextEditBase
    {
        #region Fields

        private FormatInfoTypes format;

        #endregion
        
        #region Props

        /// <summary>
        /// FormatInfoTypes instance, used to format date to string representation.
        /// </summary>
        [Description("FormatInfoTypes instance, used to format date to string representation.")]
        [DefaultValue(typeof(FormatInfoTypes), "ShortDate")]
        public FormatInfoTypes FormatInfo
        {
            get { return format; }
            set
            {
                if (format == value)
                    return;

                format = value;
                UpdateTextValue();
            }
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Updates text representation of the selected value.
        /// </summary>
        public virtual void UpdateTextValue()
        {
        }

        /// <summary>
        /// Returns a string representation of the FormatInfoTypes.
        /// </summary>
        /// <param name="fi"></param>
        /// <returns></returns>
        internal static string GetFormatByFormatInfo(FormatInfoTypes fi)
        {
            switch (fi)
            {
                case FormatInfoTypes.DateShortTime:
                    return "g";

                case FormatInfoTypes.FullDateTime:
                    return "G";

                case FormatInfoTypes.ShortDate:
                default:
                    return "d";
            }
        }

        #endregion

    }
}
