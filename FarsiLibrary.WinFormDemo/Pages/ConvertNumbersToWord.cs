using System;
using FarsiLibrary.Utils;
using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class ConvertNumbersToWord : DemoBase
    {
        #region Ctor

        public ConvertNumbersToWord()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                long num;
                if(long.TryParse(txtNumber.Text, out num))
                {
                    lblResult.Text = ToWords.ToString(num);
                }
                else
                {
                    lblResult.Text = "لطفا عدد کوچکتری را وارد کنید";
                }
            }
            catch(Exception)
            {
                lblResult.Text = "خطا";
            }
        }

        #endregion
    }
}