using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using FarsiLibrary.Localization;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Win.DevExpress
{
    public class VistaPersianCalendarViewInfo : VistaCalendarViewInfo
    {
        private readonly PopupPersianCalendarControl calendarControl;

        public VistaPersianCalendarViewInfo(PopupPersianCalendarControl calendarControl) : base(calendarControl)
        {
            this.calendarControl = calendarControl;
        }

        protected override string GetAbbreviatedDayNameCore(int day)
        {
            string dayname = Calendar.DateFormat.AbbreviatedDayNames[(Convert.ToInt32(Calendar.FirstDayOfWeek) + day) % 7];
            return dayname;
        }

        protected override CalendarHeaderViewInfoBase CreateHeaderInfo()
        {
            return new VistaPersianCalendarHeaderViewInfo(this);
        }

        protected override CalendarFooterViewInfoBase CreateFooterInfo()
        {
            return new VistaPersianCalendarFooterViewInfo(this);
        }

        protected override Size CalcDayCellTextSize()
        {
            return CalcDayCellTextSizeCore(DayCellContentTextTemplate);
        }

        protected override Size CalcMonthInfoCellTextSize()
        {
            return CalcDayCellTextSizeCore("111");
        }

        protected override CalendarObjectViewInfo CreateCalendar(int index)
        {
            var calendarObjectViewInfo = new PersianCalendarObjectViewInfo(Calendar)
            {
                ShouldShowHeader = ShowCalendarHeader(index),
                ViewType = Calendar.View
            };

            return calendarObjectViewInfo;
        }

        //protected override void CalcDayNumberCells()
        //{
        //    DayCells.Clear();
        //    WeekCells.Clear();
        //    int x = MonthNumbersRect.Left, y = MonthNumbersRect.Top;
        //    DateTime cur = MinValue;
        //    for (int j = 0; j < 6; j++, y += TextHeight + TextHeightIndent)
        //    {
        //        int penalty = (j == 0 ? PenaltyIndex : 0);
        //        for (int i = 0; i < 7; i++, x += DayCellWidth + TextWidthIndent)
        //        {
        //            if (j == 0 && i < PenaltyIndex) continue;
        //            try { cur = FirstVisibleDate.AddDays(j * 7 + i - PenaltyIndex); }
        //            catch
        //            {
        //                j = 6;
        //                break;
        //            }
        //            if (CanAddDate(cur))
        //            {
        //                DayNumberCellInfo cell = CreateDayCell(cur);
        //                cell.Bounds = new Rectangle(x, y, DayCellWidth + TextWidthIndent, TextHeight + TextHeightIndent);
        //                cell.Text = GetDayText(cur);
        //                cell.TextBounds = CalcDateNumberBounds(cell.Bounds);
        //                DayCells.Add(cell);
        //            }
        //        }
        //        x = MonthNumbersRect.Left;
        //        if (ShowWeekNumbers && cur != MinValue)
        //        {
        //            DayNumberCellInfo weekCell = CreateDayCell(cur);
        //            int? weekNumber = GetWeekNumber(cur);
        //            weekCell.Text = weekNumber.HasValue ? weekNumber.ToString() : "";
        //            weekCell.Bounds = CalcWeekCellBounds(weekCell.Text, x, y);
        //            weekCell.SetAppearance(Appearance);
        //            weekCell.SetAppearance(weekCell.Appearance.Clone() as AppearanceObject);
        //            weekCell.Appearance.ForeColor = Color.Red;
        //            WeekCells.Add(weekCell);
        //        }
        //    }
        //    UpdateExistingCellsState();
        //}

        //protected override DayNumberCellInfo CreateMonthCellInfo(int row, int col)
        //{
        //    DayNumberCellInfo currInfo;
        //    var currentDate = (PersianDate)DateTime;
        //    PersianDate pd = new PersianDate(currentDate.Year, 1 + row * 4 + col, 1);
        //    DateTime dt = pd;
        //    if (dt > calendar.MaxDateValue) return null;
        //    if (dt < calendar.MinDateValue && dt.Month < calendar.MinDateValue.Month) return null;
        //    currInfo = new DayNumberCellInfo(dt);
        //    currInfo.Text = pd.LocalizedMonthName;
        //    return currInfo;
        //}

        //protected override DayNumberCellInfo CreateQuarterCellInfo(int row, int col)
        //{
        //    return base.CreateQuarterCellInfo(row, col);
        //}

        //protected override DayNumberCellInfo CreateYearCellInfo(int row, int col)
        //{
        //    DayNumberCellInfo currInfo;
        //    var currentYear = ((PersianDate)DateTime).Year;
        //    int beginYear = (currentYear / 10) * 10 - 1;
        //    int currYear = beginYear + row * 4 + col;
        //    if (currYear <= 0 || currYear >= 10000) return null;
        //    PersianDate pd = new PersianDate(currYear, 1, 1);
        //    DateTime dt = pd;

        //    if (dt > calendar.MaxDateValue) return null;
        //    if (dt < calendar.MinDateValue && dt.Year < calendar.MinDateValue.Year) return null;

        //    currInfo = new CalendarCellViewInfo(dt);
        //    currInfo.Text = toFarsi.Convert(pd.Year.ToString());
        //    if (currYear < (pd.Year / 10) * 10 || currYear > (pd.Year / 10) * 10 + 1) currInfo.State = ObjectState.Disabled;
        //    return currInfo;
        //}

        //protected override CalendarCellViewInfo CreateYearsGroupCellInfo(int row, int col)
        //{
        //    CalendarCellViewInfo currInfo;
        //    var currentDate = (PersianDate)DateTime;
        //    var currentYear = currentDate.Year;
        //    int beginYearGroup = (currentYear / 100) * 100 - 10;
        //    int currYearGroup = beginYearGroup + (row * 4 + col) * 10;
        //    if (currYearGroup < 0 || currYearGroup >= 10000) return null;
        //    int endYearGroup = currYearGroup + 9;
        //    if (currYearGroup == 0) currYearGroup = 1;
        //    PersianDate dateStart = new PersianDate(currYearGroup, 1, 1);
        //    DateTime dt1 = dateStart;
        //    if (dt1 > calendar.MaxDateValue) return null;
        //    if (dt1 < calendar.MinDateValue && endYearGroup < calendar.MinDateValue.Year) return null;
        //    currInfo = new CalendarCellViewInfo(dt1);

        //    var dateEnd = new PersianDate(endYearGroup, 1, 1);
        //    currInfo.Text = toFarsi.Convert(dateStart.Year.ToString(CultureInfo.CurrentUICulture) + "-\n" +
        //                                    dateEnd.Year.ToString(CultureInfo.CurrentUICulture));
        //    return currInfo;
        //}


        public PersianDate GetTodayDate()
        {
            return PersianDate.Today;
        }
    }

    public class PersianCalendarObjectViewInfo : CalendarObjectViewInfo
    {
        public PersianCalendarObjectViewInfo(CalendarControlBase calendar) : base(calendar)
        {
        }

        public bool ShouldShowHeader
        {
            get { return ShowHeader; }
            set { ShowHeader = value; }
        }

        public DateEditCalendarViewType ViewType
        {
            get { return View; }
            set { View = value; }
        }

        protected override bool IsHolidayDate(CalendarCellViewInfo cell)
        {
            return View == DateEditCalendarViewType.MonthInfo &&
                   cell.Date.DayOfWeek == DayOfWeek.Friday;
        }

        protected override void CalcFirstVisibleDate()
        {
            SetFirstVisibleDate(GetFirstVisibleDate(CurrentDate));
        }

        public override DateTime GetFirstVisibleDate(DateTime editDate)
        {
            try
            {
                DateTime firstMonthDate = GetFarsiFirstMonthDate(editDate);
                return firstMonthDate;
            }
            catch
            {
                return MinValue;
            }
        }

        private static DateTime GetFarsiFirstMonthDate(DateTime date)
        {
            var pd = (PersianDate)date;
            return pd.StartOfMonth();
        }

        protected override bool IsToday(CalendarCellViewInfo cell)
        {
            return AreDatesEqual(cell.Date, PersianDate.Today);
        }

        protected override bool IsDateSelected(CalendarCellViewInfo cell)
        {
            var currentDate = (PersianDate)DateTime;
            var cellDate = (PersianDate)cell.Date;

            if (View == DateEditCalendarViewType.YearInfo)
            {
                return cellDate.Month == currentDate.Month;
            }

            if (View == DateEditCalendarViewType.YearsInfo)
            {
                return cellDate.Year == currentDate.Year;
            }

            if (View == DateEditCalendarViewType.YearsGroupInfo)
            {
                return currentDate.Year >= cellDate.Year &&
                       currentDate.Year < cellDate.Year + 10;
            }

            return AreDatesEqual(cell.Date, DateTime);
        }

        protected bool AreDatesEqual(PersianDate dt1, PersianDate dt2)
        {
            return (dt1.Year == dt2.Year && dt1.Month == dt2.Month && dt1.Day == dt2.Day);
        }

        protected override bool IsDateActive(CalendarCellViewInfo cell)
        {
            var current = (PersianDate)CurrentDate;
            var cellDate = (PersianDate)cell.Date;

            if (View != DateEditCalendarViewType.MonthInfo)
                return true;

            return current.Month == cellDate.Month;
        }

        protected override CalendarCellViewInfo CreateDayCell(DateTime date)
        {
            return new PersianCalendarCellViewInfo(date, this);
        }

        protected override string GetCellText(DateTime date, int row, int column)
        {
            var pd = (PersianDate) date;

            if (View == DateEditCalendarViewType.QuarterInfo)
                return (row * 2 + column + 1).ToString();

            if (View == DateEditCalendarViewType.YearInfo)
                return pd.LocalizedMonthName;

            if (View == DateEditCalendarViewType.YearsInfo)
                return toFarsi.Convert(pd.Year.ToString());

            if (View == DateEditCalendarViewType.YearsGroupInfo)
            {
                var dateEnd = new PersianDate(pd.Year + 9, 1, 1);
                return toFarsi.Convert(pd.Year + "-\n" + dateEnd.Year);
            }

            return toFarsi.Convert(pd.Day.ToString());
        }

        protected virtual void CalcDayCells()
        {
            this.DayCells.Clear();
            int x1 = this.DayCellsBounds.X;
            int y = this.DayCellsBounds.Y;
            DateTime minValue = this.MinValue;
            this.RowCount = this.GetCellRowCount();
            this.ColumnCount = this.GetCellColumnCount();
            bool rightToLeftLayout = WindowsFormsSettings.GetIsRightToLeftLayout((Control)this.Calendar);
            for (int index1 = 0; index1 < this.RowCount; ++index1)
            {
                int x2 = rightToLeftLayout ? this.DayCellsBounds.Right - this.ActualCellSize.Width : this.DayCellsBounds.X;
                for (int index2 = 0; index2 < this.ColumnCount; ++index2)
                {
                    bool correctDate = true;
                    if (index1 == 0)
                    {
                        if (index2 < this.PenaltyIndex)
                        {
                            x2 += rightToLeftLayout ? -this.ActualCellSize.Width : this.ActualCellSize.Width;
                            continue;
                        }
                    }
                    DateTime date;
                    try
                    {
                        date = this.CalcDate(index1, index2, out correctDate);
                    }
                    catch
                    {
                        index1 = this.RowCount;
                        break;
                    }
                    if (correctDate && this.CanAddDate(date))
                    {
                        PersianCalendarCellViewInfo dayCell = (PersianCalendarCellViewInfo) this.CreateDayCell(date);
                        dayCell.UpdateVisualState();
                        dayCell.Bounds = this.CalcCellBounds(x2, y);
                        dayCell.Text = this.GetCellText(date, index1, index2);
                        dayCell.ContentBounds = this.CalcDayCellContentBounds(dayCell.Bounds);
                        dayCell.CalculateTextBounds();
                        dayCell.Row = index1;
                        dayCell.Column = index2;
                        this.UpdateDayCell(dayCell);
                        this.DayCells.Add(dayCell);
                        this.CalendarInfo.AddCellToNavigationGrid(dayCell, this.Row, this.Column, index1, index2);
                    }
                    x2 += rightToLeftLayout ? -this.ActualCellSize.Width : this.ActualCellSize.Width;
                }
                y += this.ActualCellSize.Height;
            }
        }

        private DateTime CalcDate(int row, int column, out bool correctDate)
        {
            correctDate = true;
            if (this.View == DateEditCalendarViewType.QuarterInfo)
                return new DateTime(this.CurrentDate.Year, row * 6 + column * 3 + 1, 1);
            if (this.View == DateEditCalendarViewType.YearInfo)
                return new DateTime(this.CurrentDate.Year, 1 + row * 4 + column, 1);
            if (this.View == DateEditCalendarViewType.YearsInfo)
            {
                int year = this.CurrentDate.Year / 10 * 10 - 1 + row * 4 + column;
                if (year > 0 && year < 10000)
                    return new DateTime(year, 1, 1);
                correctDate = false;
                return DateTime.MinValue;
            }
            if (this.View != DateEditCalendarViewType.YearsGroupInfo)
                return this.FirstVisibleDate.AddDays((double)(row * this.GetCellColumnCount() + column - this.PenaltyIndex));
            int year1 = this.CurrentDate.Year / 100 * 100 - 10 + (row * 4 + column) * 10;
            if (year1 < 0 || year1 >= 10000)
            {
                correctDate = false;
                return DateTime.MinValue;
            }
            if (year1 == 0)
                year1 = 1;
            return new DateTime(year1, 1, 1);
        }

    }

    public class PersianCalendarCellViewInfo : CalendarCellViewInfo
    {
        public PersianCalendarCellViewInfo(DateTime date, PersianCalendarObjectViewInfo viewInfo) : base(date, viewInfo)
        {
        }

        public void CalculateTextBounds()
        {
            CalcTextBounds();
        }
    }

    public class VistaPersianCalendarFooterViewInfo : VistaCalendarFooterViewInfo
    {
        private BaseLocalizer localizer;

        public VistaPersianCalendarFooterViewInfo(VistaPersianCalendarViewInfo viewInfo) : base(viewInfo)
        {
            localizer = FALocalizeManager.Instance.GetLocalizer();
        }

        public override void UpdateContent()
        {
            base.UpdateContent();
            TodayButton.Caption = localizer.GetLocalizedString(StringID.FAMonthView_Today);
            ClearButton.Caption = localizer.GetLocalizedString(StringID.FAMonthView_None);
            OkButton.Caption = localizer.GetLocalizedString(StringID.MessageBox_Ok);
            CancelButton.Caption = localizer.GetLocalizedString(StringID.MessageBox_Cancel);
        }
    }

    public class VistaPersianCalendarHeaderViewInfo : VistaCalendarHeaderViewInfo
    {
        private readonly VistaPersianCalendarViewInfo viewInfo;

        public VistaPersianCalendarHeaderViewInfo(VistaPersianCalendarViewInfo viewInfo) : base(viewInfo)
        {
            this.viewInfo = viewInfo;
        }

        protected override string GetTodayCaption()
        {
            var pd = (PersianDate)viewInfo.DateTime;
            return toFarsi.Convert(pd.ToWritten());
        }

        protected override string GetCurrentDateCaption()
        {
            if (!IsLocalizedDateTimeValid(ViewInfo.DateTime))
                return string.Empty;

            var pd = (PersianDate) viewInfo.DateTime;

            if (Calendar.View == DateEditCalendarViewType.MonthInfo)
                return pd.LocalizedMonthName + " " + toFarsi.Convert(pd.Year.ToString());

            if (Calendar.View == DateEditCalendarViewType.YearInfo ||
                Calendar.View == DateEditCalendarViewType.QuarterInfo)
            {
                return toFarsi.Convert(pd.Year.ToString());
            }

            if (Calendar.View == DateEditCalendarViewType.YearsInfo)
            {
                var dtFrom = new PersianDate(Math.Max(1, pd.Year/10*10), 1, 1);
                var dtTo = new PersianDate(pd.Year/10*10 + 9, 1, 1);

                if (!IsLocalizedDateTimeValid(dtFrom) || !IsLocalizedDateTimeValid(dtTo))
                    return "";

                return toFarsi.Convert(dtFrom.Year + "-" + dtTo.Year);
            }

            if (Calendar.View == DateEditCalendarViewType.YearsGroupInfo)
            {
                var dtFrom = new PersianDate(Math.Max(1, pd.Year/100*100), 1, 1);
                var dtTo = new PersianDate(pd.Year/100*100 + 99, 1, 1);

                if (!IsLocalizedDateTimeValid(dtFrom) || !IsLocalizedDateTimeValid(dtTo))
                    return string.Empty;

                return toFarsi.Convert(dtFrom.Year + "-" + pd.Year);
            }

            return string.Empty;
        }

        private string GetFarsiCaption()
        {
            var date = (PersianDate)ViewInfo.DateTime;
            var year = toFarsi.Convert(date.Year.ToString());

            if (Calendar.View == DateEditCalendarViewType.MonthInfo)
                return year + " " + date.LocalizedMonthName;

            if (Calendar.View == DateEditCalendarViewType.YearInfo ||
                Calendar.View == DateEditCalendarViewType.QuarterInfo)
                return year;

            if (Calendar.View == DateEditCalendarViewType.YearsInfo)
            {
                int yearNo = Math.Max(1, (date.Year / 10) * 10);
                var dtFrom = new PersianDate(yearNo, 1, 1);
                var dtTo = new PersianDate(((date.Year / 10) * 10) + 9, 1, 1);

                return toFarsi.Convert(dtFrom.Year.ToString()) + "-" +
                       toFarsi.Convert(dtTo.Year.ToString());
            }

            if (Calendar.View == DateEditCalendarViewType.YearsGroupInfo)
            {
                int yearNo = Math.Max(1, (date.Year / 100) * 100);
                PersianDate dtFrom = new PersianDate(yearNo, 1, 1);
                PersianDate dtTo = new PersianDate(((date.Year / 100) * 100) + 99, 1, 1);
                return toFarsi.Convert(dtFrom.Year.ToString()) + "-" +
                       toFarsi.Convert(dtTo.Year.ToString());
            }

            return string.Empty;
        }
    }

    public class VistaPopupPersianDateEditForm : VistaPopupDateEditForm
    {
        public VistaPopupPersianDateEditForm(DateEdit ownerEdit) : base(ownerEdit)
        {
        }

        protected override CalendarControl CreateCalendar()
        {
            var calendar = new PopupPersianCalendarControl();
            return calendar;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Calendar != null)
            {
            }
            base.Dispose(disposing);
        }
    }

    public class PopupPersianCalendarControl : PopupCalendarControl
    {
        private static Utils.PersianCalendar pc;
        private static PersianCultureInfo ci;

        static PopupPersianCalendarControl()
        {
            ci = new PersianCultureInfo();
            pc = new Utils.PersianCalendar();
        }

        public PopupPersianCalendarControl()
        {
        }

        //protected override BaseControlPainter CreatePainter()
        //{
        //    if (ActualCalendarView == CalendarView.Vista)
        //        return new FAVistaDateEditCalendarObjectPainter();

        //    return new CalendarPainter();
        //}

        protected override BaseStyleControlViewInfo CreateViewInfo()
        {
            if (ActualCalendarView == CalendarView.Vista)
                return new VistaPersianCalendarViewInfo(this);

            return new CalendarViewInfo(this);
        }

        //protected override DateTime CalculateFirstMonthDate(DateTime dateTime)
        //{
        //    return FAVistaDateEditInfoArgs.GetFirstMonthDate(dateTime);
        //}

        public override DateTimeFormatInfo DateFormat
        {
            get
            {
                if (fFormat == null)
                    return ci.DateTimeFormat;

                return fFormat;
            }
            set { }
        }

        //protected override bool ShouldUpdateOnDateChange => useDateChangeOverride ? shouldUpdateOnDateChange : base.ShouldUpdateOnDateChange;

        //protected override void OnItemClick(CalendarHitInfo hitInfo)
        //{
        //    useDateChangeOverride = true;
        //    DayNumberCellInfo cell = hitInfo.HitObject as DayNumberCellInfo;
        //    DateEditCalendarViewType prevView = View;
        //    DateEditCalendarViewType nextView = DecView();

        //    if (cell != null)
        //    {
        //        PersianDate cellDate = cell.Date;
        //        PersianDate currentDate = DateTime;

        //        shouldUpdateOnDateChange = false;
        //        if (View == DateEditCalendarViewType.MonthInfo)
        //            DateTime = CorrectPersianDateTime(new PersianDate(cellDate.Year, cellDate.Month, CorrectPersianDay(currentDate.Year, cellDate.Month, cellDate.Day), currentDate.Hour, currentDate.Minute, currentDate.Second, currentDate.Millisecond));
        //        else if (View == DateEditCalendarViewType.YearInfo)
        //            DateTime = CorrectPersianDateTime(new PersianDate(currentDate.Year, cellDate.Month, CorrectPersianDay(currentDate.Year, cellDate.Month, currentDate.Day), currentDate.Hour, currentDate.Minute, currentDate.Second, currentDate.Millisecond));
        //        else if (View == DateEditCalendarViewType.YearsInfo)
        //            DateTime = CorrectPersianDateTime(new PersianDate(cellDate.Year, currentDate.Month, CorrectPersianDay(cellDate.Year, currentDate.Month, currentDate.Day), currentDate.Hour, currentDate.Minute, currentDate.Second, currentDate.Millisecond));
        //        else if (View == DateEditCalendarViewType.YearsGroupInfo)
        //        {
        //            int year = cellDate.Year == 1 ? 0 : cellDate.Year;
        //            DateTime = CorrectPersianDateTime(new PersianDate(year + currentDate.Year % 10, currentDate.Month, CorrectPersianDay(year + currentDate.Year % 10, currentDate.Month, currentDate.Day), currentDate.Hour, currentDate.Minute, currentDate.Second, currentDate.Millisecond));
        //        }
        //        shouldUpdateOnDateChange = true;
        //        ClearCellsSelection();
        //        cell.Selected = true;
        //        if (View != nextView)
        //            OnViewChanging(View, nextView, false);
        //    }
        //    if (View != nextView)
        //    {
        //        ClearSelection();
        //        SetViewCore(nextView);
        //        OnViewChanged(prevView, nextView);
        //    }
        //    else if (Multiselect)
        //    {
        //        SetSelectionRange(DateTime);
        //        Calendars[0].UpdateExistingCellsState();
        //        Invalidate();
        //    }
        //    if (Multiselect)
        //    {
        //        RaiseSelectionChanged();
        //    }

        //    useDateChangeOverride = false;
        //}

        private int CorrectPersianDay(int year, int month, int day)
        {
            if (day <= 0) return 1;
            if (day > pc.GetDaysInMonth(year, month)) return pc.GetDaysInMonth(year, month);

            return day;
        }

        private DateTime CorrectPersianDateTime(PersianDate persianDate)
        {
            return persianDate;
        }

        //internal int AnimationLock => LockAnimation;
        //internal Rectangle RectCaption => CaptionRect;
        //internal Rectangle RectCaptionButton => CaptionButtonRect;
        internal bool RightToLeftSet => IsRightToLeft;
        internal DateTime MaxDateValue => MaxValue;
        internal DateTime MinDateValue => MinValue;
    }
}