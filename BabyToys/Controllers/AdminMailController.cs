using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;

namespace BabyToys.Controllers
{
    public class AdminMailController : admin_baseController
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /AdminMail/

        public ActionResult Index()
        {
            return View(db.Mails.ToList());
        }

        //
        // GET: /AdminMail/Details/5

        public ActionResult Details(int id = 0)
        {
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        //
        // GET: /AdminMail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminMail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mail mail)
        {
            if (ModelState.IsValid)
            {
                db.Mails.Add(mail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mail);
        }

        //
        // GET: /AdminMail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        //
        // POST: /AdminMail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mail mail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mail);
        }

        //
        // GET: /AdminMail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        //
        // POST: /AdminMail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mail mail = db.Mails.Find(id);
            db.Mails.Remove(mail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}