using System.Drawing;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Drawing
{
    public class FAPainterOffice2007 : FAPainterOffice2003
    {
        private readonly Office2007Renderer renderer = new Office2007Renderer();

        private readonly Color r0 = Color.FromArgb(249, 252, 255);
        private readonly Color r1 = Color.FromArgb(223, 237, 255);
        private readonly Color r2 = Color.FromArgb(201, 223, 255);
        private readonly Color r3 = Color.FromArgb(174, 210, 255);
        private readonly Color r4 = Color.FromArgb(191, 218, 255);
        private readonly Color r5 = Color.FromArgb(101, 147, 207);

        public override void DrawButton(Graphics g, Rectangle rectangle, string text, Font font, StringFormat fmt, ItemState state, bool hasBorder, bool enabled)
        {
            if (enabled)
            {
                switch(state)
                {
                    case ItemState.Open:
                    case ItemState.Pressed:
                        renderer.DrawGradientItem(g, rectangle, Office2007Renderer._itemToolItemCheckPressColors);
                        break;

                    case ItemState.HotTrack:
                        renderer.DrawGradientItem(g, rectangle, Office2007Renderer._itemToolItemSelectedColors);
                        break;

                    case ItemState.Normal:
                        renderer.DrawGradientItem(g, rectangle, new Office2007Renderer.GradientItemColors(r1, r2, r3, r4, r1, r2, r3, r4, r5, r5));
                        break;
                }
            }
            else
            {
                renderer.DrawGradientItem(g, rectangle, Office2007Renderer._itemDisabledColors);
            }

            DrawString(g, rectangle, text, fmt, font, enabled);
        }

        public override void DrawString(Graphics g, Rectangle rectangle, string text, StringFormat fmt, Font font, bool enabled)
        {
            if(!string.IsNullOrEmpty(text))
            {
                if(enabled)
                {
                    using (var br = new SolidBrush(Office2007Renderer._textContextMenuItem))
                    {
                        g.DrawString(text, font, br, rectangle, fmt);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(Office2007Renderer._textDisabled))
                    {
                        g.DrawString(text, font, br, rectangle, fmt);
                    }
                }
            }
        }

        public override void DrawFilledBackground(Graphics g, Rectangle rectangle, bool isGradient, float angle)
        {
            if (isGradient)
            {
                renderer.DrawGradientItem(g, rectangle, new Office2007Renderer.GradientItemColors(r1, r2, r3, r4, r1, r2, r3, r4, r5, r5));
            }
            else
            {
                using (Brush brush = new SolidBrush(r0))
                {
                    g.FillRectangle(brush, rectangle);
                }
            }
        }


        public override void DrawSelectionBorder(Graphics g, Rectangle r)
        {
            using (Pen p = new Pen(Office2007Renderer._separatorMenuLight))
                g.DrawRectangle(p, r);
        }

        public override void DrawSeparator(Graphics g, Point ptFrom, Point ptTo)
        {
            Pen p = new Pen(Office2007Renderer._statusStripBorderDark);
            g.DrawLine(p, ptFrom, ptTo);
            p.Dispose();
        }

        public override void DrawSelectedPanel(Graphics g, Rectangle r)
        {
            renderer.DrawGradientItem(g, r, Office2007Renderer._itemToolItemCheckPressColors);
        }

        public override void DrawBorder(Graphics g, Rectangle rectangle, bool enabled)
        {
            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
                Color c = enabled ? r2 : r3;
                using (Pen p = new Pen(c))
                {
                    g.DrawRectangle(p, rectangle);
                }
            }
        }
    }
}
