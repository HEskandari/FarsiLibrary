using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace FarsiLibrary.Win.Design
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class AboutDialogEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();

            return null;
        }
    }
}
