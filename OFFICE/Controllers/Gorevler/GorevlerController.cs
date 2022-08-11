using OFFICE.Models;
using OFFICE.Service;
using System.Web.Mvc;

namespace OFFICE.Controllers.Gorevler
{
    public class GorevlerController : Controller
    {
        DapperService _dapperService;
        public GorevlerController()
        {
            _dapperService = new DapperService();
        }
        // GET: Gorevler
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult YeniGorev()
        {
            ViewBag.crud = "Yeni Görev Tanımı";
            Models.Gorevler gorevler = new Models.Gorevler();
            gorevler.firma_model = _dapperService.Firmalar();
            gorevler.kullanici_model = _dapperService.kullanicilar();
            return View(gorevler);
        }
        [HttpPost]
        public ActionResult Kayit(Models.GorevKayit gorev)
        {


            if (gorev.firma_id == null || gorev.firma_id.ToString() == "" || gorev.firma_id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                return Json(new JsonService { success = false, message = "firma boş olamaz" });
            }
            else if (gorev.konu == null || gorev.konu == "")
            {
                return Json(new JsonService { success = false, message = "Konu boş olamaz" });
            }
            else if (gorev.user_id == null || gorev.user_id == "")
            {
                return Json(new JsonService { success = false, message = "Personel boş olamaz" });
            }
            else
            {
                var gelen_deger = _dapperService.gorev_crud(gorev, gorev.id);
                return Json(new { success = true });
            }

        }
    }
}