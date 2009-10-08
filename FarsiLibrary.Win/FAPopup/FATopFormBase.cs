using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FarsiLibrary.Win.Win32;

namespace FarsiLibrary.Win.FAPopup
{
    /// <summary>
    /// Base class for top forms (i.e. Shadows, PopupForms, and Container forms).
    /// </summary>
    [ToolboxItem(false)]
    public class FATopFormBase : Form
    {
        #region Fields

        #region Constants

        protected const int MA_NOACTIVATE = 3;
        protected const int WM_MOUSEACTIVATE = 0x0021,
                            WM_LBUTTONDOWN = 0x0201,
                            WM_LBUTTONDBLCLK = 0x0203,
                            WM_NCACTIVATE = 0x0086,
                            WM_NCCREATE = 0x0081,
                            WM_CAPTURECHANGED = 0x215, 
                            WM_WINDOWPOSCHANGED = 0x0047, 
                            WM_WINDOWPOSCHANGING = 0x0046;

        private const int SWP_NOSIZE = 0x0001,
                          SWP_NOMOVE = 0x0002,
                          SWP_NOACTIVATE = 0x0010,
                          SWP_SHOWWINDOW = 0x0040,
                          HWND_TOP = 0,
                          HWND_TOPMOST = -1;

        #endregion

        #region Events

        public event EventHandler PopupMove;
        public event EventHandler PopupResize;

        #endregion

        private bool drawShadow;
        private ArrayList shadows;
        private int lockUpdate;
        private Control owner = null;
        private int shadowSize = FAShadow.DefaultShadowSize;

        #endregion

        #region Ctor

   		public FATopFormBase() 
        {
            shadows = new ArrayList();
            RightToLeft = RightToLeft.Yes;
            lockUpdate = 0;
            drawShadow = true;
			MinimumSize = new Size(0, 0);
			Parent = null;
			TopLevel = true;
			ControlBox = false;
			StartPosition = FormStartPosition.Manual;
			FormBorderStyle = FormBorderStyle.None;
			ShowInTaskbar = false;
			Visible = false;
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}

        #endregion

        #region Props

        [Browsable(false)]
        protected virtual ArrayList Shadows 
        {
            get { return shadows; } 
        }

        public virtual int ShadowSize
        {
            get { return shadowSize; }
            set { shadowSize = value; }
        }

        public virtual Control OwnerEdit
        {
            get { return owner; }
        }

        public virtual bool DrawShadow
        {
            get { return drawShadow; }
            set { drawShadow = value; }
        }

        public virtual Rectangle RealBounds
        {
            get 
            { 
                return Rectangle.Empty; 
            }
            set
            {
            }
        }

        protected virtual bool IsTopMost 
        {
            get { return true; } 
        }

        protected virtual IntPtr InsertAfterWindow
        {
            get 
            { 
                return (IntPtr)(IsTopMost ? HWND_TOPMOST : HWND_TOP); 
            }
        }

        protected virtual bool AllowMouseActivate 
        { 
            get { return false; }
        }

        #endregion

        #region Methods

        protected void UpdateShadows()
        {
            if (OwnerEdit == null)
                return;

            bool visible = Visible && Bounds.X != -10000;
            if (visible)
            {
                Rectangle r = OwnerEdit.RectangleToScreen(OwnerEdit.ClientRectangle);
                if (!OwnerEdit.Bounds.IsEmpty) r = OwnerEdit.RectangleToScreen(new Rectangle(Point.Empty, OwnerEdit.Bounds.Size));
                FAShadow.CreateShadows(Shadows, FAShadow.DefaultShadowSize, true, this, r);
            }
            else
                FAShadow.HideShadows(Shadows);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (PopupResize != null)
                PopupResize(this, e);
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
         
            if (PopupMove != null)
                PopupMove(this, e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            LayoutChanged();
            UpdateShadows();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEACTIVATE:
                    if (!AllowMouseActivate)
                    {
                        m.Result = (IntPtr)MA_NOACTIVATE;
                        return;
                    }
                    break;
                case WM_LBUTTONDBLCLK:
                    OnDoubleClick(new EventArgs());
                    return;
                case WM_LBUTTONDOWN:
                    break;
                case WM_CAPTURECHANGED:
                    if (m.LParam == Handle)
                        OnGotCapture();
                    else
                        OnLostCapture();
                    break;
            }

            base.WndProc(ref m);
        }

        public virtual void BeginUpdate()
        {
            ++lockUpdate;
        }

        public virtual void EndUpdate()
        {
            if (--lockUpdate == 0)
            {
                LayoutChanged();
            }
        }
        public virtual void CancelUpdate()
        {
            --lockUpdate;
        }

        protected virtual void UpdateZOrder(IntPtr after)
        {
            if (after == IntPtr.Zero)
                after = InsertAfterWindow;

            uint flags = SWP_NOACTIVATE;
            Rectangle realBounds = RealBounds;
            flags |= SWP_NOSIZE | SWP_NOMOVE;
            User32.SetWindowPos(Handle, after, realBounds.X, realBounds.Y, realBounds.Width, realBounds.Height, flags);
        }

        protected override void SetVisibleCore(bool newVisible)
        {
            if (!newVisible)
            {
                base.SetVisibleCore(newVisible);
            }
            else
            {
                uint flags = SWP_NOACTIVATE | SWP_SHOWWINDOW;
                Rectangle realBounds = RealBounds;
                flags |= SWP_NOSIZE | SWP_NOMOVE;
                User32.SetWindowPos(Handle, InsertAfterWindow, realBounds.X, realBounds.Y, realBounds.Width, realBounds.Height, flags);
                User32.ShowWindow(Handle, 8);
            }
        }

        protected virtual void LayoutChanged()
        {
            if (lockUpdate != 0) return;
            if (!IsHandleCreated) return;
            Invalidate();
        }

        #endregion

        #region Event Methods

        protected virtual void OnLostCapture() 
        {
        }

        protected virtual void OnGotCapture() 
        {
        }

        #endregion
    }
}
