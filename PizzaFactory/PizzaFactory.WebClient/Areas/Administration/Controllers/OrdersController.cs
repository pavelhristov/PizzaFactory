using System.Web.Mvc;

namespace PizzaFactory.WebClient.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        // GET: Administration/Orders
        public ActionResult Index()
        {
            return View();
        }
    }
}