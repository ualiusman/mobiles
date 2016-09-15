using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Mobiles.Models;
using Newtonsoft.Json;

namespace Mobiles
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            List<SelectListItem> lst = new List<SelectListItem>();
            using (StreamReader sr = new StreamReader(Server.MapPath(@"\App_Data\Common-Currency.json")))
            {
                string js = sr.ReadToEnd();
                Dictionary<string, Currency> items = JsonConvert.DeserializeObject<Dictionary<string, Currency>>(js);
                Application["CurrencyList"] = (IEnumerable<SelectListItem>)items.Select(x => new SelectListItem() { Text = x.Value.Name, Value = x.Key.ToString() });
            }

        }
    }
}