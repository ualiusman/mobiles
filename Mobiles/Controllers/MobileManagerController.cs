using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mobiles.Models;

namespace Mobiles.Controllers
{
    [Authorize(Roles = "admin")]
    public class MobileManagerController : Controller
    {
        private MobilesContext db = new MobilesContext();

        //
        // GET: /MobileManager/

        public ActionResult Index()
        {
            var mobiles = db.Mobiles.Include(m => m.Manufacturer);
            return View(mobiles.ToList());
        }

        //
        // GET: /MobileManager/Details/5

        public ActionResult Details(int id = 0)
        {
            Mobile mobile = db.Mobiles.Find(id);
            if (mobile == null)
            {
                return HttpNotFound();
            }
            return View(mobile);
        }

        //
        // GET: /MobileManager/Create

        public ActionResult Create()
        {
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name");
            ViewBag.ManufacturerId = new SelectList(new List<string>());
            return View();
        }

        //
        // POST: /MobileManager/Create

        [HttpPost]
        public ActionResult Create(Mobile mobile)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase;
                if (file != null && file.ContentLength > 0)
                {
                    string guid = Guid.NewGuid().ToString();
                    var fileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(file.FileName);
                    if ((fileExtension == ".jpeg") || (fileExtension == ".jpg") || (fileExtension == ".png"))
                    {
                        mobile.Image = guid + fileExtension;
                        var path = Path.Combine(Server.MapPath("~/Data/Images/"), guid + fileExtension);
                        file.SaveAs(path);
                    }
                }

                db.Mobiles.Add(mobile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManufacturerId = new SelectList(db.Manfacturers, "ManufacturerId", "Name", mobile.ManufacturerId);
            return View(mobile);
        }

        //
        // GET: /MobileManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Mobile mobile = db.Mobiles.Find(id);
            if (mobile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerId = new SelectList(db.Manfacturers, "ManufacturerId", "Name", mobile.ManufacturerId);
            return View(mobile);
        }

        //
        // POST: /MobileManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Mobile mobile)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase;
                if (file != null && file.ContentLength > 0)
                {
                    string guid = Guid.NewGuid().ToString();
                    var fileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(file.FileName);
                    if ((fileExtension == ".jpeg") || (fileExtension == ".jpg") || (fileExtension == ".png"))
                    {
                        string completePath = Server.MapPath("~/Data/Images/" + mobile.Image);
                        if (System.IO.File.Exists(completePath))
                            System.IO.File.Delete(completePath);
                        mobile.Image = guid + fileExtension;
                        var path = Path.Combine(Server.MapPath("~/Data/Images/"), guid + fileExtension);
                        file.SaveAs(path);
                    }
                }
                db.Entry(mobile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerId = new SelectList(db.Manfacturers, "ManufacturerId", "Name", mobile.ManufacturerId);
            return View(mobile);
        }

        //
        // GET: /MobileManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Mobile mobile = db.Mobiles.Find(id);
            if (mobile == null)
            {
                return HttpNotFound();
            }
            return View(mobile);
        }

        //
        // POST: /MobileManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Mobile mobile = db.Mobiles.Find(id);
            db.Mobiles.Remove(mobile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetManufacturersList(string distributorId)
        {
            try
            {
                int id = Convert.ToInt32(distributorId);
                var manufacturers = db.Manfacturers.ToList().Where(m => m.DistributorId == id).Select(m => new SelectListItem() { Text = m.Name, Value = m.ManufacturerId.ToString() });
                if (manufacturers != null)
                {
                    return Json(new { manufacturers = manufacturers });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}