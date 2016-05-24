using System.ComponentModel;
using System.Windows;
using Microsoft.Windows.Design.Interaction;
using Microsoft.Windows.Design.Model;
using FarsiLibrary.WPF.Controls;
using System;

namespace FarsiLibrary.WPF.VisualStudio.Design
{
public class MonthViewDesignAdorner : PrimarySelectionAdornerProvider
{
    private MonthViewDesignerUI designerUI;
    private AdornerPanel adornersPanel;
    private ModelItem calendarModelItem;

    public MonthViewDesignAdorner()
    {
        designerUI = new MonthViewDesignerUI();
    }

    protected override void Activate(ModelItem item)
    {
        calendarModelItem = item;

        CreateAdornerPanel();
        PlaceAdornerPanel();
        SubscribeDesignerEvents();

        base.Activate(item);
    }

    protected override void Deactivate()
    {
        UnsubscribeDesignerEvents();

        base.Deactivate();
    }

private void OnDesignerUIPropertyChanged(object sender, PropertyChangedEventArgs e)
{
    ModelProperty prop = calendarModelItem.Properties[e.PropertyName];
    var args = e as DesignerPropertyChangedEventArgs;

    if(prop != null && args != null)
    {
        prop.SetValue(args.Value);
    }
}

    private void PlaceAdornerPanel()
    {
        AdornerPanel.SetHorizontalStretch(designerUI, AdornerStretch.Stretch);
        AdornerPanel.SetVerticalStretch(designerUI, AdornerStretch.Stretch);

        var placement = new AdornerPlacementCollection();
        placement.PositionRelativeToContentHeight(0, -10);
        placement.PositionRelativeToContentWidth(1, 0);
        placement.SizeRelativeToAdornerDesiredHeight(1, 0);
        placement.SizeRelativeToAdornerDesiredWidth(1, 0);
        AdornerPanel.SetPlacements(designerUI, placement);
    }

    private void OnDesignerUILoaded(object sender, RoutedEventArgs e)
    {
        designerUI.ShowEmptyButton = (bool) calendarModelItem.Properties[FXMonthView.ShowEmptyButtonProperty.Name].ComputedValue;
        designerUI.ShowTodayButton = (bool) calendarModelItem.Properties[FXMonthView.ShowTodayButtonProperty.Name].ComputedValue;
        designerUI.ShowWeekDayNames = (bool) calendarModelItem.Properties[FXMonthView.ShowWeekDayNamesProperty.Name].ComputedValue;
        designerUI.SelectedDateTime = (DateTime?) calendarModelItem.Properties[FXMonthView.SelectedDateTimeProperty.Name].ComputedValue;
        designerUI.MaxDate = (DateTime) calendarModelItem.Properties[FXMonthView.MaxDateProperty.Name].ComputedValue;
        designerUI.MinDate = (DateTime) calendarModelItem.Properties[FXMonthView.MinDateProperty.Name].ComputedValue;
    }

    private void SubscribeDesignerEvents()
    {
        designerUI.Loaded += OnDesignerUILoaded;
        designerUI.PropertyChanged += OnDesignerUIPropertyChanged;
    }

    private void UnsubscribeDesignerEvents()
    {
        designerUI.Loaded -= OnDesignerUILoaded;
        designerUI.PropertyChanged -= OnDesignerUIPropertyChanged;
    }

    private void CreateAdornerPanel()
    {
        if (this.adornersPanel == null)
        {
            adornersPanel = new AdornerPanel();
            adornersPanel.IsContentFocusable = true;
            adornersPanel.Children.Add(designerUI);
            Adorners.Add(adornersPanel);
        }
    }
}
}
