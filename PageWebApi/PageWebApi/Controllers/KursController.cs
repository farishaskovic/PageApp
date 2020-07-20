using PageWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PageWebApi.Controllers
{
    public class KursController : ApiController
    {
        private PageEntities db = new PageEntities();

        [HttpPost]
        [Route("api/Kurs/AddKurs/{KursNaziv}")]
        [Authorize]
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "*")]
        public void AddKurs(string KursNaziv,[FromBody] List<int> listaStudenataId)
        {
            Kurs kurs = new Kurs
            {
                KursNaziv = KursNaziv
            };
            foreach (int item in listaStudenataId)
            {
                kurs.Student.Add(db.Student.Where(x => x.StudentId == item).FirstOrDefault());
                db.Student.Where(x => x.StudentId == item).FirstOrDefault().Kurs.Add(kurs);
            }
            db.Kurs.Add(kurs);

            db.SaveChanges();
        }

        [HttpGet]
        [Route("api/Kurs/GetAllKurs")]
        [Authorize]
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "*")]
        public List<Kurs> GetAllKurs()
        {
            return db.Kurs.ToList();
        }

        [HttpGet]
        [Route("api/Kurs/GetKursStudents/{KursId}")]
        [Authorize]
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "*")]
        public List<getAllStudentsOnKurs_Result> GetKursStudents(int KursId)
        {
            return db.getAllStudentsOnKurs(KursId).ToList();
        }
    }
}