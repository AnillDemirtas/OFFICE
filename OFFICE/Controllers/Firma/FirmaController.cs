using OFFICE.Models;
using System.Web.Mvc;

namespace OFFICE.Controllers.Firma
{
    public class FirmaController : Controller
    {
        // GET: Firma
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(Models.Firma firma)
        {
            if (firma.firma == null || firma.firma == "")
            {
                return Json(new JsonService { success = false , message = "firma boş olamaz" });
            }
            else
            {
                return Json(new { success = true });
            }

        }
    }
}