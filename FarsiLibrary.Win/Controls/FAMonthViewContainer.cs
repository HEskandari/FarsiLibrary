using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FarsiLibrary.Win.FAPopup;
using FarsiLibrary.Win.Helpers;

namespace FarsiLibrary.Win.Controls
{
    /// <summary>
    /// FAMonthViewContainer is a control which hosts a <see cref="FAMonthView"/> control, and displays in <see cref="FADatePicker"/> control when user wants to select a date.
    /// </summary>
    [ToolboxItem(false)]
    public class FAMonthViewContainer : FAPopupContainer, IPopupControl
    {
        #region Fields

        private FAMonthView mv;
        private Control owner;
        private FAHookPopup hook;
        private IPopupServiceControl serviceObject;
        private static IPopupServiceControl popupServiceControl = new FAHookPopupController();

        #endregion

        #region Props

        /// <summary>
        /// Owner control of this Popup control.
        /// </summary>
        [Browsable(false)]
        public Control OwnerControl
        {
            get { return owner; }
            set { owner = value; }
        }

        /// <summary>
        /// Service object which handles popup behaviors.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IPopupServiceControl ServiceObject
        {
            get { return serviceObject; }
            set
            {
                if (value == null) return;
                serviceObject = value;
            }
        }

        /// <summary>
        /// Actual control that is being displayed.
        /// </summary>
        [Browsable(false)]
        public FAMonthView MonthViewControl
        {
            get { return mv; }
        }

        /// <summary>
        /// Editor which shows the popup control.
        /// </summary>
        public override Control OwnerEdit
        {
            get { return owner; }
        }


        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of FAMonthViewContainer class.
        /// </summary>
        public FAMonthViewContainer() : this(null)
        {
        }

        /// <summary>
        /// Creates a new instance of FAMonthViewContainer which hosts a <see cref="FAMonthView"/> control in popup mode.
        /// </summary>
        /// <param name="ownerControl"></param>
        public FAMonthViewContainer(Control ownerControl)
        {
            hook = new FAHookPopup(this);
            mv = new FAMonthView(true);
            mv.Dock = DockStyle.Fill;
            Size = new Size(mv.Size.Width - 2, mv.Size.Height - 2);
            Controls.Add(mv);
            mv.IsPopupMode = true;
            serviceObject = popupServiceControl;
            base.RealBounds = new Rectangle(mv.Bounds.X, mv.Bounds.Y, mv.Bounds.Width, mv.Bounds.Height);
            Parent = owner;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            ControlBox = false;
            owner = ownerControl;
            SetStyle(ControlStyles.Opaque, true);
            base.ShadowSize = 3;
            base.RightToLeft = ownerControl.RightToLeft;
        }

        /// <summary>
        /// Disposes the control.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
                hook.Dispose();

            base.Dispose(disposing);
        }
        
        #endregion

        #region Methods

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            UpdateShadows();
        }

        private void ShowCalendar()
        {
            Rectangle r = OwnerEdit.RectangleToScreen(OwnerEdit.ClientRectangle);
            Point showLocation;
            Point topLocation;

            if (OwnerEdit.RightToLeft == RightToLeft.Yes)
            {
                topLocation = new Point(r.Left, r.Bottom);
            }
            else
            {
                topLocation = new Point(r.Right - Width, r.Bottom);
            }

            Point bottomLocation = new Point(topLocation.X, topLocation.Y);
            showLocation = ControlUtils.CalcLocation(bottomLocation, topLocation, Size);

            ClientSize = Size;
            Location = showLocation;

            CalendarChanged(true);
            Visible = true;
        }

        public void ShowCalendar(Point position)
        {
            Point topLocation = position;
            Point bottomLocation = new Point(topLocation.X, topLocation.Y);
            Point newLoc = ControlUtils.CalcLocation(bottomLocation, topLocation, Size);

            ClientSize = Size;
            Location = newLoc;

            CalendarChanged(true);
            Visible = true;
        }

        private void HideCalendar()
        {
            Visible = false;

            Form form = OwnerEdit.FindForm();
            if (form != null && ActiveForm == form)
                form.Activate();
        }

        protected virtual void CalendarChanged(bool makeVisible)
        {
            if (!Visible && !makeVisible)
                return;

            Invalidate();
            if (makeVisible)
                Visible = true;
        }

        #endregion

        #region IPopupControl Members

        /// <summary>
        /// Closes the Popup window.
        /// </summary>
        public void ClosePopup()
        {
            HideCalendar();
        }

        /// <summary>
        /// Shows the Popup window.
        /// </summary>
        public void ShowPopup()
        {
            ShowCalendar();
        }

        /// <summary>
        /// Popup control that will be shown.
        /// </summary>
        public Control PopupWindow
        {
            get { return mv; }
        }

        /// <summary>
        /// Is mouse clicks on the control allowed?
        /// </summary>
        /// <param name="control"></param>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public bool AllowMouseClick(Control control, Point mousePosition)
        {
            return false;
        }

        internal FAHookPopup PopupHook
        {
            get { return hook; }
        }

        #endregion
    }
}
