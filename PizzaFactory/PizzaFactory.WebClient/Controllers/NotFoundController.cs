using System.Web.Mvc;

namespace PizzaFactory.WebClient.Controllers
{
    public class NotFoundController : Controller
    {
        // GET: AccessDenied
        public ActionResult Index()
        {
            return View("Error");
        }
    }
}