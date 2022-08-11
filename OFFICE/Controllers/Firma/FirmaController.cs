using OFFICE.Models;
using OFFICE.Service;
using System.Web.Mvc;

namespace OFFICE.Controllers.Firma
{
    public class FirmaController : Controller
    {
        DapperService _dapperService;
        public FirmaController()
        {
            _dapperService = new DapperService();
        }
        // GET: Firma
        public ActionResult Index(string id = null)
        {


            var firma_bilgileri = _dapperService.Firma(id);
            if (firma_bilgileri.Count != 0)
            {
                ViewBag.crud = "Firma Düzenle";
                return View(firma_bilgileri[0]);
            }
            else
            {
                ViewBag.crud = "Yeni Firma";
                Models.Firma bos_firma = new Models.Firma();
                return View(bos_firma);
            }
          


        }
        public ActionResult Firmalar()
        {
            var gelen_deger = _dapperService.Firmalar();
            return View(gelen_deger);
        }

        [HttpPost]
        public ActionResult Kayit(Models.Firma firma)
        {

            var gelen_deger = _dapperService.musteri_crud(firma, firma.id);
            if (firma.unvan == null || firma.unvan == "")
            {
                return Json(new JsonService { success = false, message = "firma boş olamaz" });
            }
            else
            {
                return Json(new { success = true });
            }

        }

    }
}