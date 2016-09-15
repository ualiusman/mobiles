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
    public class OrderManagerController : Controller
    {
        private MobilesContext db = new MobilesContext();

        //
        // GET: /OrderManager/

        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Model);
            return View(orders.ToList());
        }

        //
        // GET: /OrderManager/Details/5

        public ActionResult Details(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        

        //
        // GET: /OrderManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.MobileId = new SelectList(db.Mobiles, "MobileId", "Model", order.MobileId);
            return View(order);
        }

        //
        // POST: /OrderManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                if (order.Status == "Completed")
                {
                    string subject = "Mobiles Request Completed";
                    string body = "Hi, "+order.ClientName+"<br/><br/>Your Mobile Request is Completed<br/> IMEI:"+order.IMEI+"<br/>Phone:"+order.Phone+"<br/><br/> Thanks.<br. [Mobiles]";
                    EmailSender.Send(order.ClientName, order.Email, subject, body);
                }
                return RedirectToAction("Index");
            }
            ViewBag.MobileId = new SelectList(db.Mobiles, "MobileId", "Model", order.MobileId);
            return View(order);
        }

        //
        // GET: /OrderManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /OrderManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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