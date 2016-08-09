using DevExpress.Utils;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraEditors.Repository;

namespace FarsiLibrary.Win.DevExpress
{
    public static class RepositoryItemDateEditExtentions
    {
        public static bool UseVistaPainter(this RepositoryItemDateEdit properties)
        {
            if (properties.VistaDisplayMode == DefaultBoolean.True) return true;
            if (properties.VistaDisplayMode == DefaultBoolean.False) return false;

            return NativeVista.IsVista;
        }
    }
}