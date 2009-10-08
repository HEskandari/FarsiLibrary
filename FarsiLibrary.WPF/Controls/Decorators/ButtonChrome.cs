using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FarsiLibrary.WPF.Controls.Decorators
{
    public abstract class ButtonChrome : Decorator
    {
        #region Dependency Properties

        public static readonly DependencyProperty BackgroundProperty = Control.BackgroundProperty.AddOwner(typeof(ButtonChrome), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty BorderBrushProperty = Border.BorderBrushProperty.AddOwner(typeof(ButtonChrome), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty RenderDefaultedProperty = DependencyProperty.Register("RenderDefaulted", typeof(bool), typeof(ButtonChrome), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(RenderDefaultedChanged)));
        public static readonly DependencyProperty RenderMouseOverProperty = DependencyProperty.Register("RenderMouseOver", typeof(bool), typeof(ButtonChrome), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(RenderMouseOverChanged)));
        public static readonly DependencyProperty RenderPressedProperty = DependencyProperty.Register("RenderPressed", typeof(bool), typeof(ButtonChrome), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(RenderPressedChanged)));
        public static readonly DependencyProperty RoundCornersProperty = DependencyProperty.Register("RoundCorners", typeof(bool), typeof(ButtonChrome), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region Ctor

        static ButtonChrome()
        {
            IsEnabledProperty.OverrideMetadata(typeof(ButtonChrome), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        #endregion

        #region Props

        public bool RenderDefaulted
        {
            get { return (bool)base.GetValue(RenderDefaultedProperty); }
            set { base.SetValue(RenderDefaultedProperty, value); }
        }

        public bool RenderMouseOver
        {
            get { return (bool)base.GetValue(RenderMouseOverProperty); }
            set { base.SetValue(RenderMouseOverProperty, value); }
        }

        public bool RenderPressed
        {
            get { return (bool)base.GetValue(RenderPressedProperty); }
            set { base.SetValue(RenderPressedProperty, value); }
        }

        public bool RoundCorners
        {
            get { return (bool)base.GetValue(RoundCornersProperty); }
            set { base.SetValue(RoundCornersProperty, value); }
        }

        public Brush Background
        {
            get { return (Brush)base.GetValue(BackgroundProperty); }
            set { base.SetValue(BackgroundProperty, value); }
        }

        public Brush BorderBrush
        {
            get { return (Brush)base.GetValue(BorderBrushProperty); }
            set { base.SetValue(BorderBrushProperty, value); }
        }

        #endregion

        #region Coerce Methods

        private static void RenderDefaultedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chrome = (ButtonChrome)d;
            chrome.OnRenderDefaultedChanged(chrome, e);
        }

        private static void RenderMouseOverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chrome = (ButtonChrome)d;
            chrome.OnRenderMouseOverChanged(chrome, e);
        }

        private static void RenderPressedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chrome = (ButtonChrome)d;
            chrome.OnRenderPressedChanged(chrome, e);
        }

        protected virtual void OnRenderDefaultedChanged(ButtonChrome chrome, DependencyPropertyChangedEventArgs args)
        {
        }

        protected virtual void OnRenderMouseOverChanged(ButtonChrome chrome, DependencyPropertyChangedEventArgs args)
        {
        }

        protected virtual void OnRenderPressedChanged(ButtonChrome chrome, DependencyPropertyChangedEventArgs args)
        {
        }

        #endregion

        #region Sizing

        protected virtual double SmallestWidthHeight
        {
            get { return 4.0; }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var finalRect = new Rect
            {
                Width = Math.Max(0.0, finalSize.Width - SmallestWidthHeight),
                Height = Math.Max(0.0, finalSize.Height - SmallestWidthHeight)
            };

            finalRect.X = (finalSize.Width - finalRect.Width) * 0.5;
            finalRect.Y = (finalSize.Height - finalRect.Height) * 0.5;

            var child = this.Child;
            if (child != null)
            {
                child.Arrange(finalRect);
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var child = this.Child;
            if (child != null)
            {
                var size = new Size();

                var smallWidth = availableSize.Width < SmallestWidthHeight;
                var smallHeight = availableSize.Height < SmallestWidthHeight;

                if (!smallWidth)
                    size.Width = availableSize.Width - SmallestWidthHeight;

                if (!smallHeight)
                    size.Height = availableSize.Height - SmallestWidthHeight;

                child.Measure(size);
                var desiredSize = child.DesiredSize;

                if (!smallWidth)
                {
                    desiredSize.Width += SmallestWidthHeight;
                }

                if (!smallHeight)
                {
                    desiredSize.Height += SmallestWidthHeight;
                }

                return desiredSize;
            }

            return new Size(Math.Min(SmallestWidthHeight, availableSize.Width), Math.Min(SmallestWidthHeight, availableSize.Height));
        }

        #endregion

        #region Rendering

        protected override void OnRender(DrawingContext drawingContext)
        {
            var bounds = new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight);

            this.DrawBackground(drawingContext, bounds);
            this.DrawShades(drawingContext, bounds);
            this.DrawDropShadows(drawingContext, bounds);
            this.DrawInnerHighlight(drawingContext, bounds);
            this.DrawBorder(drawingContext, bounds);
            this.DrawInnerBorder(drawingContext, bounds);
        }

        protected virtual void DrawBackground(DrawingContext drawingContext, Rect bounds)
        {
        }

        protected virtual void DrawShades(DrawingContext drawingContext, Rect bounds)
        {
        }

        protected virtual void DrawDropShadows(DrawingContext drawingContext, Rect bounds)
        {
        }

        protected virtual void DrawInnerHighlight(DrawingContext drawingContext, Rect bounds)
        {
        }

        protected virtual void DrawBorder(DrawingContext drawingContext, Rect bounds)
        {
        }

        protected virtual void DrawInnerBorder(DrawingContext drawingContext, Rect bounds)
        {
        }

        #endregion

        #region Create Brush

        protected static LinearGradientBrush CreateFrozenBrush(GradientStopCollection stops, Point start, Point end)
        {
            var brush = new LinearGradientBrush(stops, start, end);

            brush.Freeze();

            return brush;
        }

        protected static LinearGradientBrush CreateFrozenBrush(GradientStop[] stops, Point start, Point end)
        {
            return CreateFrozenBrush(new GradientStopCollection(stops), start, end);
        }

        protected static SolidColorBrush CreateFrozenBrush(Color color)
        {
            var brush = new SolidColorBrush(color);
            brush.Freeze();

            return brush;
        }

        protected static Pen CreateFrozenPen(Brush brush, double thickness)
        {
            var pen = new Pen(brush, thickness);
            if(pen.CanFreeze)
            {
                pen.Freeze();
            }

            return pen;
        }

        protected static Pen CreateFrozenPen(Color color, double thickness)
        {
            return CreateFrozenPen(new SolidColorBrush(color), thickness);
        }

        protected static Pen CreateFrozenPen(GradientStop[] stops, Point start, Point end, double thickness)
        {
            return CreateFrozenPen(new GradientStopCollection(stops), start, end, thickness);
        }

        protected static Pen CreateFrozenPen(GradientStopCollection stops, Point start, Point end, double thickness)
        {
            var brush = new LinearGradientBrush(stops, start, end);
            var pen = new Pen(brush, thickness);
            
            pen.Freeze();

            return pen;
        }

        protected static RadialGradientBrush CreateFrozenBrush(GradientStopCollection stops, double radX, double radY, Point center, Point origin)
        {
            var brush = new RadialGradientBrush(stops)
            {
                RadiusX = radX,
                RadiusY = radY,
                Center = center,
                GradientOrigin = origin
            };

            brush.Freeze();

            return brush;
        }

        #endregion

    }
}