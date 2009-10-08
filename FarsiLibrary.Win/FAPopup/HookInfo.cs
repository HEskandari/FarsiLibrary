using System;
using System.Collections;
using System.Security.Permissions;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.Win.FAPopup
{
    [UIPermission(SecurityAction.Assert, Window = UIPermissionWindow.AllWindows, Clipboard = UIPermissionClipboard.OwnClipboard)]
    [ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.AllFlags)]
    [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlAppDomain | SecurityPermissionFlag.ControlThread)]
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
