using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.WPF.Automation;
using FarsiLibrary.WPF.Converters;
using System.Windows.Automation.Peers;
using FarsiLibrary.Localization;

namespace FarsiLibrary.WPF.Controls
{
    [Flags]
    internal enum DatePickerFlags
    {
        IsContextMenuOpen = 0x1,
        IsNormalVisibleMonthChanged = 0x2,
        IgnoreDateSelectionChanged = 0x4,
    }

    /// <summary>
    /// The FXDatePicker control allows the user to enter or select a date and display it in 
    /// the specified format. User can limit the date that can be selected by setting the 
    /// selection range.  You might consider using a FXDatePicker control instead of a MonthView 
    /// if you need custom date formatting and limit the selection to just one date.
    /// </summary>
    [TemplatePart(Name = PART_MonthView, Type = typeof(FXMonthView))]
    [TemplatePart(Name = PART_DropDownButton, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PART_ValuePresenter, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_SelectedItemBorder, Type = typeof(Border))]
    public class FXDatePicker : ComboBox
    {
        #region Part Names

        public const string PART_MonthView = "PART_MonthView";
        public const string PART_DropDownButton = "PART_DropDownButton";
        public const string PART_ValuePresenter = "PART_ValuePresenter";
        public const string PART_SelectedItemBorder = "PART_SelectedItemBorder";

        #endregion

        #region Private Fields

        private FXMonthView mv;
        private TextBlock valuePresenter;
        private DateTime? prevValue;
        private readonly IValueConverter defaultConverter = new DateTimeValueConverter();

        #endregion

        #region Property Keys

        private static readonly DependencyPropertyKey IsValidPropertyKey = DependencyProperty.RegisterReadOnly("IsValid", typeof(bool), typeof(FXDatePicker), new FrameworkPropertyMetadata(false));
        private static readonly DependencyPropertyKey TextPropertyKey = DependencyProperty.RegisterReadOnly("Text", typeof(string), typeof(FXDatePicker), new FrameworkPropertyMetadata(""));

        #endregion

        #region Dependency Properties

        public new static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(FXDatePicker), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDropDownOpenChanged, CoerceIsDropDownOpen));
        public new static readonly DependencyProperty TextProperty = TextPropertyKey.DependencyProperty;
        public static readonly DependencyProperty IsValidProperty = IsValidPropertyKey.DependencyProperty;
        public static readonly DependencyProperty NullValueTextProperty = DependencyProperty.Register("NullValueText", typeof(string), typeof(FXDatePicker), new FrameworkPropertyMetadata(FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_NullText), new PropertyChangedCallback(OnNullValueTextChanged)));
        public static readonly DependencyProperty SelectedDateTimeProperty = DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(FXDatePicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, OnSelectedDateTimeChanged, CoerceSelectedDateTime));
        public static readonly DependencyProperty MinDateProperty = DependencyProperty.Register("MinDate", typeof(DateTime), typeof(FXDatePicker), new FrameworkPropertyMetadata(CultureHelper.MinCultureDateTime, new PropertyChangedCallback(OnMinDateChanged)));
        public static readonly DependencyProperty MaxDateProperty = DependencyProperty.Register("MaxDate", typeof(DateTime), typeof(FXDatePicker), new FrameworkPropertyMetadata(CultureHelper.MaxCultureDateTime, new PropertyChangedCallback(OnMaxDateChanged), CoerceMaxDate));
        public static readonly DependencyProperty DateConverterProperty = DependencyProperty.Register("DateConverter", typeof(IValueConverter), typeof(FXDatePicker), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDateConverterChanged)));
        public static readonly DependencyProperty MonthViewStyleProperty = DependencyProperty.Register("MonthViewStyle", typeof(Style), typeof(FXDatePicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty DropDownButtonStyleProperty = DependencyProperty.Register("DropDownButtonStyle", typeof(Style), typeof(FXDatePicker), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDropDownButtonStyleChanged)));
        public static readonly DependencyProperty ViewDateTimeProperty = DependencyProperty.Register("ViewDateTime", typeof(DateTime), typeof(FXDatePicker), new FrameworkPropertyMetadata(DateTime.Now.Date, new PropertyChangedCallback(OnViewDateTimeChanged), CoerceViewDateTime));
        public static readonly DependencyProperty ShowWeekDayNamesProperty = DependencyProperty.Register("ShowWeekDayNames", typeof(bool), typeof(FXDatePicker), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowTodayButtonProperty = DependencyProperty.Register("ShowTodayButton", typeof(bool), typeof(FXDatePicker), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowEmptyButtonProperty = DependencyProperty.Register("ShowEmptyButton", typeof(bool), typeof(FXDatePicker), new PropertyMetadata(true));

        #endregion

        #region Routed Events

        public static readonly RoutedEvent DropDownOpenedEvent = EventManager.RegisterRoutedEvent("DropDownOpened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FXDatePicker));
        public static readonly RoutedEvent DropDownClosedEvent = EventManager.RegisterRoutedEvent("DropDownClosed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FXDatePicker));
        public static readonly RoutedEvent InvalidEntryEvent = EventManager.RegisterRoutedEvent("InvalidEntry", RoutingStrategy.Bubble, typeof(InvalidEntryEventHandler), typeof(FXDatePicker));
        public static readonly RoutedEvent SelectedDateTimeChangedEvent = EventManager.RegisterRoutedEvent("SelectedDateTimeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime?>), typeof(FXDatePicker));

        #endregion

        #region Events

        /// <summary>
        /// Add / Remove DropDownOpened handler
        /// </summary>
        public new event RoutedEventHandler DropDownOpened
        {
            add { AddHandler(DropDownOpenedEvent, value); }
            remove { RemoveHandler(DropDownOpenedEvent, value); }
        }

        /// <summary>
        /// Add / Remove DropDownClosed handler
        /// </summary>
        public new event RoutedEventHandler DropDownClosed
        {
            add { AddHandler(DropDownClosedEvent, value); }
            remove { RemoveHandler(DropDownClosedEvent, value); }
        }

        /// <summary>
        /// Add / Remove InvalidEntry handler
        /// </summary>
        public event InvalidEntryEventHandler InvalidEntry
        {
            add { AddHandler(InvalidEntryEvent, value); }
            remove { RemoveHandler(InvalidEntryEvent, value); }
        }

        /// <summary>
        /// An event reporting that the SelectedDateTime property changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<DateTime?> SelectedDateTimeChanged
        {
            add { AddHandler(SelectedDateTimeChangedEvent, value); }
            remove { RemoveHandler(SelectedDateTimeChangedEvent, value); }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        static FXDatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXDatePicker), new FrameworkPropertyMetadata(typeof(FXDatePicker)));

            EventManager.RegisterClassHandler(typeof(FXDatePicker), Keyboard.KeyDownEvent, new KeyEventHandler(KeyDownHandler), true);
            EventManager.RegisterClassHandler(typeof(FXDatePicker), Mouse.MouseDownEvent, new MouseButtonEventHandler(OnMouseButtonDown), true);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The DateTime value of FXDatePicker
        /// </summary>
        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        /// <summary>
        /// The min date of FXDatePicker
        /// </summary>
        public DateTime MinDate
        {
            get { return (DateTime)GetValue(MinDateProperty); }
            set { SetValue(MinDateProperty, value); }
        }

        /// <summary>
        /// The max date of FXDatePicker
        /// </summary>
        public DateTime MaxDate
        {
            get { return (DateTime)GetValue(MaxDateProperty); }
            set { SetValue(MaxDateProperty, value); }
        }

        /// <summary>
        /// Preview DateTime
        /// </summary>
        public DateTime ViewDateTime
        {
            get { return (DateTime)GetValue(ViewDateTimeProperty); }
            set { SetValue(ViewDateTimeProperty, value); }
        }

        /// <summary>
        /// Show Today Button
        /// </summary>
        public bool ShowTodayButton
        {
            get { return (bool)GetValue(ShowTodayButtonProperty); }
            set { SetValue(ShowTodayButtonProperty, value); }
        }

        /// <summary>
        /// Show Empty Button
        /// </summary>
        public bool ShowEmptyButton
        {
            get { return (bool)GetValue(ShowEmptyButtonProperty); }
            set { SetValue(ShowEmptyButtonProperty, value); }
        }

        /// <summary>
        /// ShowWeekDayNames
        /// </summary>
        public bool ShowWeekDayNames
        {
            get { return (bool)GetValue(ShowWeekDayNamesProperty); }
            set { SetValue(ShowWeekDayNamesProperty, value); }
        }

        /// <summary>
        /// This property is used to parse/format between SelectedDateTime and text
        /// </summary>
        /// <remarks>
        /// ConvertBack is used to customize the parsing logic
        /// Convert is used to customimze the formatting logic
        /// If the converter can't parse the input text correctly, throw FormatException will fire InvalidEntry event
        /// </remarks>
        public IValueConverter DateConverter
        {
            get { return (IValueConverter)GetValue(DateConverterProperty); }
            set { SetValue(DateConverterProperty, value); }
        }

        /// <summary>
        /// DropDownButtonStyle property
        /// </summary>
        public Style DropDownButtonStyle
        {
            get { return (Style)GetValue(DropDownButtonStyleProperty); }
            set { SetValue(DropDownButtonStyleProperty, value); }
        }

        /// <summary>
        /// The style of drop-down MonthView
        /// </summary>
        public Style MonthViewStyle
        {
            get { return (Style)GetValue(MonthViewStyleProperty); }
            set { SetValue(MonthViewStyleProperty, value); }
        }

        /// <summary>
        /// This property indicates which input string should convert the SelectedDateTime of FXDatePicker into the null value.
        /// </summary>
        public string NullValueText
        {
            get { return (string)GetValue(NullValueTextProperty); }
            set { SetValue(NullValueTextProperty, value); }
        }

        /// <summary>
        /// Whether or not the "popup" for this control is currently open
        /// </summary>
        public new bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        /// <summary>
        /// A property indicating whether the SelectedDateTime is valid or not
        /// </summary>
        public bool IsValid
        {
            get { return SelectedDateTime.HasValue; }
        }

        /// <summary>
        /// Text store the formated SelectedDateTime, if the SelectedDateTime is null, it should store the NullValueText property
        /// </summary>
        public new string Text
        {
            get { return (string)GetValue(TextProperty); }
        }

        private bool HasCapture
        {
            get { return Mouse.Captured == this; }
        }

        private TextBlock ValuePresenter
        {
            get { return valuePresenter; }
            set { valuePresenter = value; }
        }

        private FXMonthView MonthView
        {
            get { return mv; }
            set { mv = value; }
        }

        #endregion

        #region Methods

        #region Overrides

        /// <summary>
        /// Called when the Template's tree has been generated
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AttachToVisualTree();
            DoFormat();
        }

        /// <summary>
        /// Returns a string representation for this control. "FXDatePicker, SelectedDateTime:06/02/2006"
        /// </summary>
        public override string ToString()
        {
            string s = base.ToString();

            if (IsValid)
                s += ", SelectedDateTime:" + SelectedDateTime.Value.ToShortDateString();

            return s;
        }

        /// <summary>
        /// Close the dropdown content if FXDatePicker lost the mouse capture
        /// </summary>
        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            if (Mouse.Captured != this)
            {
                if (e.OriginalSource == this)
                {
                    // If capture is null or it's not below the datepicker, close.
                    if (Mouse.Captured == null || !IsDescendant(this, Mouse.Captured as Visual))
                    {
                        IsDropDownOpen = false;
                    }
                }
                else
                {
                    if (IsDescendant(this, e.OriginalSource as Visual))
                    {
                        // Take capture if one of our children gave up capture (by closing their drop down)
                        if (IsDropDownOpen && Mouse.Captured == null)
                        {
                            Mouse.Capture(this, CaptureMode.SubTree);
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        IsDropDownOpen = false;
                    }
                }
            }
        }

        #endregion

        #region Coerce

        protected internal static object CoerceViewDateTime(DependencyObject d, object value)
        {
            var dp = (FXDatePicker) d;

            if (value != null)
            {
                var newValue = (DateTime)value;
                return FXMonthView.ValidateDateRange(newValue);
            }

            return value;
        }

        /// <summary>
        /// Coerce IsDropDownOpen with IsLoaded, so set IsDropDownOpen to true before UI ready can work
        /// </summary>
        private static object CoerceIsDropDownOpen(DependencyObject d, object value)
        {
            if ((bool)value)
            {
                FXDatePicker dp = (FXDatePicker)d;
                if (!dp.IsLoaded)
                {
                    //Defer setting IsDropDownOpen to true after Loaded event is fired to show popup window correctly
                    dp.Loaded += dp.OpenOnLoad;
                    return false;
                }
            }

            return value;
        }

        private static object CoerceSelectedDateTime(DependencyObject d, object value)
        {
            FXDatePicker datepicker = (FXDatePicker)d;

            if (value != null)
            {
                DateTime newValue = (DateTime)value;

                DateTime min = datepicker.MinDate;
                if (newValue < min)
                {
                    return min;
                }

                DateTime max = datepicker.MaxDate;
                if (newValue > max)
                {
                    return max;
                }
            }
            return value;
        }

        private static object CoerceMaxDate(DependencyObject d, object value)
        {
            FXDatePicker datepicker = (FXDatePicker)d;
            DateTime newValue = (DateTime)value;

            DateTime min = datepicker.MinDate;
            if (newValue < min)
            {
                return min;
            }

            return value;
        }

        #endregion

        #region Change Callback

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var datepicker = (FXDatePicker)d;

            if(datepicker.MonthView != null)
            {
                datepicker.MonthView.ShowTodayButton = datepicker.ShowTodayButton;
                datepicker.MonthView.ShowEmptyButton = datepicker.ShowEmptyButton;
                datepicker.MonthView.ShowWeekDayNames = datepicker.ShowWeekDayNames;
            }

            if ((bool)e.NewValue)
            {
                //Remember the previous SelectedDateTime for cancel action (Key.Escape)
                datepicker.prevValue = datepicker.SelectedDateTime;

                if (datepicker.MonthView != null && datepicker.SelectedDateTime != datepicker.MonthView.SelectedDateTime)
                {
                    datepicker.MonthView.SelectedDateTime = datepicker.SelectedDateTime;
                }

                // When the drop down opens, take capture
                Mouse.Capture(datepicker, CaptureMode.SubTree);

                // Popup.IsOpen is databound to IsDropDownOpen.  We don't know
                // if IsDropDownOpen will be invalidated before Popup.IsOpen.
                // If we are invalidated first and we try to focus the item, we
                // might succeed. When the popup finally opens, Focus
                // will be sent to null because Core doesn't know what else to do.
                // So, we must focus the element only after we are sure the popup
                // has opened. We will queue an operation (at Send priority) to
                // do this work -- this is the soonest we can make this happen.
                if (datepicker.MonthView != null && datepicker.SelectedDateTime.HasValue)
                {
                    datepicker.Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate(object arg)
                     {
                         FXDatePicker dp = (FXDatePicker)arg;
                         if (dp.IsKeyboardFocusWithin)
                         {
                             FXMonthViewItem item = dp.MonthView.GetMonthViewItemFromDate(dp.SelectedDateTime.Value);
                             if (item != null)
                             {
                                 item.Focus();
                             }
                         }
                         return null;
                     }, datepicker);
                }

                datepicker.OnDropDownOpened(new RoutedEventArgs(DropDownOpenedEvent));
            }
            else
            {
                // If focus is within the subtree, make sure we have the focus so that focus isn't in the disposed hwnd
                if (datepicker.IsKeyboardFocusWithin)
                {
                    // If use Mouse to select a date, DateSelectionChanged is fired in ListBox.MakeSingleSelection
                    // Then ListBoxItem.Focus() will be called which will grab the focus from FXDatePicker
                    // So use Dispatcher.BeginInvoke to set Focus to FXDatePicker after ListBoxItem.Focus()
                    datepicker.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (DispatcherOperationCallback)delegate(object arg)
                    {
                         FXDatePicker dp = (FXDatePicker)arg;
                         if (dp.IsKeyboardFocusWithin)
                         {
                             dp.Focus();
                         }
                         return null;
                    }, datepicker);

                    if (datepicker.HasCapture)
                    {
                        // It's not editable, make sure the datepicker has focus
                        datepicker.Focus();
                    }
                }

                if (datepicker.HasCapture)
                {
                    Mouse.Capture(null);
                }

                datepicker.OnDropDownClosed(new RoutedEventArgs(DropDownClosedEvent));
            }
        }

        private static void OnSelectedDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FXDatePicker datepicker = (FXDatePicker)d;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;

            //Invalid the IsValid and Text property when SelectedDateTime is changed
            datepicker.SetValue(IsValidPropertyKey, newValue.HasValue);
            datepicker.DoFormat(newValue);

            if (datepicker.MonthView != null)
            {
                datepicker.MonthView.SelectedDateTime = newValue;
            }

            RoutedPropertyChangedEventArgs<DateTime?> routedArgs = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue, SelectedDateTimeChangedEvent);
            datepicker.OnSelectedDateTimeChanged(routedArgs);
        }

        private static void OnDateConverterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FXDatePicker)d).DoFormat();
        }

        private static void OnNullValueTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FXDatePicker datepicker = (FXDatePicker)d;

            if (!datepicker.SelectedDateTime.HasValue)
                datepicker.DoFormat(null);
        }

        private static void OnMinDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FXDatePicker datepicker = (FXDatePicker)d;

            datepicker.CoerceValue(MaxDateProperty);
            datepicker.CoerceValue(SelectedDateTimeProperty);
        }

        private static void OnMaxDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FXDatePicker datepicker = (FXDatePicker)d;
            datepicker.CoerceValue(SelectedDateTimeProperty);
        }

        private static void OnDropDownButtonStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FXDatePicker)d).RefreshDropDownButtonStyle();
        }

        private void OnValuePresenterDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(!e.Handled)
                IsDropDownOpen = !IsDropDownOpen;
        }

        private void OnMonthViewDateTimeSelectionChanged(object sender, DateSelectionChangedEventArgs e)
        {
            if (SelectedDateTime != MonthView.SelectedDateTime)
            {
                SelectedDateTime = MonthView.SelectedDateTime;
                if (IsDropDownOpen && InputManager.Current.MostRecentInputDevice is MouseDevice)
                {
                    IsDropDownOpen = false;
                }
            }
        }

        #endregion

        #region Protected

        private static void OnViewDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dp = (FXDatePicker)d;
            FXMonthView.OnViewDateTimeChanged(dp.MonthView, e);
        }

        /// <summary>
        /// This method is invoked when the SelectedDateTime property changes.
        /// </summary>
        /// <param name="e">RoutedPropertyChangedEventArgs contains the old and new value.</param>
        protected virtual void OnSelectedDateTimeChanged(RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// Raise DropDownOpened event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDropDownOpened(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// Raise DropDownClosed event
        /// </summary>
        protected virtual void OnDropDownClosed(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// This event is invoked when datepicker can't parse the input string correctly
        /// </summary>
        protected virtual void OnInvalidEntry(InvalidEntryEventArgs e)
        {
            RaiseEvent(e);
        }

        #endregion

        #region Private

        private void OpenOnLoad(object sender, RoutedEventArgs e)
        {
            CoerceValue(IsDropDownOpenProperty);
            Loaded -= OpenOnLoad;
        }

        /// <summary>
        /// Detaches the EditableTextBox, MonthView from old child tree and attaches them to a new one
        /// </summary>
        private void AttachToVisualTree()
        {
            DetachFromVisualTree();

            MonthView = GetTemplateChild(PART_MonthView) as FXMonthView;
            ValuePresenter = GetTemplateChild(PART_ValuePresenter) as TextBlock;

            if (MonthView != null)
            {
                CommandManager.AddPreviewExecutedHandler(MonthView, OnMonthViewCommandPreviewExecuted);
                MonthView.SelectedDateTimeChanged += OnMonthViewDateTimeSelectionChanged;
            }

            if(ValuePresenter != null)
            {
                ValuePresenter.MouseLeftButtonDown += OnValuePresenterDoubleClick;
            }

            RefreshDropDownButtonStyle();
        }

        /// <summary>
        /// Clear the event, and detach our current EditableTextBox from ComboBox
        /// </summary>
        private void DetachFromVisualTree()
        {
            if (MonthView != null)
            {
                CommandManager.RemovePreviewExecutedHandler(MonthView, OnMonthViewCommandPreviewExecuted);
                MonthView.SelectedDateTimeChanged -= OnMonthViewDateTimeSelectionChanged;
                MonthView = null;
            }

            if(ValuePresenter != null)
            {
                ValuePresenter.MouseLeftButtonDown -= OnValuePresenterDoubleClick;
            }
        }

        private void RefreshDropDownButtonStyle()
        {
            ButtonBase dropdownButton = GetTemplateChild(PART_DropDownButton) as ButtonBase;
            if (dropdownButton != null && DropDownButtonStyle != null)
            {
                dropdownButton.Style = DropDownButtonStyle;
            }
        }

        /// <summary>
        /// If we (or one of our children) are clicked, claim the focus
        /// </summary>
        private static void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            FXDatePicker datepicker = (FXDatePicker)sender;
            e.Handled = true;

            if (Mouse.Captured == datepicker && e.OriginalSource == datepicker)
            {
                // When we have capture, all clicks off the popup will have the datepicker as
                // the OriginalSource.  So when the original source is the datepicker, that
                // means the click was off the popup and we should dismiss.
                datepicker.IsDropDownOpen = false;
            }
            else
            {
                // If mouse click the selected date, close the popup
                FrameworkElement fe = e.OriginalSource as FrameworkElement;
                if (fe != null && fe.DataContext is CalendarDay)
                {
                    if (datepicker.SelectedDateTime.HasValue && datepicker.SelectedDateTime.Value == ((CalendarDay)fe.DataContext).Date)
                    {
                        datepicker.IsDropDownOpen = false;
                    }
                }
            }
        }

        private static void OnContextMenuOpen(object sender, ContextMenuEventArgs e)
        {
            //((FXDatePicker)sender).IsContextMenuOpened = true;
        }

        private static void OnContextMenuClose(object sender, ContextMenuEventArgs e)
        {
            //((FXDatePicker)sender).IsContextMenuOpened = false;
        }

        /// <summary>
        /// True, if node is derived from reference
        /// </summary>
        private bool IsDescendant(Visual reference, Visual node)
        {
            bool success = false;

            Visual curr = node;

            while (curr != null)
            {
                if (curr == reference)
                {
                    success = true;
                    break;
                }

                // Try to jump up logical links if possible.
                FrameworkElement logicalCurrent = curr as FrameworkElement;
                Visual logicalCurrentVisualParent = null;
                // Check for logical parent and make sure it's a Visual
                if (logicalCurrent != null)
                {
                    logicalCurrentVisualParent = logicalCurrent.Parent as Visual;
                }

                if (logicalCurrentVisualParent != null)
                {
                    curr = logicalCurrentVisualParent;
                }
                else
                {
                    // Logical link isn't there; use child link
                    curr = VisualTreeHelper.GetParent(curr) as Visual;
                }
            }

            return success;
        }

        private void OnMonthViewCommandPreviewExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            if (args.Command == FXMonthViewCommands.ChangeToPrevMonth || 
                args.Command == FXMonthViewCommands.ChangeToPrevYear || 
                args.Command == FXMonthViewCommands.ChangeToNextYear || 
                args.Command == FXMonthViewCommands.ChangeToNextMonth)
            {
                //Check for container commands
            }
        }

        /// <summary>
        /// Called when a key event occurs.
        /// </summary>
        private static void KeyDownHandler(object sender, KeyEventArgs e)
        {
            ((FXDatePicker)sender).KeyDownHandler(e);
        }

        private void KeyDownHandler(KeyEventArgs e)
        {
            bool handled = false;
            Key key = e.Key;

            // Only process key events if they haven't been handled or are from our text box
            if (e.Handled == false)
            {
                // We want to handle Alt key. Get the real key if it is Key.System.
                if (key == Key.System)
                {
                    key = e.SystemKey;
                }

                switch (key)
                {
                    case Key.Up:
                        if ((e.KeyboardDevice.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                        {
                            KeyboardToggleDropDown(!IsDropDownOpen, true /* commitSelection */);
                            handled = true;
                        }
                        break;

                    case Key.Down:
                        if ((e.KeyboardDevice.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                        {
                            KeyboardToggleDropDown(!IsDropDownOpen, true /* commitSelection */);
                            handled = true;
                        }
                        else if (IsDropDownOpen)
                        {
                            SelectFocusableDate();
                            handled = true;
                        }
                        break;

                    case Key.F4:
                        if ((e.KeyboardDevice.Modifiers & ModifierKeys.Alt) == 0)
                        {
                            KeyboardToggleDropDown(!IsDropDownOpen, true /* commitSelection */);
                            handled = true;
                        }
                        break;

                    case Key.Escape:
                        if (IsDropDownOpen)
                        {
                            KeyboardToggleDropDown(false, false /* commitSelection */);
                            handled = true;
                        }
                        break;

                    case Key.Enter:
                        if (IsDropDownOpen)
                        {
                            KeyboardToggleDropDown(false, true /* commitSelection */);
                            handled = true;
                        }
                        break;

                    case Key.Tab:
                        if (IsDropDownOpen)
                        {
                            IsDropDownOpen = false;
                        }
                        break;

                    default:
                        break;
                }

                if (handled)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Close the dropdown and commit the selection if requested.
        /// Make sure to set the selection after the dropdown has closed
        /// Don't trigger any unnecessary navigation as a result of changing the selection.
        /// </summary>
        private void KeyboardToggleDropDown(bool openDropDown, bool commitSelection)
        {
            IsDropDownOpen = openDropDown;

            if (!openDropDown)
            {
                if (commitSelection && MonthView != null)
                {
                    SelectedDateTime = MonthView.SelectedDateTime;
                }
                else
                {
                    SelectedDateTime = prevValue;
                }
            }
        }

        /// <summary>
        /// Select the focusable date
        /// </summary>
        private void SelectFocusableDate()
        {
            if (MonthView == null)
            {
                return;
            }

            //If SelectedDateTime isn't null, select it; if not, select the first focusable date
            FXMonthViewItem focusableItem = null;
            if (SelectedDateTime.HasValue)
            {
                focusableItem = MonthView.GetMonthViewItemFromDate(SelectedDateTime.Value);
            }
            else
            {
                DateTime firstDayOfMonth = new DateTime(MonthView.ViewDateTime.Year, MonthView.ViewDateTime.Month, 1);

                for (int i = 0; i < DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month); ++i)
                {
                    focusableItem = MonthView.GetMonthViewItemFromDate(firstDayOfMonth);
                    if (IsFocusable(focusableItem))
                    {
                        break;
                    }
                    firstDayOfMonth = firstDayOfMonth.AddDays(1);
                }
            }

            if (focusableItem != null)
            {
                focusableItem.IsSelected = true;
                focusableItem.Focus();
            }
        }

        /// <summary>
        /// True if the element can be focused
        /// </summary>
        private bool IsFocusable(FrameworkElement fe)
        {
            return fe != null && fe.Focusable && (bool)fe.GetValue(IsTabStopProperty) && fe.IsEnabled && fe.Visibility == Visibility.Visible;
        }

        /// <summary>
        /// Format SelectedDateTime property to a formatted string
        /// </summary>
        internal void DoFormat()
        {
            DoFormat(SelectedDateTime);
        }

        internal string DoFormat(DateTime? date)
        {
            string text;

            if (date.HasValue)
            {
                object o;
                if (DateConverter != null)
                {
                    o = DateConverter.Convert(date.Value, typeof(string), null, CultureHelper.CurrentCulture);
                }
                else
                {
                    o = defaultConverter.Convert(date.Value, typeof(string), null, CultureHelper.CurrentCulture);
                }

                text = Convert.ToString(o, CultureHelper.CurrentCulture);
            }
            else
            {
                text = NullValueText;
            }

            SetValue(TextPropertyKey, text);
            return text;
        }

        #endregion

        #region Public

        public void SetTodayDate()
        {
            mv.SetTodayDate();
        }

        public void SetNoneDate()
        {
            mv.SetNoneDate();
        }

        #endregion

        #endregion

        #region AutomationPeer

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new FXDatePickerAutomationPeer(this);
        }

        #endregion
    }
}