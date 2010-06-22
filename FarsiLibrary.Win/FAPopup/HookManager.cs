using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using FarsiLibrary.Win.Events;

namespace FarsiLibrary.Win.FAPopup
{
    [UIPermission(SecurityAction.Assert, Unrestricted = true, Window = UIPermissionWindow.AllWindows, Clipboard = UIPermissionClipboard.OwnClipboard)]
    [ReflectionPermission(SecurityAction.Assert, Unrestricted = true, Flags = ReflectionPermissionFlag.MemberAccess)]
    [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlAppDomain | SecurityPermissionFlag.ControlThread | SecurityPermissionFlag.UnmanagedCode)]
    [SecurityCritical]
    internal class HookManager
    {
        #region Fields

        public ArrayList HookControllers;
        private static HookManager defaultManager = new HookManager();
        private Hashtable hookHash;

        #endregion

        #region Ctor & Dtor
        
        public HookManager()
        {
            Application.ApplicationExit += OnApplicationExit;
            Application.ThreadExit += OnThreadExit;

            hookHash = new Hashtable();
            HookControllers = new ArrayList();
        }
        
        [SecuritySafeCritical]
        ~HookManager()
        {
            RemoveHooks();
            Application.ApplicationExit -= OnApplicationExit;
            Application.ThreadExit -= OnThreadExit;
        }

        #endregion

        #region Props

        public static HookManager DefaultManager
        {
            get { return defaultManager; }
        }

        public Hashtable HookHash 
        {
            get { return hookHash; }
        }

        public static int CurrentThread 
        {
            get { return GetCurrentThreadId(); } 
        }

        #endregion

        #region Methods
        
        public void CheckController(IHookController ctrl)
        {
            HookInfo hInfo = GetInfoByThread();
            if (hInfo.HookControllers.Contains(ctrl)) return;
            AddController(ctrl);
        }

        public void AddController(IHookController ctrl)
        {
            HookInfo hInfo = GetInfoByThread();
            hInfo.HookControllers.Add(ctrl);
            if (hInfo.HookControllers.Count == 1) InstallHook(hInfo);
        }

        public void RemoveController(IHookController ctrl)
        {
            HookInfo hInfo = GetInfoByThread();
            hInfo.HookControllers.Remove(ctrl);
            if (hInfo.HookControllers.Count == 0) RemoveHook(hInfo, false);
        }
        
        protected virtual HookInfo GetInfoByThread()
        {
            int thId = CurrentThread;
            HookInfo hInfo = HookHash[thId] as HookInfo;
            if (hInfo == null)
            {
                hInfo = new HookInfo();//(this);
                HookHash[thId] = hInfo;
            }
            return hInfo;

        }

        internal void InstallHook(HookInfo hInfo)
        {
            if (hInfo.wndHookHandle != IntPtr.Zero) return;
            hInfo.mouseHookProc = new Hook(MouseHook);
            hInfo.wndHookProc = new Hook(WndHook);
            hInfo.getMessageHookProc = new Hook(GetMessageHook);
            hInfo.wndHookHandle = SetWindowsHookEx(4, hInfo.wndHookProc, 0, hInfo.ThreadId);
            hInfo.mouseHookHandle = SetWindowsHookEx(7, hInfo.mouseHookProc, 0, hInfo.ThreadId);
            hInfo.getMessageHookHandle = IntPtr.Zero;
        }

        internal void RemoveHook(HookInfo hInfo, bool disposing)
        {
            if (hInfo.wndHookHandle != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hInfo.wndHookHandle);
                hInfo.wndHookHandle = IntPtr.Zero;
                hInfo.wndHookProc = null;

                hInfo.getMessageHookHandle = IntPtr.Zero;
                hInfo.getMessageHookProc = null;
                UnhookWindowsHookEx(hInfo.mouseHookHandle);
                hInfo.mouseHookHandle = IntPtr.Zero;
                hInfo.mouseHookProc = null;
                HookHash.Remove(hInfo.ThreadId);
            }
        }

        private void OnThreadExit(object sender, EventArgs e)
        {
            RemoveHook(GetInfoByThread(), true);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Application.ThreadExit -= new EventHandler(OnThreadExit);
            Application.ApplicationExit -= new EventHandler(OnApplicationExit);
            RemoveHooks();
        }

        protected virtual void RemoveHooks()
        {
            ArrayList list = new ArrayList();
            foreach (HookInfo h in HookHash.Values)
            {
                list.Add(h);
            }
            HookHash.Clear();
            for (int n = 0; n < list.Count; n++)
            {
                RemoveHook(list[n] as HookInfo, true);
            }

        }

        protected int WndHook(int ncode, IntPtr wParam, IntPtr lParam)
        {
            HookInfo hInfo = GetInfoByThread();
            int res;
            CWPSTRUCT hookStr = (CWPSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPSTRUCT));
            Control ctrl = null;

            try
            {
                if (!hInfo.inHook && lParam != IntPtr.Zero)
                {
                    try
                    {
                        ctrl = Control.FromHandle(hookStr.hwnd);
                        hInfo.inHook = true;
                        res = InternalPreFilterMessage(hInfo, hookStr.message, ctrl, hookStr.hwnd, hookStr.wParam, hookStr.lParam) ? 1 : 0;
                    }
                    finally
                    {
                        hInfo.inHook = false;
                    }
                }
                else return CallNextHookEx(hInfo.wndHookHandle, ncode, wParam, lParam);
                res = CallNextHookEx(hInfo.wndHookHandle, ncode, wParam, lParam);
            }
            finally
            {
                InternalPostFilterMessage(hInfo, hookStr.message, ctrl, hookStr.hwnd, hookStr.wParam, hookStr.lParam);
            }
            return res;
        }

        protected int GetMessageHook(int ncode, IntPtr wParam, IntPtr lParam)
        {
            HookInfo hInfo = GetInfoByThread();
            API_MSG hookStr = (API_MSG)Marshal.PtrToStructure(lParam, typeof(API_MSG));
            if (!hInfo.inHook && lParam != IntPtr.Zero)
            {
                try
                {
                    hInfo.inHook = true;
                    InternalGetMessage(ref hookStr);

                }
                finally
                {
                    hInfo.inHook = false;
                }
            }
            return CallNextHookEx(hInfo.wndHookHandle, ncode, wParam, lParam);
        }

        protected int MouseHook(int ncode, IntPtr wParam, IntPtr lParam)
        {
            HookInfo hInfo = GetInfoByThread();
            int res;
            bool allowFutureProcess = true;

            if (ncode == 0)
            {
                MOUSEHOOKSTRUCT hookStr = (MOUSEHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MOUSEHOOKSTRUCT));
                if (!hInfo.inMouseHook && lParam != IntPtr.Zero)
                {
                    try
                    {
                        Control ctrl = Control.FromHandle(hookStr.hwnd);
                        hInfo.inMouseHook = true;
                        allowFutureProcess = !InternalPreFilterMessage(hInfo, wParam.ToInt32(), ctrl, hookStr.hwnd, IntPtr.Zero, new IntPtr((hookStr.Pt.X << 16) | hookStr.Pt.Y));
                    }
                    finally
                    {
                        hInfo.inMouseHook = false;
                    }
                }
                else return CallNextHookEx(hInfo.mouseHookHandle, ncode, wParam, lParam);
            }
            res = CallNextHookEx(hInfo.mouseHookHandle, ncode, wParam, lParam);
            if (!allowFutureProcess) res = -1;
            return res;
        }

        internal bool InternalPreFilterMessage(HookInfo hInfo, int Msg, Control wnd, IntPtr HWnd, IntPtr WParam, IntPtr LParam)
        {
            bool result = false;
            for (int n = 0; n < hInfo.HookControllers.Count; n++)
            {
                IHookController ctrl = hInfo.HookControllers[n] as IHookController;
                result |= ctrl.InternalPreFilterMessage(Msg, wnd, HWnd, WParam, LParam);
            }
            return result;
        }
        
        internal bool InternalPostFilterMessage(HookInfo hInfo, int Msg, Control wnd, IntPtr HWnd, IntPtr WParam, IntPtr LParam)
        {
            bool result = false;
            for (int n = hInfo.HookControllers.Count - 1; n >= 0; n--)
            {
                IHookController ctrl = hInfo.HookControllers[n] as IHookController;
                result |= ctrl.InternalPostFilterMessage(Msg, wnd, HWnd, WParam, LParam);
                if (Msg == 0x2)
                {
                    if (ctrl.OwnerHandle == HWnd)
                    {
                        RemoveController(ctrl);
                    }
                }

            }
            return result;
        }

        internal void InternalGetMessage(ref API_MSG msg)
        {
            HookInfo hInfo = GetInfoByThread();
            for (int n = 0; n < hInfo.HookControllers.Count; n++)
            {
                IHookController2 ctrl = hInfo.HookControllers[n] as IHookController2;
                if (ctrl != null)
                {
                    Message m = msg.ToMessage();
                    ctrl.WndGetMessage(ref m);
                    msg.FromMessage(ref m);
                }
            }
        }

        #endregion

        #region Native Methods

        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetCurrentThreadId();

        [StructLayout(LayoutKind.Sequential)]
        internal struct CWPSTRUCT
        {
            public IntPtr lParam;
            public IntPtr wParam;
            public int message;
            public IntPtr hwnd;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public int message;
            public IntPtr hwnd;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEHOOKSTRUCT
        {
            public POINT Pt;
            public IntPtr hwnd;
            public uint wHitTestCode;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct API_MSG
        {
            public IntPtr Hwnd;
            public int Msg;
            public IntPtr WParam;
            public IntPtr LParam;
            public int Time;
            public POINT Pt;
            
            public Message ToMessage()
            {
                Message res = new Message();
                res.HWnd = Hwnd;
                res.Msg = Msg;
                res.WParam = WParam;
                res.LParam = LParam;
                return res;
            }
        
            public void FromMessage(ref Message msg)
            {
                Hwnd = msg.HWnd;
                Msg = msg.Msg;
                WParam = msg.WParam;
                LParam = msg.LParam;
            }
        }

        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        protected static extern IntPtr SetWindowsHookEx(int idHook, Hook lpfn, int hMod, int dwThreadId);
        
        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        protected static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        protected static extern bool UnhookWindowsHookEx(IntPtr hhk);

        #endregion

    }
}
