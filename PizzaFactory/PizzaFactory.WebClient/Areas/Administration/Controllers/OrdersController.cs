using PizzaFactory.WebClient.Attributes;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Areas.Administration.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        // GET: Administration/Orders
        public ActionResult Index()
        {
            return View();
        }
    }
}