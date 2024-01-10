using CodeHub.Filters;
using System.Web.Mvc;

namespace CodeHub.Controllers
{
    [Authentication]
    public class SourceController : Controller
    {
        public ActionResult Language()
        {
            return View();
        }
        public ActionResult Repository()
        {
            return View();
        }

        public ActionResult Type()
        {
            return View();
        }
        public ActionResult UploadCode()
        {
            return View();
        }
        public ActionResult UserRequest()
        {
            return View();
        }
    }
}