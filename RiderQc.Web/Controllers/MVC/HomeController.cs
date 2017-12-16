using RiderQc.Web.Helpers;
using System.Web.Mvc;

namespace RiderQc.Web.Controllers
{
    [RoutePrefix("admin")]
    public class HomeController : Controller
    {
        [Route("home")]
        public ActionResult Index()
        {
            if (Authenticate())
                return View();
            return Redirect("/admin/account/login");
        }

        public bool Authenticate()
        {
            if (Session != null)
            {
                if (Session["Username"] != null)
                    return true; ;
            }
            return false;
        }
    }
}