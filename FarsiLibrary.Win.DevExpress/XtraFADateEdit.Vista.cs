using System;
using System.Drawing;
using System.Globalization;
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
    public class FAVistaDateEditInfoArgs : VistaDateEditInfoArgs
    {
        private readonly FAVistaDateEditCalendar calendar;

        public FAVistaDateEditInfoArgs(FAVistaDateEditCalendar calendar) : base(calendar)
        {
            this.calendar = calendar;
        }

        public override string CaptionString => GetFarsiCaption();
        public override string ClearString => GetFarsiClearCaption();
        public override string OkString => GetFarsiOkString();
        public override string CancelString => GetCancelString();
        public override string CurrentDateString => GetFarsiCurrentDate();

        protected override string GetAbbreviatedDayName(string abbrDayName, string insufficientPrefix)
        {
            if (!string.IsNullOrWhiteSpace(abbrDayName) && abbrDayName.Length > 1)
                return abbrDayName.Substring(0, 1);

            return base.GetAbbreviatedDayName(abbrDayName, insufficientPrefix);
        }

        private string GetFarsiCurrentDate()
        {
            PersianDate pd = DateTime.Today;
            return toFarsi.Convert(pd.ToWritten());
        }

        private string GetFarsiOkString()
        {
            return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.MessageBox_Ok);
        }

        private string GetCancelString()
        {
            return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_Cancel);
        }

        private string GetFarsiClearCaption()
        {
            return FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.FAMonthView_None);
        }

        private string GetFarsiCaption()
        {
            var date = (PersianDate)DateTime;
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

        protected override void CalcDayNumberCells()
        {
            DayCells.Clear();
            WeekCells.Clear();
            int x = MonthNumbersRect.Left, y = MonthNumbersRect.Top;
            DateTime cur = MinValue;
            for (int j = 0; j < 6; j++, y += TextHeight + TextHeightIndent)
            {
                int penalty = (j == 0 ? PenaltyIndex : 0);
                for (int i = 0; i < 7; i++, x += DayCellWidth + TextWidthIndent)
                {
                    if (j == 0 && i < PenaltyIndex) continue;
                    try { cur = FirstVisibleDate.AddDays(j * 7 + i - PenaltyIndex); }
                    catch
                    {
                        j = 6;
                        break;
                    }
                    if (CanAddDate(cur))
                    {
                        DayNumberCellInfo cell = CreateDayCell(cur);
                        cell.Bounds = new Rectangle(x, y, DayCellWidth + TextWidthIndent, TextHeight + TextHeightIndent);
                        cell.Text = GetDayText(cur);
                        cell.TextBounds = CalcDateNumberBounds(cell.Bounds);
                        DayCells.Add(cell);
                    }
                }
                x = MonthNumbersRect.Left;
                if (ShowWeekNumbers && cur != MinValue)
                {
                    DayNumberCellInfo weekCell = CreateDayCell(cur);
                    int? weekNumber = GetWeekNumber(cur);
                    weekCell.Text = weekNumber.HasValue ? weekNumber.ToString() : "";
                    weekCell.Bounds = CalcWeekCellBounds(weekCell.Text, x, y);
                    weekCell.SetAppearance(Appearance);
                    weekCell.SetAppearance(weekCell.Appearance.Clone() as AppearanceObject);
                    weekCell.Appearance.ForeColor = Color.Red;
                    WeekCells.Add(weekCell);
                }
            }
            UpdateExistingCellsState();
        }

        protected override DayNumberCellInfo CreateMonthCellInfo(int row, int col)
        {
            DayNumberCellInfo currInfo;
            var currentDate = (PersianDate)DateTime;
            PersianDate pd = new PersianDate(currentDate.Year, 1 + row * 4 + col, 1);
            DateTime dt = pd;
            if (dt > calendar.MaxDateValue) return null;
            if (dt < calendar.MinDateValue && dt.Month < calendar.MinDateValue.Month) return null;
            currInfo = new DayNumberCellInfo(dt);
            currInfo.Text = pd.LocalizedMonthName;
            return currInfo;
        }

        protected override DayNumberCellInfo CreateQuarterCellInfo(int row, int col)
        {
            return base.CreateQuarterCellInfo(row, col);
        }

        protected override DayNumberCellInfo CreateYearCellInfo(int row, int col)
        {
            DayNumberCellInfo currInfo;
            var currentYear = ((PersianDate)DateTime).Year;
            int beginYear = (currentYear / 10) * 10 - 1;
            int currYear = beginYear + row * 4 + col;
            if (currYear <= 0 || currYear >= 10000) return null;
            PersianDate pd = new PersianDate(currYear, 1, 1);
            DateTime dt = pd;

            if (dt > calendar.MaxDateValue) return null;
            if (dt < calendar.MinDateValue && dt.Year < calendar.MinDateValue.Year) return null;

            currInfo = new DayNumberCellInfo(dt);
            currInfo.Text = toFarsi.Convert(pd.Year.ToString());
            if (currYear < (pd.Year / 10) * 10 || currYear > (pd.Year / 10) * 10 + 1) currInfo.State = ObjectState.Disabled;
            return currInfo;
        }

        protected override DayNumberCellInfo CreateYearsGroupCellInfo(int row, int col)
        {
            DayNumberCellInfo currInfo;
            var currentDate = (PersianDate)DateTime;
            var currentYear = currentDate.Year;
            int beginYearGroup = (currentYear / 100) * 100 - 10;
            int currYearGroup = beginYearGroup + (row * 4 + col) * 10;
            if (currYearGroup < 0 || currYearGroup >= 10000) return null;
            int endYearGroup = currYearGroup + 9;
            if (currYearGroup == 0) currYearGroup = 1;
            PersianDate dateStart = new PersianDate(currYearGroup, 1, 1);
            DateTime dt1 = dateStart;
            if (dt1 > calendar.MaxDateValue) return null;
            if (dt1 < calendar.MinDateValue && endYearGroup < calendar.MinDateValue.Year) return null;
            currInfo = new DayNumberCellInfo(dt1);

            var dateEnd = new PersianDate(endYearGroup, 1, 1);
            currInfo.Text = toFarsi.Convert(dateStart.Year.ToString(CultureInfo.CurrentUICulture) + "-\n" +
                                            dateEnd.Year.ToString(CultureInfo.CurrentUICulture));
            return currInfo;
        }

        protected override bool IsToday(DayNumberCellInfo cell)
        {
            return AreDatesEqual(cell.Date, PersianDate.Today);
        }

        protected override bool IsDateSelected(DayNumberCellInfo cell)
        {
            var currentDate = (PersianDate)DateTime;
            var cellDate = (PersianDate)cell.Date;

            if (calendar.View == DateEditCalendarViewType.YearInfo)
            {
                return cellDate.Month == currentDate.Month;
            }

            if (calendar.View == DateEditCalendarViewType.YearsInfo)
            {
                return cellDate.Year == currentDate.Year;
            }

            if (calendar.View == DateEditCalendarViewType.YearsGroupInfo)
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

        protected override bool IsDateActive(DayNumberCellInfo cell)
        {
            var current = (PersianDate)CurrentMonth;
            var cellDate = (PersianDate)cell.Date;

            if (calendar.View != DateEditCalendarViewType.MonthInfo)
                return true;

            return current.Month == cellDate.Month;
        }

        protected override bool IsHolidayDate(DayNumberCellInfo cell)
        {
            return cell.Date.DayOfWeek == DayOfWeek.Friday;
        }

        static internal DateTime GetFirstMonthDate(DateTime date)
        {
            //if (CultureHelper.IsFarsiCulture)
                return GetFarsiFirstMonthDate(date);

            //return new DateTime(date.Year, date.Month, 1, date.Hour, date.Minute, date.Second, date.Millisecond, date.Kind);
        }

        private static DateTime GetFarsiFirstMonthDate(DateTime date)
        {
            var pd = (PersianDate)date;
            return pd.StartOfMonth();
        }

        protected override void CalcFirstVisibleDate()
        {
            SetFirstVisibleDate(GetFirstVisibleDate(CurrentMonth));
        }

        public override DateTime GetFirstVisibleDate(DateTime editDate)
        {
            try
            {
                DateTime firstMonthDate = GetFirstMonthDate(editDate);
                //TimeSpan delta = TimeSpan.FromDays(-GetFirstDayOffset(firstMonthDate));
                //if (firstMonthDate.Ticks + delta.Ticks < 0)
                //    return PersianDate.MinValue;

                //return firstMonthDate + delta;
                return firstMonthDate;
            }
            catch
            {
                return MinValue;
            }
        }

        private string GetDayText(DateTime cur)
        {
            var pd = (PersianDate)cur;
            return toFarsi.Convert(pd.Day.ToString(CultureInfo.CurrentUICulture));
        }
    }

    public class FAVistaPopupDateEditForm : VistaPopupDateEditForm
    {
        public FAVistaPopupDateEditForm(DateEdit ownerEdit) : base(ownerEdit)
        {
        }

        protected override DateEditCalendar CreateCalendar()
        {
            var calendar = new FAVistaDateEditCalendar(OwnerEdit.Properties, OwnerEdit.EditValue);
            calendar.OkClick += OnOkClick;
            return calendar;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Calendar != null)
            {
                ((FAVistaDateEditCalendar)Calendar).OkClick -= OnOkClick;
            }
            base.Dispose(disposing);
        }
    }

    public class FAVistaDateEditCalendar : VistaDateEditCalendar
    {
        private static Utils.PersianCalendar pc;
        private static PersianCultureInfo ci;
        private bool shouldUpdateOnDateChange;
        private bool useDateChangeOverride;

        static FAVistaDateEditCalendar()
        {
            ci = new PersianCultureInfo();
            pc = new Utils.PersianCalendar();
        }

        public FAVistaDateEditCalendar(RepositoryItemDateEdit item, object editDate) : base(item, editDate)
        {
        }

        protected override DateEditPainter CreatePainter()
        {
            if (Properties.UseVistaPainter()) return new FAVistaDateEditPainter(this);
            return new FADateEditPainter(this);
        }

        protected override DateEditInfoArgs CreateInfoArgs()
        {
            return new FAVistaDateEditInfoArgs(this);
        }

        protected override DateTime CalculateFirstMonthDate(DateTime dateTime)
        {
            return FAVistaDateEditInfoArgs.GetFirstMonthDate(dateTime);
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

        protected override bool ShouldUpdateOnDateChange => useDateChangeOverride ? shouldUpdateOnDateChange : base.ShouldUpdateOnDateChange;

        protected override void OnItemClick(CalendarHitInfo hitInfo)
        {
            useDateChangeOverride = true;
            DayNumberCellInfo cell = hitInfo.HitObject as DayNumberCellInfo;
            DateEditCalendarViewType prevView = View;
            DateEditCalendarViewType nextView = DecView();

            if (cell != null)
            {
                PersianDate cellDate = cell.Date;
                PersianDate currentDate = DateTime;

                shouldUpdateOnDateChange = false;
                if (View == DateEditCalendarViewType.MonthInfo)
                    DateTime = CorrectPersianDateTime(new PersianDate(cellDate.Year, cellDate.Month, CorrectPersianDay(currentDate.Year, cellDate.Month, cellDate.Day), currentDate.Hour, currentDate.Minute, currentDate.Second, currentDate.Millisecond));
                else if (View == DateEditCalendarViewType.YearInfo)
                    DateTime = CorrectPersianDateTime(new PersianDate(currentDate.Year, cellDate.Month, CorrectPersianDay(currentDate.Year, cellDate.Month, currentDate.Day), currentDate.Hour, currentDate.Minute, currentDate.Second, currentDate.Millisecond));
                else if (View == DateEditCalendarViewType.YearsInfo)
                    DateTime = CorrectPersianDateTime(new PersianDate(cellDate.Year, currentDate.Month, CorrectPersianDay(cellDate.Year, currentDate.Month, currentDate.Day), currentDate.Hour, currentDate.Minute, currentDate.Second, currentDate.Millisecond));
                else if (View == DateEditCalendarViewType.YearsGroupInfo)
                {
                    int year = cellDate.Year == 1 ? 0 : cellDate.Year;
                    DateTime = CorrectPersianDateTime(new PersianDate(year + currentDate.Year % 10, currentDate.Month, CorrectPersianDay(year + currentDate.Year % 10, currentDate.Month, currentDate.Day), currentDate.Hour, currentDate.Minute, currentDate.Second, currentDate.Millisecond));
                }
                shouldUpdateOnDateChange = true;
                ClearCellsSelection();
                cell.Selected = true;
                if (View != nextView)
                    OnViewChanging(View, nextView, false);
            }
            if (View != nextView)
            {
                ClearSelection();
                SetViewCore(nextView);
                OnViewChanged(prevView, nextView);
            }
            else if (Multiselect)
            {
                SetSelectionRange(DateTime);
                Calendars[0].UpdateExistingCellsState();
                Invalidate();
            }
            if (Multiselect)
            {
                RaiseSelectionChanged();
            }

            useDateChangeOverride = false;
        }

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

        internal int AnimationLock => LockAnimation;
        internal Rectangle RectCaption => CaptionRect;
        internal Rectangle RectCaptionButton => CaptionButtonRect;
        internal bool RightToLeftSet => IsRightToLeft;
        internal DateTime MaxDateValue => MaxValue;
        internal DateTime MinDateValue => MinValue;
    }

    public class FAVistaDateEditPainter : VistaDateEditPainter
    {
        internal new FAVistaDateEditCalendar Calendar;

        public FAVistaDateEditPainter(FAVistaDateEditCalendar calendar) : base(calendar)
        {
            Calendar = calendar;
        }

        protected override void DrawDayCell(CalendarObjectInfoArgs info, DayNumberCellInfo cell)
        {
            FAVistaDateEditInfoArgs vdi = info as FAVistaDateEditInfoArgs;
            new FAVistaDateEditCellPainter(this, vdi).DrawObject(cell);
        }
    }

    public class FAVistaDateEditCellPainter : VistaDateEditCellPainter
    {
        private readonly FAVistaDateEditPainter ownerPainter;

        public FAVistaDateEditCellPainter(FAVistaDateEditPainter ownerPainter, FAVistaDateEditInfoArgs ownerInfo) : base(ownerPainter, ownerInfo)
        {
            this.ownerPainter = ownerPainter;
        }

        public override void DrawDayCellText(VistaDateEditInfoArgs vdi, DayNumberCellInfo cell, Brush brush)
        {
            HorzAlignment prev = cell.Appearance.TextOptions.HAlignment;
            if (Calendar.View == DateEditCalendarViewType.MonthInfo)
            {
                if (ownerPainter.Calendar.RightToLeftSet) cell.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                OwnerPainter.DrawDayCellText(cell, brush);
                cell.Appearance.TextOptions.HAlignment = prev;
                return;
            }
            if (Calendar.View == DateEditCalendarViewType.YearsGroupInfo) cell.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            else cell.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            cell.Appearance.DrawString(cell.Cache, DrawCellText(cell), cell.TextBounds);
            cell.Appearance.TextOptions.HAlignment = prev;
        }

        private string DrawCellText(DayNumberCellInfo cell)
        {
            return toFarsi.Convert(cell.Text);
        }
    }

}