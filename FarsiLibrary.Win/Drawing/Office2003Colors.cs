using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Drawing
{
    #region Internal Class

    internal struct ColorInfo
    {
        public int BtnFaceColorPart;
        public int HighlightColorPart;
        public int WindowColorPart;
    }

    #endregion

    #region Office Colors

    internal class Office2003Colors
    {
        #region Fields

        private readonly Hashtable colors;
        private static ColorInfo colorInfo;
        private static readonly Office2003Colors defaultColors;
        private static readonly int[][] Office11Colors = new[]
                                                    {
                                                        new[]
                                                            {
                                                                0xddecfe, 0x81a9e2, 0x9ebef5, 0xc3daf9, 0x3b619c,
                                                                0x274176, 0xFFFFFF, 0x6a8ccb, 0xf1f9ff,
                                                                0x000080, 0xFFF6CF, 0xffd091, 0xfe8e4b, 0xffd28e,
                                                                0x000000, 0x8D8D8D, 0x75a6f1, 0x053995,
                                                                0xF6F6F6, 0x002d96, 0x2a66c9, 0xc4dbf9, 0xbedafb,
                                                                0x7ba2e7, 0x6375d6, 0xd6dff7, 0x215dc6,
                                                                0x428eff, 0xffffff, 0xc6d3f7, 0xb1b9d8, 0xd3d3d3
                                                            },
                                                        new[]
                                                            {
                                                                0xf4f7de, 0xb7c691, 0xd9d9a7, 0xf2f0e4, 0x608058,
                                                                0x515e33, 0xFFFFFF, 0x608058, 0xf4f7de,
                                                                0x3f5d38, 0xFFF6CF, 0xffd091, 0xfe8e4b, 0xffd28e,
                                                                0x000000, 0x8D8D8D, 0xb0c28c, 0x60776b,
                                                                0xF4F4F4, 0x758d5e, 0x74865e, 0xe1dead, 0xafba91,
                                                                0xccd9ad, 0xa5bd84, 0xf6f6ec, 0x56662d,
                                                                0x72921d, 0xfffcec, 0xe0e7b8, 0xcad5be, 0xd3d3d3
                                                            },
                                                        new[]
                                                            {
                                                                0xf3f4fa, 0x9997b5, 0xd7d7e5, 0xf3f3f7, 0x7c7c94,
                                                                0x545475, 0xFFFFFF, 0x6e6d8f, 0xFFFFFF,
                                                                0x000080, 0xFFF6CF, 0xffd091, 0xfe8e4b, 0xffd28e,
                                                                0x000000, 0x8D8D8D, 0xb3b2c8, 0x767492,
                                                                0xfdfaff, 0x7c7c94, 0x7a7999, 0xdbdae4, 0xe5e5eb,
                                                                0xc4c8d4, 0xb1b3c8, 0xf0f1f5, 0x3f3d3d,
                                                                0x7e7c7c, 0xffffff, 0xd6d7e0, 0xc4c6d0, 0xd3d3d3
                                                            },
                                                    };

        #endregion

        #region Static and Normal Ctor

        static Office2003Colors()
        {
            colorInfo = new ColorInfo();
            defaultColors = new Office2003Colors();
        }

        protected Office2003Colors()
        {
            colors = new Hashtable();
            InitColors();
        }

        #endregion

        #region Props

        public static Office2003Colors Default
        {
            get { return defaultColors; }
        }

        public Hashtable Colors 
        { 
            get { return colors; } 
        }

        public Color this[Office2003Color color]
        {
            get
            {
                object val = Colors[color];
                if (val == null) return SystemColors.Control;
                return (Color)val;
            }
        }

        public Color FlatBarBorderColor
        {
            get
            {
                Color res = SystemColors.ControlDark;
                res = Color.FromArgb(GetDarkValue(res.R), GetDarkValue(res.G), GetDarkValue(res.B));
                return GetRealColor(res);
            }
        }

        public Color FlatBarBackColor
        {
            get
            {
                Color res = SystemColors.Control;
                res = Color.FromArgb(GetLightValue(res.R), GetLightValue(res.G), GetLightValue(res.B));
                return GetRealColor(res);
            }
        }

        public Color FlatBarItemPressedBackColor
        {
            get
            {
                return GetRealColor(GetLightColor(14, 44, 40));
            }
        }

        public Color FlatBarItemHighLightBackColor
        {
            get
            {
                return GetRealColor(GetLightColor(-2, 30, 72));
            }
        }

        public Color FlatBarItemDownedColor
        {
            get { return GetRealColor(GetLightColor(11, 9, 73)); }
        }

        #endregion

        #region Methods

        public void ReinitializeColors()
        {
            InitColors();
        }

        protected void InitColors()
        {
            XPThemeType themeType = GetThemeType();
            if(themeType != XPThemeType.Unknown) 
            {
                int id = ((int)themeType) - 1;
                InitOfficeColors(id);
            }
            else 
            {
                InitStandardColors();
            }

            colors[Office2003Color.TabPageForeColor] = colors[Office2003Color.Text];
            colors[Office2003Color.TabBackColor1] = colors[Office2003Color.Button1];
            colors[Office2003Color.TabBackColor2] = colors[Office2003Color.Button2];
            colors[Office2003Color.TabPageBackColor1] = colors[Office2003Color.Button1Pressed];
            colors[Office2003Color.TabPageBackColor2] = colors[Office2003Color.Button2Pressed];
            colors[Office2003Color.TabPageBorderColor] = colors[Office2003Color.Border];
        }

        protected virtual void InitOfficeColors(int id)
        {
            colors[Office2003Color.Button1] = FromRgb(Office11Colors[id][0]);
            colors[Office2003Color.Button2] = FromRgb(Office11Colors[id][1]);
            colors[Office2003Color.Border] = FromRgb(Office11Colors[id][9]);
            colors[Office2003Color.Button1Hot] = FromRgb(Office11Colors[id][10]);
            colors[Office2003Color.Button2Hot] = FromRgb(Office11Colors[id][11]);
            colors[Office2003Color.Button1Pressed] = FromRgb(Office11Colors[id][12]);
            colors[Office2003Color.Button2Pressed] = FromRgb(Office11Colors[id][13]);
            colors[Office2003Color.Text] = FromRgb(Office11Colors[id][14]);
            colors[Office2003Color.TextDisabled] = FromRgb(Office11Colors[id][15]);
            colors[Office2003Color.GroupRow] = FromRgb(Office11Colors[id][22]);
            colors[Office2003Color.Header] = GetMiddleRGB(this[Office2003Color.Button1], this[Office2003Color.Button2], 50);
            colors[Office2003Color.Header2] = this[Office2003Color.Button2];
            colors[Office2003Color.Header2] = ControlPaint.Dark(this[Office2003Color.Button2], 0.05f);
            colors[Office2003Color.LinkBorder] = FromRgb(Office11Colors[id][31]);
            colors[Office2003Color.NavBarBackColor1] = FromRgb(Office11Colors[id][23]);
            colors[Office2003Color.NavBarBackColor2] = FromRgb(Office11Colors[id][24]);
            colors[Office2003Color.NavBarGroupClientBackColor] = FromRgb(Office11Colors[id][25]);
            colors[Office2003Color.NavBarLinkTextColor] = FromRgb(Office11Colors[id][26]);
            colors[Office2003Color.NavBarLinkHightlightedTextColor] = FromRgb(Office11Colors[id][27]);
            colors[Office2003Color.NavBarLinkDisabledTextColor] = ControlPaint.Light(this[Office2003Color.NavBarLinkHightlightedTextColor], 0.5f);
            colors[Office2003Color.NavBarGroupCaptionBackColor1] = FromRgb(Office11Colors[id][28]);
            colors[Office2003Color.NavBarGroupCaptionBackColor2] = FromRgb(Office11Colors[id][29]);
            colors[Office2003Color.NavBarExpandButtonRoundColor] = FromRgb(Office11Colors[id][30]);
            colors[Office2003Color.NavPaneBorderColor] = ControlPaint.Dark(SystemColors.Highlight, 0.05f);
            colors[Office2003Color.NavBarNavPaneHeaderBackColor] = SystemColors.Highlight;
        }

        protected virtual void InitStandardColors()
        {
            colors[Office2003Color.Button1] = GetMiddleRGB(SystemColors.Control, SystemColors.Window, 22);
            colors[Office2003Color.Button2] = GetMiddleRGB(SystemColors.Control, SystemColors.Window, 96);
            colors[Office2003Color.Border] = SystemColors.Highlight;
            colors[Office2003Color.Button2Hot] = colors[Office2003Color.Button1Hot] = GetRealColor(GetLightColor(-2, 30, 72));
            colors[Office2003Color.Button1Pressed] = colors[Office2003Color.Button2Pressed] = GetRealColor(GetLightColor(14, 44, 40));
            colors[Office2003Color.Text] = SystemColors.ControlText;
            colors[Office2003Color.TextDisabled] = SystemColors.GrayText;
            colors[Office2003Color.Header] = GetMiddleRGB(this[Office2003Color.Button1], this[Office2003Color.Button2], 50);
            colors[Office2003Color.Header2] = ControlPaint.Dark(this[Office2003Color.Button2], 0.5f);
            colors[Office2003Color.GroupRow] = SystemColors.Control;
            colors[Office2003Color.LinkBorder] = CalcColor(SystemColors.Window, .29f, SystemColors.Control, .72f);
            colors[Office2003Color.NavBarBackColor1] = CalcNavColor(-10);
            colors[Office2003Color.NavBarBackColor2] = CalcNavColor(-29);
            colors[Office2003Color.NavBarGroupClientBackColor] = ControlPaint.LightLight(SystemColors.InactiveCaption);
            colors[Office2003Color.NavBarLinkTextColor] = CalcNavColor(-50);
            colors[Office2003Color.NavBarLinkHightlightedTextColor] = ControlPaint.Light(CalcNavColor(-50));
            colors[Office2003Color.NavBarGroupCaptionBackColor1] = SystemColors.Window;
            colors[Office2003Color.NavBarGroupCaptionBackColor2] = ControlPaint.LightLight(SystemColors.Highlight);
            colors[Office2003Color.NavBarExpandButtonRoundColor] = SystemColors.ControlDark;
            colors[Office2003Color.NavPaneBorderColor] = ControlPaint.Dark(GetLightColor(40, 0, 0), 0.05f);
            colors[Office2003Color.NavBarNavPaneHeaderBackColor] = GetLightColor(40, 0, 0);

            if (this[Office2003Color.NavBarLinkTextColor] == Color.FromArgb(0, 0x15, 0x5b))
            {
                colors[Office2003Color.NavBarLinkDisabledTextColor] = Color.Gray;
            }
            else
            {
                colors[Office2003Color.NavBarLinkDisabledTextColor] = ControlPaint.LightLight(CalcNavColor(-50));
            }
        }

        private XPThemeType GetThemeType()
        {
            string themeName = VisualStyleInformation.ColorScheme.ToUpper();

            if (themeName == "NORMALCOLOR")
            {
                return XPThemeType.NormalColor;
            }

            if (themeName == "HOMESTEAD")
            {
                return XPThemeType.Homestead;
            }

            if (themeName == "METALLIC")
            {
                return XPThemeType.Metallic;
            }

            return XPThemeType.Unknown;
        }

        public Color GetLightColor(int btnFaceColorPart, int highlightColorPart, int windowColorPart)
        {
            colorInfo.BtnFaceColorPart = btnFaceColorPart;
            colorInfo.HighlightColorPart = highlightColorPart;
            colorInfo.WindowColorPart = windowColorPart;

            Color btnFace = SystemColors.Control;
            Color highlight = SystemColors.Highlight;
            Color window = SystemColors.Window;
            Color res;

            if (btnFace == Color.White || btnFace == Color.Black)
            {
                res = highlight;
            }
            else
            {
                res = Color.FromArgb(
                    GetLightIndex(colorInfo, btnFace.R, highlight.R, window.R),
                    GetLightIndex(colorInfo, btnFace.G, highlight.G, window.G),
                    GetLightIndex(colorInfo, btnFace.B, highlight.B, window.B));
            }
            return res;
        }

        public int GetLightIndex(ColorInfo info, byte btnFaceValue, byte highlightValue, byte windowValue)
        {
            int res = (btnFaceValue * info.BtnFaceColorPart) / 100 +
                      (highlightValue * info.HighlightColorPart) / 100 +
                      (windowValue * info.WindowColorPart) / 100;

            if (res < 0) 
                res = 0;

            if (res > 255) 
                res = 255;

            return res;
        }

        public Color FlatTabBackColor
        {
            get
            {
                Color clr = SystemColors.Control;
                int r = clr.R, g = clr.G, b = clr.B;
                int max = Math.Max(Math.Max(r, g), b);
                int delta = 0x23;

                int maxDelta = (255 - (max + delta));

                if (maxDelta > 0) maxDelta = 0;
                r += (delta + maxDelta);
                g += (delta + maxDelta);
                b += (delta + maxDelta);
                return Color.FromArgb(r, g, b);
            }
        }

        public int GetDarkValue(int val)
        {
            return (val * 8) / 10;
        }

        public int GetLightValue(byte val)
        {
            return val + ((255 - val) * 16) / 100;
        }

        public Color GetRealColor(Color clr)
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            Color res = g.GetNearestColor(clr);
            g.Dispose();
            return res;
        }

        public Color OffsetColor(Color clr, int dR, int dG, int dB)
        {
            dR = Math.Min(255, clr.R + dR);
            dG = Math.Min(255, clr.G + dG);
            dB = Math.Min(255, clr.B + dB);
            return Color.FromArgb(dR, dG, dB);
        }

        public Color FromRgb(int rgb) 
        { 
            return Color.FromArgb((int)(rgb + 0xff000000)); 
        }

        public Color CalcNavColor(int d)
        {
            Color clr = SystemColors.Highlight;
            int r = clr.R, g = clr.G, b = clr.B;
            int max = Math.Max(Math.Max(r, g), b);
            int delta = 0x23 + d;

            int maxDelta = (255 - (max + delta));

            if (maxDelta > 0) maxDelta = 0;
            r += (delta + maxDelta + 5);
            g += (delta + maxDelta);
            b += (delta + maxDelta);
            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            return Color.FromArgb(Math.Abs(r), Math.Abs(g), Math.Abs(b));
        }

        public Color CalcColor(Color color1, float percent1, Color color2, float percent2)
        {
            return CalcColor(color1, percent1, color2, percent2, Color.Empty, 0f);
        }

        private Color GetMiddleRGB(Color clr1, Color clr2, int percent)
        {
            Color r = Color.FromArgb(CalcValue(clr1.R, clr2.R, percent), CalcValue(clr1.G, clr2.G, percent), CalcValue(clr1.B, clr2.B, percent));
            return r;
        }

        private int CalcValue(int v1, int v2, int percent)
        {
            int i;
            i = (v1 * percent) / 100 + (v2 * (100 - percent)) / 100;
            if (i > 255) i = 255;

            return i;
        }

        public Color CalcColor(Color color1, float percent1, Color color2, float percent2, Color color3, float percent3)
        {
            percent1 = Math.Max(0, Math.Min(1, percent1));
            percent2 = Math.Max(0, Math.Min(1, percent2));
            percent3 = Math.Max(0, Math.Min(1, percent3));
            int r = (int)(color1.R * percent1 + color2.R * percent2 + color3.R * percent3);
            int g = (int)(color1.G * percent1 + color2.G * percent2 + color3.G * percent3);
            int b = (int)(color1.B * percent1 + color2.B * percent2 + color3.B * percent3);
            r = Math.Min(r, 255);
            g = Math.Min(g, 255);
            b = Math.Min(b, 255);

            return Color.FromArgb(r, g, b);
        }

        #endregion
    }

    #endregion
}
