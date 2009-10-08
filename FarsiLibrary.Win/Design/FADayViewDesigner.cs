using System.Collections;
using System.Windows.Forms.Design;
using FarsiLibrary.Win.Controls;

namespace FarsiLibrary.Win.Design
{
    internal class FADayViewDesigner : FABaseDesigner
    {
        public override SelectionRules SelectionRules
        {
            get { return SelectionRules.AllSizeable | SelectionRules.Moveable | SelectionRules.Visible; }
        }

        public new virtual FADayView Control
        {
            get
            {
                return base.Control as FADayView;
            }
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);

			properties.Remove("Dock");
			properties.Remove("AutoScroll");
			properties.Remove("AutoScrollMargin");
			properties.Remove("AutoScrollMinSize");
			properties.Remove("DockPadding");
			properties.Remove("DrawGrid");
            properties.Remove("Font");
            properties.Remove("Size");
            properties.Remove("Padding");
            properties.Remove("MinimumSize");
            properties.Remove("MaximumSize");
            properties.Remove("Margin");
            properties.Remove("ForeColor");
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
        }
    }
}