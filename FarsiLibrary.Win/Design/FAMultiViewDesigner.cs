using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using FarsiLibrary.Win.Controls;

namespace FarsiLibrary.Win.Design
{
    internal class FAMultiViewDesigner : FABaseDesigner
    {
        private readonly DesignerVerb toDayView;
        private readonly DesignerVerb toMonthView;
        private readonly DesignerVerb showEmptyButton;
        private readonly DesignerVerb showTodayButton;

        public FAMultiViewDesigner()
        {
            toDayView = new DesignerVerb("To DayView", (sender, e) => ShowDayView()) { Checked = false }; 
            toMonthView = new DesignerVerb("To MonthView", (sender, e) => ShowMonthView()) { Checked = false };
            showTodayButton = new DesignerVerb("Show/Hide Today Button", (sender, e) => ShowTodayButton()) { Checked = false };
            showEmptyButton = new DesignerVerb("Show/Hide Empty Button", (sender, e) => ShowEmptyButton()) { Checked = false };

            designerVerbs.Add(toDayView);
            designerVerbs.Add(toMonthView);
            designerVerbs.Add(showTodayButton);
            designerVerbs.Add(showEmptyButton);
        }

        private void ShowEmptyButton()
        {
            OnComponentChanging();
            Control.ShowEmptyButton = !Control.ShowEmptyButton;
            OnComponentChanged();
        }

        private void ShowTodayButton()
        {
            OnComponentChanging();
            Control.ShowTodayButton = !Control.ShowTodayButton;
            OnComponentChanged();
        }

        private void ShowMonthView()
        {
            OnComponentChanging();
            Control.ShowMonthView();
            OnComponentChanged();
        }

        private void ShowDayView()
        {
            OnComponentChanging();
            Control.ShowDayView();
            OnComponentChanged();
        }

        public override SelectionRules SelectionRules
        {
            get { return SelectionRules.Moveable | SelectionRules.Visible; }
        }

        public new virtual FAMultiView Control
        {
            get
            {
                return base.Control as FAMultiView;
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