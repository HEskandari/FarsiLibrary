using System.Threading;
using System.Windows.Forms;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Controls
{
    ///<summary>
    ///FarsiLibrary project, provides UI Controls to work with multiple different calendars. UI Controls
    /// currently include <see cref="FAMonthView"/> control which is a month view of a calendar,
    /// <see cref="FADatePicker"/> control that acts as a date picker control that shows the FAMonthView control as the calendar view,
    /// 
    /// Controls support three themes. On WindowsXP or above you can use <see cref="ThemeTypes.WindowsXP"/>, <see cref="ThemeTypes.Office2003"/> and <see cref="ThemeTypes.Office2000"/>, but
    /// on Windows 2000, you can only use the latter. Also you should enable themes on your WindowsXP OS, and set the <see cref="Application.EnableVisualStyles" /> property to true for the themes
    /// to be displayed :
    /// 
    /// <code>
    /// public static void Main()
    /// {
    ///    Application.EnableVisualStyles(); // To Support XP and Office2003 themes.
    ///    Application.Run(new MainForm());
    /// }
    /// </code>
    /// 
    /// UI Controls display MonthNames, Years, Weekday, Days and Numeric values bases on current thread's <see cref="System.Globalization.CultureInfo"/>.
    /// If you want to display the control in a different culture, you'll have to set cutrrent thread's Culture properties like this :
    /// 
    /// <code>
    /// public static void Main()
    /// {
    ///    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-IR");
    ///    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
    ///    Application.Run(new MainForm());
    /// }
    /// </code>
    /// 
    /// Currently three cultures are implemented : FA-IR (Farsi) for Persian culture that is used in Iran and some other countries, AR-SA (Arabic) for arabic culture that is used in arab countries, and an Invariant
    /// Culture that is used to display control in other cultures. By default, (also when the user drops a control on a form in VS.NET) control draws itself based on the Regional Settings in control panel.
    /// 
    /// In Cultures that has Right-To-Left reading form (i.e. Arabic and Farsi), control draws itself in a RTL manner (Months, Days and WeekDay reading form), otherwise in Left-To-Right form.
    /// <remarks>Remember to set both <see cref="Thread.CurrentUICulture"/> and <see cref="Thread.CurrentCulture"/> values for the control to be displayed properly.</remarks>
    ///</summary>
    internal class NamespaceDoc
    {
        /// <summary>
        /// 
        /// </summary>
        public NamespaceDoc()
        {
        }
    }
}
