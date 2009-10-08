using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FarsiLibrary.WPF.Controls.Decorators
{
    public class LunaButtonChrome : ButtonChrome
    {
        #region Fields

        private static LinearGradientBrush _commonBottomShadeHS;
        private static LinearGradientBrush _commonBottomShadeM;
        private static LinearGradientBrush _commonBottomShadeNC;
        private static LinearGradientBrush _commonHoverFillM;
        private static LinearGradientBrush _commonPressedBottomShade;
        private static LinearGradientBrush _commonPressedFillHS;
        private static LinearGradientBrush _commonPressedFillM;
        private static LinearGradientBrush _commonPressedFillNC;
        private static LinearGradientBrush _commonPressedLeftShade;
        private static LinearGradientBrush _commonPressedTopShade;
        private static LinearGradientBrush _commonRightShadeHS;
        private static LinearGradientBrush _commonRightShadeM;
        private static LinearGradientBrush _commonRightShadeNC;
        private static Pen _commonBorderPen;
        private static Pen _commonDefaultedInnerHighlightHS;
        private static Pen _commonDefaultedInnerHighlightM;
        private static Pen _commonDefaultedInnerHighlightNC;
        private static Pen _commonDisabledBorderPenHS;
        private static Pen _commonDisabledBorderPenM;
        private static Pen _commonDisabledBorderPenNC;
        private static Pen _commonHoverInnerHighlightHS;
        private static Pen _commonHoverInnerHighlightM;
        private static Pen _commonHoverInnerHighlightNC;
        private static Pen _commonOuterHighlight;
        private static SolidColorBrush _commonDisabledFillHS;
        private static SolidColorBrush _commonDisabledFillM;
        private static SolidColorBrush _commonDisabledFillNC;

        private static object locker = new object();

        private const double SideThickness = 4.0;
        private const double ChromeThickness = 6.0;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ThemeColorProperty = DependencyProperty.Register("ThemeColor", typeof(ThemeColor), typeof(LunaButtonChrome), new FrameworkPropertyMetadata(ThemeColor.NormalColor, FrameworkPropertyMetadataOptions.AffectsRender), IsValidThemeColor);
        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(typeof(LunaButtonChrome), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region Rendering

        protected override void OnRender(DrawingContext drawingContext)
        {
            var bounds = new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight);

            if (this.DrawOuterHighlight(drawingContext, bounds) || 
                this.DrawBackgroundCore(drawingContext, bounds))
                return;

            this.DrawShades(drawingContext, bounds);
            this.DrawInnerHighlight(drawingContext, bounds);
            this.DrawBorder(drawingContext, bounds);
        }

        protected override void DrawShades(DrawingContext dc, Rect bounds)
        {
            bounds.Inflate(-0.3, -0.3);

            if (this.TopShade != null)
            {
                dc.DrawRoundedRectangle(this.TopShade, null, new Rect(bounds.Left, bounds.Top, bounds.Width, ChromeThickness), SideThickness, SideThickness);
            }

            if (this.BottomShade != null)
            {
                dc.DrawRoundedRectangle(this.BottomShade, null, new Rect(bounds.Left, bounds.Bottom - ChromeThickness, bounds.Width, ChromeThickness), SideThickness, SideThickness);
            }

            if (this.LeftShade != null)
            {
                dc.DrawRoundedRectangle(this.LeftShade, null, new Rect(bounds.Left, bounds.Top, ChromeThickness, bounds.Height), SideThickness, SideThickness);
            }

            if (this.RightShade != null)
            {
                dc.DrawRoundedRectangle(this.RightShade, null, new Rect(bounds.Right - ChromeThickness, bounds.Top, ChromeThickness, bounds.Height), SideThickness, SideThickness);
            }

            bounds.Inflate(0.3, 0.3);
        }

        protected virtual bool DrawOuterHighlight(DrawingContext dc, Rect bounds)
        {
            if (bounds.Width < 1.33 || bounds.Height < 1.33)
                return true;

            if (this.OuterHighlight != null)
                dc.DrawRoundedRectangle(null, this.OuterHighlight, new Rect(0.66, 0.66, bounds.Width - 1.33, bounds.Height - 1.33), 3.0, 3.0);

            if (bounds.Width < 1.5 || bounds.Height < 1.5)
                return true;
            
            bounds.Inflate(-0.75, -0.75);

            return false;
        }

        protected virtual bool DrawBackgroundCore(DrawingContext dc, Rect bounds)
        {
            if (this.BackgroundBrush != null)
                dc.DrawRoundedRectangle(this.BackgroundBrush, null, bounds, 4.0, 4.0);
            
            if (bounds.Width >= 0.6 && bounds.Height >= 0.6)
                return false;
            
            return true;
        }

        protected override void DrawBorder(DrawingContext dc, Rect bounds)
        {
            if (this.BorderPen != null && bounds.Width >= 1.0 && bounds.Height >= 1.0)
            {
                dc.DrawRoundedRectangle(null, this.BorderPen, new Rect(bounds.Left + 0.5, bounds.Top + 0.5, bounds.Width - 1.0, bounds.Height - 1.0), 3.0, 3.0);
            }
        }

        protected override void DrawInnerHighlight(DrawingContext dc, Rect bounds)
        {
            if (this.InnerHighlight != null && bounds.Width >= 2.66 && bounds.Height >= 2.66)
            {
                dc.DrawRoundedRectangle(null, this.InnerHighlight, new Rect(bounds.Left + 1.33, bounds.Top + 1.33, bounds.Width - 2.66, bounds.Height - 2.66), 3.0, 3.0);
            }
        }

        #endregion

        #region Coerce

        private static bool IsValidThemeColor(object o)
        {
            var color = (ThemeColor)o;

            //Note: Defaults to true. Added in case a new enum is added
            //at a later stage.
            return color == ThemeColor.NormalColor || 
                   color == ThemeColor.Homestead || 
                   color == ThemeColor.Metallic;
        }

        #endregion

        #region Props

        public ThemeColor ThemeColor
        {
            get { return (ThemeColor) base.GetValue(ThemeColorProperty); }
            set { base.SetValue(ThemeColorProperty, value); }
        }

        public Brush Fill
        {
            get { return (Brush)base.GetValue(FillProperty); }
            set { base.SetValue(FillProperty, value); }
        }
 
        #endregion

        #region Colors and Brushes

        private LinearGradientBrush TopShade
        {
            get
            {
                if (base.IsEnabled && this.RenderPressed)
                    return CommonPressedTopShade;
                
                return null;
            }
        }

        private LinearGradientBrush RightShade
        {
            get
            {
                if (!base.IsEnabled || this.RenderPressed)
                    return null;
                
                if (this.ThemeColor == ThemeColor.NormalColor)
                    return CommonRightShadeNC;
                
                if (this.ThemeColor == ThemeColor.Homestead)
                    return CommonRightShadeHS;
                
                return CommonRightShadeM;
            }
        }

        private LinearGradientBrush LeftShade
        {
            get
            {
                if (base.IsEnabled && this.RenderPressed)
                    return CommonPressedLeftShade;
                
                return null;
            }
        }

        private LinearGradientBrush BottomShade
        {
            get
            {
                if (!base.IsEnabled)
                    return null;
                
                if (this.RenderPressed)
                    return CommonPressedBottomShade;
                
                if (this.ThemeColor == ThemeColor.NormalColor)
                    return CommonBottomShadeNC;
                
                if (this.ThemeColor == ThemeColor.Homestead)
                    return CommonBottomShadeHS;
                
                return CommonBottomShadeM;
            }
        }
 
        private Pen OuterHighlight
        {
            get
            {
                if (!base.IsEnabled)
                    return null;
                
                return CommonOuterHighlight;
            }
        }

        private Pen InnerHighlight
        {
            get
            {
                if (!base.IsEnabled || this.RenderPressed || !this.RenderDefaulted)
                    return null;

                if (this.RenderMouseOver)
                {
                    if (this.ThemeColor == ThemeColor.NormalColor)
                        return CommonHoverInnerHighlightNC;
                    
                    if (this.ThemeColor == ThemeColor.Homestead)
                        return CommonHoverInnerHighlightHS;
                    
                    return CommonHoverInnerHighlightM;
                }

                if (this.ThemeColor == ThemeColor.NormalColor)
                    return CommonDefaultedInnerHighlightNC;

                if (this.ThemeColor == ThemeColor.Homestead)
                    return CommonDefaultedInnerHighlightHS;

                return CommonDefaultedInnerHighlightM;
            }
        }

        private static LinearGradientBrush CommonRightShadeNC
        {
            get
            {
                if (_commonRightShadeNC == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(0, 89, 47, 0), 0.5),
                            new GradientStop(Color.FromArgb(40, 89, 47, 0), 1.0)
                        };

                        _commonRightShadeNC = CreateFrozenBrush(stops, new Point(0.0, 0.5), new Point(1.0, 0.5));
                    }
                }

                return _commonRightShadeNC;
            }
        }

        private static LinearGradientBrush CommonRightShadeM
        {
            get
            {
                if (_commonRightShadeM == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(0, 89, 47, 0), 0.5),
                            new GradientStop(Color.FromArgb(40, 89, 47, 0), 1.0)
                        };

                        _commonRightShadeM = CreateFrozenBrush(stops, new Point(0.0, 0.5), new Point(1.0, 0.5));
                    }
                }

                return _commonRightShadeM;
            }
        }

        private static LinearGradientBrush CommonRightShadeHS
        {
            get
            {
                if (_commonRightShadeHS == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(0, 227, 209, 184), 0.5),
                            new GradientStop(Color.FromArgb(255, 227, 209, 184), 1.0)
                        };

                        _commonRightShadeHS = CreateFrozenBrush(stops, new Point(0.0, 0.5), new Point(1.0, 0.5));
                    }
                }

                return _commonRightShadeHS;
            }
        }

        private static LinearGradientBrush CommonPressedTopShade
        {
            get
            {
                if (_commonPressedTopShade == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 151, 139, 114), 1.0),
                            new GradientStop(Color.FromArgb(0, 255, 255, 255), 0.6)
                        };

                        _commonPressedTopShade = CreateFrozenBrush(stops, new Point(0.5, 1.0), new Point(0.5, 0.0));
                    }
                }

                return _commonPressedTopShade;
            }
        }

        private static LinearGradientBrush CommonPressedLeftShade
        {
            get
            {
                if (_commonPressedLeftShade == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 170, 157, 135), 1.0),
                            new GradientStop(Color.FromArgb(0, 255, 255, 255), 0.6)
                        };

                        _commonPressedLeftShade = CreateFrozenBrush(stops, new Point(1.0, 0.5), new Point(0.0, 0.5));
                    }
                }

                return _commonPressedLeftShade;
            }
        }

        private static LinearGradientBrush CommonPressedFillNC
        {
            get
            {
                if (_commonPressedFillNC == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 230, 230, 224), 0.0),
                            new GradientStop(Color.FromArgb(255, 226, 226, 218), 1.0)
                        };

                        _commonPressedFillNC = CreateFrozenBrush(stops, new Point(0.5, 1.0), new Point(0.5, 0.0));
                    }
                }

                return _commonPressedFillNC;
            }
        }

        private static LinearGradientBrush CommonPressedFillM
        {
            get
            {
                if (_commonPressedFillM == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 179, 178, 197), 0.0),
                            new GradientStop(Color.FromArgb(255, 218, 219, 229), 0.6),
                            new GradientStop(Color.FromArgb(255, 255, 255, 255), 0.8)
                        };

                        _commonPressedFillM = CreateFrozenBrush(stops, new Point(0.5, 0.0), new Point(0.5, 1.0));
                    }
                }

                return _commonPressedFillM;
            }
        }

        private static LinearGradientBrush CommonPressedFillHS
        {
            get
            {
                if (_commonPressedFillHS == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 238, 233, 213), 0.0),
                            new GradientStop(Color.FromArgb(255, 236, 228, 206), 1.0)
                        };

                        _commonPressedFillHS = CreateFrozenBrush(stops, new Point(0.5, 1.0), new Point(0.5, 0.0));
                    }
                }

                return _commonPressedFillHS;
            }
        }

        private static LinearGradientBrush CommonPressedBottomShade
        {
            get
            {
                if (_commonPressedBottomShade == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(0, 255, 255, 255), 0.6),
                            new GradientStop(Color.FromArgb(255, 255, 255, 255), 1.0)
                        };

                        _commonPressedBottomShade = CreateFrozenBrush(stops, new Point(0.5, 0.0), new Point(0.5, 1.0));
                    }
                }

                return _commonPressedBottomShade;
            }
        }

        private static Pen CommonOuterHighlight
        {
            get
            {
                if (_commonOuterHighlight == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(32, 0, 0, 0), 0.0),
                            new GradientStop(Color.FromArgb(0, 255, 255, 255), 0.5),
                            new GradientStop(Color.FromArgb(128, 255, 255, 255), 1.0)
                        };

                        _commonOuterHighlight = CreateFrozenPen(stops, new Point(0.0, 0.0), new Point(0.4, 1.0), 1.33);
                    }
                }

                return _commonOuterHighlight;
            }
        }

        private static Pen CommonHoverInnerHighlightNC
        {
            get
            {
                if (_commonHoverInnerHighlightNC == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 255, 240, 207), 0.0),
                            new GradientStop(Color.FromArgb(255, 252, 210, 121), 0.03),
                            new GradientStop(Color.FromArgb(255, 248, 183, 59), 0.75),
                            new GradientStop(Color.FromArgb(255, 229, 151, 0), 1.0)
                        };

                        _commonHoverInnerHighlightNC = CreateFrozenPen(stops, new Point(0.5, 0.0), new Point(0.5, 1.0), 2.66);
                    }
                }

                return _commonHoverInnerHighlightNC;
            }
        }

        private static Pen CommonHoverInnerHighlightM
        {
            get
            {
                if (_commonHoverInnerHighlightM == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 255, 240, 207), 0.0),
                            new GradientStop(Color.FromArgb(255, 252, 210, 121), 0.03),
                            new GradientStop(Color.FromArgb(255, 248, 183, 59), 0.75),
                            new GradientStop(Color.FromArgb(255, 229, 151, 0), 1.0)
                        };

                        _commonHoverInnerHighlightM = CreateFrozenPen(stops, new Point(0.5, 0.0), new Point(0.5, 1.0), 2.66);
                    }
                }

                return _commonHoverInnerHighlightM;
            }
        }

        private static Pen CommonHoverInnerHighlightHS
        {
            get
            {
                if (_commonHoverInnerHighlightHS == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 252, 197, 149), 0.0),
                            new GradientStop(Color.FromArgb(255, 237, 189, 150), 0.03),
                            new GradientStop(Color.FromArgb(255, 227, 145, 79), 0.97),
                            new GradientStop(Color.FromArgb(255, 207, 114, 37), 1.0)
                        };

                        _commonHoverInnerHighlightHS = CreateFrozenPen(stops, new Point(0.5, 0.0), new Point(0.5, 1.0), 2.66);
                    }
                }

                return _commonHoverInnerHighlightHS;
            }
        }

        private static LinearGradientBrush CommonHoverFillM
        {
            get
            {
                if (_commonHoverFillM == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 255, 255, 255), 0.0),
                            new GradientStop(Color.FromArgb(255, 227, 229, 240), 0.7),
                            new GradientStop(Color.FromArgb(255, 198, 197, 215), 1.0)
                        };

                        _commonHoverFillM = CreateFrozenBrush(stops, new Point(0.5, 0.0), new Point(0.5, 1.0));
                    }
                }

                return _commonHoverFillM;
            }
        }

        private static SolidColorBrush CommonDisabledFillNC
        {
            get
            {
                if (_commonDisabledFillNC == null)
                {
                    lock (locker)
                    {
                        _commonDisabledFillNC = CreateFrozenBrush(Color.FromArgb(255, 245, 244, 234));
                    }
                }

                return _commonDisabledFillNC;
            }
        }

        private static SolidColorBrush CommonDisabledFillM
        {
            get
            {
                if (_commonDisabledFillM == null)
                {
                    lock (locker)
                    {
                        _commonDisabledFillM = CreateFrozenBrush(Color.FromArgb(255, 241, 241, 237));
                    }
                }

                return _commonDisabledFillM;
            }
        }

        private static SolidColorBrush CommonDisabledFillHS
        {
            get
            {
                if (_commonDisabledFillHS == null)
                {
                    lock (locker)
                    {
                        _commonDisabledFillHS = CreateFrozenBrush(Color.FromArgb(255, 246, 242, 233));
                    }
                }

                return _commonDisabledFillHS;
            }
        }

        private static Pen CommonDisabledBorderPenNC
        {
            get
            {
                if (_commonDisabledBorderPenNC == null)
                {
                    lock (locker)
                    {
                        if (_commonDisabledBorderPenNC == null)
                        {
                            _commonDisabledBorderPenNC = CreateFrozenPen(Color.FromArgb(255, 201, 199, 186), 1.0);
                        }
                    }
                }

                return _commonDisabledBorderPenNC;
            }
        }

        private static Pen CommonDisabledBorderPenM
        {
            get
            {
                if (_commonDisabledBorderPenM == null)
                {
                    lock (locker)
                    {
                        _commonDisabledBorderPenM = CreateFrozenPen(Color.FromArgb(255, 196, 195, 191), 1.0);
                    }
                }

                return _commonDisabledBorderPenM;
            }
        }

        private static Pen CommonDisabledBorderPenHS
        {
            get
            {
                if (_commonDisabledBorderPenHS == null)
                {
                    lock (locker)
                    {
                        _commonDisabledBorderPenHS = CreateFrozenPen(Color.FromArgb(255, 202, 196, 184), 1.0);
                    }
                }

                return _commonDisabledBorderPenHS;
            }
        }

        private static Pen CommonDefaultedInnerHighlightNC
        {
            get
            {
                if (_commonDefaultedInnerHighlightNC == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 206, 231, 255), 0.0),
                            new GradientStop(Color.FromArgb(255, 188, 212, 246), 0.3),
                            new GradientStop(Color.FromArgb(255, 137, 173, 228), 0.97),
                            new GradientStop(Color.FromArgb(255, 105, 130, 238), 1.0)
                        };

                        _commonDefaultedInnerHighlightNC = CreateFrozenPen(stops, new Point(0.5, 0.0), new Point(0.5, 1.0), 2.66);
                    }
                }
                return _commonDefaultedInnerHighlightNC;
            }
        }

        private static Pen CommonDefaultedInnerHighlightM
        {
            get
            {
                if (_commonDefaultedInnerHighlightM == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 206, 231, 255), 0.0),
                            new GradientStop(Color.FromArgb(255, 188, 212, 246), 0.3),
                            new GradientStop(Color.FromArgb(255, 137, 173, 228), 0.97),
                            new GradientStop(Color.FromArgb(255, 105, 130, 238), 1.0)
                        };

                        _commonDefaultedInnerHighlightM = CreateFrozenPen(stops, new Point(0.5, 0.0), new Point(0.5, 1.0), 2.66);
                    }
                }

                return _commonDefaultedInnerHighlightM;
            }
        }

        private static Pen CommonDefaultedInnerHighlightHS
        {
            get
            {
                if (_commonDefaultedInnerHighlightHS == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(255, 194, 209, 143), 0.0),
                            new GradientStop(Color.FromArgb(255, 177, 203, 128), 0.3),
                            new GradientStop(Color.FromArgb(255, 144, 193, 84), 0.97),
                            new GradientStop(Color.FromArgb(255, 168, 167, 102), 1.0)
                        };

                        _commonDefaultedInnerHighlightHS = CreateFrozenPen(stops, new Point(0.5, 0.0), new Point(0.5, 1.0), 2.66);
                    }
                }

                return _commonDefaultedInnerHighlightHS;
            }
        }

        private static LinearGradientBrush CommonBottomShadeNC
        {
            get
            {
                if (_commonBottomShadeNC == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(0, 255, 255, 255), 0.5),
                            new GradientStop(Color.FromArgb(53, 89, 47, 0), 1.0)
                        };

                        _commonBottomShadeNC = CreateFrozenBrush(stops, new Point(0.5, 0.0), new Point(0.5, 1.0));
                    }
                }

                return _commonBottomShadeNC;
            }
        }

        private static LinearGradientBrush CommonBottomShadeM
        {
            get
            {
                if (_commonBottomShadeM == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(0, 227, 209, 184), 0.5),
                            new GradientStop(Color.FromArgb(255, 227, 209, 184), 1.0)
                        };

                        _commonBottomShadeM = CreateFrozenBrush(stops, new Point(0.5, 0.0), new Point(0.5, 1.0));
                    }
                }

                return _commonBottomShadeM;
            }
        }

        private static LinearGradientBrush CommonBottomShadeHS
        {
            get
            {
                if (_commonBottomShadeHS == null)
                {
                    lock (locker)
                    {
                        var stops = new[]
                        {
                            new GradientStop(Color.FromArgb(0, 255, 255, 255), 0.5),
                            new GradientStop(Color.FromArgb(255, 227, 209, 184), 1.0)
                        };

                        _commonBottomShadeHS = CreateFrozenBrush(stops, new Point(0.5, 0.0), new Point(0.5, 1.0));
                    }
                }

                return _commonBottomShadeHS;
            }
        }

        private Pen BorderPen
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return GetDisabledBorderBrush();
                }

                var borderBrush = this.BorderBrush;
                if (this.BorderBrush != null)
                {
                    if (_commonBorderPen == null)
                        borderBrush = CreateCommonBorderPen(borderBrush);

                    if (_commonBorderPen != null && borderBrush == _commonBorderPen.Brush)
                        return _commonBorderPen;
                    
                    if (!borderBrush.IsFrozen && borderBrush.CanFreeze)
                    {
                        borderBrush = borderBrush.Clone();
                        borderBrush.Freeze();
                    }

                    return CreateFrozenPen(borderBrush, 1.0);
                }

                return null;
            }
        }

        private Pen GetDisabledBorderBrush()
        {
            if (this.ThemeColor == ThemeColor.NormalColor)
                return CommonDisabledBorderPenNC;
                    
            if (this.ThemeColor == ThemeColor.Homestead)
                return CommonDisabledBorderPenHS;
                    
            return CommonDisabledBorderPenM;
        }

        private Brush CreateCommonBorderPen(Brush borderBrush)
        {
            lock (locker)
            {
                if (_commonBorderPen == null)
                {
                    if (!borderBrush.IsFrozen && borderBrush.CanFreeze)
                    {
                        borderBrush = borderBrush.Clone();
                        borderBrush.Freeze();
                    }

                    var p = new Pen(borderBrush, 1.0);
                    if (p.CanFreeze)
                    {
                        p.Freeze();
                        _commonBorderPen = p;
                    }
                }
            }
            return borderBrush;
        }

        private Brush BackgroundBrush
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return GetDisabledFillBrush();
                }

                if (this.RenderPressed)
                {
                    return GetPressedFillBrush();
                }

                if (this.RenderMouseOver && ThemeColor == ThemeColor.Metallic)
                {
                    return CommonHoverFillM;
                }

                return this.Fill;
            }
        }

        private Brush GetPressedFillBrush()
        {
            switch (ThemeColor)
            {
                case ThemeColor.NormalColor:
                    return CommonPressedFillNC;

                case ThemeColor.Homestead:
                    return CommonPressedFillHS;

                case ThemeColor.Metallic:
                    return CommonPressedFillM;
            }

            return null;
        }

        private Brush GetDisabledFillBrush()
        {
            if (this.ThemeColor == ThemeColor.NormalColor)
                return CommonDisabledFillNC;
                    
            if (this.ThemeColor == ThemeColor.Homestead)
                return CommonDisabledFillHS;
                    
            return CommonDisabledFillM;
        }

        #endregion

    }
}