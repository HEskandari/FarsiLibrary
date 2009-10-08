using System;
using FarsiLibrary.Utils;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DateCalculation : DemoBase
    {
        #region Fields
        
        PersianCalendar calendar = new PersianCalendar();
        
        #endregion
        
        #region Ctor

        public DateCalculation()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void btnToday_Click(object sender, EventArgs e)
        {
            PersianDate pd = PersianDate.Now;
            lblMessage.Text = pd.ToString();
            lblToWritten.Text = pd.ToWritten();
        }

        private void btnAddYears_Click(object sender, EventArgs e)
        {
            PersianDate pd = new PersianDate(calendar.AddYears(DateTime.Now, 10));
            lblMessage.Text = pd.ToString();
            lblToWritten.Text = pd.ToWritten();
        }

        private void btnDeductDays(object sender, EventArgs e)
        {
            PersianDate pd = new PersianDate(calendar.AddDays(DateTime.Now, -66));
            lblMessage.Text = pd.ToString();
            lblToWritten.Text = pd.ToWritten();
        }

        private void btnAddMonth_Click(object sender, EventArgs e)
        {
            PersianDate pd = new PersianDate(calendar.AddMonths(DateTime.Now, 2));
            lblMessage.Text = pd.ToString();
            lblToWritten.Text = pd.ToWritten();
        }

        #endregion
    }
}