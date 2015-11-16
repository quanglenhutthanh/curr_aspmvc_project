using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using PagedList;
using PagedList.Mvc;
namespace BabyToys.Controllers
{
    public class AdminLogController : admin_baseController
    {
        //
        // GET: /AdminLog/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index(int? page)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_log"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var pageNumber = page ?? 1;
            var logs = db.Logs.OrderByDescending(h=>h.IdLog).ToList() ;
            ViewBag.onePageOfLog = logs.ToPagedList(pageNumber, 10);
            return View();
        }

    }
}
