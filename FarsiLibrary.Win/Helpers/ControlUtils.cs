using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FarsiLibrary.Win.Helpers
{
    internal class ControlUtils
    {
        #region Native Methods
        
        [DllImport("USER32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        #endregion

        #region Props

		public static MouseButtons MouseButtons
        {
            get
            {
                MouseButtons ms = MouseButtons.None;
                if (GetAsyncKeyState(1) != 0) ms |= MouseButtons.Left;
                if (GetAsyncKeyState(2) != 0) ms |= MouseButtons.Right;
                if (GetAsyncKeyState(4) != 0) ms |= MouseButtons.Middle;
                return ms;
            }
        }

	    #endregion        
        
        #region Methods

		public static Point CalcLocation(Point bottomLocation, Point topLocation, Size popupSize)
        {
            Point location = bottomLocation;
            Rectangle rect = SystemInformation.WorkingArea;
            if (SystemInformation.MonitorCount > 1)
            {
                Screen scrBottom = Screen.FromPoint(bottomLocation), scrTop = Screen.FromPoint(topLocation);
                if (scrBottom.Equals(scrTop)) rect = scrTop.WorkingArea;
                else
                {
                    rect = scrBottom.WorkingArea;
                }
            }
            int bottom = bottomLocation.Y + popupSize.Height;
            int top = topLocation.Y - popupSize.Height;
            int maxBottomOutsize = bottom - rect.Bottom;
            int maxTopOutsize = rect.Top - top;
            if (maxBottomOutsize > 0 && maxBottomOutsize > maxTopOutsize)
            {
                location = topLocation;
                location.Y -= popupSize.Height;
            }
            if (location.X + popupSize.Width > rect.Right)
            {
                location.X = (rect.Right - popupSize.Width);
            }
            if (location.X < rect.Left) location.X = rect.Left;
            return location;
        }

	    #endregion
    }
}
