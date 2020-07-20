using PageWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PageWebApi.Controllers
{
    public class KorisnikController : ApiController
    {
        private PageEntities db = new PageEntities();

        [HttpGet]
        [Route("api/Korisnik/Login/{email}/{password}")]
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "*")]
        public Korisnik Login(string email, string password)
        {
            Korisnik korisnik = db.Korisnik.Where(x => x.Email == email && x.Sifra == password).SingleOrDefault();

            if(korisnik!=null)
            {
                korisnik.Sifra = string.Empty;
            }
            return korisnik;
        }
    }
}

