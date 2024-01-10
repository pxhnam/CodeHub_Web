using CodeHub.Filters;
using System.Web.Mvc;

namespace CodeHub.Controllers
{
    [Authentication]
    public class UserController : Controller
    {
        public ActionResult Developer()
        {
            return View();
        }

        public ActionResult Customer()
        {
            return View();
        }
    }
}