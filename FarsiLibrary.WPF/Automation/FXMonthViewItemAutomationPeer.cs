using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using FarsiLibrary.WPF.Controls;

namespace FarsiLibrary.WPF.Automation
{
    public class FXMonthViewItemAutomationPeer : SelectorItemAutomationPeer, ISelectionItemProvider
    {
        #region Ctor

        public FXMonthViewItemAutomationPeer(object owner, SelectorAutomationPeer selectorAutomationPeer)
            : base(owner, selectorAutomationPeer)
        {
        }

        #endregion

        #region Props

        public FXMonthViewItem OwnerControl
        {
            get { return base.Item as FXMonthViewItem; }
        }

        #endregion
        
        #region Implementation of ISelectionItemProvider
        
        public void Select()
        {
            this.OwnerControl.IsSelected = true;
        }

        public void AddToSelection()
        {
        }

        public void RemoveFromSelection()
        {
        }

        public bool IsSelected
        {
            get { return this.OwnerControl.IsSelected; }
        }

        #endregion

        #region Overrides of AutomationPeer

        protected override string GetClassNameCore()
        {
            return "FXMonthViewItem";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.ListItem;
        }

        #endregion
    }
}