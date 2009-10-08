using System.Windows.Controls;
using FarsiLibrary.WPF.Automation;

namespace FarsiLibrary.WPF.Controls
{
    public class FXMonthViewContainer : ListBox
    {
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is FXMonthViewItem;
        }

        protected override System.Windows.DependencyObject GetContainerForItemOverride()
        {
            return new FXMonthViewItem();
        }

        protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
        {
            return new FXMonthViewContainerPeer(this);
        }
    }
}