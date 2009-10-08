using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FarsiLibrary.Resources;
using FarsiLibrary.Utils;
using FarsiLibrary.Web.Helper;
using System.Threading;
using PersianCalendar=FarsiLibrary.Utils.PersianCalendar;

namespace FarsiLibrary.Web.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:FADatePicker runat=server></{0}:FADatePicker>")]
    public class FADatePicker : CompositeControl, ICallbackEventHandler, INamingContainer
    {
        #region Const

        public const string OpenButtonID = "openButton";
        public const string MonthViewID = "monthView";

        #endregion

        #region Fields

        internal TextBox datebox;
        internal FAMonthView mv;
        internal HtmlImage openButton;
        internal string callbackEventReference;

        private TableItemStyle monthViewDayHeaderStyle;
        private TableItemStyle monthViewDayStyle;
        private TableItemStyle monthViewNextPrevStyle;
        private TableItemStyle monthViewOtherMonthDayStyle;
        private TableItemStyle monthViewSelectedDayStyle;
        private TableItemStyle monthViewTitleStyle;
        private TableItemStyle monthViewTodayDayStyle;
        private TableItemStyle monthViewWeekendDayStyle;
        private System.Globalization.Calendar customCalendar;
        private readonly PersianCalendar pc;
        private readonly GregorianCalendar gc;
        private readonly HijriCalendar hc;
        private TableItemStyle _monthViewFooterStyle;

        #endregion

        #region Events

        /// <summary>
        /// Fires when Day value changes.
        /// </summary>
        public event EventHandler<DateChangedEventArgs> DayChanged;

        /// <summary>
        /// Fires when MonthValue changes.
        /// </summary>
        public event EventHandler<DateChangedEventArgs> MonthChanged;

        /// <summary>
        /// Fires when Year value changes.
        /// </summary>
        public event EventHandler<DateChangedEventArgs> YearChanged;

        #endregion

        #region Init

        public FADatePicker()
        {
            callbackEventReference = string.Empty;
        }

        protected override void RecreateChildControls()
        {
            base.RecreateChildControls();
            this.EnsureChildControls();
        }

        #endregion

        #region Create Child Controls

        protected override void CreateChildControls()
        {
            Controls.Clear();

            ScriptManagerHelper.RegisterCallbackScripts(this);
            CreateTextBox();
            CreateOpenButton();
            CreateMonthView();

            Controls.Add(datebox);
            Controls.Add(mv);
            Controls.Add(openButton);

            base.CreateChildControls();
        }

        protected virtual void CreateTextBox()
        {
            datebox = new TextBox { Width = DateBoxWidth };
            SetDateValue(null);
        }

        protected virtual void CreateOpenButton()
        {
            openButton = new HtmlImage();
            openButton.ID = OpenButtonID;
            openButton.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
            openButton.Style.Add(HtmlTextWriterStyle.VerticalAlign, "bottom");
            openButton.Style.Add(HtmlTextWriterStyle.PaddingBottom, "1px");
            openButton.Style.Add(HtmlTextWriterStyle.PaddingLeft, "1px");
        }

        protected virtual void CreateMonthView()
        {
            mv = new FAMonthView();
            mv.ID = MonthViewID;
            mv.SelectionMode = CalendarSelectionMode.None;
            mv.DayRender += MonthViewDayRender;
        }

        #endregion

        #region PreRender

        protected override void OnPreRender(EventArgs e)
        {
            SetButtonProperties();
            SetMonthViewProperties();
            ScriptManagerHelper.RegisterControlScripts(this);

            base.OnPreRender(e);
        }

        private void SetMonthViewProperties()
        {
            mv.Font.MergeWith(MonthViewFont);
            mv.DayHeaderStyle.MergeWith(MonthViewDayHeaderStyle);
            mv.DayStyle.MergeWith(MonthViewDayStyle);
            mv.NextPrevStyle.MergeWith(MonthViewNextPrevStyle);
            mv.OtherMonthDayStyle.MergeWith(MonthViewOtherMonthDayStyle);
            mv.SelectedDayStyle.MergeWith(MonthViewSelectedDayStyle);
            mv.TitleStyle.MergeWith(MonthViewTitleStyle);
            mv.TodayDayStyle.MergeWith(MonthViewTodayDayStyle);
            mv.WeekendDayStyle.MergeWith(MonthViewWeekendDayStyle);
            mv.FirstDayOfWeek = FirstDayOfWeek;
            mv.DayNameFormat = CalendarDayNameFormat;
        }

        private void SetButtonProperties()
        {
            openButton.Src = OpenButtonImageUrl ?? Page.ClientScript.GetWebResourceUrl(typeof (FADatePicker), "FarsiLibrary.Web.Images.OpenButton.gif");
        }

        #endregion

        #region Render

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "FADatePicker_" + this.ClientID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline");
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, this.ZIndex.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            
            this.AddAttributesToRender(writer);
            this.datebox.RenderControl(writer);
            this.openButton.RenderControl(writer);
            
            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "FADatePickerCalendar_" + this.ClientID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, this.ZIndex.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        #endregion

        #region Min & Max Value Coercing

        private void MonthViewDayRender(object sender, DayRenderEventArgs e)
        {
            if ((e.Day.Date >= this.MinDate) && (e.Day.Date <= this.MaxDate))
            {
                e.Cell.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                e.Cell.Attributes.Add("onclick", "SetDate_" + this.ClientID + "('" + e.Day.Date.ToString(this.DateFormat) + "')");
            }
            else
            {
                e.Cell.Enabled = false;
            }

            e.Cell.Text = e.Day.DayNumberText;
            e.Cell.Controls.Clear();
        }

        #endregion

        #region Callbacks

        public string GetCallbackResult()
        {
            //return string.Empty;
            ThemeHelper theme = new ThemeHelper(Theme);
            if (!Enabled)
            {
                return string.Empty;
            }

            mv.Width = DateBoxWidth;
            mv.BorderStyle = BorderStyle.None;
            mv.ShowTitle = false;
            mv.DayRender += MonthViewDayRender;

            mv.Font.Name = "Tahoma";
            mv.DayStyle.MergeWith(theme.DayStyle);
            mv.DayHeaderStyle.MergeWith(theme.DayStyle);
            mv.SelectedDayStyle.MergeWith(theme.SelectedDayStyle);
            mv.TodayDayStyle.MergeWith(theme.TodayDayStyle);

            //next & prev formats

            DropDownList list = theme.CreateFooterDropDownList();
            //list.MergeStyle(monthYearDropDownStyle);


            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            HtmlTextWriter tw = new HtmlTextWriter(writer);

            if (this.EnableDropShadow)
            {
                tw.AddStyleAttribute(HtmlTextWriterStyle.Display, "#EFEFEF");
            }
            tw.AddAttribute(HtmlTextWriterAttribute.Id, "FADatePickerDropShadow_" + this.ClientID);
            tw.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, (this.ZIndex + 1).ToString());
            tw.AddStyleAttribute(HtmlTextWriterStyle.Left, this.CalendarOffsetX.ToString());
            tw.AddStyleAttribute(HtmlTextWriterStyle.Top, this.CalendarOffsetY.ToString());
            tw.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            tw.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline");
            tw.AddStyleAttribute("min-width", this.mv.Width.ToString());
            tw.AddAttribute("CellPadding", "0");
            tw.AddAttribute("CellSpacing", "0");
            tw.RenderBeginTag(HtmlTextWriterTag.Table);
            tw.RenderBeginTag(HtmlTextWriterTag.Tr);
            tw.RenderBeginTag(HtmlTextWriterTag.Td);
            tw.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white");
            tw.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, "gray");
            tw.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "solid");
            tw.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "1px");
            tw.AddStyleAttribute(HtmlTextWriterStyle.Left, "-4px");
            tw.AddStyleAttribute(HtmlTextWriterStyle.Top, "-4px");
            tw.AddStyleAttribute(HtmlTextWriterStyle.Width, this.mv.Width.ToString());
            tw.AddAttribute(HtmlTextWriterAttribute.Id, "FADatePickerCalendarContainer_" + this.ClientID);
            tw.AddAttribute("CellPadding", "0");
            tw.AddAttribute("CellSpacing", "0");
            tw.RenderBeginTag(HtmlTextWriterTag.Table);
            tw.RenderBeginTag(HtmlTextWriterTag.Tr);
            tw.RenderBeginTag(HtmlTextWriterTag.Td);

            Table table = theme.CreateTitleTable(this.Page);
            table.Width = Unit.Percentage(100.0);
            table.MergeStyle(this.MonthViewTitleStyle);
            table.ID = "FADatePickerCalendarTitle_" + this.ClientID;
            table.RenderBeginTag(tw);
            tw.RenderBeginTag(HtmlTextWriterTag.Tr);
            TableCell cell = new TableCell();
            cell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            cell.MergeStyle(this.MonthViewNextPrevStyle);
            cell.RenderBeginTag(tw);

            if ((this.mv.VisibleDate.Month > this.MinDate.Month) || (this.mv.VisibleDate.Year > this.MinDate.Year))
            {
                try
                {
                    DateTime dt = this.mv.VisibleDate.AddMonths(-1);
                    dt = dt.AddDays((double)-(dt.Day - 1));
                    if (this.NextPrevFormat == NextPrevFormat.ShortMonth)
                    {
                        tw.WriteLine("<div style='cursor:pointer;' onclick=\"javascript:CallServer_" + this.ClientID + "('" + dt.ToShortDateString() + "')\">" + dt.ToString("MMM") + "</div>");
                    }
                    else if (this.NextPrevFormat == NextPrevFormat.FullMonth)
                    {
                        tw.WriteLine("<div style='cursor:pointer;' onclick=\"javascript:CallServer_" + this.ClientID + "('" + dt.ToShortDateString() + "')\">" + dt.ToString("MMMM") + "</div>");
                    }
                    else
                    {
                        tw.WriteLine("<div style='cursor:pointer;' onclick=\"javascript:CallServer_" + this.ClientID + "('" + dt.ToShortDateString() + "')\">" + this.PrevMonthText + "</div>");
                    }
                }
                catch
                {
                }
            }

            cell.RenderEndTag(tw);
            tw.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
            tw.RenderBeginTag(HtmlTextWriterTag.Td);
            tw.WriteLine(this.mv.VisibleDate.ToString("MMMM yyyy"));
            tw.RenderEndTag();
            TableCell cell2 = new TableCell();
            cell2.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            cell2.MergeStyle(this.MonthViewNextPrevStyle);
            cell2.RenderBeginTag(tw);

            if ((this.mv.VisibleDate.Month < this.MaxDate.Month) || (this.mv.VisibleDate.Year < this.MaxDate.Year))
            {
                try
                {
                    DateTime time9 = this.mv.VisibleDate.AddMonths(1);
                    time9 = time9.AddDays((double)-(time9.Day - 1));
                    if (this.NextPrevFormat == NextPrevFormat.ShortMonth)
                    {
                        tw.WriteLine("<div style='cursor:pointer;' onclick=\"javascript:CallServer_" + this.ClientID + "('" + time9.ToShortDateString() + "')\">" + time9.ToString("MMM") + "</div>");
                    }
                    else if (this.NextPrevFormat == NextPrevFormat.FullMonth)
                    {
                        tw.WriteLine("<div style='cursor:pointer;' onclick=\"javascript:CallServer_" + this.ClientID + "('" + time9.ToShortDateString() + "')\">" + time9.ToString("MMMM") + "</div>");
                    }
                    else
                    {
                        tw.WriteLine("<div style='cursor:pointer;' onclick=\"javascript:CallServer_" + this.ClientID + "('" + time9.ToShortDateString() + "')\">" + this.NextMonthText + "</div>");
                    }
                }
                catch
                {
                }
            }
            cell2.RenderEndTag(tw);
            tw.RenderEndTag();
            table.RenderEndTag(tw);

            this.mv.RenderControl(tw);

            Table table2 = theme.CreateFooterTable(this.Page);
            table2.ID = string.Format("{0}_Footer", this.ClientID);
            table2.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            table2.Width = Unit.Percentage(100.0);
            table2.MergeStyle(this.MonthViewFooterStyle);
            table2.RenderBeginTag(tw);
            tw.RenderBeginTag(HtmlTextWriterTag.Tr);
            tw.RenderBeginTag(HtmlTextWriterTag.Td);
            if (this.ShowTodayButton)
            {
                Button button = theme.CreateFooterButton();
                //button.MergeStyle(this.TodayButtonStyle);
                //button.Text = this.TodayButtonText;
                button.OnClientClick = "javascript:SetDate_" + this.ClientID + "('" + DateTime.Today.ToString(this.DateFormat) + "'); return false;";
                button.RenderControl(tw);
            }

            if (this.ShowEmptyButton)
            {
                Button button2 = theme.CreateFooterButton();
                //button2.MergeStyle(this.TodayButtonStyle);
                //button2.Text = this.NoneButtonText;
                button2.OnClientClick = "javascript:SetDate_" + this.ClientID + "(''); return false;";
                button2.RenderControl(tw);
            }
            list.RenderControl(tw);
            //list2.RenderControl(tw);
            tw.RenderEndTag();
            tw.RenderEndTag();
            table2.RenderEndTag(tw);
            tw.RenderEndTag();
            tw.RenderEndTag();
            tw.RenderEndTag();
            tw.RenderEndTag();
            tw.RenderEndTag();
            tw.RenderEndTag();

            return sb.ToString();
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            if (!Enabled) 
                return;

            //if (eventArgument.StartsWith("SETDATE:"))
            //{
            //    try
            //    {
            //        DateTime time = DateTime.Parse(eventArgument.Substring(8));
            //        if (time > this.MaxDate)
            //        {
            //            this.Date = this.MaxDate;
            //        }
            //        else if (time < this.MinDate)
            //        {
            //            this.Date = this.MinDate;
            //        }
            //        else
            //        {
            //            this.Date = time;
            //        }
            //    }
            //    catch
            //    {
            //    }
            //    this.pickCalendar.SelectedDate = this.Date;
            //    if (this.IsNull)
            //    {
            //        this.pickCalendar.VisibleDate = DateTime.Today;
            //    }
            //    else
            //    {
            //        this.pickCalendar.VisibleDate = this.Date;
            //    }
            //}
            //else
            //{
            //    DateTime result = new DateTime();
            //    if (DateTime.TryParse(eventArgument, this.Culture, DateTimeStyles.None, out result))
            //    {
            //        if (result.CompareTo(new DateTime(0L)) == 0)
            //        {
            //            this.pickCalendar.VisibleDate = DateTime.Today;
            //        }
            //        else
            //        {
            //            this.pickCalendar.VisibleDate = result;
            //        }
            //    }
            //    else if (string.IsNullOrEmpty(eventArgument))
            //    {
            //        this.Date = DateTime.MinValue;
            //    }
            //    else if (this.IsNull)
            //    {
            //        this.pickCalendar.VisibleDate = DateTime.Today;
            //    }
            //    else
            //    {
            //        this.pickCalendar.VisibleDate = this.Date;
            //    }
            //}
        }

        #endregion

        #region Get / Set Date Value

        private DateTime? GetDateValue()
        {
            EnsureChildControls();

            if(!string.IsNullOrEmpty(datebox.Text))
            {
                DateTime dt;
                if(DateTime.TryParse(datebox.Text, DefaultCulture, DateTimeStyles.None, out dt))
                {
                    if(dt > MaxDate)
                    {
                        dt = MaxDate;
                    }
                    
                    if(dt < MinDate)
                    {
                        dt = MinDate;
                    }

                    return dt;
                }
            }

            return null;
        }

        private void SetDateValue(DateTime? value)
        {
            EnsureChildControls();

            if (!value.HasValue)
            {
                IsNull = true;
                datebox.Text = FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_NullText);
            }
            else
            {
                datebox.Text = FormatDate(value.Value);
            }
        }

        private void CheckDateValueRange(ref DateTime? value)
        {
            if(!value.HasValue)
                return;

            if (value.Value > MaxDate)
                value = MaxDate;

            if (value.Value < MinDate)
                value = MinDate;
        }

        protected virtual string FormatDate(DateTime value)
        {
            return value.ToString(DateFormat);
        }

        #endregion

        #region Selection Changed

        private void UpdateDayMonthYearValues()
        {
            var args = new DateChangedEventArgs(SelectedDateTime, SelectedDateTime);
            UpdateDayMonthYearValues(args);
        }

        private void UpdateDayMonthYearValues(DateChangedEventArgs args)
        {
        }

        #endregion

        #region Calendar & Culture

        private System.Globalization.Calendar GetDefaultCalendar()
        {
            if (customCalendar != null)
                return customCalendar;

            if (DefaultCulture.Equals(FALocalizeManager.Instance.FarsiCulture))
                return pc;

            if (DefaultCulture.Equals(FALocalizeManager.Instance.ArabicCulture))
                return hc;

            return gc;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Default calendar of the control, based on <see cref="Thread.CurrentCulture"/> and <see cref="Thread.CurrentUICulture"/> properties.
        /// </summary>
        [Browsable(false)]
        public System.Globalization.Calendar DefaultCalendar
        {
            get
            {
                return GetDefaultCalendar();
            }
            set
            {
                customCalendar = value;
                UpdateDayMonthYearValues();
            }
        }

        [Browsable(false)]
        public CultureInfo DefaultCulture
        {
            get
            {
                if (FALocalizeManager.Instance.CustomCulture != null)
                    return FALocalizeManager.Instance.CustomCulture;

                if (Thread.CurrentThread.CurrentUICulture.Equals(FALocalizeManager.Instance.FarsiCulture))
                    return FALocalizeManager.Instance.FarsiCulture;

                if (Thread.CurrentThread.CurrentUICulture.Equals(FALocalizeManager.Instance.ArabicCulture))
                    return FALocalizeManager.Instance.ArabicCulture;

                return FALocalizeManager.Instance.InvariantCulture;
            }
            set
            {
                FALocalizeManager.Instance.CustomCulture = value;
                UpdateDayMonthYearValues();
            }
        }

        [Bindable(true)]
        [DefaultValue(1)]
        [Category("Appearance")]
        [Localizable(true)]
        public DayNameFormat CalendarDayNameFormat
        {
            get { return ViewState.GetValue("CalendarDayNameFormat", DayNameFormat.Short); }
            set { ViewState.SetValue("CalendarDayNameFormat", value); }
        }

        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle MonthViewDayHeaderStyle
        {
            get
            {
                if (monthViewDayHeaderStyle == null)
                    monthViewDayHeaderStyle = new TableItemStyle();

                return monthViewDayHeaderStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle MonthViewOtherMonthDayStyle
        {
            get
            {
                if (monthViewOtherMonthDayStyle == null)
                    monthViewOtherMonthDayStyle = new TableItemStyle();

                return monthViewOtherMonthDayStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle MonthViewSelectedDayStyle
        {
            get
            {
                if (monthViewSelectedDayStyle == null)
                    monthViewSelectedDayStyle = new TableItemStyle();

                return monthViewSelectedDayStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DefaultValue(null)]
        public TableItemStyle MonthViewWeekendDayStyle
        {
            get
            {
                if (monthViewWeekendDayStyle == null)
                    monthViewWeekendDayStyle = new TableItemStyle();
                
                return monthViewWeekendDayStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle MonthViewTodayDayStyle
        {
            get
            {
                if (monthViewTodayDayStyle == null)
                    monthViewTodayDayStyle = new TableItemStyle();
                
                return monthViewTodayDayStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle MonthViewTitleStyle
        {
            get
            {
                if (monthViewTitleStyle == null)
                    monthViewTitleStyle = new TableItemStyle();
                
                return monthViewTitleStyle;
            }
        }
 
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableItemStyle MonthViewDayStyle
        {
            get
            {
                if (monthViewDayStyle == null)
                    monthViewDayStyle = new TableItemStyle();

                return monthViewDayStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DefaultValue(null)]
        public TableItemStyle MonthViewNextPrevStyle
        {
            get
            {
                if (monthViewNextPrevStyle == null)
                    monthViewNextPrevStyle = new TableItemStyle();
                
                return monthViewNextPrevStyle;
            }
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FontInfo MonthViewFont
        {
            get { return mv.Font; }
        }

        public bool IsNull
        {
            get; set;
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("100")]
        [Localizable(true)]
        public int DateBoxWidth
        {
            get { return ViewState.GetValue("DateBoxWidth", 100); }
            set { ViewState.SetValue("DateBoxWidth", value); }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        public string OpenButtonImageUrl
        {
            get { return ViewState.GetValue<string>("OpenButtonImageUrl", null); }
            set { ViewState.SetValue("ImageUrl", value); }
        }

        [DefaultValue(FirstDayOfWeek.Saturday)]
        [Localizable(true)]
        [Bindable(true)]
        [Category("Appearance")]
        public FirstDayOfWeek FirstDayOfWeek
        {
            get { return ViewState.GetValue("FirstDayOfWeek", FirstDayOfWeek.Default); }
            set { ViewState.SetValue("FirstDayOfWeek", value); }
        }

        [PersistenceMode(PersistenceMode.Attribute)]
        [Bindable(true)]
        [Category("Appearance")]
        public bool PlaceAutomatically
        {
            get { return ViewState.GetValue("PlaceAutomatically", true); }
            set { ViewState.SetValue("AutoPosition", value); }
        }

        public string CallbackEventReference
        {
            get { return this.callbackEventReference; }
            set { this.callbackEventReference = value; }
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue("1")]
        [Bindable(false)]
        public int ZIndex
        {
            get { return ViewState.GetValue("ZIndex", 1); }
            set { ViewState.SetValue("ZIndex", value); }
        }

        [Localizable(true)]
        [DefaultValue("d")]
        [Bindable(false)]
        [Category("Behavior")]
        public string DateFormat
        {
            get { return ViewState.GetValue("DateFormat", "d"); }
            set { ViewState.SetValue("DateFormat", value); }
        }

        [Bindable(true), Category("Behavior"), Description("Specifies the latest date allowed by the date picker"), PersistenceMode(PersistenceMode.Attribute)]
        public DateTime MaxDate
        {
            get { return ViewState.GetValue("MaxDate", DateTime.MaxValue); }
            set { ViewState.SetValue("MaxDate", value); }
        }

        [Bindable(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [Category("Behavior")]
        public DateTime MinDate
        {
            get { return ViewState.GetValue("MinDate", PersianDate.MinValue); }
            set { ViewState.SetValue("MinDate", value); }
        }

        [Category("Data")]
        [Localizable(true)]
        [Bindable(true)]
        [DefaultValue(null)]
        public DateTime? SelectedDateTime
        {
            get { return GetDateValue(); }
            set
            {
                CheckDateValueRange(ref value);
                SetDateValue(value);
            }
        }

        [Category("Behavior")]
        [Localizable(true)]
        [Bindable(true)]
        [DefaultValue(typeof(ThemeTypes), "Office2000")]
        public ThemeTypes Theme
        {
            get { return ViewState.GetValue("Theme", ThemeTypes.Office2000); }
            set { ViewState.SetValue("Theme", value); }
        }

        [DefaultValue(1)]
        [Localizable(true)]
        [Bindable(true)]
        [Category("Appearance")]
        public NextPrevFormat NextPrevFormat
        {
            get { return ViewState.GetValue("NextPrevFormat", NextPrevFormat.CustomText); }
            set { ViewState.SetValue("NextPrevFormat", value); }
        }

        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue(null)]
        [Localizable(true)]
        public bool EnableDropShadow
        {
            get { return ViewState.GetValue("EnableDropShadow", true); }
            set { ViewState.SetValue("EnableDropShadow", value); }
        }
 
        [Bindable(true)]
        [Category("Appearance")]
        [PersistenceMode(PersistenceMode.Attribute)]
        public Unit CalendarOffsetX
        {
            get { return ViewState.GetValue("CalendarOffsetX", new Unit(0.0 - CalendarWidth.Value)); }
            set { ViewState.SetValue("CalendarOffsetX", value); }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [PersistenceMode(PersistenceMode.Attribute)]
        public Unit CalendarOffsetY
        {
            get { return ViewState.GetValue("CalendarOffsetY", new Unit(25)); }
            set { ViewState.SetValue("CalendarOffsetY", value); }
        }

        public Unit CalendarWidth
        {
            get { return ViewState.GetValue("CalendarWidth", 200); }
            set { ViewState.SetValue("CalendarWidth", value); }
        }

        private string PrevMonthText
        {
            get { return "P"; }
        }

        private string NextMonthText
        {
            get { return "N"; }
        }

        public bool ShowEmptyButton
        {
            get { return ViewState.GetValue("ShowEmptyButton", true); }
            set { ViewState.SetValue("ShowEmptyButton", value); }
        }

        public bool ShowTodayButton
        {
            get { return ViewState.GetValue("ShowTodayButton", true); }
            set { ViewState.SetValue("ShowTodayButton", value); }
        }

        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        public TableItemStyle MonthViewFooterStyle
        {
            get
            {
                if (this._monthViewFooterStyle == null)
                {
                    this._monthViewFooterStyle = new TableItemStyle();
                }
                return this._monthViewFooterStyle;
            }
        } 

        #endregion
    }
}
