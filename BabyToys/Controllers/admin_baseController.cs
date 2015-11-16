using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
namespace BabyToys.Controllers
{
    public class admin_baseController : Controller
    {
        //
        // GET: /admin_base/

        DatabaseContext db = new DatabaseContext();
        [NonAction]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User su = (User)Session["admin"];

            if (su == null)
            {
                filterContext.Result = new RedirectResult("~/AdminLogin");
            }
            else
            {
                User user = db.Users.Include("Quyen").SingleOrDefault(u => u.Id == su.Id);
                Models.User.CurrentUser = user;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
