using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;

namespace FarsiLibrary.WPF.Automation
{
    public class FXMonthViewContainerPeer : SelectorAutomationPeer
    {
        public FXMonthViewContainerPeer(Selector owner) : base(owner)
        {
        }

        protected override ItemAutomationPeer CreateItemAutomationPeer(object item)
        {
            return new FXMonthViewItemAutomationPeer(item, this);
        }
    }
}