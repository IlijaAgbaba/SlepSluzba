using RsokProject.DataLayer;
using RsokProject.ViewModel;
using RsokProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RsokProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Listanje svih komentara
            Baza baza = new Baza();
            List<Komentar> sviKomentari = baza.sviKomentari;

            return View(sviKomentari);
        }

        [HttpPost]
        public ActionResult Index(string tekstKomentara)
        {
            //Čuvanje imena, teksta komentara i datum
            var ident = System.Web.HttpContext.Current.User.Identity.Name;
            Komentar komentar = new Komentar();
            komentar.ime = ident.ToString();
            komentar.tekstKomentara = tekstKomentara;
            komentar.datum = DateTime.Now;

            //Upisivanje komentara u bazu
            Baza baza = new Baza();
            baza.upisiKomentar(komentar);

            return RedirectToAction("Index");
        }

    }
}