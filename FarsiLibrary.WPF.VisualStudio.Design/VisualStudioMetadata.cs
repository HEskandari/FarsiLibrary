using System.ComponentModel;
using FarsiLibrary.WPF.Controls;
using Microsoft.Windows.Design;
using Microsoft.Windows.Design.Features;
using Microsoft.Windows.Design.Metadata;

namespace FarsiLibrary.WPF.VisualStudio.Design
{
    public class VisualStudioMetadata : IRegisterMetadata
    {
        public void Register()
        {
            AttributeTableBuilder builder = new FarsiLibraryVisualStudioAttributeTableBuilder();
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
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
            var builder = new AttributeTableBuilder();

            builder.AddCustomAttributes(typeof(FXMonthViewButton),            ToolboxBrowsableAttribute.No);
            builder.AddCustomAttributes(typeof(FXMonthViewContainer),         ToolboxBrowsableAttribute.No);
            builder.AddCustomAttributes(typeof(FXMonthViewHeader),            ToolboxBrowsableAttribute.No);
            builder.AddCustomAttributes(typeof(FXMonthViewItem),              ToolboxBrowsableAttribute.No);
            builder.AddCustomAttributes(typeof(FXMonthViewWeekDayHeaderCell), ToolboxBrowsableAttribute.No);
            builder.AddCustomAttributes(typeof(FXPopup),                      ToolboxBrowsableAttribute.No);

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        private void AddMonthViewAttributes()
        {
            AddCallback(typeof(FXMonthView), builder =>
            {
                builder.AddCustomAttributes(FXMonthView.ButtonStyleProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.WeekDayHeaderStyleProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.HeaderMonthStyleProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.DayContainerStyleProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.HeaderYearStyleProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.DayContainerStyleSelectorProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.DayTemplateSelectorProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.DayTemplateProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.ViewPreChangeAnimationProperty, BrowsableAttribute.No);
                builder.AddCustomAttributes(FXMonthView.ViewPostChangeAnimationProperty, BrowsableAttribute.No);

                var behaviorCategory = new CategoryAttribute("Behavior");
                builder.AddCustomAttributes(FXMonthView.ViewDateTimeProperty, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.SelectedDateTimeProperty, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.ShowTodayButtonProperty, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.ShowEmptyButtonProperty, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.ShowWeekDayNamesProperty, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.MaxDateProperty, behaviorCategory);
                builder.AddCustomAttributes(FXMonthView.MinDateProperty, behaviorCategory);

                builder.AddCustomAttributes();
            });
        }

        private void AddMonthViewMenuItems()
        {
            AddCallback(typeof (FXMonthView), builder => builder.AddCustomAttributes(new FeatureAttribute(typeof (MonthViewDesignMenuProvider))));
        }

        private void AddMonthViewDesigners()
        {
            var builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(typeof (FXMonthView), new FeatureAttribute(typeof (MonthViewDesignAdorner)));

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}