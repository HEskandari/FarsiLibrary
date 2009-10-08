using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Drawing
{
    /// <summary>
    /// Painter class used to paint drawing objects in Windows XP theme, using managed UxTheme provider of .NET 2.
    /// </summary>
    public class FAPainterWindowsXP : FAPainterBase, IFAPainter
    {
        public void DrawButtonFocusRect(Graphics g, Rectangle r, ItemState state)
        {
            Rectangle focus;

            if (state == ItemState.Pressed)
            {
                focus = new Rectangle(r.X + 3, r.Y + 3, r.Width - 6, r.Height - 6);
            }
            else
            {
                focus = new Rectangle(r.X + 3, r.Y + 3, r.Width - 6, r.Height - 6);
            }

            ControlPaint.DrawFocusRectangle(g, focus);
        }

        public void DrawSelectionBorder(Graphics g, Rectangle r)
        {
            using(Pen p = new Pen(SystemColors.Highlight))
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
            using (Brush brush = new SolidBrush(SystemColors.ControlLightLight))
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
            VisualStyleRenderer renderer = null;

            if (!enabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Disabled);
            }
            else
            {
                switch (state)
                {
                    case ItemState.Open:
                        renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Default);
                        break;

                    case ItemState.Normal:
                        renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
                        break;

                    case ItemState.HotTrack:
                        renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Hot);
                        break;

                    case ItemState.Pressed:
                        renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed);
                        break;
                }
            }

            renderer.DrawBackground(g, rectangle);

            if (!string.IsNullOrEmpty(text))
            {
                if (enabled)
                {
                    using (SolidBrush br = new SolidBrush(SystemColors.ControlText))
                        g.DrawString(text, font, br, rectangle, fmt);
                }
                else
                {
                    using (SolidBrush br = new SolidBrush(SystemColors.GrayText))
                        g.DrawString(text, font, br, rectangle, fmt);
                }
            }
        }

        public void DrawFocusRect(Graphics g, Rectangle r)
        {
            ControlPaint.DrawFocusRectangle(g, new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1));
        }

        public void DrawSeparator(Graphics g, Point ptFrom, Point ptTo)
        {
            // Find point halfway across for separator lines
            Pen p = new Pen(SystemColors.ControlDark);

            // Draw two lines to give 3D effect and indent by 2 pixels
            g.DrawLine(p, ptFrom, ptTo);

            p.Dispose();
        }

        public void DrawSelectedPanel(Graphics g, Rectangle r)
        {
            using (SolidBrush brush = new SolidBrush(SystemColors.Control))
            {
                g.FillRectangle(brush, r);
            }

            DrawSelectionBorder(g, r);
        }
    }
}
