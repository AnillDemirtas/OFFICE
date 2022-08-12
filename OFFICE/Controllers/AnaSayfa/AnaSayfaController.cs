using OFFICE.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OFFICE.Controllers.AnaSayfa
{
    public class AnaSayfaController : Controller
    {
        DapperService _dapperService;
        public AnaSayfaController()
        {
            _dapperService = new DapperService();
        }
        // GET: AnaSayfa
        public ActionResult Index()
        {
            var gorevlerim = _dapperService.gorevlerim();
            return View(gorevlerim);
        }
    }
}