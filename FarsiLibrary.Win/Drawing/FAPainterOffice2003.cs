using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Drawing
{
    /// <summary>
    /// Painter class used to paint drawing objects in Office 2003 theme.
    /// </summary>
    public class FAPainterOffice2003 : FAPainterBase, IFAPainter
    {
        public virtual void DrawButtonFocusRect(Graphics g, Rectangle r, ItemState state)
        {
            Rectangle focus = new Rectangle(r.X, r.Y, r.Width, r.Height);
            focus.Inflate(-2, -2);
            ControlPaint.DrawFocusRectangle(g, focus);
        }

        public virtual void DrawSelectionBorder(Graphics g, Rectangle r)
        {
            using (Pen p = new Pen(Office2003Colors.Default[Office2003Color.TabPageBackColor1]))
                g.DrawRectangle(p, r);
        }

        public virtual void DrawVerticalSeparator(Graphics g, Point from, Point to)
        {
            Pen pen1 = new Pen(Office2003Colors.Default[Office2003Color.Border]);
            g.DrawLine(pen1, from, to);
        }

        public virtual void DrawFilledBackground(Graphics g, Rectangle rectangle, bool isGradient, float angle)
        {
            if (isGradient)
            {
                // Create DrawTab linear gradient brush that covers the area of the parent form
                using (LinearGradientBrush brush = new LinearGradientBrush(rectangle, Office2003Colors.Default[Office2003Color.Button1], Office2003Colors.Default[Office2003Color.Button2], angle))
                {
                    // Blend from the dark to the light over the first 58% of distance and then
                    // the rest should be all in the light colour, this matches Office2003
                    Blend blending = new Blend();
                    blending.Factors = new float[] { 0f, 1f, 1f };
                    blending.Positions = new float[] { 0f, 0.58f, 1f };
                    brush.Blend = blending;

                    // Finally we draw using the brush
                    g.FillRectangle(brush, rectangle);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(brush, rectangle);
                }
            }
        }

        public virtual void DrawWhiteBackground(Graphics g, Rectangle r, bool isGradient, float angle)
        {
            using (Brush brush = new SolidBrush(SystemColors.ControlLightLight))
            {
                g.FillRectangle(brush, r);
            }
        }

        public virtual void DrawBorder(Graphics g, Rectangle rectangle, bool enabled)
        {
            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
                Color c = enabled ? Office2003Colors.Default[Office2003Color.NavPaneBorderColor] : Office2003Colors.Default[Office2003Color.TextDisabled];
                using (Pen p = new Pen(c))
                {
                    g.DrawRectangle(p, rectangle);
                }
            }
        }

        public virtual void DrawButton(Graphics g, Rectangle rectangle, string text, Font font, StringFormat fmt, ItemState state, bool hasBorder, bool enabled)
        {
            float angle = 90;

            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
                if (!enabled)
                {
                    using (Brush backBrush = new LinearGradientBrush(rectangle, Office2003Colors.Default[Office2003Color.Button1], Office2003Colors.Default[Office2003Color.Button2], angle))
                    {
                        g.FillRectangle(backBrush, rectangle);
                    }
                }
                else
                {
                    switch (state)
                    {
                        case ItemState.Normal:
                            using (Brush backBrush = new LinearGradientBrush(rectangle, Office2003Colors.Default[Office2003Color.Button1], Office2003Colors.Default[Office2003Color.Button2], angle))
                                g.FillRectangle(backBrush, rectangle);
                            break;
                        case ItemState.HotTrack:
                            using (Brush trackBrush = new LinearGradientBrush(rectangle, Office2003Colors.Default[Office2003Color.Button1Hot], Office2003Colors.Default[Office2003Color.Button2Hot], angle))
                                g.FillRectangle(trackBrush, rectangle);
                            break;
                        case ItemState.Open:
                        case ItemState.Pressed:
                            using (Brush trackBrush = new LinearGradientBrush(rectangle, Office2003Colors.Default[Office2003Color.Button1Pressed], Office2003Colors.Default[Office2003Color.Button2Pressed], angle))
                                g.FillRectangle(trackBrush, rectangle);
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(text))
                {
                    if (enabled)
                    {
                        using (SolidBrush br = new SolidBrush(Office2003Colors.Default[Office2003Color.Text]))
                            g.DrawString(text, font, br, rectangle, fmt);
                    }
                    else
                    {
                        using (SolidBrush br = new SolidBrush(Office2003Colors.Default[Office2003Color.TextDisabled]))
                            g.DrawString(text, font, br, rectangle, fmt);
                    }
                }

                if(hasBorder)
                    DrawBorder(g, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1), enabled);
            }
        }

        public virtual void DrawFocusRect(Graphics g, Rectangle r)
        {
            ControlPaint.DrawFocusRectangle(g, r);
        }

        public virtual void DrawSeparator(Graphics g, Point ptFrom, Point ptTo)
        {
            // Find point halfway across for separator lines
            Pen p = new Pen(Office2003Colors.Default[Office2003Color.TabPageBorderColor]);

            // Draw two lines to give 3D effect and indent by 2 pixels
            g.DrawLine(p, ptFrom, ptTo);

            p.Dispose();
        }

        public virtual void DrawSelectedPanel(Graphics g, Rectangle r)
        {
            using (SolidBrush brush = new SolidBrush(Office2003Colors.Default[Office2003Color.TabPageBackColor1]))
            {
                g.FillRectangle(brush, r);
            }
        }
    }
}
