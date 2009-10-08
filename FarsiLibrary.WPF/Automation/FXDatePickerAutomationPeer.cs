using System;
using System.Globalization;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using FarsiLibrary.WPF.Controls;

namespace FarsiLibrary.WPF.Automation
{
    public class FXDatePickerAutomationPeer : FrameworkElementAutomationPeer, IExpandCollapseProvider, IValueProvider
    {
        #region Ctor

        /// <summary>
        /// Creates a new instance of FXDatePickerAutomationPeer
        /// </summary>
        /// <param name="owner"></param>
        public FXDatePickerAutomationPeer(FXDatePicker owner) 
            : base(owner)
        {
        }

        #endregion

        #region Props

        public FXDatePicker OwnerControl
        {
            get { return this.Owner as FXDatePicker; }
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
            if (patternInterface == PatternInterface.ExpandCollapse || patternInterface == PatternInterface.Value)
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
            return AutomationControlType.ComboBox;
        }

        /// <summary>
        /// Called by GetClassName that gets a human readable name that, in addition to AutomationControlType, 
        /// differentiates the control represented by this AutomationPeer.
        /// </summary>
        /// <returns>The string that contains the name.</returns>
        protected override string GetClassNameCore()
        {
            return Owner.GetType().Name;
        }

        /// <summary>
        /// Overrides the GetLocalizedControlTypeCore method for DatePicker
        /// </summary>
        /// <returns></returns>
        protected override string GetLocalizedControlTypeCore()
        {
            return "FXDatePicker";
        }

        /// <summary>
        /// GetNameCore
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

        #endregion

        #region IExpandCollapseProvider

        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
        {
            get
            {
                return this.OwnerControl.IsDropDownOpen ? 
                    ExpandCollapseState.Expanded : 
                    ExpandCollapseState.Collapsed;
            }
        }

        void IExpandCollapseProvider.Collapse()
        {
            this.OwnerControl.IsDropDownOpen = false;
        }

        void IExpandCollapseProvider.Expand()
        {
            this.OwnerControl.IsDropDownOpen = true;
        }

        #endregion

        #region IValueProvider

        bool IValueProvider.IsReadOnly
        {
            get { return false; }
        }

        string IValueProvider.Value
        {
            get { return this.OwnerControl.SelectedDateTime.HasValue ?  this.OwnerControl.SelectedDateTime.Value.ToString(CultureInfo.InvariantCulture) : this.OwnerControl.NullValueText; }
        }

        void IValueProvider.SetValue(string value)
        {
            this.OwnerControl.SelectedDateTime = DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        #endregion IValueProvider
    }
}