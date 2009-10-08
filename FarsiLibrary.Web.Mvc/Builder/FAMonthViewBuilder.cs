using System;
using FarsiLibrary.Resources;
using FarsiLibrary.Web.Mvc.Controls;

namespace FarsiLibrary.Web.Mvc.Builder
{
    public class FAMonthViewBuilder : ControlBuilder<FAMonthView, FAMonthViewBuilder>, IHideMembers
    {
        public FAMonthViewBuilder(FAMonthView control) : base(control)
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Control.SelectionEmpty = FALocalizeManager.Instance.GetLocalizer().GetLocalizedString(StringID.Validation_NullText);
        }

        public FAMonthViewBuilder SetViewDate(DateTime viewDate)
        {
            Control.ViewDateTime = viewDate;

            return this;
        }

        public FAMonthViewBuilder SetSelectedDateTime(DateTime? selectedDate)
        {
            Control.SelectedDateTime = selectedDate;

            return this;
        }

        public FAMonthViewBuilder SetEmptySelectionMessage(string message)
        {
            Control.SelectionEmpty = message;

            return this;
        }
    }
}