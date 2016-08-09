using System;
using System.Collections.Generic;
using FarsiLibrary.Utils;
using FarsiLibrary.Utils.Internals;
using FarsiLibrary.Win.Events;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class MultiSelection : DemoBase
    {
        #region Ctor

        public MultiSelection()
        {
            InitializeComponent();

            if (DesignMode)
                return;

            faMonthView.SelectedDateRange.CollectionChanged += SelectedDateRange_CollectionChanged;
        }

        #endregion

        #region Methods

        private void btnChangeSelectionMode_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            faMonthView.IsMultiSelect = !faMonthView.IsMultiSelect;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            faMonthView.SelectedDateRange.Clear();
        }

        private void btnSelectDays_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            faMonthView.SelectedDateRange.AddRange(new[] { DateTime.Now.Date, DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(-1) });
        }

        private void btnSetNull_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            faMonthView.SelectedDateTime = null;
            faMonthView.SelectedDateRange.Clear();
        }

        private void btnSelectMonth_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            var selection = new List<DateTime>();
            var pd = PersianDate.Now;
            var dt = DateTime.Now;
            var numberOfDays = CultureHelper.CurrentCalendar.GetDaysInMonth(pd.Year, pd.Month);

            for (int dayNo = 1; dayNo <= numberOfDays; dayNo++)
            {
                if(faMonthView.DefaultCulture.IsFarsiCulture())
                {
                    selection.Add(new PersianDate(pd.Year, pd.Month, dayNo, 0, 0, 0, 0));
                }
                else
                {
                    selection.Add(new DateTime(dt.Year, dt.Month, dayNo, 0, 0, 0, 0));
                }
            }

            faMonthView.SelectedDateRange.Clear();
            faMonthView.SelectedDateRange.AddRange(selection.ToArray());
        }

        private void faMonthView_SelectedDateRangeChanged(object sender, SelectedDateRangeChangedEventArgs e)
        {
            textBox1.Text += "SelectedDateRangeChanged event fired\r\n";
            UpdateCount();
        }

        private void SelectedDateRange_CollectionChanged(object sender, CollectionChangedEventArgs e)
        {
            textBox1.Text += "CollectionChanged [" + e.ChangeType + "] event fired\r\n";
            UpdateCount();
        }

        private void faMonthView_SelectedDateTimeChanged(object sender, EventArgs e)
        {
            textBox1.Text += "SelectedDateTimeChanged event fired.\r\n";
            UpdateCount();
        }

        private void UpdateCount()
        {
            lblMessage.Text = "Selected Date Count : " + faMonthView.SelectedDateRange.Count + "\r\n";
        }

        #endregion

        private void frm20_Load(object sender, EventArgs e)
        {
            faMonthView.SelectedDateTime = DateTime.Now;
        }
    }
}