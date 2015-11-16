using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyToys.Controllers
{
    public class PageNotFoundController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult error404()
        {
            return View();
        }
    }
}
