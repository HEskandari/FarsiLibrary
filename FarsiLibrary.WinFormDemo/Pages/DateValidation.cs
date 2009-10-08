using System;
using FarsiLibrary.Utils;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DateValidation : DemoBase
    {
        #region Ctor

        public DateValidation()
        {
            InitializeComponent();
        }

        #endregion

        private void faDatePicker1_SelectedDateTimeChanging(object sender, FarsiLibrary.Win.Events.SelectedDateTimeChangingEventArgs e)
        {
            if (!faDatePicker1.IsNull && e.NewValue.Value.Year > 2000)
            {
                e.Message = "This is a custom error message.";
            }
        }

        private void faDatePicker2_SelectedDateTimeChanging(object sender, FarsiLibrary.Win.Events.SelectedDateTimeChangingEventArgs e)
        {
            if (!faDatePicker2.IsNull)
            {
                PersianDate pd = e.NewValue.Value;
                if (pd.Day != 20)
                {
                    e.Message = "Invalid date. Default Date is applied.";
                    e.NewValue = new DateTime(2010, 1, 20, 0, 0, 0);
                }
            }
        }

        private void faDatePickerConverter1_SelectedDateTimeChanging(object sender, FarsiLibrary.Win.Events.SelectedDateTimeChangingEventArgs e)
        {
            PersianDate pd = e.NewValue;
            
            if(pd.Day != 8 || pd.Month != 4 || pd.Year != 1385)
            {
                e.Cancel = true;
            }
        }

    }
}