using OFFICE.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            Models.Gorevler ff = new Models.Gorevler();
            ff.firma_model = _dapperService.Firmalar();
            ff.user_model = _dapperService.users();

            return View(ff);
        }
    }
}