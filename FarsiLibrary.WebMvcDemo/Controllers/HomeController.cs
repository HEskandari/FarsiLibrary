using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using FarsiLibrary.Utils;

namespace FarsiLibrary.WebMvcDemo.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Demos()
        {
            return View("Demos");
        }

        public ActionResult ToEnglish()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            return View("Demos");
        }

        public ActionResult ToFarsi()
        {
            Thread.CurrentThread.CurrentUICulture = new PersianCultureInfo();

            return View("Demos");
        }
    }
}
