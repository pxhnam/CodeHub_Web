using CodeHub.Filters;
using System.Web.Mvc;

namespace CodeHub.Controllers
{
    [Authentication]
    public class StatisticalController : Controller
    {
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Transaction()
        {
            return View();
        }
    }
}