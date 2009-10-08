using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FarsiLibrary.Win.Win32
{
    internal class User32
    {
        internal static int MakeLong(int lo, int hi)
        {
            return ((lo & 0xffff) | (((short)hi) << 0x10));
        }

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool SystemParametersInfo(int uiAction, int uiParam, ref NONCLIENTMETRICS ncMetrics, int fWinIni);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("User32.dll")]
        internal static extern bool MessageBeep(int beep);
       
        [DllImport("comctl32.dll")]
        internal static extern void InitCommonControls();

        [DllImport("comctl32.dll")]
        internal static extern bool InitCommonControls(INITCOMMONCONTROLSEX iccex);

        [DllImport("kernel32", CharSet = CharSet.Auto)]
        internal static extern int LoadLibraryEx(string lpLibFileName, int hFile, int dwFlags);

        [DllImport("kernel32", CharSet = CharSet.Auto)]
        internal static extern int FreeLibrary(int hLibModule);

        [DllImport("user32")]
        internal static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32")]
        internal static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("User32.dll")]
        internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetSysColor(int color);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PtInRect(ref RECT lpRect, int x, int y);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ScrollWindow(IntPtr hWnd, int xAmount, int yAmount, ref RECT rectScrollRegion, ref RECT rectClip);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ScrollWindow(IntPtr hWnd, int xAmount, int yAmount, IntPtr rectScrollRegion, IntPtr rectClip);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, IntPtr rectScrollRegion, IntPtr rectClip, IntPtr hrgnUpdate, IntPtr prcUpdate, int flags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool LockWindowUpdate(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetWindow(IntPtr hwnd, int wCmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SetWindowText(IntPtr hwnd, ref string lpString);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int ShowWindow(IntPtr hWnd, short cmdShow);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, [MarshalAs(UnmanagedType.LPTStr)]string lpszClass, [MarshalAs(UnmanagedType.LPTStr)]string lpszWindow);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool AnimateWindow(IntPtr hWnd, uint dwTime, FlagsAnimateWindow dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DragDetect(IntPtr hWnd, Point pt);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetSysColorBrush(int index);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool InvalidateRect(IntPtr hWnd, ref RECT rect, bool erase);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr LoadCursor(IntPtr hInstance, uint cursor);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetCursor(IntPtr hCursor);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetFocus();

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ReleaseCapture();

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool WaitMessage();

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool TranslateMessage(ref MSG msg);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DispatchMessage(ref MSG msg);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern uint SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, string lParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(HandleRef hWnd, int wMsg, int wParam, int lParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(HandleRef hWnd, int wMsg, int wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PeekMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, uint flags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int X, int Y, int Width, int Height, FlagsSetWindowPos flags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32")]
        internal extern static int GetClientRect(IntPtr hwnd, ref RECT rc);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref POINT pt);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern ushort GetKeyState(int virtKey);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DrawFocusRect(IntPtr hWnd, ref RECT rect);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool HideCaret(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SystemParametersInfo(SystemParametersInfoActions uAction, uint uParam, ref uint lpvParam, uint fuWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr WindowFromPoint(POINT point);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
    }
}
