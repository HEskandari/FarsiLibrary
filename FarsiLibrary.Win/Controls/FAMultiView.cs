using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FarsiLibrary.Win.BaseClasses;
using FarsiLibrary.Win.Design;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Controls
{
    [ToolboxItem(true)]
    [Designer(typeof(FAMultiViewDesigner))]
    [DefaultEvent("SelectedDateTimeChanged")]
    [DefaultProperty("SelectedDateTime")]
    [DefaultBindingProperty("SelectedDateTime")]
    public class FAMultiView : BaseDateControl
    {
        #region Fields

        protected FAMonthView monthView;
        protected FADayView dayView;
        private ViewType viewtype = ViewType.Month;
        private bool showEmptyButton = true;
        private bool showTodayButton = true;
        private bool showFocusRect = false;
        private bool showBorder = true;
        private ScrollOptionTypes scrollOptions = ScrollOptionTypes.Month;
        private readonly bool isInitialized;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of FAMultiView
        /// </summary>
        public FAMultiView()
        {
            base.Size = new Size(ControlWidth, ControlHeight);
            base.Font = new Font("Tahoma", 8.25F);
            
            CreateMonthView();
            CreateDayView();
            ShowCurrentView();

            isInitialized = true;
        }

        #endregion

        #region Views

        private void ShowCurrentView()
        {
            var newView = GetDefaultView();
            var oldView = Controls[0];

            ChangeViewVisibility(oldView, newView);

            Controls.SetChildIndex(newView, 0);
        }

        private void ChangeViewVisibility(Control oldView, Control newView)
        {
            newView.Visible = true;

            if (oldView != newView)
                oldView.Visible = false;
        }

        private Control GetDefaultView()
        {
            switch (viewtype)
            {
                case ViewType.Month:
                    return monthView;
                case ViewType.Day:
                    return dayView;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void CreateDayView()
        {
            dayView = new FADayView
            {
                Visible = false, 
                Name = "DayView"
            };

            dayView.SelectedDateTimeChanged += (sender, e) => SelectedDateTime = dayView.SelectedDateTime;
            dayView.ViewDateTimeChanged += (sender, e) => ViewDateTime = dayView.ViewDateTime;
            dayView.Click += (sender, e) => ShowMonthView();
            Controls.Add(dayView);
        }

        protected virtual void CreateMonthView()
        {
            monthView = new FAMonthView(false)
            {
                Visible = false, 
                Name = "MonthView"
            };

            monthView.SelectedDateTimeChanged += (sender, e) => SelectedDateTime = monthView.SelectedDateTime;
            monthView.ViewDateTimeChanged += (sender, e) => ViewDateTime = monthView.ViewDateTime;
            monthView.ButtonClicked += (sender, e) =>
            {
                if (e.Rect.Action == FocusedPart.MonthDay && e.Rect.IsFocused)
                {
                    ShowDayView();
                }
            };

            Controls.Add(monthView);
        }

        private void UpdateViewDates()
        {
            if (!isInitialized) return;

            if (monthView.SelectedDateTime != SelectedDateTime)
            {
                monthView.SelectedDateTime = SelectedDateTime;
            }

            if (monthView.ViewDateTime != ViewDateTime)
            {
                monthView.ViewDateTime = ViewDateTime;
            }

            if (SelectedDateTime.HasValue && dayView.ViewDateTime != SelectedDateTime.Value)
            {
                dayView.ViewDateTime = SelectedDateTime.Value;
            }
        }

        /// <summary>
        /// Switches to Month view
        /// </summary>
        public void ShowMonthView()
        {
            View = ViewType.Month;
        }

        /// <summary>
        /// Switches to Day view
        /// </summary>
        public void ShowDayView()
        {
            View = ViewType.Day;
        }

        #endregion

        #region ViewType

        /// <summary>
        /// Type of the displayed view
        /// </summary>
        [DefaultValue(typeof(ViewType), "Month")]
        public ViewType View
        {
            get { return viewtype; }
            set
            {
                viewtype = value;
                ShowCurrentView();
            }
        }

        #endregion

        #region Date

        protected override void OnSelectedDateTimeChanged(EventArgs e)
        {
            base.OnSelectedDateTimeChanged(e);

            UpdateViewDates();
        }

        #endregion

        #region Theme

        protected override void OnThemeChanged(EventArgs e)
        {
            monthView.Theme = Theme;
            dayView.Theme = Theme;

            base.OnThemeChanged(e);
        }

        #endregion

        #region Delegated Properties

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
                monthView.ShowEmptyButton = value;
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
                monthView.ShowTodayButton = value;
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
                showFocusRect = value;
                monthView.ShowFocusRect = value;
            }
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
                showBorder = value;
                monthView.ShowBorder = value;
            }
        }

        /// <summary>
        /// Determinces scrolling option of the FAMonthView control.
        /// </summary>
        [DefaultValue(typeof(ScrollOptionTypes), "Month")]
        [Description("Determinces scrolling option of the FAMonthView control.")]
        public ScrollOptionTypes ScrollOptions
        {
            get { return scrollOptions; }
            set
            {
                scrollOptions = value;
                monthView.ScrollOption = value;
            }
        }

        #endregion

        #region Resize

        /// <summary>
        /// Executed when control is resized.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (Width < ControlWidth)
                Width = ControlWidth;

            if (Height < ControlHeight)
                Height = ControlHeight;

            Invalidate();
        }

        /// <summary>
        /// Size of the control that can not be changes. Control's size is fixed to 166 x 166 pixels.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size Size
        {
            get { return base.Size; }
            set
            {
                if(value.Width != ControlWidth && value.Height != ControlHeight)
                    value = new Size(ControlWidth, ControlHeight);
			    
                base.Size = value;
            }
        }

        #endregion
    }
}