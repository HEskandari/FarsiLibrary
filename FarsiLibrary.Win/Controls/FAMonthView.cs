using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.Win.BaseClasses;
using FarsiLibrary.Win.Design;
using FarsiLibrary.Win.Enums;
using FarsiLibrary.Win.Events;
using System.Drawing.Design;
using System.Linq;
using FarsiLibrary.Localization;

namespace FarsiLibrary.Win.Controls
{
    /// <summary>
    /// MonthView control is a calendar control that displays days of a month in a view, and user can select dates in various formats.
    /// This control currently supports three cultures and calendars, both RTL and LTR rendering and displaying numeric values in correct localized format.
    /// 
    /// If you want to use the control in FA-IR culture, main entry of your application should look like this :
    /// <code>
    ///     public static void Main()
    ///     {
    ///                     Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-IR");
    ///                     Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
    ///          
    ///                     Application.EnableVisualStyles(); // To Support XP and Office2003 themes.
    ///                     Application.Run(new MainForm());
    ///     }
    /// </code>
    /// 
    /// If the <see cref="System.Globalization.CultureInfo"/> has Right-To-Left reading format, control renders months and days in RTL, otherwise in LTR.
    /// </summary>
    [ToolboxItem(true)]
    [Designer(typeof(FAMonthViewDesigner))]
    [DefaultEvent("SelectedDateTimeChanged")]
    [DefaultProperty("SelectedDateTime")]
    [ToolboxBitmap(typeof(FAMonthView))]
    public class FAMonthView : BaseDateControl
    {
        #region Constants

        private const int DEF_HEADER_SIZE = 21;
        private const int DEF_ARROW_SIZE = 3;
        private const int DEF_FOOTER_SIZE = 27;
        private const int DEF_BUTTON_WIDTH = 60;
        private const int DEF_BUTTON_HEIGHT = 23;
        private const int DEF_WEEK_DAY_HEIGHT = 20;
        private const int DEF_ROWS_MARGIN = 3;
        private const int DEF_COLUMNS_COUNT = 7;
        private const int DEF_ROWS_COUNT = 7;
        private const int DEF_TODAY_TAB_INDEX = 100;
        private const int DEF_NONE_TAB_INDEX = 101;

        #endregion

        #region ActRect

        internal class ActRect
        {
            #region Class Properties

            public Rectangle Rect
            {
                get;
                set;
            }

            public TRectangleStatus State
            {
                get;
                set;
            }

            public bool InvalidateOnChange
            {
                get;
                set;
            }

            public FocusedPart Action
            {
                get;
                set;
            }

            public object Tag
            {
                get;
                set;
            }

            public bool IsFocused
            {
                get { return (State & TRectangleStatus.Focused) == TRectangleStatus.Focused; }
            }

            public bool IsSelected
            {
                get { return (State & TRectangleStatus.Selected) == TRectangleStatus.Selected; }
            }

            public bool IsActive
            {
                get { return (State & TRectangleStatus.Active) == TRectangleStatus.Active; }
            }

            #endregion

            #region Ctor

            public ActRect(Rectangle rc, TRectangleStatus state, FocusedPart act, bool invalidate)
            {
                Rect = rc;
                State = state;
                InvalidateOnChange = invalidate;
                Action = act;
            }

            public ActRect(Rectangle rc, TRectangleStatus state, FocusedPart act) : this(rc, state, act, true)
            {
            }

            public ActRect(Rectangle rc, FocusedPart act, object tag)
            {
                Rect = rc;
                State = TRectangleStatus.Normal;
                Action = act;
                Tag = tag;
            }

            public ActRect(Rectangle rc, FocusedPart act) : this(rc, TRectangleStatus.Normal, act, true)
            {
            }

            public ActRect(Rectangle rc, TRectangleStatus state) : this(rc, state, FocusedPart.None, true)
            {
            }

            public ActRect(Rectangle rc) : this(rc, TRectangleStatus.Normal, FocusedPart.None, true)
            {
            }

            public ActRect() : this(Rectangle.Empty, TRectangleStatus.Normal, FocusedPart.None, true)
            {
            }

            #endregion
        }

        #endregion

        #region Fields

        private Rectangle rcHeader;
        private Rectangle rcFooter;
        private Rectangle rcBody;

        private readonly List<ActRect> rects = new List<ActRect>(100);
        private readonly ArrayList selectedRects = new ArrayList();
        private readonly DateTimeCollection selectedDateRange;

        private int iLastFocused = 1;
        private bool rectsCreated;
        private bool btnTodayActive;
        private bool btnNoneActive;
        private bool isMultiSelect;
        private bool showFocusRect;
        private bool showEmptyButton;
        private bool showTodayButton;
        private bool showBorder = true;
        private ScrollOptionTypes scrollOption;

        #endregion

        #region Events

        /// <summary>
        /// Fires when a date is added/removes to SelectedDateRange collection, if the control is in <see cref="IsMultiSelect"/> mode.
        /// </summary>
        public event EventHandler<SelectedDateRangeChangedEventArgs> SelectedDateRangeChanged;

        /// <summary>
        /// Fires when current day is being printed.
        /// </summary>
        public event CustomDrawDayEventHandler DrawCurrentDay;

        /// <summary>
        /// Fires when user clicks on a day, None button or Today button.
        /// </summary>
        public event CalendarButtonClickedEventHandler ButtonClicked;

        #endregion

        #region Props

        /// <summary>
        /// Determines if Empty button should be shown
        /// </summary>
        [DefaultValue(true)]
        [Description("Determines if Empty button should be shown")]
        [RefreshProperties(RefreshProperties.All)]
        public bool ShowEmptyButton
        {
            get { return showEmptyButton; }
            set
            {
                showEmptyButton = value;
                rectsCreated = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Determines if Today button should be shown
        /// </summary>
        [DefaultValue(true)]
        [Description("Determines if Today button should be shown")]
        [RefreshProperties(RefreshProperties.All)]
        public bool ShowTodayButton
        {
            get { return showTodayButton; }
            set
            {
                showTodayButton = value;
                rectsCreated = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Determinces scrolling option of the FAMonthView control.
        /// </summary>
        [DefaultValue(typeof(ScrollOptionTypes), "Month")]
        [Description("Determinces scrolling option of the FAMonthView control.")]
        public ScrollOptionTypes ScrollOption
        {
            get { return scrollOption; }
            set
            {
                if (scrollOption == value)
                    return;

                scrollOption = value;
            }
        }

        /// <summary>
        /// Determines if the control has not made any selection yet.
        /// </summary>
        [DefaultValue(true)]
        [RefreshProperties(RefreshProperties.All)]
        public bool IsNull
        {
            get { return !SelectedDateTime.HasValue; }
        }

        /// <summary>
        /// Gets or Sets to show a border around the control.
        /// </summary>
        [DefaultValue(true)]
        [Description("Gets or Sets to show a border around the control.")]
        public bool ShowBorder
        {
            get { return showBorder; }
            set
            {
                if (showBorder == value)
                    return;

                showBorder = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or Sets to show the focus rectangle around the selected day.
        /// </summary>
        [DefaultValue(false)]
        [Description("Gets or Sets to show the focus rectangle around the selected day.")]
        public bool ShowFocusRect
        {
            get { return showFocusRect; }
            set
            {
                if (showFocusRect == value)
                    return;

                showFocusRect = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Selected values collection, if the control is in MultiSelect mode.
        /// </summary>
        [Editor(typeof(DateTimeCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(DateTimeConverter))]
        [Description("Selected values collection, if the control is in MultiSelect mode.")]
        public DateTimeCollection SelectedDateRange
        {
            get
            {
                if (IsMultiSelect)
                {
                    return selectedDateRange;
                }

                var singleSelection = new DateTimeCollection();
                if (!IsNull)
                    singleSelection.Add(SelectedDateTime.Value);

                return singleSelection;
            }
        }

        /// <summary>
        /// Gets or Sets the control in MultiSelect mode.
        /// </summary>
		[DefaultValue(false)]
        [Description("Gets or Sets the control in MultiSelect mode.")]
        public bool IsMultiSelect
        {
            get
            {
                return isMultiSelect;
            }
            set
            {
                isMultiSelect = value;
                if (isMultiSelect)
                {
                    SelectedDateTime = null;
                }
                Repaint();
            }
        }

        /// <summary>
        /// Size of the control that can not be changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size Size
        {
            get { return base.Size; }
            set
            {
                if (value.Width != ControlWidth && value.Height != ControlHeight)
                    value = new Size(ControlWidth, ControlHeight);

                base.Size = value;
            }
        }

        /// <summary>
        /// Is control in popup mode?
        /// </summary>
        [Browsable(false)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsPopupMode
        {
            get;
            set;
        }

        #endregion

        #region Hidden Props

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DefaultValue(typeof(DockStyle), "None")]
        public override DockStyle Dock
        {
            get
            {
                return DockStyle.None;
            }
            set
            {
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DefaultValue(typeof(AnchorStyles), "None")]
        public override AnchorStyles Anchor
        {
            get
            {
                return AnchorStyles.None;
            }
            set
            {
            }
        }

        #endregion

        #region Ctor & Dispose

        /// <summary>
        /// Creates a new instance of FAMonthView class. Initiated control could be uses in PopupMode or Normal mode, depending on the use.
        /// </summary>
        /// <param name="popupMode"></param>
        public FAMonthView(bool popupMode)
        {
            IsPopupMode = popupMode;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            selectedDateRange = new DateTimeCollection();
            selectedDateRange.CollectionChanged += OnSelectionCollectionChanged;

            base.Size = new Size(166, 166);
            base.Font = new Font("Tahoma", 8.25F);
            base.Enabled = true;

            scrollOption = ScrollOptionTypes.Month;
            showEmptyButton = true;
            showTodayButton = true;

            FALocalizeManager.Instance.LocalizerChanged += OnLocalizerChanged;
        }

        /// <summary>
        /// Creates a new instance of FAMonthView for normal mode usage.
        /// </summary>
        public FAMonthView() : this(false)
        {
        }

        #endregion

        #region Paint Methods

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (!CanUpdate)
                return;

            try
            {
                BeginUpdate();

                var rc = new Rectangle(0, 0, Width, Height);

                //Active rectangles must be rebuild
                if (rectsCreated == false)
                    rects.Clear();

                OnDrawHeader(new PaintEventArgs(pe.Graphics, rcHeader));
                OnDrawFooter(new PaintEventArgs(pe.Graphics, rcFooter));

                if (rectsCreated == false)
                {
                    rcBody = new Rectangle(rc.X, rcHeader.Bottom, rc.Width, rcFooter.Top - rcHeader.Bottom);
                    rcBody = Rectangle.Inflate(rcBody, -4, -1);
                }

                OnDrawBody(new PaintEventArgs(pe.Graphics, rcBody));
                OnDrawBorder(new PaintEventArgs(pe.Graphics, rc));

                rectsCreated = true;
            }
            finally
            {
                EndUpdate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (!CanUpdate)
                return;

            try
            {
                BeginUpdate();

                var g = pevent.Graphics;
                var rc = new Rectangle(0, 0, Width, Height);

                Painter.DrawFilledBackground(g, rc, false, 90f);

                // Draw header background
                rcHeader = new Rectangle(rc.X + 4, rc.Y + 4, rc.Width - 8, DEF_HEADER_SIZE);
                Painter.DrawButton(g, rcHeader, string.Empty, Font, null, ItemState.Normal, false, true);

                // Construct footer rect
                var yBott = rc.Bottom - DEF_FOOTER_SIZE - 1;
                rcFooter = new Rectangle(rc.X + 6, yBott, rc.Width - 12, DEF_FOOTER_SIZE);
            }
            finally
            {
                EndUpdate();
            }
        }

        private void OnDrawBorder(PaintEventArgs e)
        {
            if (!ShowBorder)
                return;

            var border = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            Painter.DrawBorder(e.Graphics, border, Enabled);
        }

        private void OnDrawHeader(PaintEventArgs pevent)
        {
            var rc = pevent.ClipRectangle;
            var rcOut = Rectangle.Inflate(rc, -6, -1);

            var ev = new PaintEventArgs(pevent.Graphics, rcOut);

            OnDrawMonthHeader(ev);
            OnDrawYearHeader(ev);
        }

        private void OnDrawMonthHeader(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            var rc = pevent.ClipRectangle;

            var strMonth = GetMonthName(ViewMonth);
            var strLongestMonth = PersianDateTimeFormatInfo.MonthGenitiveNames[1]; //Ordibehesht

            // Draw left arrow and add it as Active Rectangle
            var rect = Painter.DrawArrow(g, rc, true, !Enabled, DEF_ARROW_SIZE);
            AddActiveRect(rect, FocusedPart.MonthPrev);

            var sz = g.MeasureString(strLongestMonth, Font);
            var rcText = new Rectangle(rect.Right + 4, rc.Y, (int)sz.Width + 20, rc.Height);
            g.DrawString(strMonth, Font, SystemBrushes.WindowText, rcText, OneLineNoTrimming);

            // draw  right arrow and add it like Active Rectangle
            rect = Painter.DrawArrow(g, new Rectangle(rcText.Right, rc.Y, 100, rc.Height), false, !Enabled, DEF_ARROW_SIZE);
            AddActiveRect(rect, FocusedPart.MonthNext);
        }

        private void OnDrawYearHeader(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            var rc = pevent.ClipRectangle;

            var strYear = toFarsi.Convert(ViewYear.ToString(), DefaultCulture);

            var rect = Painter.DrawArrow(g, new Rectangle(rc.Right - 4 - DEF_ARROW_SIZE - 2, rc.Y, DEF_ARROW_SIZE * 2, rc.Height), false, !Enabled, DEF_ARROW_SIZE);
            AddActiveRect(rect, FocusedPart.YearNext);

            var sz = g.MeasureString(strYear, Font);
            var rcText = new Rectangle(rect.Left - 4 - (int)sz.Width - 8, rc.Y, (int)sz.Width + 8, rc.Height);
            g.DrawString(strYear, Font, SystemBrushes.WindowText, rcText, OneLineNoTrimming);

            rect = Painter.DrawArrow(g, new Rectangle(rcText.Left - 4 - DEF_ARROW_SIZE - 2, rc.Y, DEF_ARROW_SIZE * 2, rc.Height), true, !Enabled, DEF_ARROW_SIZE);
            AddActiveRect(rect, FocusedPart.YearPrev);
        }

        private void OnDrawFooter(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            OnDrawFooterButtons(new PaintEventArgs(g, rcFooter));
        }

        private void OnDrawFooterButtons(PaintEventArgs pevent)
        {
            var visibleButtonCount = GetVisibleButtonCount();
            var g = pevent.Graphics;
            var rc = pevent.ClipRectangle;

            if (visibleButtonCount == 0)
                return;

            var buttonSpace = 10;
            var margin = ((rc.Width - (DEF_BUTTON_WIDTH * visibleButtonCount) - buttonSpace) / 2);

            var fmt = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.None
            };

            fmt.FormatFlags |= StringFormatFlags.DirectionRightToLeft | StringFormatFlags.NoWrap;

            var rcToday = Rectangle.Empty;
            var rcNone = Rectangle.Empty;

            if (visibleButtonCount == 2)
            {
                rcToday = new Rectangle(rc.X + margin, rc.Y + rc.Height / 2 - DEF_BUTTON_HEIGHT / 2, DEF_BUTTON_WIDTH, DEF_BUTTON_HEIGHT);
                rcNone = new Rectangle(rcToday.Right + buttonSpace, rcToday.Y, rcToday.Width, rcToday.Height);
            }
            else
            {
                if (ShowTodayButton)
                {
                    rcToday = new Rectangle(margin + buttonSpace, rc.Y + rc.Height / 2 - DEF_BUTTON_HEIGHT / 2, DEF_BUTTON_WIDTH, DEF_BUTTON_HEIGHT);
                }
                else if (ShowEmptyButton)
                {
                    rcNone = new Rectangle(margin + buttonSpace, rc.Y + rc.Height / 2 - DEF_BUTTON_HEIGHT / 2, DEF_BUTTON_WIDTH, DEF_BUTTON_HEIGHT);
                }
            }

            if (ShowTodayButton)
            {
                AddActiveRect(rcToday, FocusedPart.TodayButton, DEF_TODAY_TAB_INDEX);
                var todayState = ItemState.Normal;

                if (btnTodayActive)
                    todayState = ItemState.HotTrack;

                Painter.DrawButton(g, rcToday, FALocalizeManager.Instance.GetLocalizerByCulture(DefaultCulture).GetLocalizedString(StringID.FAMonthView_Today), Font, fmt, todayState, true, true);
            }
            else
            {
                AddActiveRect(rcToday, FocusedPart.Hidden, DEF_TODAY_TAB_INDEX);
            }

            if (ShowEmptyButton)
            {
                AddActiveRect(rcNone, FocusedPart.NoneButton, DEF_NONE_TAB_INDEX);

                var noneState = ItemState.Normal;

                if (btnNoneActive)
                    noneState = ItemState.HotTrack;

                Painter.DrawButton(g, rcNone, FALocalizeManager.Instance.GetLocalizerByCulture(DefaultCulture).GetLocalizedString(StringID.FAMonthView_None), Font, fmt, noneState, true, true);
            }
            else
            {
                AddActiveRect(rcNone, FocusedPart.Hidden, DEF_NONE_TAB_INDEX);
            }

            fmt.Dispose();
        }

        private int GetVisibleButtonCount()
        {
            if (ShowTodayButton && ShowEmptyButton)
                return 2;

            if (ShowTodayButton || ShowEmptyButton)
                return 1;

            return 0;
        }

        private void OnDrawBody(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            var rc = pevent.ClipRectangle;

            var iColWidth = rc.Width / DEF_COLUMNS_COUNT;
            var iRowHeight = (rc.Height - DEF_WEEK_DAY_HEIGHT) / DEF_ROWS_COUNT;

            #region Top Separator

            Painter.DrawSeparator(g, new Point(rc.X + 2, rc.Y + DEF_WEEK_DAY_HEIGHT - 3),
                                  new Point(rc.Right - 2, rc.Y + DEF_WEEK_DAY_HEIGHT - 3));

            #endregion

            #region Weekday Name

            var rcHead = new Rectangle(rc.X, rc.Y, iColWidth, DEF_WEEK_DAY_HEIGHT - 3);

            if (IsRightToLeftCulture)
            {
                for (var i = DEF_COLUMNS_COUNT; i > 0; i--)
                {
                    rcHead.X = rc.Width - (i * iColWidth);
                    DrawDayValue(g, i - 1, rcHead);
                }
            }
            else
            {
                for (var i = 0; i < DEF_COLUMNS_COUNT; i++)
                {
                    rcHead.X = rc.X + (i * iColWidth);
                    DrawDayValue(g, i, rcHead);
                }
            }

            #endregion

            #region Calculate Month Values

            //How many days are in DrawTab month and first day of month
            var numDays = DefaultCalendar.GetDaysInMonth(ViewYear, ViewMonth);
            var dtStartOfMonth = new DateTime(ViewYear, ViewMonth, 1, 0, 0, 0, DefaultCalendar);
            var dtStartOfNextMonth = dtStartOfMonth.AddDays(numDays);
            var firstDay = GetFirstDayOfWeek(dtStartOfMonth);
            var rowNo = 1;
            var iLastMonth = ViewMonth;
            var iLastYear = ViewYear;

            if (ViewMonth - 1 < 1 && iLastYear > 1)
            {
                iLastMonth = 12;
                iLastYear--;
            }
            else if (iLastMonth - 1 > 0)
            {
                iLastMonth--;
            }

            var prevMonthDays = DefaultCalendar.GetDaysInMonth(iLastYear, iLastMonth);
            var lastingDays = prevMonthDays - firstDay;
            var brush = Brushes.Gray;
            Rectangle rcDay;

            if (IsRightToLeftCulture)
            {
                rcDay = new Rectangle(rcHead.X, rc.Y + DEF_WEEK_DAY_HEIGHT, rcHead.Width - 2, iRowHeight + 1);
            }
            else
            {
                rcDay = new Rectangle(rc.X, rc.Y + DEF_WEEK_DAY_HEIGHT, rcHead.Width - 2, iRowHeight + 1);
            }

            #endregion

            #region Pre-Day Padding

            for (var y = lastingDays; y < prevMonthDays; y++)
            {
                //Raise the custom-darw event
                var lastMonthDay = new DateTime(iLastYear, iLastMonth, y + 1, DefaultCalendar);
                var args = new CustomDrawDayEventArgs(rcDay, g, iLastYear, iLastMonth, y + 1, false, lastMonthDay);
                OnDrawCurrentDay(args);

                if (!args.Handled)
                {
                    //Disabled Days
                    var disabledDay = toFarsi.Convert((y + 1).ToString(), DefaultCulture);
                    g.DrawString(disabledDay, Font, brush, rcDay, OneLineNoTrimming);
                }

                if (IsRightToLeftCulture)
                {
                    rcDay.X = rcDay.X - iColWidth;
                }
                else
                {
                    rcDay.X = rcDay.X + iColWidth;
                }
            }

            #endregion

            #region Current Day

            for (var x = 1; x <= numDays; x++)
            {
                var dtInPaint = new DateTime(ViewYear, ViewMonth, x, DefaultCalendar);
                brush = SystemBrushes.WindowText;

                //draw weekday header names
                var dayNo = toFarsi.Convert(x.ToString(), DefaultCulture);
                var index = x;

                //Selected Days
                if (IsMultiSelect && (selectedRects.Contains(index) || SelectedDateRange.Contains(dtInPaint)))
                {
                    if (!ShowFocusRect)
                    {
                        Painter.DrawSelectedPanel(g, rcDay);
                    }
                    else
                    {
                        Painter.DrawFocusRect(g, rcDay);
                    }

                    g.DrawString(dayNo, Font, SystemBrushes.ControlText, rcDay, OneLineNoTrimming);
                    AddActiveRect(rcDay, FocusedPart.MonthDay, index);
                }
                else if (SelectedDay == x) //Current Day
                {
                    AddActiveRect(rcDay, FocusedPart.MonthDay, index);
                    var args = new CustomDrawDayEventArgs(rcDay, g, ViewYear, ViewMonth, x, true, dtInPaint);
                    OnDrawCurrentDay(args);

                    if (!args.Handled)
                    {
                        if (!IsNull && !ShowFocusRect)
                        {
                            Painter.DrawSelectedPanel(g, rcDay);
                        }
                        else if (!IsNull && ShowFocusRect)
                        {
                            Painter.DrawFocusRect(g, rcDay);
                        }
                        else if (IsNull)
                        {
                            Painter.DrawSelectionBorder(g, rcDay);
                        }

                        g.DrawString(dayNo, Font, SystemBrushes.ControlText, rcDay, OneLineNoTrimming);
                    }
                }
                else //Other Days
                {
                    AddActiveRect(rcDay, FocusedPart.MonthDay, index);
                    var args = new CustomDrawDayEventArgs(rcDay, g, ViewYear, ViewMonth, x, false, dtInPaint);
                    OnDrawCurrentDay(args);

                    if (!args.Handled)
                    {
                        g.DrawString(dayNo, Font, brush, rcDay, OneLineNoTrimming);
                    }
                }

                if (IsRightToLeftCulture)
                {
                    rcDay.X = rcDay.X - iColWidth;

                    if (rcDay.X < 0)
                    {
                        rowNo++;
                        rcDay.X = rcHead.X;
                        rcDay.Y = rcDay.Y + iRowHeight + DEF_ROWS_MARGIN;
                    }
                }
                else
                {
                    rcDay.X = rcDay.X + iColWidth;

                    if (rcDay.X > rc.Width - rcDay.Width)
                    {
                        rowNo++;
                        rcDay.X = rc.X;
                        rcDay.Y = rcDay.Y + iRowHeight + DEF_ROWS_MARGIN;
                    }
                }
            }

            #endregion

            #region Post-Day Padding

            //Draw next month starting days as disabled
            int endDay;
            brush = Brushes.Gray;

            if (firstDay != 0)
            {
                endDay = numDays + 1;
            }
            else
            {
                endDay = numDays;
            }

            for (var i = endDay; i < 42; i++)
            {
                if (rowNo > 6)
                    break;

                var dayNo = i - endDay + 1;
                var nextDay = DefaultCalendar.GetDayOfMonth(dtStartOfNextMonth);
                var nextMonth = DefaultCalendar.GetMonth(dtStartOfNextMonth);
                var nextYear = DefaultCalendar.GetYear(dtStartOfNextMonth);

                var args = new CustomDrawDayEventArgs(rcDay, g, nextYear, nextMonth, nextDay, false, dtStartOfNextMonth);
                OnDrawCurrentDay(args);

                if (!args.Handled)
                {
                    var disabledDay = toFarsi.Convert(dayNo.ToString(), DefaultCulture);
                    g.DrawString(disabledDay, Font, brush, rcDay, OneLineNoTrimming);
                }

                if (IsRightToLeftCulture)
                {
                    rcDay.X = rcDay.X - iColWidth;

                    if (rcDay.X < 0)
                    {
                        rowNo++;
                        rcDay.X = rcHead.X;
                        rcDay.Y = rcDay.Y + iRowHeight + DEF_ROWS_MARGIN;
                    }
                }
                else
                {
                    rcDay.X = rcDay.X + iColWidth;

                    if (rcDay.X > rc.Width - rcDay.Width)
                    {
                        rowNo++;
                        rcDay.X = rc.X;
                        rcDay.Y = rcDay.Y + iRowHeight + DEF_ROWS_MARGIN;
                    }
                }

                dtStartOfNextMonth = dtStartOfNextMonth.AddDays(1);
            }

            #endregion
        }

        private void DrawDayValue(Graphics g, int day, Rectangle rcHead)
        {
            var strDayWeek = CultureHelper.GetCultureDayOfWeek(day, DefaultCulture);
            var value = GetAbbrDayName(strDayWeek);

            g.DrawString(value, Font, SystemBrushes.WindowText, rcHead, OneLineNoTrimming);
        }

        /// <summary>
        /// Executed when control is resized.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (Width < 166)
                Width = 166;

            if (Height < 166)
                Height = 166;

            rectsCreated = false;
            Invalidate();
        }

        private void AddActiveRect(Rectangle rc, FocusedPart action, object tag)
        {
            if (rectsCreated == false)
            {
                rects.Add(new ActRect(rc, action, tag));
            }
        }

        private void AddActiveRect(Rectangle rc, FocusedPart action)
        {
            if (rectsCreated == false)
            {
                rects.Add(new ActRect(rc, action));
            }
        }

        #endregion

        #region Overrides

        internal void OnRecalculateRequired()
        {
            ResetAllRectangleStates();

            if (rectsCreated)
                rectsCreated = false;

            var rect = FindActiveRectByTag(SelectedDay);
            if (iLastFocused < DEF_TODAY_TAB_INDEX)
                iLastFocused = SelectedDay;

            if (rect != null)
                rect.State |= TRectangleStatus.FocusSelect;
        }

        /// <summary>
        /// Scrolls days in the view to the Left.
        /// </summary>
        public void ScrollDaysLeft()
        {
            if (IsNull == false && iLastFocused < DEF_TODAY_TAB_INDEX)
            {
                SelectedDateTime = DefaultCalendar.AddDays(new DateTime(SelectedDateTime.Value.Year, SelectedDateTime.Value.Month, SelectedDateTime.Value.Day), -1);
            }
        }

        /// <summary>
        /// Scrolls days in the view to the Right.
        /// </summary>
        public void ScrollDaysRight()
        {
            if (IsNull == false && iLastFocused < DEF_TODAY_TAB_INDEX)
            {
                SelectedDateTime = DefaultCalendar.AddDays(new DateTime(SelectedDateTime.Value.Year, SelectedDateTime.Value.Month, SelectedDateTime.Value.Day), 1);
            }
        }

        /// <summary>
        /// Scrolls days in the view to the Up.
        /// </summary>
        public void ScrollDaysUp()
        {
            if (IsNull == false && iLastFocused < DEF_TODAY_TAB_INDEX)
            {
                SelectedDateTime = DefaultCalendar.AddDays(SelectedDateTime.Value, -7);
            }
        }

        /// <summary>
        /// Scrolls days in the view to the Down.
        /// </summary>
        public void ScrollDaysDown()
        {
            if (IsNull == false && iLastFocused < DEF_TODAY_TAB_INDEX)
            {
                SelectedDateTime = DefaultCalendar.AddDays(SelectedDateTime.Value, 7);
            }
        }

        internal void SetFocusOnNextControl()
        {
            ResetFocusedRectangleState();

            if (iLastFocused < DEF_TODAY_TAB_INDEX)
            {
                iLastFocused = DEF_TODAY_TAB_INDEX;
            }
            else if (iLastFocused == DEF_TODAY_TAB_INDEX)
            {
                iLastFocused = DEF_NONE_TAB_INDEX;
            }
            else
            {
                var f = FindForm();
                if (f != null)
                {
                    var ctrl = f.GetNextControl(this, true);
                    if (ctrl != null) ctrl.Focus();
                }
            }
        }

        internal void SetFocusOnPrevControl()
        {
            ResetFocusedRectangleState();

            if (iLastFocused < DEF_TODAY_TAB_INDEX)
            {
                iLastFocused = DEF_NONE_TAB_INDEX;
            }
            else if (iLastFocused == DEF_TODAY_TAB_INDEX && SelectedDay != 0)
            {
                iLastFocused = SelectedDay;

                var rc = FindActiveRectByTag(SelectedDay);
                if (rc != null)
                {
                    rc.State |= TRectangleStatus.Focused | TRectangleStatus.Selected;
                }
            }
            else if (iLastFocused == DEF_NONE_TAB_INDEX)
            {
                iLastFocused = DEF_TODAY_TAB_INDEX;
            }
            else
            {
                var f = FindForm();
                if (f != null)
                {
                    var ctrl = f.GetNextControl(this, false);
                    if (ctrl != null) ctrl.Focus();
                }
            }
        }

        /// <summary>
        /// Changes the Year value to the Next Year.
        /// </summary>
        public void ToNextYear()
        {
            try
            {
                ViewDateTime = DefaultCalendar.AddYears(ViewDateTime, 1);
                OnRecalculateRequired();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// Changes the Year value to the Previous Year.
        /// </summary>
        public void ToPrevYear()
        {
            try
            {
                ViewDateTime = DefaultCalendar.AddYears(ViewDateTime, -1);
                OnRecalculateRequired();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// Changes the Month value to the Next Month.
        /// </summary>
        public void ToNextMonth()
        {
            try
            {
                ViewDateTime = DefaultCalendar.AddMonths(ViewDateTime, 1);
                OnRecalculateRequired();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// Changes the Month value to the Previous Month.
        /// </summary>
        public void ToPrevMonth()
        {
            try
            {
                ViewDateTime = DefaultCalendar.AddMonths(ViewDateTime, -1);
                OnRecalculateRequired();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        private void RecalculateSelectionUp()
        {
            if (IsNull) return;

            var dtSelected = SelectedDateTime.Value;
            SelectedDateTime = ViewDateTime.AddDays(-7);

            if (SelectedDateTime.Value.Month != dtSelected.Month)
            {
                ScrollDaysUp();
            }
            else
            {
                ResetFocusedRectangleState();
            }
        }

        private void RecalculateSelectionDown()
        {
            if (IsNull) return;
            var dtSelected = SelectedDateTime.Value;
            SelectedDateTime = dtSelected.AddDays(7);

            if (SelectedDateTime.Value.Month != dtSelected.Month) // switch to another month
            {
                ScrollDaysDown();
            }
            else
            {
                ResetFocusedRectangleState();
            }
        }

        private void RecalculateSelectionLeft()
        {
            if (IsNull) return;
            var dtSelected = SelectedDateTime.Value;
            SelectedDateTime = dtSelected.AddDays(-1);

            if (SelectedDateTime.Value.Month != dtSelected.Month) // switch to another month
            {
                ScrollDaysLeft();
            }
            else
            {
                ResetFocusedRectangleState();
            }
        }

        private void RecalculateSelectionRight()
        {
            if (IsNull) return;
            var dtSelected = SelectedDateTime.Value;
            SelectedDateTime = dtSelected.AddDays(1);

            if (SelectedDateTime.Value.Month != dtSelected.Month) // switch to another month
            {
                ScrollDaysRight();
            }
            else
            {
                ResetFocusedRectangleState();
            }
        }

        private void OnRectangleClick(ActRect rc)
        {
            if (!Enabled)
                return;

            switch (rc.Action)
            {
                case FocusedPart.Hidden:
                    break;
                case FocusedPart.MonthPrev:
                    ToPrevMonth();
                    break;
                case FocusedPart.MonthNext:
                    ToNextMonth();
                    break;
                case FocusedPart.YearPrev:
                    ToPrevYear();
                    break;
                case FocusedPart.YearNext:
                    ToNextYear();
                    break;
                case FocusedPart.TodayButton:
                    iLastFocused = DEF_TODAY_TAB_INDEX;
                    SetTodayDay();
                    OnButtonClicked(new CalendarButtonClickedEventArgs(FAMonthViewButtons.Today) { Rect = rc });
                    break;
                case FocusedPart.NoneButton:
                    iLastFocused = DEF_NONE_TAB_INDEX;
                    SetNoneDay();
                    OnButtonClicked(new CalendarButtonClickedEventArgs(FAMonthViewButtons.None) { Rect = rc });
                    break;
                case FocusedPart.MonthDay:
                    if (SelectedDay == 0) return;

                    var index = (int)rc.Tag;
                    iLastFocused = index;

                    SelectedDateTime = new DateTime(ViewYear, ViewMonth, index, 0, 0, 0, DefaultCalendar);

                    if (IsMultiSelect)
                    {
                        if (!IsNull && !SelectedDateRange.Contains(SelectedDateTime.Value))
                        {
                            SelectedDateRange.Add(SelectedDateTime.Value);
                            if (!selectedRects.Contains(rc.Tag))
                                selectedRects.Add(rc.Tag);
                        }
                    }

                    OnButtonClicked(new CalendarButtonClickedEventArgs(FAMonthViewButtons.MonthDay) { Rect = rc });
                    break;
            }

            Invalidate();
        }

        private void OnSelectionClick(ActRect rc)
        {
            if (rc.Action == FocusedPart.MonthDay)
            {
                if (rc.IsSelected == false)
                {
                    rc.State |= TRectangleStatus.Selected;

                    SelectedDateTime = new DateTime(ViewYear, ViewMonth, (int)rc.Tag, 0, 0, 0, DefaultCalendar);
                    if (!IsNull && !SelectedDateRange.Contains(SelectedDateTime.Value))
                    {
                        SelectedDateRange.Add(SelectedDateTime.Value);

                        if (!selectedRects.Contains(rc.Tag))
                            selectedRects.Add(rc.Tag);
                    }
                }
                else
                {
                    rc.State = (TRectangleStatus)((int)rc.State & ~(int)TRectangleStatus.Selected);
                    selectedRects.Remove(rc.Tag);
                }

                iLastFocused = (int)rc.Tag;
            }

            Invalidate();
        }

        private void OnSelectionCollectionChanged(object sender, CollectionChangedEventArgs e)
        {
            ResetSelectedRectangleState();
            selectedRects.Clear();

            OnRecalculateRequired();
            Invalidate();
            OnSelectedDateRangeChanged(new SelectedDateRangeChangedEventArgs(SelectedDateRange.ToList()));
        }

        private void OnEnterPressed()
        {
            ResetSelectedRectangleState();

            var rect = FindActiveRectByTag(iLastFocused);
            if (rect == null)
                return;

            switch (rect.Action)
            {
                case FocusedPart.TodayButton:
                    SetTodayDay();
                    break;

                case FocusedPart.NoneButton:
                    SetNoneDay();
                    break;
            }
        }

        #endregion

        #region Public Methods

        public override Calendar DefaultCalendar
        {
            get { return base.DefaultCalendar; }
            set
            {
                base.DefaultCalendar = value;
                UpdateSelectedDayMonthYearValues();
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override CultureInfo DefaultCulture
        {
            get { return base.DefaultCulture; }
            set
            {
                base.DefaultCulture = value;
                rectsCreated = false;
                UpdateSelectedDayMonthYearValues();
            }
        }

        /// <summary>
        /// Current Month name shown in the view.
        /// </summary>
        [Browsable(false)]
        [Obsolete("Use GetMonthName method instead.")]
        public string CurrentMonthName
        {
            get { return base.GetMonthName(ViewMonth); }
        }

        /// <summary>
        /// Clears the selection of the control. Also clears any selected date in MultiSelect mode. 
        /// </summary>
        public void SetNoneDay()
        {
            SelectedDateTime = null;
            SelectedDateRange.Clear();
            OnSelectedDateTimeChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Sets the selection value to Today.
        /// </summary>
        public void SetTodayDay()
        {
            SelectedDateRange.Clear();
            SelectedDateTime = DateTime.Now;
            SelectedDateRange.Add(SelectedDateTime.Value);
        }

        #endregion

        #region Custom Events

        protected virtual void OnButtonClicked(CalendarButtonClickedEventArgs e)
        {
            if (ButtonClicked != null)
                ButtonClicked(this, e);
        }

        protected override void OnSelectedDateTimeChanged(EventArgs e)
        {
            rectsCreated = false;
            if (SelectedDateTime.HasValue && ViewDateTime != SelectedDateTime.Value)
            {
                ViewDateTime = SelectedDateTime.Value;
            }

            Invalidate();

            base.OnSelectedDateTimeChanged(e);
        }

        protected virtual void OnSelectedDateRangeChanged(SelectedDateRangeChangedEventArgs e)
        {
            if (SelectedDateRangeChanged != null)
                SelectedDateRangeChanged(this, e);
        }

        protected virtual void OnDrawCurrentDay(CustomDrawDayEventArgs e)
        {
            if (DrawCurrentDay != null)
                DrawCurrentDay(this, e);
        }

        #endregion

        #region Helper Methods

        private ActRect FindActiveRectByPoint(Point pnt)
        {
            var rect = rects.Find(x => x.Rect.Contains(pnt));
            return rect;
        }

        private ActRect FindActiveRectByTag(object tag)
        {
            var rect = rects.Find(x => x.Tag != null && x.Tag.Equals(tag));
            return rect;
        }

        private void ResetActiveRectanglesState()
        {
            foreach (var rc in rects)
            {
                if ((rc.State & TRectangleStatus.Active) > 0)
                {
                    rc.State = (TRectangleStatus)((int)rc.State & ~(int)TRectangleStatus.Active);
                }
            }
        }

        private void ResetSelectedRectangleState()
        {
            foreach (var rc in rects)
            {
                if ((rc.State & TRectangleStatus.Selected) > 0)
                {
                    rc.State = (TRectangleStatus)((int)rc.State & ~(int)TRectangleStatus.Selected);
                }
            }
        }

        private void ResetFocusedRectangleState()
        {
            foreach (var rc in rects)
            {
                if ((rc.State & TRectangleStatus.Focused) > 0)
                {
                    rc.State = (TRectangleStatus)((int)rc.State & ~(int)TRectangleStatus.Focused);
                }
            }
        }

        private void ResetAllRectangleStates()
        {
            rects.ForEach(x => x.State = TRectangleStatus.Normal);
        }

        #endregion

        #region Mouse Events

        protected override void OnMouseEnter(EventArgs e)
        {
            var pnt = MousePosition;
            pnt = PointToClient(pnt);
            btnTodayActive = false;
            btnNoneActive = false;

            ResetActiveRectanglesState();

            var rect = FindActiveRectByPoint(pnt);

            if (rect != null && rect.Action != FocusedPart.WeekDay)
            {
                rect.State |= TRectangleStatus.Active;
            }

            if (rect != null && ShowTodayButton && rect.Action == FocusedPart.TodayButton)
            {
                btnTodayActive = true;
            }

            if (rect != null && ShowEmptyButton && rect.Action == FocusedPart.NoneButton)
            {
                btnNoneActive = true;
            }

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            btnNoneActive = false;
            btnTodayActive = false;

            ResetActiveRectanglesState();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                switch (ScrollOption)
                {
                    case ScrollOptionTypes.Day:
                        ScrollDaysLeft();
                        break;

                    case ScrollOptionTypes.Month:
                        ToNextMonth();
                        break;

                    case ScrollOptionTypes.Year:
                        ToNextYear();
                        break;
                }
            }
            else
            {
                switch (ScrollOption)
                {
                    case ScrollOptionTypes.Day:
                        ScrollDaysRight();
                        break;

                    case ScrollOptionTypes.Month:
                        ToPrevMonth();
                        break;

                    case ScrollOptionTypes.Year:
                        ToPrevYear();
                        break;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var pnt = MousePosition;
            pnt = PointToClient(pnt);
            btnTodayActive = false;
            btnNoneActive = false;

            ResetActiveRectanglesState();

            var rect = FindActiveRectByPoint(pnt);

            if (rect != null && rect.Action != FocusedPart.WeekDay)
            {
                rect.State |= TRectangleStatus.Active;
            }

            if (rect != null && ShowTodayButton && rect.Action == FocusedPart.TodayButton)
            {
                btnTodayActive = true;
            }

            if (rect != null && ShowEmptyButton && rect.Action == FocusedPart.NoneButton)
            {
                btnNoneActive = true;
            }

            Invalidate();
        }

        private void OnLocalizerChanged(object sender, EventArgs e)
        {
            UpdateSelectedDayMonthYearValues();
            OnRecalculateRequired();
            Repaint();
        }

        internal void OnInternalMouseDown(Point mousePoint)
        {
            OnRecalculateRequired();

            var rect = FindActiveRectByPoint(mousePoint);
            if (rect == null)
                return;

            if (rect.Action != FocusedPart.WeekDay)
            {
                if (iLastFocused == DEF_NONE_TAB_INDEX)
                {
                    rect.State |= TRectangleStatus.Pressed;
                }

                if (iLastFocused == DEF_TODAY_TAB_INDEX)
                {
                    rect.State |= TRectangleStatus.Pressed;
                }

                switch (rect.Action)
                {
                    case FocusedPart.MonthPrev:
                        ToPrevMonth();
                        break;

                    case FocusedPart.MonthNext:
                        ToNextMonth();
                        break;

                    case FocusedPart.YearPrev:
                        ToPrevYear();
                        break;

                    case FocusedPart.YearNext:
                        ToNextYear();
                        break;

                    default:
                        break;
                }
            }
        }

        internal void OnInternalMouseClick(Point location)
        {
            if (!IsPopupMode)
                Focus();

            var rect = FindActiveRectByPoint(location);

            if (rect == null)
                return;

            if (rect.Action != FocusedPart.WeekDay)
            {
                ResetActiveRectanglesState();
                ResetFocusedRectangleState();

                // if selection begin
                if ((ModifierKeys & (Keys.Control | Keys.Shift)) == 0)
                {
                    selectedRects.Clear();
                    selectedDateRange.Clear();
                    ResetSelectedRectangleState();
                    OnRectangleClick(rect);
                }
                else
                {
                    if (!selectedRects.Contains(rect))
                        selectedRects.Add(rect);

                    OnSelectionClick(rect);
                }
            }
        }

        /// <summary>
        /// Returns the part of the control that is tested.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public FocusedPart HitTest(Point location)
        {
            var rect = FindActiveRectByPoint(location);
            return rect != null ? rect.Action : FocusedPart.None;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            OnInternalMouseClick(e.Location);

            base.OnMouseClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            var pnt = MousePosition;
            pnt = PointToClient(pnt);

            var rect = FindActiveRectByPoint(pnt);

            if (rect != null && rect.Action != FocusedPart.WeekDay)
            {
                ResetActiveRectanglesState();
                ResetSelectedRectangleState();
                ResetFocusedRectangleState();

                OnRectangleClick(rect);
            }

            base.OnDoubleClick(e);
        }

        #endregion

        #region Keyboard And Focus Event Handlers

        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            ResetFocusedRectangleState();
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Modifiers & (Keys.Alt | Keys.Control | Keys.Shift))
            {
                //Only Shift key is pressed
                case Keys.Shift:
                    switch (e.KeyCode)
                    {
                        case Keys.Tab:
                            SetFocusOnPrevControl();
                            break;
                        //TODO: Fix keyboard selection
                        //      Needs a special case when there is no selection
                        //      and selected date time is null.

                        case Keys.Down:
                            RecalculateSelectionDown();
                            break;
                        case Keys.Up:
                            RecalculateSelectionUp();
                            break;
                        case Keys.Left:
                            RecalculateSelectionLeft();
                            break;
                        case Keys.Right:
                            RecalculateSelectionRight();
                            break;
                    }
                    break;

                //Only Alt key is pressed
                case Keys.Alt:
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            ToPrevMonth();
                            break;
                        case Keys.Right:
                            ToNextMonth();
                            break;
                        case Keys.N:
                            SetNoneDay();
                            break;
                        case Keys.T:
                            SetTodayDay();
                            break;
                    }
                    break;

                //Only Control key is pressed
                case Keys.Control:
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            ToNextYear();
                            break;
                        case Keys.Down:
                            ToPrevYear();
                            break;
                    }
                    break;

                default:
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            if (iLastFocused == DEF_TODAY_TAB_INDEX || iLastFocused == DEF_NONE_TAB_INDEX)
                                SetFocusOnNextControl();
                            else
                                ScrollDaysDown();
                            break;
                        case Keys.Up:
                            if (iLastFocused == DEF_TODAY_TAB_INDEX || iLastFocused == DEF_NONE_TAB_INDEX)
                                SetFocusOnPrevControl();
                            else
                                ScrollDaysUp();
                            break;
                        case Keys.Left:
                            if (iLastFocused == DEF_TODAY_TAB_INDEX || iLastFocused == DEF_NONE_TAB_INDEX)
                                SetFocusOnPrevControl();
                            else
                                ScrollDaysLeft();
                            break;
                        case Keys.Right:
                            if (iLastFocused == DEF_TODAY_TAB_INDEX || iLastFocused == DEF_NONE_TAB_INDEX)
                                SetFocusOnNextControl();
                            else
                                ScrollDaysRight();
                            break;
                        case Keys.Tab:
                            SetFocusOnNextControl();
                            break;

                        case Keys.Space:
                        case Keys.Enter:
                            OnEnterPressed();
                            break;

                    }
                    break;
            }

            base.OnKeyDown(e);
            Invalidate();
        }

        #endregion

        #region Designer Methods

        /// <summary>
        /// Determines to serialize Size property or not.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeSize()
        {
            return false;
        }

        /// <summary>
        /// Determines to serialize SelectedDateRange property or not.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeSelectedDateRange()
        {
            return SelectedDateRange != null && SelectedDateRange.Count > 0;
        }

        /// <summary>
        /// Determines to serialize Anchor property or not
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeAnchor()
        {
            return false;
        }

        #endregion
    }
}