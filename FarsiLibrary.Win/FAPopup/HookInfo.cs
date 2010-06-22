using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.Win.FAPopup
{
    [UIPermission(SecurityAction.Assert, Unrestricted = true, Window = UIPermissionWindow.AllWindows, Clipboard = UIPermissionClipboard.OwnClipboard)]
    [ReflectionPermission(SecurityAction.Assert, Unrestricted = true, Flags = ReflectionPermissionFlag.MemberAccess)]
    [SecurityPermission(SecurityAction.Assert, Unrestricted = true, Flags = SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlAppDomain | SecurityPermissionFlag.ControlThread)]
    [SecurityCritical]
    internal class HookInfo
    {
        #region Fields

        ArrayList hookControllers;
        int threadId;
        public bool inHook, inMouseHook;
        public IntPtr wndHookHandle, getMessageHookHandle, mouseHookHandle;
        public Hook wndHookProc, mouseHookProc, getMessageHookProc;
        //HookManager manager;

        #endregion

        #region Ctor

        public HookInfo()//(HookManager manager)
        {
            //this.manager = manager;
            inMouseHook = false;
            inHook = false;
            wndHookHandle = getMessageHookHandle = mouseHookHandle = IntPtr.Zero;
            wndHookProc = mouseHookProc = getMessageHookProc = null;
            threadId = HookManager.GetCurrentThreadId();
            hookControllers = new ArrayList();
        }

        #endregion

        #region Props

        public ArrayList HookControllers 
        { 
            get { return hookControllers; } 
        }

        public int ThreadId 
        {
            get { return threadId; } 
        }

        #endregion
    }
}
