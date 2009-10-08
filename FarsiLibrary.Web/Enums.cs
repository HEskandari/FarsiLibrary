using System;

namespace FarsiLibrary.Web
{
    /// <summary>
    /// Specifies themes supported by UI Controls.
    /// </summary>
    public enum ThemeTypes
    {
        /// <summary>
        /// Office 2000 theme and style.
        /// </summary>
        Office2000 = 0,

        /// <summary>
        /// WindowsXP theme and style.
        /// </summary>
        WindowsXP = 1,

        /// <summary>
        /// Office 2003 theme and style.
        /// </summary>
        Office2003 = 2,

        /// <summary>
        /// Office 2007 theme and style
        /// </summary>
        Office2007 = 3
    }

    [Flags]
    public enum AttributesRange
    {
        All = 0xFF, Common = 1, Cell = 2, Font = 4
    };

}
