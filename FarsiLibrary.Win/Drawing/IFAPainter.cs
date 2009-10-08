using System.Drawing;
using FarsiLibrary.Win.Enums;

namespace FarsiLibrary.Win.Drawing
{
    /// <summary>
    /// Base interface that Painter classes should implement in order to
    /// define a new Style.
    /// </summary>
    public interface IFAPainter
    {
        /// <summary>
        /// When implemented by a class, Draws the background in the filled mode.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="rectangle">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        /// <param name="isGradient">Specifies if the background should be painted in gradient mode or just filled with a color.</param>
        /// <param name="angle">Angle of the gradient, if the background should be painted in gradient mode.</param>
        void DrawFilledBackground(Graphics g, Rectangle rectangle, bool isGradient, float angle);

        /// <summary>
        /// When implemented by a class, Fills the background area, with a solid color.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="r">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        /// <param name="isGradient">Specifies if the background should be painted in gradient mode or just filled with a color.</param>
        /// <param name="angle">Angle of the gradient, if the background should be painted in gradient mode.</param>
        void DrawWhiteBackground(Graphics g, Rectangle r, bool isGradient, float angle);

        /// <summary>
        /// When implemented by a class, Should draw a shape that functions as a button object.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="rectangle">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        /// <param name="text">Text value of the button.</param>
        /// <param name="font">Font value of the button, that Text value will be displayed with.</param>
        /// <param name="fmt">Instance of <see cref="System.Drawing.StringFormat"/> class that formats text value when drawn.</param>
        /// <param name="state">Specifies the state of the button to be drawn.</param>
        /// <param name="hasBorder">Specifies if the button has borders around it.</param>
        /// <param name="enabled">Specifies if the button is in enabled or disbled state.</param>
        void DrawButton(Graphics g, Rectangle rectangle, string text, Font font, StringFormat fmt, ItemState state, bool hasBorder, bool enabled);

        /// <summary>
        /// When implemented by a class, Draws a border, in enabled or disabled mode.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="rectangle">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        /// <param name="enabled">Specifies if the control state is enabled or disabled.</param>
        void DrawBorder(Graphics g, Rectangle rectangle, bool enabled);

        /// <summary>
        /// When implemented by a class, Draws a seperator line.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="ptFrom">From point (start) of the separator.</param>
        /// <param name="ptTo">To point (end) of the separator.</param>
        void DrawSeparator(Graphics g, Point ptFrom, Point ptTo);

        /// <summary>
        /// Draws the text
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rectangle"></param>
        /// <param name="text"></param>
        /// <param name="fmt"></param>
        /// <param name="font"></param>
        /// <param name="enabled"></param>
        void DrawString(Graphics g, Rectangle rectangle, string text, StringFormat fmt, Font font, bool enabled);

        /// <summary>
        /// When implemented by a class, Draws the Focus rectangle on the specified area.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="r">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        void DrawFocusRect(Graphics g, Rectangle r);

        /// <summary>
        /// When implemented by a class, Draws Left-Right (Horizontal) arrows.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="rc">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        /// <param name="isLeft">Specifies if head of the arrow should point to Left or Right.</param>
        /// <param name="isDisabled">Specifies if the arrow should painted in Disabled or Enabled mode.</param> 
        /// <param name="arrowSize">Specifies arrow size.</param>   
        /// <returns></returns>
        Rectangle DrawArrow(Graphics g, Rectangle rc, bool isLeft, bool isDisabled, int arrowSize);

        /// <summary>
        /// When implemented by a class, Draws the Top-Bottom (Vertical) arrows.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="rc">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        /// <param name="isLeft">Specifies if head of the arrow should point to Left or Right.</param>
        /// <param name="isDisabled">Specifies if the arrow should painted in Disabled or Enabled mode.</param> 
        /// <param name="arrowSize">Specifies arrow size.</param>   
        /// <returns></returns>
        Rectangle DrawVerticalArrow(Graphics g, Rectangle rc, bool isLeft, bool isDisabled, int arrowSize);

        /// <summary>
        /// When implemented by a class, Draws an area in selected mode.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="r">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        void DrawSelectedPanel(Graphics g, Rectangle r);

        /// <summary>
        /// When implemented by a class, Draws a vertical separator line.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="from">From point (start) of the separator.</param>
        /// <param name="to">To point (end) of the separator.</param>
        void DrawVerticalSeparator(Graphics g, Point from, Point to);

        /// <summary>
        /// When implemented by a class, Draws the border of the selected area.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="r">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies bounds to be drawn.</param>
        void DrawSelectionBorder(Graphics g, Rectangle r);

        /// <summary>
        /// When implemented by a class, Draws focus rectangle on a button.
        /// </summary>
        /// <param name="g">Instance of the <see cref="System.Drawing.Graphics"/> class.</param>
        /// <param name="r">Instance of the <see cref="System.Drawing.Rectangle"/> class that specifies content rectangle.</param>
        void DrawButtonFocusRect(Graphics g, Rectangle r, ItemState state);
    }
}
