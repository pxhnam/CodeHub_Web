using CodeHub.Models;
using CodeHub.Service;
using System.Linq;
using System.Web.Mvc;

namespace CodeHub.Controllers
{
    public class HomeController : Controller
    {
        private Connection con;
        public HomeController()
        {
            con = new Connection();
        }
        public ActionResult Index()
        {
            if (Session["User"] != null && Session["User"] is Manager)
            {
                return View(con.Languages.ToList());
            }
            return View("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["User"] != null && Session["User"] is Manager)
            {
                return View("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Username, string PasswordHash)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = MD5.hasd(PasswordHash);
                var user = con.Managers.SingleOrDefault(x => (x.Username.Equals(Username) || x.Email.Equals(Username)) && x.PasswordHash.Equals(hashedPassword));
                if (user != null)
                {
                    if (user.IsActive == true)
                    {
                        Session["User"] = user;
                        return RedirectToRoute("Default", new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tài khoản này đã bị khóa!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin tài khoản không chính xác!");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return View("Login");
        }
    }
}