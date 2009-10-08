using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarsiLibrary.Win.FAPopup
{
    public interface IPopupServiceControl
    {
        bool SetVisibleCore(IntPtr handle, bool newVisible);
        bool SetSimpleVisibleCore(IntPtr handle, IntPtr parentForm, bool newVisible);
        bool WndProc(ref Message m);
        bool IsDummy { get; }
        void UpdateTopMost(IntPtr handle);
        void PopupShowing(IPopupControl popup);
        void PopupClosed(IPopupControl popup);
        void EmulateFormFocus(IntPtr formHandle);
    }

    public interface IPopupControl
    {
        Control PopupWindow { get; }
        void ClosePopup();
        void ShowPopup();
        bool AllowMouseClick(Control control, Point mousePosition);
    }

}
