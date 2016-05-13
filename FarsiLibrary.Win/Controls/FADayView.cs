using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FarsiLibrary.Utils;
using FarsiLibrary.Win.BaseClasses;
using FarsiLibrary.Win.Design;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.Win.Controls
{
    /// <summary>
    /// A view control which can display a date in DayView format.
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("SelectedDateTimeChanged")]
    [DefaultProperty("SelectedDateTime")]
    [Designer(typeof(FADayViewDesigner))]
    [DefaultBindingProperty("SelectedDateTime")]
    public class FADayView : BaseDateControl
    {
        #region Fields

        private bool showBorder = true;
        private Font defaultDayFont;
        private Font defaultHeaderFont;
        private Font defaultFooterFont;
        private Font dayFont;
        private Font headerFont;
        private Font footerFont;

        #endregion

        #region Events

        /// <summary>
        /// Fired when header of the control is painted.
        /// </summary>
        public event CustomDrawEventHandler DrawHeader;

        /// <summary>
        /// Fired when footer of the control is painted.
        /// </summary>
        public event CustomDrawEventHandler DrawFooter;

        /// <summary>
        /// Fired when body (middle part) of the control is painted.
        /// </summary>
        public event CustomDrawEventHandler DrawBody;

        /// <summary>
        /// Fired when control is being drawn.
        /// </summary>
        public event CustomDrawEventHandler Draw;

        #endregion

        #region Ctor

        public FADayView()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            dayFont = defaultDayFont = new Font("Tahoma", 30, FontStyle.Regular);
            headerFont = defaultHeaderFont = new Font("Tahoma", 14, FontStyle.Regular);
            footerFont = defaultFooterFont = new Font("Tahoma", 12, FontStyle.Regular);

            base.Size = new Size(ControlWidth, ControlHeight);
            base.Font = new Font("Tahoma", 8.25F);
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

        #region Painting

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (!CanUpdate)
                return;

            try
            {
                BeginUpdate();

                var rc = new Rectangle(0, 0, Width, Height);
                
                OnDrawBody(new PaintEventArgs(pe.Graphics, rc));
                OnDrawBorder(new PaintEventArgs(pe.Graphics, rc));
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
            }
            finally
            {
                EndUpdate();
            }
        }

        protected virtual void OnDrawBody(PaintEventArgs args)
        {
            var drawArg = new CustomDrawEventArgs(args.ClipRectangle, args.Graphics, false);
            if(Draw != null)
                Draw(this, drawArg);

            if(drawArg.Handled)
                return;

            //split the space into three equal parts
            var rect = args.ClipRectangle;
            var height = rect.Height/4;

            var top = new Rectangle(0, 0, rect.Width, height);
            var middle = new Rectangle(0, top.Bottom + 1, rect.Width, height * 2);
            var bottom = new Rectangle(0, middle.Bottom + 1, rect.Width, height);

            OnDrawDayName(new PaintEventArgs(args.Graphics, top));
            OnDrawDayOfMonth(new PaintEventArgs(args.Graphics, middle));
            OnDrawMonthAndYear(new PaintEventArgs(args.Graphics, bottom));
        }

        protected virtual void OnDrawDayName(PaintEventArgs args)
        {
            var drawArg = new CustomDrawEventArgs(args.ClipRectangle, args.Graphics, false);
            if(DrawHeader != null)
                DrawHeader(this, drawArg);

            if(drawArg.Handled)
                return;

            Painter.DrawString(args.Graphics, args.ClipRectangle, GetHeaderValue(), OneLineNoTrimming, HeaderFont, Enabled);
        }

        protected virtual void OnDrawDayOfMonth(PaintEventArgs args)
        {
            var drawArg = new CustomDrawEventArgs(args.ClipRectangle, args.Graphics, false);
            if(DrawBody != null)
                DrawBody(this, drawArg);

            if(drawArg.Handled)
                return;

            Painter.DrawString(args.Graphics, args.ClipRectangle, GetBodyValue(), OneLineNoTrimming, DayFont, Enabled);
        }

        protected virtual void OnDrawMonthAndYear(PaintEventArgs args)
        {
            var drawArg = new CustomDrawEventArgs(args.ClipRectangle, args.Graphics, false);
            if(DrawFooter != null)
                DrawFooter(this, drawArg);

            if(drawArg.Handled)
                return;

            Painter.DrawString(args.Graphics, args.ClipRectangle, GetFooterValue(), OneLineNoTrimming, FooterFont, Enabled);
        }

        protected virtual void OnDrawBorder(PaintEventArgs args)
        {
            if (!ShowBorder) 
                return;

            var border = new Rectangle(args.ClipRectangle.X, args.ClipRectangle.Y, args.ClipRectangle.Width - 1, args.ClipRectangle.Height - 1);
            Painter.DrawBorder(args.Graphics, border, Enabled);
        }

        #endregion
        
        #region Getting Painted Values

        protected virtual string GetHeaderValue()
        {
            var dt = ViewDateTime;
            var dayName = base.GetDayName(dt.DayOfWeek);

            return dayName;
        }

        protected virtual string GetBodyValue()
        {
            var dt = ViewDateTime;
            var day = DefaultCalendar.GetDayOfMonth(dt);
            var dayValue = toFarsi.Convert(day.ToString(), DefaultCulture);

            return dayValue;
        }

        protected virtual string GetFooterValue()
        {
            var dt = ViewDateTime;
            var year = toFarsi.Convert(ViewYear.ToString(), DefaultCulture);
            var month = base.GetMonthName(ViewMonth);
            var monthAndYear = string.Format("{0} - {1}", month, year);

            return monthAndYear;
        }

        #endregion
        
        #region Props

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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Calendar DefaultCalendar
        {
            get { return base.DefaultCalendar; }
            set
            {
                base.DefaultCalendar = value;
                UpdateViewDayMonthYearValues();
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
                UpdateSelectedDayMonthYearValues();
                UpdateViewDayMonthYearValues();
                Invalidate();
            }
        }

        public Font DayFont
        {
            get { return dayFont; }
            set
            {
                dayFont = value;
                Invalidate();
            }
        }

        public Font HeaderFont
        {
            get { return headerFont; }
            set
            {
                headerFont = value;
                Invalidate();
            }
        }

        public Font FooterFont
        {
            get { return footerFont; }
            set
            {
                footerFont = value;
                Invalidate();
            }
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                dayFont.Dispose();
                headerFont.Dispose();
                footerFont.Dispose();
                defaultDayFont.Dispose();
                defaultHeaderFont.Dispose();
                defaultFooterFont.Dispose();

                dayFont = null;
                headerFont = null;
                footerFont = null;
                defaultDayFont = null;
                defaultHeaderFont = null;
                defaultFooterFont = null;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Designer Methods

        /// <summary>
        /// Determines if DayFont should be serialized.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeDayFont()
        {
            return !dayFont.Equals(defaultDayFont);
        }

        /// <summary>
        /// Determines if HeaderFont should be serialized.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeHeaderFont()
        {
            return !headerFont.Equals(defaultHeaderFont);
        }


        /// <summary>
        /// Determines if FooterFont should be serialized.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeFooterFont()
        {
            return !footerFont.Equals(defaultFooterFont);
        }

        /// <summary>
        /// Determines if Font property should be serialized
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeFont()
        {
            return false;
        }

        #endregion
    }
}