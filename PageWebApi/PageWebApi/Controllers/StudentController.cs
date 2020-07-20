using Microsoft.Owin.Security.Infrastructure;
using PageWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace PageWebApi.Controllers
{
    public class StudentController : ApiController
    {
        private PageEntities db = new PageEntities();

        [HttpGet]
        [Authorize]
        [Route("api/Student/GetStudent/{StudentId}")]
        public Student GetStudent(int StudentId)
        {

            try
            {
                return db.Student.Where(x => x.StudentId == StudentId).FirstOrDefault();
                //return from stud in db.Student join sstatus in db.StudentStatus on stud.StudentStatusId equals sstatus.StudentStatusId
                //       where stud.StudentId == StudentId
                //       select new { Student = stud, StudentStatus = sstatus };
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/Student/GetAllStudentStatus/")]
        public List<StudentStatus> GetAllStudentStatus()
        {
            try
            {
                return db.StudentStatus.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("api/Student/GetAllStudents")]
        [Authorize]
        [ResponseType(typeof(List<Korisnik>))]
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "POST, GET, OPTIONS, DELETE, PUT, PATCH")]
        public HttpResponseMessage GetAllStudents(HttpRequestMessage request)
        {

            try
            {
                return request.CreateResponse(HttpStatusCode.OK, db.Get_All_Students().ToList());
            }
            catch (Exception)
            {
                return null;
            }
        }


        [HttpPost]
        [Authorize]
        [Route("api/Student/PostStudent/{brojIndeksa}/{statusStudenta}/{ime?}/{prezime?}/{godina?}")]
        public void PostStudent(string brojIndeksa, string statusStudenta, string ime, string prezime, string godina)
        {
            if (ime == "null")
                ime = "";
            if (prezime == "null")
                prezime = "";
            if (godina == "null")
                godina = "";


            var student = new Student
            {
                IndeksBroj = brojIndeksa,
                Ime = ime,
                Prezime = prezime,
                Godina = godina,
                StudentStatusId = db.StudentStatus.Where(x => x.StatusNaziv == statusStudenta).FirstOrDefault().StudentStatusId
            };
     
       
            db.Student.Add(student);
            db.SaveChanges();
        }

        [HttpDelete]
        [Authorize]
        [Route("api/Student/DeleteStudent/{StudentId}")]
        public void DeleteStudent(int StudentId)
        {
            var student = db.Student.Where(x => x.StudentId == StudentId).First();
            db.Student.Remove(student);
            db.SaveChanges();
        }

        [HttpPatch]
        [Authorize]
        [Route("api/Student/UpdateStudent/{StudentId}/{BrojIndeksa}/{StatusStudenta}/{Ime?}/{Prezime?}/{Godina?}")]
        public void UpdateStudent(int StudentId, string BrojIndeksa, string StatusStudenta, string Ime = "", string Prezime = "", string Godina = "")
        {
            var result = db.Student.SingleOrDefault(x => x.StudentId == StudentId);
            if (result != null)
            {
                db.Student.SingleOrDefault(x => x.StudentId == StudentId).IndeksBroj = BrojIndeksa;

                if(Ime == "null")
                    db.Student.SingleOrDefault(x => x.StudentId == StudentId).Ime = "";
                else
                db.Student.SingleOrDefault(x => x.StudentId == StudentId).Ime = Ime;
                if (Prezime == "null")
                    db.Student.SingleOrDefault(x => x.StudentId == StudentId).Prezime = "";
                else
                db.Student.SingleOrDefault(x => x.StudentId == StudentId).Prezime = Prezime;
                if (Godina == "null")
                    db.Student.SingleOrDefault(x => x.StudentId == StudentId).Godina = "";
                else
                db.Student.SingleOrDefault(x => x.StudentId == StudentId).Godina = Godina;

                db.Student.SingleOrDefault(x => x.StudentId == StudentId).StudentStatusId = db.StudentStatus.Where(x => x.StatusNaziv == StatusStudenta).FirstOrDefault().StudentStatusId;
                 
                db.SaveChanges();
            }

        }


        [HttpGet]
        [Route("api/Student/GetAllKurs/{StudentId}")]
        [Authorize]
        [EnableCors(origins: "http:/localhost:8080", headers: "*", methods: "*")]
        public List<getAllKursOnStudent_Result> GetKursStudents(int StudentId)
        {
            return db.getAllKursOnStudent(StudentId).ToList();
        }



    }
}
