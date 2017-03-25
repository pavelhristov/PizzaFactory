using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Areas.Administration.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Administration/Orders
        public ActionResult Index()
        {
            return View();
        }
    }
}