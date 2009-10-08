using System;
using System.Runtime.InteropServices;

namespace FarsiLibrary.Win.Win32
{
    internal class Gdi32
    {
        /// <summary>
        /// hide ctor
        /// </summary>
        private Gdi32() { }

        [DllImport("GDI32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateBrushIndirect(ref LOGBRUSH brush);

        [DllImport("GDI32.dll")]
        internal static extern int SaveDC(IntPtr hdc);

        [DllImport("GDI32.dll")]
        internal static extern int RestoreDC(IntPtr hdc, int savedDC);
        
        [DllImport("GDI32.dll")]
        internal static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
            
        [DllImport("GDI32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);
        
        [DllImport("GDI32.dll")]
        internal static extern int ExcludeClipRect(IntPtr hdc, int left, int top, int right, int bottom);
        
        [DllImport("GDI32.dll")]
        internal static extern int GetClipRgn(IntPtr hdc, IntPtr hrgn);
        
        [DllImport("GDI32.dll")]
        internal static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);
        
        [DllImport("GDI32.dll")]
        internal static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreatePen(int nStyle, int nWidth, int crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetStockObject(int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool LineTo(IntPtr hdc, int x, int y);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool MoveToEx(IntPtr hdc, int x, int y, IntPtr pt);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PatBlt(IntPtr hdc, int left, int top, int width, int height, int rop);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetBkColor(IntPtr hDC, int clr);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetROP2(IntPtr hDC, int nDrawMode);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetClipBox(IntPtr hDC, ref RECT rectBox);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetClipRgn(IntPtr hDC, ref IntPtr hRgn);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetDeviceCaps(IntPtr hDC, int nIndex); 

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool FillRgn(IntPtr hDC, IntPtr hrgn, IntPtr hBrush);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, short[] lpvBits);
    }
}
