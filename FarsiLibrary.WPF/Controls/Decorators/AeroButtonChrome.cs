using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FarsiLibrary.WPF.Controls.Decorators
{
    public class AeroButtonChrome : ButtonChrome
    {
        #region Fields
        
        private static Pen _commonBorderPen;
        private static Pen _commonDefaultedInnerBorderPen;
        private static Pen _commonDisabledBorderOverlay;
        private static Pen _commonHoverBorderOverlay;
        private static Pen _commonInnerBorderPen;
        private static Pen _commonPressedBorderOverlay;
        private static LinearGradientBrush _commonPressedBackgroundOverlay;
        private static LinearGradientBrush _commonPressedLeftDropShadowBrush;
        private static LinearGradientBrush _commonPressedTopDropShadowBrush;
        private static LinearGradientBrush _commonHoverBackgroundOverlay;
        private static SolidColorBrush _commonDisabledBackgroundOverlay;

        private OverlayResources _localResources;
        private static object _resourceAccess = new object();
        
        #endregion

        #region Overlay Resources

        public class OverlayResources
        {
            public LinearGradientBrush BackgroundOverlay;

            public Pen BorderOverlayPen;

            public Pen InnerBorderPen;

            public LinearGradientBrush LeftDropShadowBrush;

            public LinearGradientBrush TopDropShadowBrush;

        }

        #endregion

        #region Coerce

        protected override void OnRenderDefaultedChanged(ButtonChrome d, DependencyPropertyChangedEventArgs e)
        {
            var chrome = (AeroButtonChrome)d;

            if (chrome.Animates)
            {
                if (!chrome.RenderPressed)
                {
                    if ((bool)e.NewValue)
                    {
                        if (chrome._localResources == null)
                        {
                            chrome._localResources = new OverlayResources();
                            chrome.InvalidateVisual();
                        }
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.3));
                        ColorAnimation animation = new ColorAnimation(Color.FromArgb(249, 0, 204, 255), duration);
                        GradientStopCollection gradientStops = ((LinearGradientBrush)chrome.InnerBorderPen.Brush).GradientStops;
                        gradientStops[0].BeginAnimation(GradientStop.ColorProperty, animation);
                        gradientStops[1].BeginAnimation(GradientStop.ColorProperty, animation);
                        DoubleAnimationUsingKeyFrames timeline = new DoubleAnimationUsingKeyFrames();
                        timeline.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, TimeSpan.FromSeconds(0.5)));
                        timeline.KeyFrames.Add(new DiscreteDoubleKeyFrame(1.0, TimeSpan.FromSeconds(0.75)));
                        timeline.KeyFrames.Add(new LinearDoubleKeyFrame(0.0, TimeSpan.FromSeconds(2.0)));
                        timeline.RepeatBehavior = RepeatBehavior.Forever;
                        Timeline.SetDesiredFrameRate(timeline, 10);
                        chrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, timeline);
                        chrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, timeline);
                    }
                    else if (chrome._localResources == null)
                    {
                        chrome.InvalidateVisual();
                    }
                    else
                    {
                        Duration duration2 = new Duration(TimeSpan.FromSeconds(0.2));
                        DoubleAnimation animation2 = new DoubleAnimation();
                        animation2.Duration = duration2;
                        chrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation2);
                        chrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation2);
                        ColorAnimation animation3 = new ColorAnimation();
                        animation3.Duration = duration2;
                        GradientStopCollection stops2 = ((LinearGradientBrush)chrome.InnerBorderPen.Brush).GradientStops;
                        stops2[0].BeginAnimation(GradientStop.ColorProperty, animation3);
                        stops2[1].BeginAnimation(GradientStop.ColorProperty, animation3);
                    }
                }
            }
            else
            {
                chrome._localResources = null;
                chrome.InvalidateVisual();
            }
        }

        protected override void OnRenderMouseOverChanged(ButtonChrome d, DependencyPropertyChangedEventArgs e)
        {
            var chrome = (AeroButtonChrome)d;
            if (chrome.Animates)
            {
                if (!chrome.RenderPressed)
                {
                    if ((bool)e.NewValue)
                    {
                        if (chrome._localResources == null)
                        {
                            chrome._localResources = new OverlayResources();
                            chrome.InvalidateVisual();
                        }
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.3));
                        DoubleAnimation animation = new DoubleAnimation(1.0, duration);
                        chrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                        chrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation);
                    }
                    else if (chrome._localResources == null)
                    {
                        chrome.InvalidateVisual();
                    }
                    else if (chrome.RenderDefaulted)
                    {
                        double opacity = chrome.BackgroundOverlay.Opacity;
                        double num2 = (1.0 - opacity) * 0.5;
                        DoubleAnimationUsingKeyFrames timeline = new DoubleAnimationUsingKeyFrames();
                        timeline.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, TimeSpan.FromSeconds(num2)));
                        timeline.KeyFrames.Add(new DiscreteDoubleKeyFrame(1.0, TimeSpan.FromSeconds(num2 + 0.25)));
                        timeline.KeyFrames.Add(new LinearDoubleKeyFrame(0.0, TimeSpan.FromSeconds(num2 + 1.5)));
                        timeline.KeyFrames.Add(new LinearDoubleKeyFrame(opacity, TimeSpan.FromSeconds(2.0)));
                        timeline.RepeatBehavior = RepeatBehavior.Forever;
                        Timeline.SetDesiredFrameRate(timeline, 10);
                        chrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, timeline);
                        chrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, timeline);
                    }
                    else
                    {
                        Duration duration2 = new Duration(TimeSpan.FromSeconds(0.2));
                        DoubleAnimation animation2 = new DoubleAnimation();
                        animation2.Duration = duration2;
                        chrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation2);
                        chrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation2);
                    }
                }
            }
            else
            {
                chrome._localResources = null;
                chrome.InvalidateVisual();
            }
        }

        protected override void OnRenderPressedChanged(ButtonChrome d, DependencyPropertyChangedEventArgs e)
        {
            var chrome = (AeroButtonChrome)d;
            if (chrome.Animates)
            {
                if ((bool)e.NewValue)
                {
                    if (chrome._localResources == null)
                    {
                        chrome._localResources = new OverlayResources();
                        chrome.InvalidateVisual();
                    }
                    Duration duration = new Duration(TimeSpan.FromSeconds(0.1));
                    DoubleAnimation animation = new DoubleAnimation(1.0, duration);
                    chrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation);
                    chrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                    chrome.LeftDropShadowBrush.BeginAnimation(Brush.OpacityProperty, animation);
                    chrome.TopDropShadowBrush.BeginAnimation(Brush.OpacityProperty, animation);
                    animation = new DoubleAnimation(0.0, duration);
                    chrome.InnerBorderPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                    ColorAnimation animation2 = new ColorAnimation(Color.FromRgb(194, 228, 246), duration);
                    GradientStopCollection gradientStops = ((LinearGradientBrush)chrome.BackgroundOverlay).GradientStops;
                    gradientStops[0].BeginAnimation(GradientStop.ColorProperty, animation2);
                    gradientStops[1].BeginAnimation(GradientStop.ColorProperty, animation2);
                    animation2 = new ColorAnimation(Color.FromRgb(171, 218, 243), duration);
                    gradientStops[2].BeginAnimation(GradientStop.ColorProperty, animation2);
                    animation2 = new ColorAnimation(Color.FromRgb(144, 203, 235), duration);
                    gradientStops[3].BeginAnimation(GradientStop.ColorProperty, animation2);
                    animation2 = new ColorAnimation(Color.FromRgb(44, 98, 139), duration);
                    chrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.ColorProperty, animation2);
                }
                else if (chrome._localResources == null)
                {
                    chrome.InvalidateVisual();
                }
                else
                {
                    bool renderMouseOver = chrome.RenderMouseOver;
                    Duration duration2 = new Duration(TimeSpan.FromSeconds(0.1));
                    DoubleAnimation animation3 = new DoubleAnimation();
                    animation3.Duration = duration2;
                    chrome.LeftDropShadowBrush.BeginAnimation(Brush.OpacityProperty, animation3);
                    chrome.TopDropShadowBrush.BeginAnimation(Brush.OpacityProperty, animation3);
                    chrome.InnerBorderPen.Brush.BeginAnimation(Brush.OpacityProperty, animation3);
                    if (!renderMouseOver)
                    {
                        chrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation3);
                        chrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation3);
                    }
                    ColorAnimation animation4 = new ColorAnimation();
                    animation4.Duration = duration2;
                    chrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.ColorProperty, animation4);
                    GradientStopCollection stops2 = ((LinearGradientBrush)chrome.BackgroundOverlay).GradientStops;
                    stops2[0].BeginAnimation(GradientStop.ColorProperty, animation4);
                    stops2[1].BeginAnimation(GradientStop.ColorProperty, animation4);
                    stops2[2].BeginAnimation(GradientStop.ColorProperty, animation4);
                    stops2[3].BeginAnimation(GradientStop.ColorProperty, animation4);
                }
            }
            else
            {
                chrome._localResources = null;
                chrome.InvalidateVisual();
            }
        }

        #endregion

        #region Brushes & Pens

        private static Pen CommonDefaultedInnerBorderPen
        {
            get
            {
                if (_commonDefaultedInnerBorderPen == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonDefaultedInnerBorderPen == null)
                        {
                            var pen = new Pen
                            {
                                Thickness = 1.0,
                                Brush = new SolidColorBrush(Color.FromArgb(249, 0, 204, 255))
                            };
                            pen.Freeze();
                            _commonDefaultedInnerBorderPen = pen;
                        }
                    }
                }
                return _commonDefaultedInnerBorderPen;
            }
        }

        private static SolidColorBrush CommonDisabledBackgroundOverlay
        {
            get
            {
                if (_commonDisabledBackgroundOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonDisabledBackgroundOverlay == null)
                        {
                            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(244, 244, 244));
                            brush.Freeze();
                            _commonDisabledBackgroundOverlay = brush;
                        }
                    }
                }
                return _commonDisabledBackgroundOverlay;
            }
        }

        private static Pen CommonDisabledBorderOverlay
        {
            get
            {
                if (_commonDisabledBorderOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonDisabledBorderOverlay == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(173, 178, 181));
                            pen.Freeze();
                            _commonDisabledBorderOverlay = pen;
                        }
                    }
                }
                return _commonDisabledBorderOverlay;
            }
        }

        private static LinearGradientBrush CommonHoverBackgroundOverlay
        {
            get
            {
                if (_commonHoverBackgroundOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonHoverBackgroundOverlay == null)
                        {
                            LinearGradientBrush brush = new LinearGradientBrush();
                            brush.StartPoint = new Point(0.0, 0.0);
                            brush.EndPoint = new Point(0.0, 1.0);
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 234, 246, 253), 0.0));
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 217, 240, 252), 0.5));
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 190, 230, 253), 0.5));
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 167, 217, 245), 1.0));
                            brush.Freeze();
                            _commonHoverBackgroundOverlay = brush;
                        }
                    }
                }
                return _commonHoverBackgroundOverlay;
            }
        }

        private static Pen CommonHoverBorderOverlay
        {
            get
            {
                if (_commonHoverBorderOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonHoverBorderOverlay == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(60, 127, 177));
                            pen.Freeze();
                            _commonHoverBorderOverlay = pen;
                        }
                    }
                }
                return _commonHoverBorderOverlay;
            }
        }

        private static Pen CommonInnerBorderPen
        {
            get
            {
                if (_commonInnerBorderPen == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonInnerBorderPen == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            LinearGradientBrush brush = new LinearGradientBrush();
                            brush.StartPoint = new Point(0.0, 0.0);
                            brush.EndPoint = new Point(0.0, 1.0);
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(250, 255, 255, 255), 0.0));
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(133, 255, 255, 255), 1.0));
                            pen.Brush = brush;
                            pen.Freeze();
                            _commonInnerBorderPen = pen;
                        }
                    }
                }
                return _commonInnerBorderPen;
            }
        }

        private static LinearGradientBrush CommonPressedBackgroundOverlay
        {
            get
            {
                if (_commonPressedBackgroundOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonPressedBackgroundOverlay == null)
                        {
                            LinearGradientBrush brush = new LinearGradientBrush();
                            brush.StartPoint = new Point(0.0, 0.0);
                            brush.EndPoint = new Point(0.0, 1.0);
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 194, 228, 246), 0.5));
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 171, 218, 243), 0.5));
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 144, 203, 235), 1.0));
                            brush.Freeze();
                            _commonPressedBackgroundOverlay = brush;
                        }
                    }
                }
                return _commonPressedBackgroundOverlay;
            }
        }

        private static Pen CommonPressedBorderOverlay
        {
            get
            {
                if (_commonPressedBorderOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonPressedBorderOverlay == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(44, 98, 139));
                            pen.Freeze();
                            _commonPressedBorderOverlay = pen;
                        }
                    }
                }
                return _commonPressedBorderOverlay;
            }
        }

        private static LinearGradientBrush CommonPressedLeftDropShadowBrush
        {
            get
            {
                if (_commonPressedLeftDropShadowBrush == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonPressedLeftDropShadowBrush == null)
                        {
                            LinearGradientBrush brush = new LinearGradientBrush();
                            brush.StartPoint = new Point(0.0, 0.0);
                            brush.EndPoint = new Point(1.0, 0.0);
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(128, 51, 51, 51), 0.0));
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(0, 51, 51, 51), 1.0));
                            brush.Freeze();
                            _commonPressedLeftDropShadowBrush = brush;
                        }
                    }
                }
                return _commonPressedLeftDropShadowBrush;
            }
        }

        private static LinearGradientBrush CommonPressedTopDropShadowBrush
        {
            get
            {
                if (_commonPressedTopDropShadowBrush == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonPressedTopDropShadowBrush == null)
                        {
                            LinearGradientBrush brush = new LinearGradientBrush();
                            brush.StartPoint = new Point(0.0, 0.0);
                            brush.EndPoint = new Point(0.0, 1.0);
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(128, 51, 51, 51), 0.0));
                            brush.GradientStops.Add(new GradientStop(Color.FromArgb(0, 51, 51, 51), 1.0));
                            brush.Freeze();
                            _commonPressedTopDropShadowBrush = brush;
                        }
                    }
                }
                return _commonPressedTopDropShadowBrush;
            }
        }

        private Pen InnerBorderPen
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return CommonInnerBorderPen;
                }
                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return null;
                    }
                    if (this.RenderDefaulted)
                    {
                        return CommonDefaultedInnerBorderPen;
                    }
                    return CommonInnerBorderPen;
                }
                if (this._localResources == null)
                {
                    return CommonInnerBorderPen;
                }
                if (this._localResources.InnerBorderPen == null)
                {
                    this._localResources.InnerBorderPen = CommonInnerBorderPen.Clone();
                }
                return this._localResources.InnerBorderPen;
            }
        }

        private LinearGradientBrush LeftDropShadowBrush
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return null;
                }
                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return CommonPressedLeftDropShadowBrush;
                    }
                    return null;
                }
                if (this._localResources == null)
                {
                    return null;
                }
                if (this._localResources.LeftDropShadowBrush == null)
                {
                    this._localResources.LeftDropShadowBrush = CommonPressedLeftDropShadowBrush.Clone();
                    this._localResources.LeftDropShadowBrush.Opacity = 0.0;
                }
                return this._localResources.LeftDropShadowBrush;
            }
        }

        private LinearGradientBrush TopDropShadowBrush
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return null;
                }
                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return CommonPressedTopDropShadowBrush;
                    }
                    return null;
                }
                if (this._localResources == null)
                {
                    return null;
                }
                if (this._localResources.TopDropShadowBrush == null)
                {
                    this._localResources.TopDropShadowBrush = CommonPressedTopDropShadowBrush.Clone();
                    this._localResources.TopDropShadowBrush.Opacity = 0.0;
                }
                return this._localResources.TopDropShadowBrush;
            }
        }

        private Brush BackgroundOverlay
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return CommonDisabledBackgroundOverlay;
                }

                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return CommonPressedBackgroundOverlay;
                    }

                    if (this.RenderMouseOver)
                    {
                        return CommonHoverBackgroundOverlay;
                    }
                    return null;
                }

                if (this._localResources == null)
                {
                    return null;
                }

                if (this._localResources.BackgroundOverlay == null)
                {
                    this._localResources.BackgroundOverlay = CommonHoverBackgroundOverlay.Clone();
                    this._localResources.BackgroundOverlay.Opacity = 0.0;
                }

                return this._localResources.BackgroundOverlay;
            }
        }

        private Pen BorderOverlayPen
        {
            get
            {
                if (!base.IsEnabled)
                {
                    if (this.RoundCorners)
                    {
                        return CommonDisabledBorderOverlay;
                    }
                    return null;
                }
                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return CommonPressedBorderOverlay;
                    }
                    if (this.RenderMouseOver)
                    {
                        return CommonHoverBorderOverlay;
                    }
                    return null;
                }
                if (this._localResources == null)
                {
                    return null;
                }
                if (this._localResources.BorderOverlayPen == null)
                {
                    this._localResources.BorderOverlayPen = CommonHoverBorderOverlay.Clone();
                    this._localResources.BorderOverlayPen.Brush.Opacity = 0.0;
                }
                return this._localResources.BorderOverlayPen;
            }
        }

        public bool Animates
        {
            get
            {
                return SystemParameters.PowerLineStatus == PowerLineStatus.Online &&
                       SystemParameters.ClientAreaAnimation &&
                       RenderCapability.Tier > 0 &&
                       base.IsEnabled;
            }
        } 

        #endregion

        #region Rendering

        protected override void DrawBackground(DrawingContext dc, Rect bounds)
        {
            if (!base.IsEnabled && !this.RoundCorners) 
                return;

            if (bounds.Width <= SmallestWidthHeight || bounds.Height <= SmallestWidthHeight)
                return;

            var rectangle = new Rect(bounds.Left + 1.0, bounds.Top + 1.0, bounds.Width - 2.0, bounds.Height - 2.0);

            if (Background != null)
            {
                dc.DrawRectangle(this.Background, null, rectangle);
            }

            if (BackgroundOverlay != null)
            {
                dc.DrawRectangle(BackgroundOverlay, null, rectangle);
            }
        }

        protected override void DrawDropShadows(DrawingContext dc, Rect bounds)
        {
            if (bounds.Width <= SmallestWidthHeight || bounds.Height <= SmallestWidthHeight) 
                return;

            if (LeftDropShadowBrush != null)
            {
                dc.DrawRectangle(LeftDropShadowBrush, null, new Rect(1.0, 1.0, 2.0, bounds.Bottom - 2.0));
            }

            if (TopDropShadowBrush != null)
            {
                dc.DrawRectangle(TopDropShadowBrush, null, new Rect(1.0, 1.0, bounds.Right - 2.0, 2.0));
            }
        }

        protected override void DrawInnerBorder(DrawingContext dc, Rect bounds)
        {
            if (base.IsEnabled || this.RoundCorners)
                return;
                
            if (bounds.Width >= 4.0 && bounds.Height >= 4.0)
                return;

            if (InnerBorderPen != null)
            {
                dc.DrawRoundedRectangle(null, InnerBorderPen, new Rect(bounds.Left + 1.5, bounds.Top + 1.5, bounds.Width - 3.0, bounds.Height - 3.0), 1.75, 1.75);
            }
        }

        protected override void DrawBorder(DrawingContext dc, Rect bounds)
        {
            if (bounds.Width >= 5.0 && bounds.Height >= 5.0)
            {
                var borderBrush = this.BorderBrush;
                Pen pen = null;
                if (borderBrush != null)
                {
                    if (_commonBorderPen == null)
                    {
                        lock (_resourceAccess)
                        {
                            if (_commonBorderPen == null)
                            {
                                if (!borderBrush.IsFrozen && borderBrush.CanFreeze)
                                {
                                    borderBrush = borderBrush.Clone();
                                    borderBrush.Freeze();
                                }
                                Pen pen2 = new Pen(borderBrush, 1.0);
                                if (pen2.CanFreeze)
                                {
                                    pen2.Freeze();
                                    _commonBorderPen = pen2;
                                }
                            }
                        }
                    }
                    if ((_commonBorderPen != null) && (borderBrush == _commonBorderPen.Brush))
                    {
                        pen = _commonBorderPen;
                    }
                    else
                    {
                        if (!borderBrush.IsFrozen && borderBrush.CanFreeze)
                        {
                            borderBrush = borderBrush.Clone();
                            borderBrush.Freeze();
                        }
                        pen = new Pen(borderBrush, 1.0);
                        if (pen.CanFreeze)
                        {
                            pen.Freeze();
                        }
                    }
                }

                Pen borderOverlayPen = this.BorderOverlayPen;
                if (pen != null || borderOverlayPen != null)
                {
                    if (this.RoundCorners)
                    {
                        Rect rectangle = new Rect(bounds.Left + 0.5, bounds.Top + 0.5, bounds.Width - 1.0, bounds.Height - 1.0);
                        if (base.IsEnabled && (pen != null))
                        {
                            dc.DrawRoundedRectangle(null, pen, rectangle, 2.75, 2.75);
                        }
                        if (borderOverlayPen != null)
                        {
                            dc.DrawRoundedRectangle(null, borderOverlayPen, rectangle, 2.75, 2.75);
                        }
                    }
                    else
                    {
                        var figure = new PathFigure { StartPoint = new Point(0.5, 0.5) };

                        figure.Segments.Add(new LineSegment(new Point(0.5, bounds.Bottom - 0.5), true));
                        figure.Segments.Add(new LineSegment(new Point(bounds.Right - 2.5, bounds.Bottom - 0.5), true));
                        figure.Segments.Add(new ArcSegment(new Point(bounds.Right - 0.5, bounds.Bottom - 2.5), new Size(2.0, 2.0), 0.0, false, SweepDirection.Counterclockwise, true));
                        figure.Segments.Add(new LineSegment(new Point(bounds.Right - 0.5, bounds.Top + 2.5), true));
                        figure.Segments.Add(new ArcSegment(new Point(bounds.Right - 2.5, bounds.Top + 0.5), new Size(2.0, 2.0), 0.0, false, SweepDirection.Counterclockwise, true));
                        figure.IsClosed = true;

                        var geometry = new PathGeometry();
                        geometry.Figures.Add(figure);

                        if (base.IsEnabled && pen != null)
                        {
                            dc.DrawGeometry(null, pen, geometry);
                        }

                        if (borderOverlayPen != null)
                        {
                            dc.DrawGeometry(null, borderOverlayPen, geometry);
                        }
                    }
                }
            }
        }

        #endregion
    }
}