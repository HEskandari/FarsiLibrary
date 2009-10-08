using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DevExpressIntegration : DemoBase
    {
        public DevExpressIntegration()
        {
            InitializeComponent();
        }

        private void xtraFADatePicker1_EditValueChanged(object sender, System.EventArgs e)
        {
            this.lblEditValue.Text = xtraFADatePicker1.EditValue == null ? "[Null]" : xtraFADatePicker1.EditValue.ToString();
        }
    }
}
