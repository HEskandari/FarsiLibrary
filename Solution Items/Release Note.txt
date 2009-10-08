Version 2.5 Changes:
- Added : Web controls
- Added : A new WinForm control named DayView that displays the date similiar to Windows Vista date gadget.
- Added : new controls to WinForms project.(multi view)
- Added : a new ViewDateTime property which separates the viewing date from selected date.
- Added : Implicit casting of Nullable DateTime to PersianDate instances.
- Added : Ability to set "ShowWeekDayNames", "ShowEmptyButton", "ShowTodayButton" on FXDatePicker.
- Added : helper extension methods to facilitate date calculations : "Combine", "EndOfMonth", "StartOfMonth", "EndOfWeek", "StartOfWeek" is added.
- Added : MVC set of controls to be used in ASP.NET MVC applications.
- Added : FarsiLibrary.Extensions project which contains integration with DevExpress controls. You can use these controls as standalone and grid-editor mode.
- Modified (Breaking Change) : SelectedDateTime property is changed to nullable datetime.
- Modified (Breaking Change) : Day / Month / Year properties are moved to base class and named SelectedYear / SelectedMonth / SelectedYear
- Modified : When in MultiSelection mode, you should use SelectedDateRange property to read the selected values.
- Modified : When Casting DateTime instances to PersianDate, if the range exceeds the Maximum and Minimum values of PersianCalendar, the result will be null.
- Removed (Breaking Change) : IsNull property of FAMonthView is removed. Use the new Nullable datetime property.
- Removed (Breaking Change) : FADatePickerConverter control is removed.
- Removed : old windows XP themes are gone from WPF controls. Only Aero theme is supported as well as an extra theme available in Demo project.
- Removed : Reference to PresentationFramework.Classic, PresentationFramework.Luns, PresentationFramework.Royale is no longer needed to use WPF controls.

Version 2.1 Changes: 
- Fixed : Rendering of controls in non-official themes or when a 3rd party windows skinning (e.g. WindowsBlinds) is installed.
- Fixed : Problem when setting SelectedDateTime property of FXDatePicker control to null value.
- Fixed : Painting of FADatePicker in readonly mode when Readonly property is set fixed.
- Fixed : Problem of setting ForeColor and BackColor in FADatePicker
- Fixed : Certain usage of WinForms control resulted wrong display of Week Of Day in header part of the FAMonthView.
- Fixed : Certain usage of FAMonthView resulted in wrong displaying of date when culture is invariant.
- Added : Methods SetTodayDate and SetEmptyDate added to FXDatePicker and FXMonthView which lets you call the code that represents Today and Empty buttons clicks respectively.
- Added : PersianCultureInfo which is a FA-IR Culture with correct PersianCalendar and DateTimeFormatInfo.
- Added : PersianDateTimeFormatInfo that represents datetime formatting information for FA-IR culture.
- Added : DateTimeExtensions to help convert between PersianDate and DateTime instances via extension methods.
- Added : XmlnsDefinition is added to WPF namespaces. You can reference the controls assembly with http://schemas.hightech.ir/wpf/2008/FarsiLibrary namespace.
- Added : Methods to add / remove validation errors on BaseControl.
- Added : PersianDateValueConverter which can be used in WPF applications to convert strings representing DateTime to their PersianDate equivalant.
- Added : WPF Demo to show usage of custom date converters.
- Added : Ability to show and hide Today and Empty buttons on FAMonthView. 
- Added : VisualStudio designer for WPF and actions for WinForm controls is added.
- Modified : Localization of WPF controls are using mechanism like WinForm controls (using StringIDs). Redundant .resx files are removed.
- Modified : Merged WinForm and WPF controls into one solution. 
- Modified : ValueValidatingEventArgs now passes HasErrors property of the control when raising event.
- Modified : PersianWeekDayNames and PersianMonthNames have became internal. Use PersianDateTimeFormatInfo class instead.
- Modified : When parsing a string representation of PersianDate e.g. 1382/08/23 time part was initialized from system time, but now initialized to 00:00 to be consistent with DateTime behavior.
- Modified : PersianDateConverter is changed access modifier to Internal. You should not use this class, and instead either cast instances of DateTime / PersianDate or use newly provided extension methods.
- Modified : Strings representing DateTime is now parsed using InvariantCulture when parsed to DateTime instance.
- Modified : Static Parse and TryParse method accepting DateTime instance is removed. Use constructor overload instead.
- Modified : "Readonly" property in FAContainerComboBox is made obsolete. Use IsReadonly property.
- Modified : Border color of control in Office2000 and WindowsXP was near white color. Now uses SystemColors.ControlDarkDark value.
- Modified : Created a new base class for FAMonthView, which will be base of other upcoming controls.
- Modified : CurrentMonthName property from FAMonthView is made obsolete. You can use the GetMonthName method on BaseCulturedControl class instead.
- Modified : Arrows of the FAMonthView will gray-out if the control is in disabled state. Change of selected date is not available if the control is disabled.

Version 2.0 Changes:
- Migrated to Visual Studio .NET 2008
- Modified : IComparer and IComparer<PersianDate> are converted to explicit implementation in PersianDate class.
- Modified : ICloneable interface implementation is converted to explicit implementation in PersianDate class.
- Modified : Assign method of PersianDate to internal.
- Modified : Changed the Day/Month/Year change eventhandler type to EventHandler<DateChangedEventArgs>. You can use the New/Old value properties to access the underlying values.
- Modified : FALocalizeManager now is a singleton class. Use the Instance property instead of static methods.
- Modified : FAMessageBoxManager.Delete method now returns boolean value based on whether the delete operation was successful or not.
- Added : Implementation of IsLeapMonth method in PersianCalendar.
- Added : Solution Items folder.
- Added : Utils project NUnit tests.
- Added : Partial Office 2007 Blue theme. Drawing of dropdown buttons of FADatePicker control need a better visualization.
- Added : PainterFactory to create painters based on the state of BaseStyledControl.
- Added : FAMessageBox now implementes IDisposable interface explicitly. Dispose method is made internal.
- Fixed : When accessing Now property of PersianDate class always returns the first instance created.
- Fixed : When changing the culture, Year value didn't change, e.g. when in Farsi culture in year 1385 culture is set to english, the year remain 1385 (which was a valid year in English calendar).
- Fixed : When changing the DefaultCulture property, Day and Month properties did not return the culture-specific values.

Version 1.9.1 Changes:
- Fixed : Bug checking if a year is a leap year in PersianCalendar class.

Version 1.9 Changes:

- Fixed : Bugs regarding wrong mapping of Persian/Arabic WeekDays and Gregorian Weekdays, which resulted wrong display of weekday in Gregorian calendar.
- Added : Better handling of multi-selection mode. You can add/remove the SelectedDateRange property and changes will be reflected on the UI.
- Added : PersianDayOfWeek enum with correct days order.
- Added : DayOfWeek property to PersianDate class, to return correct day of week.

Version 1.8 - Changes:

- Fixed : Some bugs posted through feedbacks.
- Added : MessageBox control with RTL and LTR views, and ability to remember the selected value, with both standard and custom MessageBox buttons.
- 
Added : CustomFormating of PersianDate.ToString() method, (like DateTime control), which gives the functionality to return formated strings, e.g. Long Date, Long Time, DateTime, etc.
- Added : CustomDraw of each prefered day. Could be used to draw some days in disabled format, along with SelectedDateTimeChanging event.


Version 1.6 Chanages:

- Fixed : Some bugs posted through feedbacks.
- Added : MessageBox control with RTL and LTR views, and ability to remember the selected value, with both standard and custom MessageBox buttons.
- 
Added : CustomFormating of PersianDate.ToString() method, (like DateTime control), which gives the functionality to return formated strings, e.g. Long Date, Long Time, DateTime, etc.
- Added : CustomDraw of each prefered day. Could be used to draw some days in disabled format, along with SelectedDateTimeChanging event.

