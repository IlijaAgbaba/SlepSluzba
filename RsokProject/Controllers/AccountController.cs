using RsokProject.Models;
using RsokProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace RsokProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        //Register forma
        [HttpPost]
        public ActionResult SaveRegisterDetails(Register registerDetails)
        {
            //Proveravamo da li je stanje modela važeće ili ne. Koristili smo atribute DataAnnotation.
            //Ako bilo koja vrednost obrasca ne prođe validaciju DataAnnotation, stanje modela postaje nevažeće.
            if (ModelState.IsValid)
            {

                //kreirati kontekst baze podataka koristeći Entite Framework
                using (var databaseContext = new AccountsEntities2())
                {
                    bool userExists = databaseContext.RegisterUsers.FirstOrDefault(x => x.ime == registerDetails.ime) != null;

                   //Proveravamo da li uneseno ime postoji u bazi
                    if (userExists)
                    {
                        //Ukoliko postoji, korisnik dobija poruku da je ime zauzeto
                        ModelState.AddModelError("ime", "Ime je zauzeto!");
                        return View("Register");
                    }
                    else
                    {
                        //Ukoliko ne postoji, podaci se cuvaju bazi
                        RegisterUser reglog = new RegisterUser();

                        reglog.ime = registerDetails.ime;
                        reglog.lozinka = registerDetails.lozinka;

                        //Čuvanje podataka
                        databaseContext.RegisterUsers.Add(reglog);
                        databaseContext.SaveChanges();

                    }
                    
                }
                //Kada se nalog napravi, dobija se poruka i korisnik biva upićen na login
                ViewBag.Message = "Nalog je napravljen!";
                return View("Login");
                    
            }
            else
            {

                //Ako validacija ne uspe, vraćamo objekat modela sa greškama u prikaz, koji će prikazati poruke o grešci.
                return View("Register", registerDetails);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        //Login forma
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //Provera stanja modela
            if (ModelState.IsValid)
            {
                //Validacija korisnika, bez obzira da li je korisnik validan ili ne
                var isValidUser = IsValidUser(model);

                //Ako je korisnik ispravan i prisutan u bazi podataka, preusmeravamo ga na početnu stranicu
                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.ime, false);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    //Ako korisnik nije prisutan, dobije poruku
                    ModelState.AddModelError("Failure", "Pogresno ime ili lozinka!");
                    return View();
                }
            }
            else
            {
                //Ako stanje modela nije važeće, model sa porukom o grešci se vraća u prikaz
                return View(model);
            }
        }

        //Funkcija za proveru da li je korisnik ispravan ili ne
        public RegisterUser IsValidUser(LoginViewModel model)
        {
            using (var dataContext = new AccountsEntities2())
            {
                //Preuzimanje podataka o korisniku iz baze podataka na osnovu korisničkog imena i lozinke koje je uneo korisnik
                RegisterUser user = dataContext.RegisterUsers.Where(query => query.ime.Equals(model.ime) && query.lozinka.Equals(model.lozinka)).SingleOrDefault();
                //Ako je korisnik prisutan, onda se vraća true
                if (user == null)
                    return null;
                //Ako korisnik nije prisutan, vraća se false
                else
                    return user;
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); //Brisanje sesije na kraju zahteva
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Back()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}

