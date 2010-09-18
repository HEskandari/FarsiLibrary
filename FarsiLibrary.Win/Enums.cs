using System;
using FarsiLibrary.Win.Controls;

namespace FarsiLibrary.Win.Enums
{
    /// <summary>
    /// Type of the MultiView
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// Month View
        /// </summary>
        Month,

        /// <summary>
        /// Day View
        /// </summary>
        Day
    }

    public enum CollectionChangeType
    {
        Add,
        Remove,
        Clear,
        Other
    }

    /// <summary>
    /// Beep Types of the system
    /// </summary>
    public enum MessageBeepType
    {
        Default = -1,
        Ok = 0,
        Error = 16,
        Question = 22,
        Warning = 48,
        Information = 64
    }

    /// <summary>
    /// Standard MessageBoxEx buttons
    /// </summary>
    public enum FAMessageBoxButtons
    {
        /// <summary>
        /// Ok
        /// </summary>
        Ok = 0,

        /// <summary>
        /// Cancel
        /// </summary>
        Cancel = 1,

        /// <summary>
        /// Yes
        /// </summary>
        Yes = 2,

        /// <summary>
        /// No
        /// </summary>
        No = 4,

        /// <summary>
        /// Abort
        /// </summary>
        Abort = 8,

        /// <summary>
        /// Retry
        /// </summary>
        Retry = 16,

        /// <summary>
        /// Ignore
        /// </summary>
        Ignore = 32,
    }

    /// <summary>
    /// Standard MessageBoxEx icons
    /// </summary>
    public enum FarsiMessageBoxExIcon
    {
        /// <summary>
        /// No icon visible
        /// </summary>
        None,

        /// <summary>
        /// Astrisk icon
        /// </summary>
        Asterisk,

        /// <summary>
        /// Error icon
        /// </summary>
        Error,

        /// <summary>
        /// Exclamation icon
        /// </summary>
        Exclamation,

        /// <summary>
        /// Hand icon
        /// </summary>
        Hand,

        /// <summary>
        /// Information icon
        /// </summary>
        Information,

        /// <summary>
        /// Question icon
        /// </summary>
        Question,

        /// <summary>
        /// Stop icon
        /// </summary>
        Stop,

        /// <summary>
        /// Warning icon
        /// </summary>
        Warning
    }

    /// <summary>
    /// FAMonthView buttons which will raise the button click event.
    /// </summary>
    public enum FAMonthViewButtons
    {
        /// <summary>
        /// None button of FAMonthView control
        /// </summary>
        None,
        
        /// <summary>
        /// Today button of FAMonthView control
        /// </summary>
        Today,
        
        /// <summary>
        /// Any normal day of FAMonthView control
        /// </summary>
        MonthDay,
    }
    
    /// <summary>
    /// Various Formatting Info for PersianDate to format its text values.
    /// </summary>
    public enum FormatInfoTypes
    {
        /// <summary>
        /// PersianDate instance in WrittenDate format equals calling ToString("d"). This is the default value
        /// when using ToString() overload.
        /// </summary>
        ShortDate,

        /// <summary>
        /// PersianDate instance in WrittenDate format equals calling ToString("g")
        /// </summary>
        DateShortTime,

        /// <summary>
        /// PersianDate instance in WrittenDate format equals calling ToString("G")
        /// </summary>
        FullDateTime
    }

    /// <summary>
    /// Decides which property to change when user scrolls mouse wheel over the <see cref="FAMonthView"/> control.
    /// </summary>
    public enum ScrollOptionTypes
    {
        /// <summary>
        /// Scroll days in the FAMonthView control.
        /// </summary>
        Day,
        
        /// <summary>
        /// Scroll months in the FAMonthView control.
        /// </summary>
        Month,
        
        /// <summary>
        /// Scroll years in the FAMonthView control.
        /// </summary>
        Year
    }
    
    /// <summary>
    /// Status of each ActRect instances in FAMonthView controls.
    /// </summary>
    [Flags]
    internal enum TRectangleStatus
    {
        Normal = 0x0000,
        Active = 0x0001,
        Selected = 0x0002,
        Focused = 0x0004,
        Pressed = 0x0008,
        ActiveSelect = Active | Selected,
        FocusSelect = Focused | Selected,
        All = Active | Selected | Focused
    }

    /// <summary>
    /// Action Type of the ActRect class.
    /// </summary>
    public enum FocusedPart
    {
        None,
        MonthPrev,
        MonthNext,
        YearPrev,
        YearNext,
        TodayButton,
        NoneButton,
        MonthDay,
        WeekDay,
        Hidden,
        YearValue,
        MonthValue,
    }

    /// <summary>
    /// Office 2003 predefined colors.
    /// </summary>
    public enum Office2003Color
    {
        Border, Button1, Button2, Button1Hot, Button2Hot,
        Button1Pressed, Button2Pressed, ButtonDisabled, Text, TextDisabled, Header, Header2, GroupRow,
        TabPageForeColor, TabBackColor1, TabBackColor2, TabPageBackColor1, TabPageBackColor2, TabPageBorderColor,
        NavBarBackColor1, NavBarBackColor2, NavBarLinkTextColor, NavBarLinkHightlightedTextColor, NavBarLinkDisabledTextColor, NavBarGroupClientBackColor,
        NavBarGroupCaptionBackColor1, NavBarGroupCaptionBackColor2, NavBarExpandButtonRoundColor, NavPaneBorderColor,
        NavBarNavPaneHeaderBackColor, LinkBorder
    }

    /// <summary>
    /// Specifies Theme types of WindowsXP.
    /// </summary>
    public enum XPThemeType
    {
        Unknown,
        NormalColor,
        Homestead,
        Metallic
    }

    /// <summary>
    /// Calendar type that should be shown by FADatePickerConverter control.
    /// </summary>
    public enum CalendarTypes
    {
        /// <summary>
        /// Persian Calendar
        /// </summary>
        Persian,

        /// <summary>
        /// English Calendar
        /// </summary>
        English
    }

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
        Office2007 = 3,
    }

    /// <summary>
    /// Specifies the state DrawTab command is in.
    /// </summary>
    public enum ItemState
    {
        /// <summary>
        /// Specifies the command is in default state.
        /// </summary>
        Normal,

        /// <summary>
        /// Specifies command is being hot tracked.
        /// </summary>
        HotTrack,

        /// <summary>
        /// Specifies command is user pressing it down.
        /// </summary>
        Pressed,

        /// <summary>
        /// Specifies command is has been opened.
        /// </summary>
        Open
    }

    public enum TextAlignment
    {
        /// <summary>
        /// Alignment based on system default settings.
        /// </summary>
        Default,

        /// <summary>
        /// Center alignment.
        /// </summary>
        Center,

        /// <summary>
        /// Near alignment, based on RTL settings.
        /// </summary>
        Near,

        /// <summary>
        /// Far alignment, based on RTL settings.
        /// </summary>
        Far
    }
}
