using OFFICE.Models;
using OFFICE.Service;
using System;
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
        public ActionResult HareketDetay(string hareket_id,string gorev_id)
        {

         
            var hareketler = _dapperService.gorev_hareket_detay(hareket_id);
            if (hareketler.Count != 0)
            {
                ViewBag.crud = "Not Düzenle";
                return View(hareketler[0]);
            }
            else
            {
                ViewBag.crud = "Yeni Not";
                Models.Hareketler bos_firma = new Models.Hareketler();
                bos_firma.gorev_id = new Guid(gorev_id);
                return View(bos_firma);
            }
           
        }
        public ActionResult GorevDetaylari(string gorev_id)
        {
            var detaylar = _dapperService.gorev_detaylari(gorev_id);
            if (detaylar.Count > 0)
            {
                ViewBag.konu = detaylar[0].konu;
                ViewBag.aciklama = detaylar[0].konu_detay;
                ViewBag.aciliyet_durumu = detaylar[0].aciliyet_durumu;
                ViewBag.tarih = detaylar[0].olusturma_tarihi;
                ViewBag.gorev_id = detaylar[0].gorev_id;
            }
            return View(detaylar);
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
        [HttpPost]
        public ActionResult HareketKayit(Models.Hareketler hareket)
        {

            var gelen_deger = _dapperService.gorev_hareket_crud(hareket, hareket.id.ToString());
            if (hareket.aciklama == null || hareket.aciklama == "")
            {
                return Json(new JsonService { success = false, message = "Açıklama boş olamaz" });
            }
            else
            {
                return Json(new { success = true });
            }

        }

    }
}