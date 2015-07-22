using System.Web.Mvc;

namespace Health.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Geri Medic Landing page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Geri Medic Home Page";

            return View();
        }
    }
}
