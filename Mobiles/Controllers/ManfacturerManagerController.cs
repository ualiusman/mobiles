using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobiles.Models;

namespace Mobiles.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManfacturerManagerController : Controller
    {
        private MobilesContext db = new MobilesContext();

        //
        // GET: /ManfacturerManager/

        public ActionResult Index()
        {
            var manfacturers = db.Manfacturers.Include(m => m.Distributor);
            return View(manfacturers.ToList());
        }

        //
        // GET: /ManfacturerManager/Details/5

        public ActionResult Details(int id = 0)
        {
            Manufacturer manufacturer = db.Manfacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // GET: /ManfacturerManager/Create

        public ActionResult Create()
        {
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name");
            return View();
        }

        //
        // POST: /ManfacturerManager/Create

        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Manfacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", manufacturer.DistributorId);
            return View(manufacturer);
        }

        //
        // GET: /ManfacturerManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Manufacturer manufacturer = db.Manfacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", manufacturer.DistributorId);
            return View(manufacturer);
        }

        //
        // POST: /ManfacturerManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", manufacturer.DistributorId);
            return View(manufacturer);
        }

        //
        // GET: /ManfacturerManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Manufacturer manufacturer = db.Manfacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // POST: /ManfacturerManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = db.Manfacturers.Find(id);
            db.Manfacturers.Remove(manufacturer);
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