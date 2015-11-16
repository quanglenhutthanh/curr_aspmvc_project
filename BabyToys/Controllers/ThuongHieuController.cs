using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
namespace BabyToys.Controllers
{
    public class ThuongHieuController : Controller
    {
        //
        // GET: /ThuongHieu/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index(int id=0)
        {
            var thuonghieu = db.ThuongHieus.SingleOrDefault(t => t.IdThuongHieu == id);
            return View(thuonghieu);
        }

    }
}
