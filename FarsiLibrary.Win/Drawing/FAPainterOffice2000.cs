using System.Drawing;
using System.Windows.Forms;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Drawing
{
    /// <summary>
    /// Painter class used to paint drawing objects in Office 2000 theme.
    /// </summary>
    public class FAPainterOffice2000 : FAPainterBase, IFAPainter
    {
        public void DrawButtonFocusRect(Graphics g, Rectangle r, ItemState state)
        {
            Rectangle focus = new Rectangle(r.X, r.Y, r.Width, r.Height);
            focus.Inflate(-2, -2);
            ControlPaint.DrawFocusRectangle(g, focus);
        }

        public void DrawSelectionBorder(Graphics g, Rectangle r)
        {
            using (Pen p = new Pen(SystemColors.Highlight))
                g.DrawRectangle(p, r);
        }

        public void DrawVerticalSeparator(Graphics g, Point from, Point to)
        {
            Pen pen1 = new Pen(SystemColors.ControlDark);
            g.DrawLine(pen1, from, to);

            Pen pen2 = new Pen(SystemColors.ControlLightLight);
            from.X += 1; 
            to.X += 1; 
            g.DrawLine(pen2, from, to);
        }

        public void DrawFilledBackground(Graphics g, Rectangle rectangle, bool isGradient, float angle)
        {
            using (Brush brush = new SolidBrush(SystemColors.Window))
            {
                g.FillRectangle(brush, rectangle);
            }
        }

        public void DrawWhiteBackground(Graphics g, Rectangle r, bool isGradient, float angle)
        {
            using (Brush brush = new SolidBrush(SystemColors.ControlLightLight))
            {
                g.FillRectangle(brush, r);
            }
        }

        public void DrawBorder(Graphics g, Rectangle rectangle, bool enabled)
        {
            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
                Color c = enabled ? SystemColors.ControlDarkDark : SystemColors.ControlDark;

                using (Pen p = new Pen(c))
                {
                    g.DrawRectangle(p, rectangle);
                }
            }
        }

        public void DrawButton(Graphics g, Rectangle rectangle, string text, Font font, StringFormat fmt, ItemState state, bool hasBorder, bool enabled)
        {
            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
                if (!enabled)
                {
                    ControlPaint.DrawButton(g, rectangle, ButtonState.Inactive);
                }
                else
                {
                    switch (state)
                    {
                        case ItemState.Normal:
                            g.FillRectangle(SystemBrushes.ButtonFace, rectangle);
                            break;
                        case ItemState.HotTrack:
                            g.FillRectangle(SystemBrushes.ControlLightLight, rectangle);
                            break;
                        case ItemState.Open:
                        case ItemState.Pressed:
                            g.FillRectangle(SystemBrushes.ControlLight, rectangle);
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(text))
                {
                    if (enabled)
                    {
                        g.DrawString(text, font, SystemBrushes.ControlText, rectangle, fmt);
                    }
                    else
                    {
                        g.DrawString(text, font, SystemBrushes.GrayText, rectangle, fmt);
                    }
                }

                if (hasBorder)
                    DrawBorder(g, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1), enabled);
            }
        }

        public void DrawSeparator(Graphics g, Point ptFrom, Point ptTo)
        {
            // Find point halfway across for separator lines
            Pen p = new Pen(SystemColors.ActiveBorder);

            // Draw two lines to give 3D effect and indent by 2 pixels
            g.DrawLine(p, ptFrom, ptTo);

            p.Dispose();
        }

        public void DrawFocusRect(Graphics g, Rectangle r)
        {
            ControlPaint.DrawFocusRectangle(g, new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1));
        }

        public void DrawSelectedPanel(Graphics g, Rectangle r)
        {
            using (SolidBrush brush = new SolidBrush(SystemColors.Control))
                g.FillRectangle(brush, r);

            DrawSelectionBorder(g, r);
        }

    }
}
