﻿using BabyToys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyToys.Controllers
{
    public class SettingController : Controller
    {
        //
        // GET: /Setting/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            return View(db.Settings.ToList());
        }

        //
        // GET: /Setting/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Setting/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Setting/Create

        [HttpPost]
        public ActionResult Create(Setting setting)
        {
            //try
            //{
                // TODO: Add insert logic here
                db.Settings.Add(setting);
                db.SaveChanges();
                return RedirectToAction("Index");
            //}
            //catch
            //{
                //return View();
            //}
        }

        //
        // GET: /Setting/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Setting/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Setting/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Setting/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
