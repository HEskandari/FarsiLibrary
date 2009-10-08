using System.Globalization;
using FarsiLibrary.Win.Events;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class CustomFormattingAndValidating : DemoBase
    {
        #region Ctor

        public CustomFormattingAndValidating()
        {
            InitializeComponent();
        }

        #endregion

        private void faDatePicker1_ValueValidating(object sender, ValueValidatingEventArgs e)
        {
            //Fired when focus is changed
            string value = e.Value;

            if(string.IsNullOrEmpty(value))
                return;

            if(value.Length == 6) //Date format entered 840125
            {
                var year = value.Substring(0, 2);
                var month = value.Substring(2, 2);
                var day = value.Substring(4, 2);

                e.Value = string.Format("{0}{3}{1}{3}{2}", year, month, day, CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);
            }
            else if(value.Length == 8) //Date format entered 13840125
            {
                var year = value.Substring(0, 4);
                var month = value.Substring(4, 2);
                var day = value.Substring(6, 2);

                e.Value = string.Format("{0}{3}{1}{3}{2}", year, month, day, CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);                
            }
        }
    }
}
