using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FarsiLibrary.WPF.Controls.Decorators
{
    public class SystemDropShadowChrome : Decorator
    {
        #region Fields
        
        private Brush[] brushes;
        private static Brush[] defaultBrushes;
        private static CornerRadius defaultCornerRadius;
        private static object locker = new object();

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(SystemDropShadowChrome), new FrameworkPropertyMetadata(Color.FromArgb(113, 0, 0, 0), FrameworkPropertyMetadataOptions.AffectsRender, ClearBrushes));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SystemDropShadowChrome), new FrameworkPropertyMetadata(new CornerRadius(), FrameworkPropertyMetadataOptions.AffectsRender, ClearBrushes), IsCornerRadiusValid);

        #endregion
        
        #region Consts

        private const int Left = 3;
        private const int Right = 5;
        private const int Top = 1;
        private const int TopLeft = 0;
        private const int TopRight = 2;
        private const int Bottom = 7;
        private const int BottomLeft = 6;
        private const int BottomRight = 8;
        private const int Center = 4;
        private const double ShadowDepth = 5.0;

        #endregion

        #region Props

        public Color Color
        {
            get { return (Color)base.GetValue(ColorProperty); }
            set { base.SetValue(ColorProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)base.GetValue(CornerRadiusProperty); }
            set { base.SetValue(CornerRadiusProperty, value); }
        } 

        #endregion

        #region Coerce

        private static void ClearBrushes(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((SystemDropShadowChrome)o).brushes = null;
        }

        private static bool IsCornerRadiusValid(object value)
        {
            var radius = (CornerRadius)value;

            return radius.TopLeft >= 0.0 && 
                   radius.TopRight >= 0.0 && 
                   radius.BottomLeft >= 0.0 && 
                   radius.BottomRight >= 0.0 && 
                   !double.IsNaN(radius.TopLeft) && 
                   !double.IsNaN(radius.TopRight) &&
                   !double.IsNaN(radius.BottomLeft) && 
                   !double.IsNaN(radius.BottomRight) &&
                   !double.IsInfinity(radius.TopLeft) && 
                   !double.IsInfinity(radius.TopRight) && 
                   !double.IsInfinity(radius.BottomLeft) && 
                   !double.IsInfinity(radius.BottomRight);
        }

        #endregion

        #region Methods

        private Brush[] GetBrushes(Color c, CornerRadius cornerRadius)
        {
            if (defaultBrushes == null)
            {
                lock (locker)
                {
                    if (defaultBrushes == null)
                    {
                        defaultBrushes = CreateBrushes(c, cornerRadius);
                        defaultCornerRadius = cornerRadius;
                    }
                }
            }

            if (c == CenterBrush.Color && cornerRadius == defaultCornerRadius)
            {
                return defaultBrushes;
            }

            this.brushes = CreateBrushes(c, cornerRadius);
         
            return brushes;
        }

        private SolidColorBrush CenterBrush
        {
            get { return (SolidColorBrush) defaultBrushes[4]; }
        }

        private Brush[] CreateBrushes(Color c, CornerRadius cornerRadius)
        {
            var defaultStops = CreateStops(c, 0.0);
            var stTopLeft = CreateStops(c, cornerRadius.TopLeft);
            var stTopRight = CreateStops(c, cornerRadius.TopRight);
            var stBottomLeft = CreateStops(c, cornerRadius.BottomLeft);
            var stBottomRight = CreateStops(c, cornerRadius.BottomRight);

            var brushArray = new Brush[]
            {
                CreateFrozenBrush(stTopLeft, 1.0, 1.0, new Point(1.0, 1.0), new Point(1.0, 1.0)),
                CreateFrozenBrush(defaultStops, new Point(0.0, 1.0), new Point(0.0, 0.0)),
                CreateFrozenBrush(stTopRight, 1.0, 1.0, new Point(0.0, 1.0), new Point(0.0, 1.0)),
                CreateFrozenBrush(defaultStops, new Point(1.0, 0.0), new Point(0.0, 0.0)),
                CreateFrozenBrush(c),
                CreateFrozenBrush(defaultStops, new Point(0.0, 0.0), new Point(1.0, 0.0)),
                CreateFrozenBrush(stBottomLeft, 1.0, 1.0, new Point(1.0, 0.0), new Point(1.0, 0.0)),
                CreateFrozenBrush(defaultStops, new Point(0.0, 0.0), new Point(0.0, 1.0)),
                CreateFrozenBrush(stBottomRight, 1.0, 1.0, new Point(0.0, 0.0), new Point(0.0, 0.0))
            };

            return brushArray;
        }

        private SolidColorBrush CreateFrozenBrush(Color color)
        {
            var solidBrush = new SolidColorBrush(color);
            solidBrush.Freeze();

            return solidBrush;
        }

        private GradientStopCollection CreateStops(Color c, double cornerRadius)
        {
            double stopPoint = 1.0 / (cornerRadius + ShadowDepth);

            var stops = new GradientStopCollection
            {
                new GradientStop(c, (ShadowDepth + cornerRadius) * stopPoint)
            };

            Color color = c;

            color.A = (byte)(0.74336 * c.A);
            stops.Add(new GradientStop(color, (1.5 + cornerRadius) * stopPoint));

            color.A = (byte)(0.38053 * c.A);
            stops.Add(new GradientStop(color, (2.5 + cornerRadius) * stopPoint));

            color.A = (byte)(0.12389 * c.A);
            stops.Add(new GradientStop(color, (3.5 + cornerRadius) * stopPoint));

            color.A = (byte)(0.02654 * c.A);
            stops.Add(new GradientStop(color, (4.5 + cornerRadius) * stopPoint));

            color.A = 0;
            stops.Add(new GradientStop(color, (5.0 + cornerRadius) * stopPoint));

            stops.Freeze();

            return stops;
        }

        private LinearGradientBrush CreateFrozenBrush(GradientStopCollection stops, Point start, Point end)
        {
            var brush = new LinearGradientBrush(stops, start, end);
            
            brush.Freeze();

            return brush;
        }

        private RadialGradientBrush CreateFrozenBrush(GradientStopCollection stops, double radX, double radY, Point center, Point origin)
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

        #region Rendering

        protected override void OnRender(DrawingContext drawingContext)
        {
            var cornerRadius = this.CornerRadius;
            var rect = new Rect(new Point(ShadowDepth, ShadowDepth), new Size(base.RenderSize.Width, base.RenderSize.Height));
            Color c = this.Color;

            if ((rect.Width <= 0.0 || rect.Height <= 0.0) || c.A <= 0) 
                return;

            var width = (rect.Right - rect.Left) - 10.0;
            var height = (rect.Bottom - rect.Top) - 10.0;
            var minSide = Math.Min(width*ShadowDepth, height*ShadowDepth);

            cornerRadius.TopLeft = Math.Min(cornerRadius.TopLeft, minSide);
            cornerRadius.TopRight = Math.Min(cornerRadius.TopRight, minSide);
            cornerRadius.BottomLeft = Math.Min(cornerRadius.BottomLeft, minSide);
            cornerRadius.BottomRight = Math.Min(cornerRadius.BottomRight, minSide);

            Brush[] brushes = this.GetBrushes(c, cornerRadius);

            var topMargin = rect.Top + ShadowDepth;
            var leftMargin = rect.Left + ShadowDepth;
            var rightMargin = rect.Right - ShadowDepth;
            var bottomMargin = rect.Bottom - ShadowDepth;

            var guidelinesX = new[]
            {
                leftMargin,
                leftMargin + cornerRadius.TopLeft,
                rightMargin - cornerRadius.TopRight,
                leftMargin + cornerRadius.BottomLeft,
                rightMargin - cornerRadius.BottomRight,
                rightMargin
            };

            var guidelinesY = new[]
            {
                topMargin,
                topMargin + cornerRadius.TopLeft,
                topMargin + cornerRadius.TopRight,
                bottomMargin - cornerRadius.BottomLeft,
                bottomMargin - cornerRadius.BottomRight,
                bottomMargin
            };

            drawingContext.PushGuidelineSet(new GuidelineSet(guidelinesX, guidelinesY));

            cornerRadius.TopLeft += ShadowDepth;
            cornerRadius.TopRight += ShadowDepth;
            cornerRadius.BottomLeft += ShadowDepth;
            cornerRadius.BottomRight += ShadowDepth;

            var shadowRect = new Rect(rect.Left, rect.Top, cornerRadius.TopLeft, cornerRadius.TopLeft);
            drawingContext.DrawRectangle(brushes[0], null, shadowRect);

            double firstX = guidelinesX[2] - guidelinesX[1];
            if (firstX > 0.0)
            {
                drawingContext.DrawRectangle(brushes[1], null, new Rect(guidelinesX[1], rect.Top, firstX, 5.0));
            }

            drawingContext.DrawRectangle(brushes[2], null, new Rect(guidelinesX[2], rect.Top, cornerRadius.TopRight, cornerRadius.TopRight));

            double firstY = guidelinesY[3] - guidelinesY[1];
            if (firstY > 0.0)
            {
                drawingContext.DrawRectangle(brushes[3], null, new Rect(rect.Left, guidelinesY[1], 5.0, firstY));
            }

            double middleY = guidelinesY[4] - guidelinesY[2];
            if (middleY > 0.0)
            {
                drawingContext.DrawRectangle(brushes[5], null, new Rect(guidelinesX[5], guidelinesY[2], 5.0, middleY));
            }

            drawingContext.DrawRectangle(brushes[6], null, new Rect(rect.Left, guidelinesY[3], cornerRadius.BottomLeft, cornerRadius.BottomLeft));

            double middleX = guidelinesX[4] - guidelinesX[3];
            if (middleX > 0.0)
            {
                drawingContext.DrawRectangle(brushes[7], null, new Rect(guidelinesX[3], guidelinesY[5], middleX, 5.0));
            }

            drawingContext.DrawRectangle(brushes[8], null, new Rect(guidelinesX[4], guidelinesY[4], cornerRadius.BottomRight, cornerRadius.BottomRight));
            
            if (cornerRadius.TopLeft == ShadowDepth &&
                cornerRadius.TopLeft == cornerRadius.TopRight &&
                cornerRadius.TopLeft == cornerRadius.BottomLeft &&
                cornerRadius.TopLeft == cornerRadius.BottomRight)
            {
                drawingContext.DrawRectangle(brushes[4], null, new Rect(guidelinesX[0], guidelinesY[0], width, height));
            }
            else
            {
                var figure = new PathFigure();

                if (cornerRadius.TopLeft > ShadowDepth)
                {
                    figure.StartPoint = new Point(guidelinesX[1], guidelinesY[0]);
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[1], guidelinesY[1]), true));
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[0], guidelinesY[1]), true));
                }
                else
                {
                    figure.StartPoint = new Point(guidelinesX[0], guidelinesY[0]);
                }

                if (cornerRadius.BottomLeft > ShadowDepth)
                {
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[0], guidelinesY[3]), true));
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[3], guidelinesY[3]), true));
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[3], guidelinesY[5]), true));
                }
                else
                {
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[0], guidelinesY[5]), true));
                }

                if (cornerRadius.BottomRight > ShadowDepth)
                {
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[4], guidelinesY[5]), true));
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[4], guidelinesY[4]), true));
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[5], guidelinesY[4]), true));
                }
                else
                {
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[5], guidelinesY[5]), true));
                }

                if (cornerRadius.TopRight > ShadowDepth)
                {
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[5], guidelinesY[2]), true));
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[2], guidelinesY[2]), true));
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[2], guidelinesY[0]), true));
                }
                else
                {
                    figure.Segments.Add(new LineSegment(new Point(guidelinesX[5], guidelinesY[0]), true));
                }

                figure.IsClosed = true;
                figure.Freeze();

                var geometry = new PathGeometry();
                geometry.Figures.Add(figure);
                geometry.Freeze();

                drawingContext.DrawGeometry(brushes[4], null, geometry);
            }

            drawingContext.Pop();
        }

        #endregion
    }
}
