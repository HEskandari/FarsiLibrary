using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using FarsiLibrary.Localization;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using PersianCalendar = FarsiLibrary.Utils.PersianCalendar;

namespace FarsiLibrary.Win.DevExpress
{
    public interface IPersianCalendarControl
    {
        DateTime GetTodayPersianDate();
    }

    public class VistaPersianCalendarViewInfo : VistaCalendarViewInfo
    {
        private readonly IPersianCalendarControl calendarControl;
        private PersianCalendarObjectViewInfo calendarObjectViewInfo;
        private readonly PersianCultureInfo culture;

        public VistaPersianCalendarViewInfo(PopupPersianCalendarControl calendarControl) : base(calendarControl)
        {
            this.calendarControl = calendarControl;
            this.culture = new PersianCultureInfo();
        }

        public VistaPersianCalendarViewInfo(XtraFACalendarControl calendarControl) : base(calendarControl)
        {
            this.calendarControl = calendarControl;
            this.culture = new PersianCultureInfo();
        }

        protected override string GetAbbreviatedDayNameCore(int day)
        {
            //Double checking the culture as this may be called in werid scenarios
            if (!CultureManager.Instance.ControlsCulture.IsFarsiCulture()) return base.GetAbbreviatedDayNameCore(day);

            string dayname = culture.DateTimeFormat.AbbreviatedDayNames[(Convert.ToInt32(Calendar.FirstDayOfWeek) + day) % 7];
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
            calendarObjectViewInfo = new PersianCalendarObjectViewInfo(calendarControl)
            {
                ShouldShowHeader = ShowCalendarHeader(index),
                ViewType = Calendar.View
            };

            return calendarObjectViewInfo;
        }

        public void UpdateCellsState()
        {
            calendarObjectViewInfo.UpdateExistingCellsState();
        }

        public CalendarHitInfo HoverData
        {
            get { return HoverInfo; }
            set { HoverInfo = value; }
        }
    }

    public class PersianCalendarObjectViewInfo : CalendarObjectViewInfo
    {
        PersianCalendar pc;

        public PersianCalendarObjectViewInfo(IPersianCalendarControl calendar) : base(calendar as CalendarControlBase)
        {
            pc = new PersianCalendar();
            FirstVisibleDate = calendar.GetTodayPersianDate();
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
            var date = DateTime;
            var firstDate = GetFirstVisibleDate(date);

            SetFirstVisibleDate(firstDate);
        }

        public override void SetFirstVisibleDate(DateTime firstVisibleDate)
        {
            PenaltyIndex = 0;
            FirstVisibleDate = new DateTime(firstVisibleDate.Ticks, firstVisibleDate.Kind);

            if (View != DateEditCalendarViewType.MonthInfo)
                return;

            PenaltyIndex = GetFirstDayOffset(FirstVisibleDate);
        }

        public override DateTime GetFirstVisibleDate(DateTime editDate)
        {
            try
            {
                var firstMonthDate = GetFarsiFirstMonthDate(editDate);
                var timeSpan = TimeSpan.FromDays(-GetFirstDayOffset(firstMonthDate));

                if (firstMonthDate.Ticks + timeSpan.Ticks < 0L)
                    return PersianDate.MinValue;

                return firstMonthDate + timeSpan;
            }
            catch
            {
                return MinValue;
            }
        }

        private static DateTime GetFarsiFirstMonthDate(DateTime date)
        {
            var pd = (PersianDate)date;
            return new PersianDate(pd.Year, pd.Month, 1, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        protected override bool IsToday(CalendarCellViewInfo cell)
        {
            return AreDatesEqual(cell.Date, PersianDate.Today);
        }

        protected override bool IsDateSelected(CalendarCellViewInfo cell)
        {
            var calendarDate = (PersianDate) DateTime;
            var cellDate = (PersianDate)cell.Date;

            if (View == DateEditCalendarViewType.YearInfo)
            {
                return cellDate.Month == calendarDate.Month;
            }

            if (View == DateEditCalendarViewType.YearsInfo)
            {
                return cellDate.Year == calendarDate.Year;
            }

            if (View == DateEditCalendarViewType.YearsGroupInfo)
            {
                return calendarDate.Year >= cellDate.Year &&
                       calendarDate.Year < cellDate.Year + 10;
            }

            return Calendar.SelectedRanges.IsDateSelected(cell.Date);
        }

        protected bool AreDatesEqual(PersianDate dt1, PersianDate dt2)
        {
            return (dt1.Year == dt2.Year && dt1.Month == dt2.Month && dt1.Day == dt2.Day);
        }

        protected override bool IsDateActive(CalendarCellViewInfo cell)
        {
            if (View != DateEditCalendarViewType.MonthInfo)
                return true;

            var current = (PersianDate)DateTime;
            var cellDate = (PersianDate)cell.Date;

            return current.Month == cellDate.Month;
        }

        protected override DateTime CalcCalendarEndDate()
        {
            var pd = (PersianDate)DateTime;
            switch (Calendar.View)
            {
                case DateEditCalendarViewType.MonthInfo:
                case DateEditCalendarViewType.QuarterInfo:
                    return new PersianDate(pd.Year, pd.Month, pc.GetDaysInMonth(pd.Year, pd.Month), 23, 59, 59);
                case DateEditCalendarViewType.YearInfo:
                    return new PersianDate(pd.Year, 12, pc.GetDaysInMonth(pd.Year, 12), 23, 59, 59);
                case DateEditCalendarViewType.YearsInfo:
                {
                    var year = Math.Min(PersianDate.MaxValue.Year, pd.Year + 9);
                    return new PersianDate(year, 12, pc.GetDaysInMonth(year, 12), 23, 59, 59);
                }
                case DateEditCalendarViewType.YearsGroupInfo:
                {
                    var year = Math.Min(PersianDate.MaxValue.Year, pd.Year + 99);
                    return new PersianDate(year, 12, pc.GetDaysInMonth(year, 12), 23, 59, 59);
                }
                default:
                    return Calendar.MaxValue;
            }
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

        protected override void CalcDayCells()
        {
            DayCells.Clear();
            RowCount = GetCellRowCount();
            ColumnCount = GetCellColumnCount();

            var rightToLeftLayout = WindowsFormsSettings.GetIsRightToLeftLayout(Calendar);
            var y = DayCellsBounds.Y;

            for (var iRow = 0; iRow < RowCount; ++iRow)
            {
                var x = rightToLeftLayout ? DayCellsBounds.Right - ActualCellSize.Width : DayCellsBounds.X;
                for (var iCol = 0; iCol < ColumnCount; ++iCol)
                {
                    bool correctDate;

                    if (iRow == 0)
                    {
                        if (iCol < PenaltyIndex)
                        {
                            x += rightToLeftLayout ? -ActualCellSize.Width : ActualCellSize.Width;
                            continue;
                        }
                    }

                    DateTime date;
                    try
                    {
                        date = CalcDate(iRow, iCol, out correctDate);
                    }
                    catch
                    {
                        iRow = RowCount;
                        break;
                    }

                    if (correctDate && CanAddDate(date))
                    {
                        var dayCell = (PersianCalendarCellViewInfo)CreateDayCell(date);
                        dayCell.UpdateVisualState();
                        dayCell.Bounds = CalcCellBounds(x, y);
                        dayCell.Text = GetCellText(date, iRow, iCol);
                        dayCell.ContentBounds = CalcDayCellContentBounds(dayCell.Bounds);
                        dayCell.CalculateTextBounds();
                        dayCell.Row = iRow;
                        dayCell.Column = iCol;

                        UpdateDayCell(dayCell);
                        DayCells.Add(dayCell);
                        CalendarInfo.AddCellToNavigationGrid(dayCell, Row, Column, iRow, iCol);
                    }

                    x += rightToLeftLayout ? -ActualCellSize.Width : ActualCellSize.Width;
                }

                y += ActualCellSize.Height;
            }
        }

        private DateTime CalcDate(int row, int column, out bool correctDate)
        {
            correctDate = true;
            var pd = (PersianDate)DateTime;

            if (View == DateEditCalendarViewType.QuarterInfo)
                return new PersianDate(pd.Year, row * 6 + column * 3 + 1, 1);

            if (View == DateEditCalendarViewType.YearInfo)
                return new PersianDate(pd.Year, 1 + row * 4 + column, 1);

            if (View == DateEditCalendarViewType.YearsInfo)
            {
                int year = pd.Year / 10 * 10 - 1 + row * 4 + column;
                if (year > 0 && year < 10000)
                    return new PersianDate(year, 1, 1);
                correctDate = false;
                return PersianDate.MinValue;
            }

            if (View == DateEditCalendarViewType.YearsGroupInfo)
            {
                var year = pd.Year / 100 * 100 - 10 + (row * 4 + column) * 10;
                if (year < 0 || year >= 10000)
                {
                    correctDate = false;
                    return PersianDate.MinValue;
                }

                if (year == 0)
                    year = 1;
                return new PersianDate(year, 1, 1);
            }

            return FirstVisibleDate.AddDays(row * GetCellColumnCount() + column - PenaltyIndex);
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
    }

    public class VistaPopupPersianDateEditForm : VistaPopupDateEditForm
    {
        public VistaPopupPersianDateEditForm(DateEdit ownerEdit) : base(ownerEdit)
        {
        }

        protected override CalendarControl CreateCalendar()
        {
            return new PopupPersianCalendarControl();
        }

        protected override DateTime CalcCalendarInitialDate()
        {
            if (OwnerEdit.EditValue != null &&
                !Equals(OwnerEdit.EditValue, OwnerEdit.Properties.NullDate) &&
                !Equals(OwnerEdit.DateTime, DateTime.MinValue) &&
                !Equals(OwnerEdit.DateTime, PersianDate.MinValue))
            {
                return OwnerEdit.DateTime;
            }
            if (!Equals(OwnerEdit.Properties.NullDateCalendarValue, DateTime.MinValue) &&
                !Equals(OwnerEdit.Properties.NullDateCalendarValue, PersianDate.MinValue))
            {
                return OwnerEdit.Properties.NullDateCalendarValue;
            }

            return DateTime.Today;
        }
    }

    public class PopupPersianCalendarControl : PopupCalendarControl, IPersianCalendarControl
    {
#pragma warning disable CS0169
        private static PersianCalendar pc;
#pragma warning restore CS0169
        private static PersianCultureInfo ci;

        static PopupPersianCalendarControl()
        {
            ci = new PersianCultureInfo();
        }

        public PopupPersianCalendarControl()
        {
            FirstDayOfWeek = DayOfWeek.Saturday;
            MinValue = PersianDate.MinValue;
            MaxValue = PersianDate.MaxValue;
        }

        public new VistaPersianCalendarViewInfo CalendarViewInfo => (VistaPersianCalendarViewInfo)base.CalendarViewInfo;

        protected override BaseStyleControlViewInfo CreateViewInfo()
        {
            if (ActualCalendarView == CalendarView.Vista)
                return new VistaPersianCalendarViewInfo(this);

            return new CalendarViewInfo(this);
        }

        protected override CalendarControlHandlerBase CreateHandler()
        {
            if (ActualCalendarView == CalendarView.Vista)
                return new VistaPersianCalendarControlHandler(this);
            return new CalendarControlHandler(this);
        }

        protected override CalendarSelectionManager CreateSelectionManager()
        {
            return new PersianCalendarSelectionManager(this);
        }

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

        public DateTime GetTodayPersianDate()
        {
            if (!PersianCalendar.IsWithInSupportedRange(TodayDate) ||
                TodayDate == PersianDate.MinValue)
            {
                return PersianDate.Today;
            }

            return TodayDate;
        }
    }

    public class PersianCalendarSelectionManager : CalendarSelectionManager
    {
        public PersianCalendarSelectionManager(CalendarControlBase calendar) : base(calendar)
        {
        }

        public bool SuppressSelectionSet
        {
            get { return SuppressSetSelection; }
            set { SuppressSetSelection = value; }
        }


    }

    public class VistaPersianCalendarControlHandler : VistaCalendarControlHandler
    {
        private readonly PopupPersianCalendarControl calendar;
        private static PersianCalendar pc;

        static VistaPersianCalendarControlHandler()
        {
            pc = new Utils.PersianCalendar();
        }

        public VistaPersianCalendarControlHandler(PopupPersianCalendarControl calendar) : base(calendar)
        {
            this.calendar = calendar;
        }

        protected override DateTime ExtractDateTimeFromCellDate(DateTime date)
        {
            PersianDate pd = date;

            if (View == DateEditCalendarViewType.MonthInfo)
                return new PersianDate(pd.Year, pd.Month, CorrectDay(pd.Year, pd.Month, pd.Day), pd.Hour, pd.Minute, pd.Second, pd.Millisecond);

            if (View == DateEditCalendarViewType.YearInfo)
                return new PersianDate(pd.Year, pd.Month, CorrectDay(pd.Year, pd.Month, pd.Day), SelectedDate.Hour, SelectedDate.Minute, SelectedDate.Second, SelectedDate.Millisecond);

            if (View == DateEditCalendarViewType.YearsInfo)
                return new PersianDate(pd.Year, pd.Month, CorrectDay(pd.Year, pd.Month, pd.Day), SelectedDate.Hour, SelectedDate.Minute, SelectedDate.Second, SelectedDate.Millisecond);

            if (View != DateEditCalendarViewType.YearsGroupInfo)
                return pd;

            var yearDiff = pd.Year == 1 ? 0 : pd.Year;
            return new PersianDate(yearDiff + pd.Year % 10, pd.Month, CorrectDay(yearDiff + pd.Year % 10, pd.Month, pd.Day), SelectedDate.Hour, SelectedDate.Minute, SelectedDate.Second, SelectedDate.Millisecond);
        }

        protected override int CorrectDay(int year, int month, int day)
        {
            if (day <= 0) return 1;
            if (day > pc.GetDaysInMonth(year, month)) return pc.GetDaysInMonth(year, month);

            return day;
        }

        protected override void ProcessCellClick(CalendarCellViewInfo cell)
        {
            base.ProcessCellClick(cell);
            calendar.CalendarViewInfo.UpdateCellsState();
        }
    }
}