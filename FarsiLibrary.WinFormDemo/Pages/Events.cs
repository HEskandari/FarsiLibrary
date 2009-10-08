using System;
using System.Windows.Forms;
using FarsiLibrary.Win.Events;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class Events : DemoBase
    {
        #region Ctor

        public Events()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods
        
        private void AddItem(object sender, string eventName)
        {
            listEvents.Items.Add(string.Format("{1} fired {0} event.", eventName, ((Control)sender).Name));
        }
        
        #endregion

        #region EventHandlers

        private void faMonthView_SelectedDateTimeChanged(object sender, EventArgs e)
        {
            AddItem(sender, "SelectedDateTimeChanged");
        }

        private void faMonthView_ThemeChanged(object sender, EventArgs e)
        {
            AddItem(sender, "ThemeChanged");
        }

        private void faDatePicker_ValueValidating(object sender, ValueValidatingEventArgs e)
        {
            AddItem(sender, "ValueValidating");
        }

        private void faDatePicker_ValueChanged(object sender, EventArgs e)
        {
            AddItem(sender, "ValueChanged");
        }

        private void faDatePicker_SelectedDateTimeChanging(object sender, SelectedDateTimeChangingEventArgs e)
        {
            AddItem(sender, "SelectedDateTimeChanging");
        }

        private void faDatePicker_SelectedDateTimeChanged(object sender, EventArgs e)
        {
            AddItem(sender, "SelectedDateTimeChanged");
        }

        private void faDatePicker_RightToLeftChanged(object sender, EventArgs e)
        {
            AddItem(sender, "RightToLeftChanged");
        }

        private void faMonthView_ViewDayChanged(object sender, DateChangedEventArgs e)
        {
            AddItem(sender, "ViewDayChanged");
        }

        private void faMonthView_ViewMonthChanged(object sender, DateChangedEventArgs e)
        {
            AddItem(sender, "ViewMonthChanged");
        }

        private void faMonthView_ViewYearChanged(object sender, DateChangedEventArgs e)
        {
            AddItem(sender, "ViewYearChanged");
        }

        private void faMonthView_ViewDateTimeChanged(object sender, EventArgs e)
        {
            AddItem(sender, "ViewDateTimeChanged");
        }

        private void faMonthView_SelectedYearChanged(object sender, DateChangedEventArgs e)
        {
            AddItem(sender, "SelectedYearChanged");
        }

        private void faMonthView_SelectedMonthChanged(object sender, DateChangedEventArgs e)
        {
            AddItem(sender, "SelectedMonthChanged");
        }

        private void faMonthView_SelectedDayChanged(object sender, DateChangedEventArgs e)
        {
            AddItem(sender, "SelectedDayChanged");
        }

        private void faMonthView_SelectedDateRangeChanged(object sender, SelectedDateRangeChangedEventArgs e)
        {
            AddItem(sender, "SelectedDateRangeChanged");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listEvents.Items.Clear();
        }

        private void btnDatePicker_Click(object sender, EventArgs e)
        {
            listEvents.Items.Clear();
            propertyGrid.SelectedObject = faDatePicker;
        }

        private void btnMonthView_Click(object sender, EventArgs e)
        {
            listEvents.Items.Clear();
            propertyGrid.SelectedObject = faMonthView;
        }

        #endregion
    }
}