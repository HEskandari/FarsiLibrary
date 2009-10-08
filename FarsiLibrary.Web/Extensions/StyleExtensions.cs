using System.Reflection;
using System.Web.UI.WebControls;

namespace FarsiLibrary.Web.Extensions
{
    public static class StyleExtensions
    {
        public static bool IsSet(this Style style, int propertyKey)
        {
            FieldInfo fi = style.GetType().GetField("setBits");
            if(fi != null)
            {
                var setbit = (int) fi.GetValue(style);
                return ((setbit & propertyKey) != 0);
            }

            return false;
        }
    }
}
