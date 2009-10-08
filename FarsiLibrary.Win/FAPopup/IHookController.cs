using System;
using System.Windows.Forms;

namespace FarsiLibrary.Win.FAPopup
{
    internal interface IHookController
    {
        IntPtr OwnerHandle { get; }
        bool InternalPreFilterMessage(int Msg, Control wnd, IntPtr HWnd, IntPtr WParam, IntPtr LParam);
        bool InternalPostFilterMessage(int Msg, Control wnd, IntPtr HWnd, IntPtr WParam, IntPtr LParam);
    }

    internal interface IHookController2 : IHookController
    {
        void WndGetMessage(ref Message msg);
    }
}
