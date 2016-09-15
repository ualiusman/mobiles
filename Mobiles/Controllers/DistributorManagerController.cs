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
    [Authorize(Roles="admin")]
    public class DistributorManagerController : Controller
    {
        private MobilesContext db = new MobilesContext();

        //
        // GET: /DistributorManager/

        public ActionResult Index()
        {
            return View(db.Distributors.ToList());
        }

        //
        // GET: /DistributorManager/Details/5

        public ActionResult Details(int id = 0)
        {
            Distributor distributor = db.Distributors.Find(id);
            if (distributor == null)
            {
                return HttpNotFound();
            }
            return View(distributor);
        }

        //
        // GET: /DistributorManager/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DistributorManager/Create

        [HttpPost]
        public ActionResult Create(Distributor distributor)
        {
            if (ModelState.IsValid)
            {
                db.Distributors.Add(distributor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(distributor);
        }

        //
        // GET: /DistributorManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Distributor distributor = db.Distributors.Find(id);
            if (distributor == null)
            {
                return HttpNotFound();
            }
            return View(distributor);
        }

        //
        // POST: /DistributorManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Distributor distributor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distributor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(distributor);
        }

        //
        // GET: /DistributorManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Distributor distributor = db.Distributors.Find(id);
            if (distributor == null)
            {
                return HttpNotFound();
            }
            return View(distributor);
        }

        //
        // POST: /DistributorManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Distributor distributor = db.Distributors.Find(id);
            db.Distributors.Remove(distributor);
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