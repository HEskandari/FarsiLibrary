using FarsiLibrary.WinFormDemo.Demo;

namespace FarsiLibrary.WinFormDemo.Pages
{
    public partial class DevExpressEditor : DemoBase
    {
        public DevExpressEditor()
        {
            InitializeComponent();
        }

        private void xtraFADatePicker1_EditValueChanged(object sender, System.EventArgs e)
        {
            lblDatePickerValue.Text = xtraFADatePicker1.EditValue?.ToString() ?? "[Null]";
        }

        private void dateEdit1_EditValueChanged(object sender, System.EventArgs e)
        {
            lblDateEditValue.Text = dateEdit1.EditValue?.ToString() ?? "[Null]";
        }

        private void xtraFADateEdit1_EditValueChanged(object sender, System.EventArgs e)
        {
            lblTouchUIValue.Text = xtraFADateEdit1.EditValue?.ToString() ?? "[Null]";
        }
    }
}
