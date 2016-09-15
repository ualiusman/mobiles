using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Mobiles.Models;
using Newtonsoft.Json;
namespace Mobiles.Controllers
{
    public class MobileModel
    {
        public string Price { get; set; }
        public string Image { get; set; }
    }


    public class ProductController : Controller
    {
        //
        // GET: /Product/

        private MobilesContext db = new MobilesContext();
        public ActionResult Index()
        {
            ViewBag.Distributors = db.Distributors.Where(d => d.IsActive == true).ToList().Select(d => new SelectListItem() { Value = d.DistributorId.ToString(), Text = d.Name });
            ViewBag.Manufacturers = new SelectList(new List<string>());
            ViewBag.MobileId = new SelectList(new List<string>());

            IEnumerable<SelectListItem> lst = HttpContext.Application["CurrencyList"] as IEnumerable<SelectListItem>;
            ViewBag.CurrencyList = lst;

            ViewBag.Result = TempData["ok"];
            Order order = new Order();
            return View(order);
        }
        [HttpPost]
        public JsonResult GetManufacturerList(string distributorId)
        {
            try
            {
                int id = Convert.ToInt32(distributorId);
                var manufacturers = db.Manfacturers.Where(m => (m.DistributorId == id) && (m.IsActive == true)).ToList().Select(m => new SelectListItem() { Text = m.Name, Value = m.ManufacturerId.ToString() });
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


        [HttpPost]
        public JsonResult GetModelList(string manufacturerId)
        {
            try
            {
                int id = Convert.ToInt32(manufacturerId);
                var models = db.Mobiles.Where(m => (m.ManufacturerId == id) && (m.IsActive == true)).ToList().Select(m => new SelectListItem() { Text = m.Model, Value = m.MobileId.ToString() });
                if (models != null)
                {
                    return Json(new { models = models });
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
        [HttpPost]
        public JsonResult GetModel(string modelId)
        {
            try
            {
                int id = Convert.ToInt32(modelId);
                Mobile mobile = db.Mobiles.ToList().Where(m => m.MobileId == id).SingleOrDefault();

                if (mobile != null)
                {
                    MobileModel mobileModel = new MobileModel() { Image = mobile.Image, Price = mobile.Price.ToString() };
                    return Json(mobileModel);
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

        [HttpPost]
        public ActionResult ReceiveOrder(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    order.Date = DateTime.Now;
                    order.Status = "InProcess";
                    db.Orders.Add(order);
                    db.SaveChanges();
                    string subject = "Mobiles Your Request is Being Processed";
                    string body = "Hi, " + order.ClientName + "<br/><br/>Your Mobile Request is being Processed, You we will inform you as soon as completed. Thanks.<br/> IMEI:" + order.IMEI + "<br/>Phone:" + order.Phone + "<br/><br/> Thanks.<br. [Mobiles]";
                    EmailSender.Send(order.ClientName, order.Email, subject, body);
                    TempData["ok"] = " Request Submited";
                }

                else
                {

                    TempData["ok"] = "Request Not Submited, Please Try Again";
                }
            }
            catch
            {
                TempData["ok"] = "Request Not Submited, Please Try Again,With More carefully!";
            }
            return RedirectToAction("Index");

        }

    }
}
