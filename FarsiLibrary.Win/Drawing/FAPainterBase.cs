using System.Drawing;

namespace FarsiLibrary.Win.Drawing
{
    /// <summary>
    /// Base painter for all painter classes. You should inherit this class to implement a new painter.
    /// </summary>
    public class FAPainterBase
    {
        #region Fields

        #endregion

        #region Props

        #endregion

        #region Base Drawing Parts

        public virtual void DrawString(Graphics g, Rectangle rectangle, string text, StringFormat fmt, Font font, bool enabled)
        {
            if(!string.IsNullOrEmpty(text))
            {
                if(enabled)
                {
                    using (var br = new SolidBrush(SystemColors.ControlText))
                    {
                        g.DrawString(text, font, br, rectangle, fmt);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(SystemColors.GrayText))
                    {
                        g.DrawString(text, font, br, rectangle, fmt);
                    }
                }
            }
        }

        public Rectangle DrawArrow(Graphics g, Rectangle rc, bool isLeft, bool isDisabled, int arrowSize)
        {
            int xLeft, xRight, yTop, yMidd, yBott;

            xLeft = rc.Left + 1;
            xRight = xLeft + arrowSize;
            yMidd = rc.Top + (rc.Height / 2);
            yTop = yMidd - arrowSize;
            yBott = yMidd + arrowSize;

            Point[] array;

            if (isLeft)
            {
                array = new[]
					{
						new Point(new Size(xLeft, yMidd)),
						new Point(new Size(xRight, yTop)),
						new Point(new Size(xRight, yBott))
					};
            }
            else
            {
                array = new[]
					{
						new Point(new Size(xLeft, yTop)),
						new Point(new Size(xLeft, yBott)),
						new Point(new Size(xRight, yMidd))
					};
            }

            g.DrawPolygon((isDisabled ? SystemPens.GrayText : SystemPens.WindowText), array);
            g.FillPolygon((isDisabled ? SystemBrushes.GrayText : SystemBrushes.WindowText), array);

            return new Rectangle(xLeft - 2, yTop - 2, arrowSize + 4, arrowSize * 2 + 4);
        }

        public Rectangle DrawVerticalArrow(Graphics g, Rectangle rc, bool isLeft, bool isDisabled, int arrowSize)
        {
            int middle = rc.Height / 2;
            Point[] pntArrow = new Point[3];
            SolidBrush br;

            if (isLeft)
            {
                pntArrow[0] = new Point(rc.Width - 11, middle - 1);
                pntArrow[1] = new Point(rc.Width - 9, middle + 2);
                pntArrow[2] = new Point(rc.Width - 6, middle - 1);
            }
            else
            {
                pntArrow[0] = new Point(rc.Left + 6, middle - 1);
                pntArrow[1] = new Point(rc.Left + 8, middle + 2);
                pntArrow[2] = new Point(rc.Left + 11, middle - 1);
            }

            if (isDisabled)
            {
                br = new SolidBrush(Color.DarkGray);
            }
            else
            {
                br = new SolidBrush(Color.Black);
            }

            g.FillPolygon(br, pntArrow);
            br.Dispose();

            return rc;
        }

        #endregion
    }
}
