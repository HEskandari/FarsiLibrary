using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarsiLibrary.Resources;
using FarsiLibrary.Utils;
using System;
using Calendar = System.Globalization.Calendar;
using PersianCalendar = FarsiLibrary.Utils.PersianCalendar;
using FarsiLibrary.Web.Helper;


namespace FarsiLibrary.Web.Controls
{
    public class FAMonthView : System.Web.UI.WebControls.Calendar
    {
        #region Events

        private static object MonthViewEventDayRender;
        private static object MonthViewEventSelectionChanged;

        public event EventHandler<RenderCalendarCellArgs> RenderCalendarCell;

        #endregion

        #region Fields

        private Calendar threadCalendar;
        private Calendar customCalendar;
        private DateTime minSupportedDate;
        private DateTime maxSupportedDate;
        private Color defaultForeColor;
        private string defaultButtonColorText;
        private static readonly DateTime baseDate;
        private static readonly Color DefaultForeColor;
        private static readonly PersianCalendar pc;
        private static readonly GregorianCalendar gc;
        private static readonly HijriCalendar hc;
        private static readonly PersianCultureInfo persianCulture;
        private static readonly string[] cachedNumbers;

        #endregion

        #region DateTime Methods

        private DateTime EffectiveVisibleDate()
        {
            DateTime visibleDate = this.VisibleDate;
            if (visibleDate.Equals(DateTime.MinValue))
            {
                visibleDate = this.TodaysDate;
            }

            if (this.IsMinSupportedYearMonth(visibleDate))
            {
                return this.minSupportedDate;
            }

            return this.threadCalendar.AddDays(visibleDate, -(this.threadCalendar.GetDayOfMonth(visibleDate) - 1));
        }

        private bool IsMinSupportedYearMonth(DateTime date)
        {
            return this.IsTheSameYearMonth(this.minSupportedDate, date);
        }

        private bool IsTheSameYearMonth(DateTime x, DateTime y)
        {
            return this.threadCalendar.GetEra(x) == this.threadCalendar.GetEra(y) &&
                   this.threadCalendar.GetYear(x) == this.threadCalendar.GetYear(y) &&
                   this.threadCalendar.GetMonth(x) == this.threadCalendar.GetMonth(y);
        }

        private DateTime FirstCalendarDay(DateTime visibleDate)
        {
            DateTime date = visibleDate;

            if (this.IsMinSupportedYearMonth(date))
                return date;
            
            int num = ((int)this.threadCalendar.GetDayOfWeek(date)) - this.NumericFirstDayOfWeek();
            if (num <= 0)
                num += 7;
            
            return this.threadCalendar.AddDays(date, -num);
        }

        private int NumericFirstDayOfWeek()
        {
            if (this.FirstDayOfWeek != FirstDayOfWeek.Default)
                return (int)this.FirstDayOfWeek;
            
            return (int)GetDateTimeFormatter().FirstDayOfWeek;
        }

        private string GetMonthName(int m, bool showFullMonth)
        {
            if (showFullMonth)
                return GetDateTimeFormatter().GetMonthName(m);
            
            return GetDateTimeFormatter().GetAbbreviatedMonthName(m);
        }

        private bool IsMaxSupportedYearMonth(DateTime date)
        {
            return this.IsTheSameYearMonth(this.maxSupportedDate, date);
        }

        private bool IsWeekEnd(DayOfWeek dow)
        {
            if(IsHijriCalendar)
            {
                return dow == DayOfWeek.Thursday || 
                       dow == DayOfWeek.Friday;
            }

            return dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday;
        }

        private string GetMonthName(DateTime date)
        {
            var formatter = GetDateTimeFormatter();
            var cal = GetDefaultCalendar();
            if(cal == null || formatter == null)
                return string.Empty;
            
            var monthNumber = cal.GetMonth(date);
            return formatter.MonthGenitiveNames[monthNumber - 1];
        }

        private string GetMonthYear(IFormattable date)
        {
            var formatter = GetDateTimeFormatter();
            var yearMonthPattern = formatter.YearMonthPattern;
            if (yearMonthPattern.IndexOf(',') >= 0)
            {
                yearMonthPattern = "MMMM yyyy";
            }
            
            return date.ToString(yearMonthPattern, DefaultCulture);
        }

        private DateTime GetPreviousDate(DateTime visibleDate)
        {
            var nextMonth = this.threadCalendar.AddMonths(this.minSupportedDate, 1);
            var prevDate = this.IsTheSameYearMonth(nextMonth, visibleDate) ?
                                this.minSupportedDate :
                                this.threadCalendar.AddMonths(visibleDate, -1);

            return prevDate;
        }

        #endregion

        #region Culture Methods

        protected virtual Calendar GetDefaultCalendar()
        {
            if (customCalendar != null)
                return customCalendar;

            if (DefaultCulture.Equals(FALocalizeManager.Instance.FarsiCulture))
                return pc;

            if (DefaultCulture.Equals(FALocalizeManager.Instance.ArabicCulture))
                return hc;

            return gc;
        }

        protected virtual DateTimeFormatInfo GetDateTimeFormatter()
        {
            if (DefaultCulture.Equals(FALocalizeManager.Instance.FarsiCulture))
            {
                return persianCulture.DateTimeFormat;
            }

            return DefaultCulture.DateTimeFormat;
        }

        #endregion

        #region Ctor

        static FAMonthView()
        {
            MonthViewEventDayRender = GetEventReference("EventDayRender");
            MonthViewEventSelectionChanged = GetEventReference("EventSelectionChanged");

            DefaultForeColor = Color.Black;
            baseDate = new DateTime(2000, 1, 1);
            persianCulture = new PersianCultureInfo();
            pc = new PersianCalendar();
            gc = new GregorianCalendar();
            hc = new HijriCalendar();
            cachedNumbers = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"};
        }

        /// <summary>
        /// Creates a new instance of FAMonthView
        /// </summary>
        public FAMonthView()
        {
            this.ControlStyle.Font.Name = "Tahoma";
            this.ControlStyle.Font.Size = FontUnit.Small;
            this.ControlStyle.Font.MergeWith(this.Font);
        }

        #endregion

        #region Rendering

        protected override void Render(HtmlTextWriter writer)
        {
            //Do not call te base render functionality
            //which has wrong rendering for Persian Calendar
            //base.Render(writer);


            bool isEnabled;
            this.threadCalendar = GetDefaultCalendar();
            this.minSupportedDate = this.threadCalendar.MinSupportedDateTime;
            this.maxSupportedDate = this.threadCalendar.MaxSupportedDateTime;
            DateTime visibleDate = this.EffectiveVisibleDate();
            DateTime firstDay = this.FirstCalendarDay(visibleDate);
            CalendarSelectionMode selectionMode = this.SelectionMode;

            if (this.Page != null)
            {
                this.Page.VerifyRenderingInServerForm(this);
            }

            if (this.Page == null || DesignMode)
            {
                isEnabled = false;
            }
            else
            {
                isEnabled = IsEnabled;
            }

            this.defaultForeColor = this.ForeColor;
            if (this.defaultForeColor == Color.Empty)
            {
                this.defaultForeColor = DefaultForeColor;
            }
            this.defaultButtonColorText = ColorTranslator.ToHtml(this.defaultForeColor);

            Table table = CreateTable();

            if (ControlStyleCreated)
            {
                table.ApplyStyle(ControlStyle);
            }

            table.Width = Width;
            table.Height = Height;
            table.CellPadding = CellPadding;
            table.CellSpacing = CellSpacing;

            if (!ControlStyleCreated || this.BorderWidth.Equals(Unit.Empty))
            {
                table.BorderWidth = Unit.Pixel(1);
            }

            table.GridLines = this.ShowGridLines ? GridLines.Both : GridLines.None;
            string caption = this.Caption;
            if (caption.Length > 0)
            {
                table.Caption = caption;
                table.CaptionAlign = this.CaptionAlign;
            }

            table.RenderBeginTag(writer);
            if (this.ShowTitle)
            {
                OnRenderTitle(writer, visibleDate, selectionMode, isEnabled);
            }

            if (this.ShowDayHeader)
            {
                OnRenderDayHeader(writer, visibleDate, selectionMode, isEnabled);
            }

            this.OnRenderDays(writer, firstDay, visibleDate, selectionMode, isEnabled);
            table.RenderEndTag(writer);
        }

        protected virtual void OnRenderTitle(HtmlTextWriter writer, DateTime visibleDate, CalendarSelectionMode selectionMode, bool buttonsActive)
        {
            string renderingTitle;
            writer.Write("<tr>");

            var titleCell = new TableCell
                                {
                                    ColumnSpan = (this.HasWeekSelectors(selectionMode) ? 8 : 7), 
                                    BackColor = Color.Silver
                                };

            var titleTable = CreateTable();
            titleTable.GridLines = GridLines.None;
            titleTable.Width = Unit.Percentage(100.0);
            titleTable.CellSpacing = 0;

            var titleStyle = this.TitleStyle;
            this.ApplyTitleStyle(titleCell, titleTable, titleStyle);
            titleCell.RenderBeginTag(writer);
            titleTable.RenderBeginTag(writer);
            writer.Write("<tr>");

            if (this.ShowNextPrevMonth)
            {
                RenderPreviousMonthNav(writer, visibleDate, buttonsActive);
            }

            RenderMonthTitle(writer, titleStyle, visibleDate);

            if (this.ShowNextPrevMonth)
            {
                RenderNextMonthNav(writer, visibleDate, buttonsActive);
            }

            writer.Write("</tr>");
            titleTable.RenderEndTag(writer);
            titleCell.RenderEndTag(writer);
            writer.Write("</tr>");
        }

        protected virtual void OnRenderDayHeader(HtmlTextWriter writer, DateTime visibleDate, CalendarSelectionMode selectionMode, bool buttonsActive)
        {
            writer.Write("<tr>");
            DateTimeFormatInfo currentInfo = GetDateTimeFormatter();

            if (this.HasWeekSelectors(selectionMode))
            {
                TableItemStyle style = new TableItemStyle();
                style.HorizontalAlign = HorizontalAlign.Center;
                if (selectionMode == CalendarSelectionMode.DayWeekMonth)
                {
                    int days = visibleDate.Subtract(baseDate).Days;
                    int dayOfMonth = this.threadCalendar.GetDaysInMonth(this.threadCalendar.GetYear(visibleDate), this.threadCalendar.GetMonth(visibleDate), this.threadCalendar.GetEra(visibleDate));
                    if (this.IsMinSupportedYearMonth(visibleDate))
                    {
                        dayOfMonth = (dayOfMonth - this.threadCalendar.GetDayOfMonth(visibleDate)) + 1;
                    }
                    else if (this.IsMaxSupportedYearMonth(visibleDate))
                    {
                        dayOfMonth = this.threadCalendar.GetDayOfMonth(this.maxSupportedDate);
                    }
                    string eventArgument = "R" + (((days * 100) + dayOfMonth)).ToString(CultureInfo.InvariantCulture);
                    style.CopyFrom(this.SelectorStyle);
                    string title = null;

                    this.OnRenderCalendarCell(writer, style, this.SelectMonthText, title, buttonsActive, eventArgument);
                }
                else
                {
                    style.CopyFrom(this.DayHeaderStyle);
                    this.OnRenderCalendarCell(writer, style, string.Empty, null, false, null);
                }
            }

            TableItemStyle weekdayStyle = new TableItemStyle();
            weekdayStyle.HorizontalAlign = HorizontalAlign.Center;
            weekdayStyle.CopyFrom(this.DayHeaderStyle);
            
            DayNameFormat dayNameFormat = this.DayNameFormat;
            int firstDay = this.NumericFirstDayOfWeek();
            for (int i = firstDay; i < (firstDay + 7); i++)
            {
                string dayName;
                int dayOfWeekNumber = i % 7;

                switch (dayNameFormat)
                {
                    case DayNameFormat.Full:
                        dayName = currentInfo.GetDayName((DayOfWeek) dayOfWeekNumber);
                        break;

                    case DayNameFormat.FirstLetter:
                        dayName = currentInfo.GetDayName((DayOfWeek) dayOfWeekNumber).Substring(0, 1);
                        break;

                    case DayNameFormat.FirstTwoLetters:
                        dayName = currentInfo.GetDayName((DayOfWeek) dayOfWeekNumber).Substring(0, 2);
                        break;

                    case DayNameFormat.Shortest:
                        dayName = currentInfo.GetShortestDayName((DayOfWeek) dayOfWeekNumber);
                        break;

                    default:
                        dayName = currentInfo.GetAbbreviatedDayName((DayOfWeek) dayOfWeekNumber);
                        break;
                }
             
                this.OnRenderCalendarCell(writer, weekdayStyle, dayName, null, false, null);
            }
            writer.Write("</tr>");
        }

        protected virtual void OnRenderCalendarCell(HtmlTextWriter writer, Style style, string text, string title, bool hasButton, string eventArgument)
        {
            OnRenderCalendarCell(writer, style, text, title, hasButton, eventArgument, null);
        }

        protected virtual void OnRenderCalendarCell(HtmlTextWriter writer, Style style, string text, string title, bool hasButton, string eventArgument, CalendarDay dayInfo)
        {
            style.AddAttributesToRender(writer, this);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            //Fix for displaying correct Hindi numbers
            //when the page is not in RTL mode.
            if (IsRightToLeftCulture)
            {
                text = toFarsi.Convert(text);
                title = toFarsi.Convert(title);
            }

            if (hasButton)
            {
                var args = new RenderCalendarCellArgs(writer, style, dayInfo, eventArgument, text, title);

                //Only raise event for days rendering
                if(dayInfo != null)
                {
                    RaiseRenderCalendarCell(args);
                }

                if (!args.Handled)
                {
                    Color foreColor = style.ForeColor;
                    writer.Write("<a href=\"");
                    writer.Write(this.Page.ClientScript.GetPostBackClientHyperlink(this, eventArgument, true));
                    writer.Write("\" style=\"color:");
                    writer.Write(foreColor.IsEmpty ? this.defaultButtonColorText : ColorTranslator.ToHtml(foreColor));

                    if (!string.IsNullOrEmpty(title))
                    {
                        writer.Write("\" title=\"");
                        writer.Write(title);
                    }

                    writer.Write("\">");
                    writer.Write(text);
                    writer.Write("</a>");
                }
            }
            else
            {
                writer.Write(text);
            }

            writer.RenderEndTag();
        }

        protected virtual void RaiseRenderCalendarCell(RenderCalendarCellArgs args)
        {
            if(RenderCalendarCell != null)
            {
                RenderCalendarCell(this, args);
            }
        }

        protected virtual void OnRenderDays(HtmlTextWriter writer, DateTime firstDay, DateTime visibleDate, CalendarSelectionMode selectionMode, bool buttonsActive)
        {
            var time = firstDay;
            var hasWeekSelector = this.HasWeekSelectors(selectionMode);
            var unit = Unit.Percentage(14.0);

            TableItemStyle selectorStyle = null;
            if (hasWeekSelector)
            {
                selectorStyle = new TableItemStyle
                {
                    Width = Unit.Percentage(12.0),
                    HorizontalAlign = HorizontalAlign.Center
                };

                selectorStyle.CopyFrom(this.SelectorStyle);
                unit = Unit.Percentage(12.0);
            }

            var hasCustomDayRenderer = base.Events[MonthViewEventDayRender] != null;
            var styleArray = new TableItemStyle[16];
            var definedStyleMask = this.GetDefinedStyleMask();
            var todaysDate = this.TodaysDate;
            var selectWeekText = this.SelectWeekText;
            var hasButton = buttonsActive && (selectionMode != CalendarSelectionMode.None);
            var month = this.threadCalendar.GetMonth(visibleDate);
            var days = firstDay.Subtract(baseDate).Days;
            var isDesignMode = base.DesignMode && (this.SelectionMode != CalendarSelectionMode.None);
            
            var firstWeek = 0;
            if (this.IsMinSupportedYearMonth(visibleDate))
            {
                firstWeek = ((int)this.threadCalendar.GetDayOfWeek(firstDay)) - this.NumericFirstDayOfWeek();
                if (firstWeek < 0)
                {
                    firstWeek += 7;
                }
            }

            var isLastSupportedDay = false;
            var firstDate = this.threadCalendar.AddMonths(this.maxSupportedDate, -1);
            var isFirstWeek = this.IsMaxSupportedYearMonth(visibleDate) || this.IsTheSameYearMonth(firstDate, visibleDate);

            for (var i = 0; i < 6; i++)
            {
                if (isLastSupportedDay)
                    return;
                
                writer.Write("<tr>");
                if (hasWeekSelector)
                {
                    var weekNo = (days * 100) + 7;
                    if (firstWeek > 0)
                    {
                        weekNo -= firstWeek;
                    }
                    else if (isFirstWeek)
                    {
                        var dateDiff = this.maxSupportedDate.Subtract(time).Days;
                        if (dateDiff < 6)
                        {
                            weekNo -= 6 - dateDiff;
                        }
                    }

                    var eventArgument = "R" + weekNo.ToString(CultureInfo.InvariantCulture);
                    this.OnRenderCalendarCell(writer, selectorStyle, selectWeekText, null, buttonsActive, eventArgument);
                }

                for (var j = 0; j < 7; j++)
                {
                    if (firstWeek > 0)
                    {
                        j += firstWeek;
                        while (firstWeek > 0)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.RenderEndTag();
                            firstWeek--;
                        }
                    }
                    else if (isLastSupportedDay)
                    {
                        while (j < 7)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.RenderEndTag();
                            j++;
                        }
                        break;
                    }
                    
                    var dayOfWeek = (int)this.threadCalendar.GetDayOfWeek(time);
                    var dow = (DayOfWeek) dayOfWeek;
                    var dayOfMonth = this.threadCalendar.GetDayOfMonth(time);
                    var monthValue = cachedNumbers[dayOfMonth];
                    
                    var day = new CalendarDay(time, IsWeekEnd(dow), time.Equals(todaysDate), this.SelectedDates != null && this.SelectedDates.Contains(time), this.threadCalendar.GetMonth(time) != month, monthValue);
                    var dayFlag = GetDayFlag(day);
                    var styleMask = definedStyleMask & dayFlag;
                    var index = styleMask & 15;
                    var mergedStyle = styleArray[index];
                    if (mergedStyle == null)
                    {
                        mergedStyle = new TableItemStyle();
                        this.SetDayStyles(mergedStyle, styleMask, unit);
                        styleArray[index] = mergedStyle;
                    }

                    if (hasCustomDayRenderer)
                    {
                        OnRenderCustomDay(buttonsActive, writer, days, monthValue, day, mergedStyle, hasButton);
                    }
                    else
                    {
                        if (isDesignMode && mergedStyle.ForeColor.IsEmpty)
                        {
                            mergedStyle.ForeColor = this.defaultForeColor;
                        }
                        this.OnRenderCalendarCell(writer, mergedStyle, monthValue, null, hasButton, days.ToString(CultureInfo.InvariantCulture), day);
                    }

                    if (isFirstWeek && time.Month == this.maxSupportedDate.Month && time.Day == this.maxSupportedDate.Day)
                    {
                        isLastSupportedDay = true;
                    }
                    else
                    {
                        time = this.threadCalendar.AddDays(time, 1);
                        days++;
                    }
                }
                writer.Write("</tr>");
            }
        }

        private void OnRenderCustomDay(bool buttonsActive, HtmlTextWriter writer, int days, string monthValue, CalendarDay day, Style mergedStyle, bool hasButton)
        {
            var cell = new TableCell();
            cell.ApplyStyle(mergedStyle);
            
            var child = new LiteralControl(monthValue);
            cell.Controls.Add(child);
            day.IsSelectable = hasButton;

            this.OnDayRender(cell, day);

            child.Text = this.GetCalendarButtonText(days.ToString(CultureInfo.InvariantCulture), monthValue, null, buttonsActive && day.IsSelectable, cell.ForeColor);
            cell.RenderControl(writer);
        }

        private static int GetDayFlag(CalendarDay day)
        {
            var dayFlag = 16;

            if (day.IsSelected)
                dayFlag |= 8;
            
            if (day.IsOtherMonth)
                dayFlag |= 2;
            
            if (day.IsToday)
                dayFlag |= 4;
            
            if (day.IsWeekend)
                dayFlag |= 1;
            
            return dayFlag;
        }

        private void RenderMonthTitle(HtmlTextWriter writer, TableItemStyle titleStyle, DateTime visibleDate)
        {
            var monthNameStyle = new TableItemStyle
             {
                 HorizontalAlign = (titleStyle.HorizontalAlign != HorizontalAlign.NotSet ? titleStyle.HorizontalAlign : HorizontalAlign.Center), 
                 Wrap = titleStyle.Wrap, 
                 Width = Unit.Percentage(70.0)
             };

            string renderingTitle;
            switch (this.TitleFormat)
            {
                default:
                case TitleFormat.Month:
                    renderingTitle = GetMonthName(visibleDate);
                    break;

                case TitleFormat.MonthYear:
                    renderingTitle = GetMonthYear(visibleDate);
                    break;
            }

            this.OnRenderCalendarCell(writer, monthNameStyle, renderingTitle, null, false, null);
        }

        private void RenderNextMonthNav(HtmlTextWriter writer, DateTime visibleDate, bool buttonsActive)
        {
            var nextPrevFormat = this.NextPrevFormat;
            var nextPrevStyle = new TableItemStyle();
            nextPrevStyle.Width = Unit.Percentage(15.0);
            nextPrevStyle.CopyFrom(this.NextPrevStyle);

            if (this.IsMinSupportedYearMonth(visibleDate))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.RenderEndTag();
            }
            else
            {
                string nextMonthText;
                nextPrevStyle.HorizontalAlign = IsRightToLeftCulture ? HorizontalAlign.Right : HorizontalAlign.Left;

                switch (nextPrevFormat)
                {
                    case NextPrevFormat.ShortMonth:
                    case NextPrevFormat.FullMonth:
                    {
                        var month = this.threadCalendar.GetMonth(this.threadCalendar.AddMonths(visibleDate, -1));
                        nextMonthText = this.GetMonthName(month, nextPrevFormat == NextPrevFormat.FullMonth);
                        break;
                    }
                    default:
                    {
                        nextMonthText = this.NextMonthText;
                        break;
                    }
                }

                var prevDate = GetPreviousDate(visibleDate);
                var eventArgument = "V" + prevDate.Subtract(baseDate).Days.ToString(CultureInfo.InvariantCulture);
                this.OnRenderCalendarCell(writer, nextPrevStyle, nextMonthText, null, buttonsActive, eventArgument);
            }
        }

        private void RenderPreviousMonthNav(HtmlTextWriter writer, DateTime visibleDate, bool buttonsActive)
        {
            var nextPrevFormat = this.NextPrevFormat;
            var nextPrevStyle = new TableItemStyle();
            nextPrevStyle.Width = Unit.Percentage(15.0);
            nextPrevStyle.CopyFrom(this.NextPrevStyle);

            if (this.IsMaxSupportedYearMonth(visibleDate))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.RenderEndTag();
            }
            else
            {
                string prevMonthText;
                nextPrevStyle.HorizontalAlign = IsRightToLeftCulture ? HorizontalAlign.Left : HorizontalAlign.Right;
                switch (nextPrevFormat)
                {
                    case NextPrevFormat.ShortMonth:
                    case NextPrevFormat.FullMonth:
                    {
                        var m = this.threadCalendar.GetMonth(this.threadCalendar.AddMonths(visibleDate, 1));
                        prevMonthText = this.GetMonthName(m, nextPrevFormat == NextPrevFormat.FullMonth);
                        break;
                    }
                    default:
                    {
                        prevMonthText = this.PrevMonthText;
                        break;
                    }
                }

                var nextDate = this.threadCalendar.AddMonths(visibleDate, 1);
                var eventArgument = "V" + nextDate.Subtract(baseDate).Days.ToString(CultureInfo.InvariantCulture);
                this.OnRenderCalendarCell(writer, nextPrevStyle, prevMonthText, null, buttonsActive, eventArgument);
            }
        }

        #endregion

        #region Styling

        private Table CreateTable()
        {
            var table = new Table();
            table.ID = this.ClientID;

            if(IsRightToLeftCulture)
                table.Attributes.Add("dir", "rtl");

            table.CopyBaseAttributes(this);

            return table;
        }

        private int GetDefinedStyleMask()
        {
            int currentMask = 8;

            if (this.DayStyle != null && !this.DayStyle.IsEmpty)
            {
                currentMask |= 16;
            }
            if (this.TodayDayStyle != null && !this.TodayDayStyle.IsEmpty)
            {
                currentMask |= 4;
            }
            if (this.OtherMonthDayStyle != null && !this.OtherMonthDayStyle.IsEmpty)
            {
                currentMask |= 2;
            }
            if (this.WeekendDayStyle != null && !this.WeekendDayStyle.IsEmpty)
            {
                currentMask |= 1;
            }

            return currentMask;
        }

        private string GetCalendarButtonText(string eventArgument, string buttonText, string title, bool showLink, Color foreColor)
        {
            if (!showLink)
            {
                return buttonText;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<a href=\"");
            builder.Append(this.Page.ClientScript.GetPostBackClientHyperlink(this, eventArgument, true));
            builder.Append("\" style=\"color:");
            builder.Append(foreColor.IsEmpty ? this.defaultButtonColorText : ColorTranslator.ToHtml(foreColor));
            if (!string.IsNullOrEmpty(title))
            {
                builder.Append("\" title=\"");
                builder.Append(title);
            }
            builder.Append("\">");
            builder.Append(buttonText);
            builder.Append("</a>");
            return builder.ToString();
        }

        private void SetDayStyles(TableItemStyle style, int styleMask, Unit defaultWidth)
        {
            style.Width = defaultWidth;
            style.HorizontalAlign = HorizontalAlign.Center;
            if ((styleMask & 16) != 0)
            {
                style.CopyFrom(this.DayStyle);
            }
            if ((styleMask & 1) != 0)
            {
                style.CopyFrom(this.WeekendDayStyle);
            }
            if ((styleMask & 2) != 0)
            {
                style.CopyFrom(this.OtherMonthDayStyle);
            }
            if ((styleMask & 4) != 0)
            {
                style.CopyFrom(this.TodayDayStyle);
            }
            if ((styleMask & 8) != 0)
            {
                style.ForeColor = Color.White;
                style.BackColor = Color.Silver;
                style.CopyFrom(this.SelectedDayStyle);
            }
        }

        private void ApplyTitleStyle(TableCell titleCell, Table titleTable, TableItemStyle titleStyle)
        {
            if (titleStyle.BackColor != Color.Empty)
            {
                titleCell.BackColor = titleStyle.BackColor;
            }
            if (titleStyle.BorderColor != Color.Empty)
            {
                titleCell.BorderColor = titleStyle.BorderColor;
            }
            if (titleStyle.BorderWidth != Unit.Empty)
            {
                titleCell.BorderWidth = titleStyle.BorderWidth;
            }
            if (titleStyle.BorderStyle != BorderStyle.NotSet)
            {
                titleCell.BorderStyle = titleStyle.BorderStyle;
            }
            if (titleStyle.Height != Unit.Empty)
            {
                titleCell.Height = titleStyle.Height;
            }
            if (titleStyle.VerticalAlign != VerticalAlign.NotSet)
            {
                titleCell.VerticalAlign = titleStyle.VerticalAlign;
            }
            if (titleStyle.CssClass.Length > 0)
            {
                titleTable.CssClass = titleStyle.CssClass;
            }
            else if (this.CssClass.Length > 0)
            {
                titleTable.CssClass = this.CssClass;
            }
            if (titleStyle.ForeColor != Color.Empty)
            {
                titleTable.ForeColor = titleStyle.ForeColor;
            }
            else if (this.ForeColor != Color.Empty)
            {
                titleTable.ForeColor = this.ForeColor;
            }
            titleTable.Font.CopyFrom(titleStyle.Font);
            titleTable.Font.MergeWith(this.Font);
        }

        #endregion

        #region Properties

        public Calendar CustomCalendar
        {
            get { return ViewState.GetValue("CustomCalendar", new GregorianCalendar()); }
            set { ViewState.SetValue("CustomCalendar", value); }
        }

        public CultureInfo DefaultCulture
        {
            get
            {
                if (FALocalizeManager.Instance.CustomCulture != null)
                {
                    return FALocalizeManager.Instance.CustomCulture;
                }

                if (Thread.CurrentThread.CurrentUICulture.Equals(FALocalizeManager.Instance.FarsiCulture))
                {
                    //Fix : Returns newly created PersianCultureInfo
                    //return FALocalizeManager.Instance.FarsiCulture;
                    return persianCulture;
                }

                if (Thread.CurrentThread.CurrentUICulture.Equals(FALocalizeManager.Instance.ArabicCulture))
                {
                    return FALocalizeManager.Instance.ArabicCulture;
                }

                return FALocalizeManager.Instance.InvariantCulture;
            }
            set
            {
                FALocalizeManager.Instance.CustomCulture = value;
            }
        }

        [DefaultValue(FirstDayOfWeek.Saturday)]
        public new FirstDayOfWeek FirstDayOfWeek
        {
            get
            {
                FirstDayOfWeek defaultVlaue = IsRightToLeftCulture ? FirstDayOfWeek.Saturday : FirstDayOfWeek.Default;
                return ViewState.GetValue("FirstDayOfWeek", defaultVlaue);
            }
            set
            {
                ViewState.SetValue("FirstDayOfWeek", value);
            }
        }
 
        private bool IsRightToLeftCulture
        {
            get { return DefaultCulture.TextInfo.IsRightToLeft; }
        }

        private bool IsHijriCalendar
        {
            get
            {
                Calendar c = GetDefaultCalendar();
                return c is PersianCalendar ||
                       c is System.Globalization.PersianCalendar ||
                       c is HijriCalendar;
            }
        }

        #endregion

        #region Helpers

        private static object GetEventReference(string eventName)
        {
            return typeof (System.Web.UI.WebControls.Calendar).GetField(eventName, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static);
        }

        #endregion
    }
}
