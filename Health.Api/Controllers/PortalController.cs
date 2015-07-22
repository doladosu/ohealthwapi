using System.Web.Mvc;

namespace Health.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PortalController : Controller
    {
        /// <summary>
        /// Geri Medic Account page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}