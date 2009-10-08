using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FarsiLibrary.Win.FAPopup
{
    #region FAHookPopupController
    
    internal class FAHookPopupController : FAWinPopupController
    {
        #region Fields

        private ArrayList Popups;

        #endregion

        #region Ctor

        public FAHookPopupController()
        {
            Popups = new ArrayList();
        }

        #endregion

        #region Methods

        protected virtual FAHookPopup FindPopup(IPopupControl popup)
        {
            for (int n = 0; n < Popups.Count; n++)
            {
                FAHookPopup ppp = Popups[n] as FAHookPopup;
                if (ppp.Popup == popup) return ppp;
            }

            return null;
        }

        public override void PopupClosed(IPopupControl popup)
        {
            FAHookPopup ppp = FindPopup(popup);
            if (ppp != null)
            {
                Popups.Remove(ppp);
                ppp.Dispose();
            }
        }

        public override void PopupShowing(IPopupControl popup)
        {
            Popups.Add(new FAHookPopup(popup));
        }

        #endregion
    }

    #endregion

    #region FAWinPopupController

    internal class FAWinPopupController : IPopupServiceControl
    {
        #region Constants

        protected const int MA_NOACTIVATE = 3;
        protected const int WM_MOUSEACTIVATE = 0x0021,
                            WM_LBUTTONDOWN = 0x0201;

        private const int SWP_NOSIZE = 0x0001,
                          SWP_NOMOVE = 0x0002,
                          SWP_NOACTIVATE = 0x0010,
                          SWP_SHOWWINDOW = 0x0040,
                          HWND_TOPMOST = -1;

        #endregion

        #region IPopupServiceControl

        public void UpdateTopMost(IntPtr handle)
        {
            SetWindowPos(handle, (IntPtr)HWND_TOPMOST, 0, 0, 0, 0, SWP_NOACTIVATE | SWP_SHOWWINDOW | SWP_NOSIZE | SWP_NOMOVE);
        }

        public bool SetVisibleCore(IntPtr handle, bool newVisible)
        {
            if (!newVisible)
            {
                return false;
            }
            else
            {
                UpdateTopMost(handle);
                ShowWindow(handle, 8);
                return true;
            }

        }

        public bool SetSimpleVisibleCore(IntPtr handle, IntPtr parentForm, bool newVisible)
        {
            if (!newVisible)
            {
                return false;
            }
            else
            {
                SetWindowPos(handle, parentForm, 0, 0, 100, 100, SWP_NOACTIVATE | SWP_SHOWWINDOW | SWP_NOMOVE | SWP_NOSIZE);
                ShowWindow(handle, 8);
                return true;
            }
        }

        public void EmulateFormFocus(IntPtr formHandle)
        {
            SendMessage(formHandle, 0x86, 1, 0);
        }

        public bool WndProc(ref Message m)
        {
            Control control = Control.FromHandle(m.HWnd);
            switch (m.Msg)
            {
                case WM_MOUSEACTIVATE:
                    m.Result = (IntPtr)MA_NOACTIVATE;
                    return true;
                case WM_LBUTTONDOWN:
                    if (control is ListBox) return true;
                    break;
            }
            return false;
        }

        public bool IsDummy
        {
            get { return false; }
        }

        #endregion

        #region Methods

        public virtual void PopupClosed(IPopupControl popup)
        {
        }

        public virtual void PopupShowing(IPopupControl popup)
        {
        }

        #endregion

        #region Native Methods

        [DllImport("USER32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("USER32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

        #endregion
    }

    #endregion
}
