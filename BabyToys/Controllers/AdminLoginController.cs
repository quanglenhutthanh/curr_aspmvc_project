using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
namespace BabyToys.Controllers
{
    public class AdminLoginController : Controller
    {
        //
        // GET: /AdminLogin/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            string matkhau = Utilities.EditString.mahoa_md5(login.PassWord);
            var user = db.Users.Include("Quyen.ChucNangs").Where(u => u.TenDangNhap == login.LoginName &&
                                              u.MatKhau == matkhau);
            if (user != null && user.Count()==1)
            {
                User u = user.First();
                Session["admin"] = u;
                Session.Add("sessionfilepath", true);
                Session["sessionfilepath"] = "~/Content/Images";
                Session.Timeout = 60;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View(login);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Models.User.CurrentUser = null;
            return RedirectToAction("Index");

        }
    }
}
