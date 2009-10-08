using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using FarsiLibrary.WPF.Controls;

namespace FarsiLibrary.WPF.Automation
{
    public class FXMonthViewAutomationPeer : FrameworkElementAutomationPeer, ISelectionProvider, IValueProvider
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the FXMonthViewAutomationPeer class.
        /// </summary>
        /// <param name="owner">Owning MonthView control</param>
        public FXMonthViewAutomationPeer(FXMonthView owner) 
            : base(owner)
        {
        }

        #endregion

        #region Props

        public FXMonthView OwnerControl
        {
            get { return this.Owner as FXMonthView; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the control pattern that is associated with the specified System.Windows.Automation.Peers.PatternInterface.
        /// </summary>
        /// <param name="patternInterface">A value from the System.Windows.Automation.Peers.PatternInterface enumeration.</param>
        /// <returns>The object that supports the specified pattern, or null if unsupported.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.Value || patternInterface == PatternInterface.Selection)
            {
                return this;
            }

            return base.GetPattern(patternInterface);
        }

        /// <summary>
        /// Gets the control type for the element that is associated with the UI Automation peer.
        /// </summary>
        /// <returns>The control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Calendar;
        }

        /// <summary>
        /// Called by GetClassName that gets a human readable name that, in addition to AutomationControlType, 
        /// differentiates the control represented by this AutomationPeer.
        /// </summary>
        /// <returns>The string that contains the name.</returns>
        protected override string GetClassNameCore()
        {
            return this.Owner.GetType().Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetNameCore()
        {
            string nameCore = base.GetNameCore();

            if (string.IsNullOrEmpty(nameCore))
            {
                AutomationPeer labeledByCore = this.GetLabeledByCore();
                if (labeledByCore != null)
                {
                    nameCore = labeledByCore.GetName();
                }

                if (string.IsNullOrEmpty(nameCore))
                {
                    nameCore = this.OwnerControl.ToString();
                }
            }

            return nameCore;
        }

        internal void RaiseSelectionEvents(SelectionChangedEventArgs e)
        {
            int numSelected = 1;
            
            // Currently only single selection is supported
            // this.OwnerControl.SelectedDates.Count;

            if (ListenerExists(AutomationEvents.SelectionItemPatternOnElementSelected) && numSelected == 1)
            {
                var selectedDay = this.OwnerControl.GetMonthViewItemFromDate((DateTime)e.AddedItems[0]);

                if (selectedDay != null)
                {
                    var peer = FromElement(selectedDay);

                    if (peer != null)
                    {
                        peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementSelected);
                    }
                }
            }
            else
            {
                if (ListenerExists(AutomationEvents.SelectionItemPatternOnElementAddedToSelection))
                {
                    foreach (DateTime date in e.AddedItems)
                    {
                        var selectedDay = this.OwnerControl.GetMonthViewItemFromDate(date);

                        if (selectedDay != null)
                        {
                            var peer = FromElement(selectedDay);

                            if (peer != null)
                            {
                                peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementAddedToSelection);
                            }
                        }
                    }
                }

                if (ListenerExists(AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection))
                {
                    foreach (DateTime date in e.RemovedItems)
                    {
                        var removedDay = this.OwnerControl.GetMonthViewItemFromDate(date);

                        if (removedDay != null)
                        {
                            AutomationPeer peer = FromElement(removedDay);

                            if (peer != null)
                            {
                                peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region ISelectionProvider

        bool ISelectionProvider.CanSelectMultiple
        {
            get
            {
                return this.OwnerControl.SelectionMode != SelectionMode.Single;
            }
        }

        bool ISelectionProvider.IsSelectionRequired
        {
            get
            {
                return false;
            }
        }

        IRawElementProviderSimple[] ISelectionProvider.GetSelection()
        {
            var providers = new List<IRawElementProviderSimple>();

            foreach (FXMonthViewItem day in this.OwnerControl.GetMonthViewItems())
            {
                if (day.IsSelected)
                {
                    AutomationPeer peer = CreatePeerForElement(day);

                    if (peer != null)
                    {
                        providers.Add(ProviderFromPeer(peer));
                    }
                }
            }

            if (providers.Count > 0)
            {
                return providers.ToArray();
            }

            return null;
        }

        #endregion ISelectionProvider

        #region Implementation of IValueProvider

        bool IValueProvider.IsReadOnly
        {
            get { return false; }
        }

        string IValueProvider.Value
        {
            get { return this.OwnerControl.SelectedDateTime.HasValue ? this.OwnerControl.SelectedDateTime.Value.ToString(CultureInfo.InvariantCulture) : "<Null>"; }
        }

        void IValueProvider.SetValue(string value)
        {
            this.OwnerControl.SelectedDateTime = DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        #endregion
    }
}