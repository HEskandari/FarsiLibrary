using System.ComponentModel;
using FarsiLibrary.WPF.Controls;
using Microsoft.Windows.Design;
using Microsoft.Windows.Design.Features;
using Microsoft.Windows.Design.Metadata;

namespace FarsiLibrary.WPF.VisualStudio.Design
{
    public class VisualStudioMetadata : IProvideAttributeTable
    {
        public VisualStudioMetadata()
        {
            AttributeTableBuilder builder = new FarsiLibraryVisualStudioAttributeTableBuilder();
            AttributeTable = builder.CreateTable();
        }

        public AttributeTable AttributeTable { get; }
    }

    internal class FarsiLibraryVisualStudioAttributeTableBuilder : AttributeTableBuilder
    {
        internal FarsiLibraryVisualStudioAttributeTableBuilder()
        {
            AddToolboxBrowsableAttributes();
            AddMonthViewAttributes();
            AddMonthViewDesigners();
            AddMonthViewMenuItems();
        }

        private void AddToolboxBrowsableAttributes()
        {
            AddCustomAttributes(typeof(FXMonthViewButton),            ToolboxBrowsableAttribute.No);
            AddCustomAttributes(typeof(FXMonthViewContainer),         ToolboxBrowsableAttribute.No);
            AddCustomAttributes(typeof(FXMonthViewHeader),            ToolboxBrowsableAttribute.No);
            AddCustomAttributes(typeof(FXMonthViewItem),              ToolboxBrowsableAttribute.No);
            AddCustomAttributes(typeof(FXMonthViewWeekDayHeaderCell), ToolboxBrowsableAttribute.No);
            AddCustomAttributes(typeof(FXPopup),                      ToolboxBrowsableAttribute.No);
        }

        private void AddMonthViewAttributes()
        {
            AddCallback(typeof(FXMonthView), builder =>
            {
                builder.AddCustomAttributes(FXMonthView.ButtonStyleProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.WeekDayHeaderStyleProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.HeaderMonthStyleProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.DayContainerStyleProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.HeaderYearStyleProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.DayContainerStyleSelectorProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.DayTemplateSelectorProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.DayTemplateProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.ViewPreChangeAnimationProperty.Name, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.ViewPostChangeAnimationProperty.Name, BrowsableAttribute.No);

                var behaviorCategory = new CategoryAttribute("Behavior");
                builder.AddCustomAttributes(FXMonthView.ViewDateTimeProperty.Name, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.SelectedDateTimeProperty.Name, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.ShowTodayButtonProperty.Name, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.ShowEmptyButtonProperty.Name, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.ShowWeekDayNamesProperty.Name, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.MaxDateProperty.Name, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.MinDateProperty.Name, behaviorCategory);

                builder.AddCustomAttributes();
            });
        }

        private void AddMonthViewMenuItems()
        {
            AddCallback(typeof (FXMonthView), builder => builder.AddCustomAttributes(new FeatureAttribute(typeof (MonthViewDesignMenuProvider))));
        }

        private void AddMonthViewDesigners()
        {
            AddCustomAttributes(typeof (FXMonthView), new FeatureAttribute(typeof (MonthViewDesignAdorner)));
        }
    }
}