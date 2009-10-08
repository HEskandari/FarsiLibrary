using System.Web.Mvc;

namespace FarsiLibrary.Web.Mvc.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static ControlFactory FarsiLibrary(this HtmlHelper htmlHelper)
        {
            return new ControlFactory(htmlHelper);
        }
    }
}