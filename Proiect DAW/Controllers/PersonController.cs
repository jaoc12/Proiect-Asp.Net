using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Controllers
{
    public class PersonController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Person
        public ActionResult Index()
        {
            List<Person> people = db.People.ToList();
            ViewBag.People = people;
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Person person = db.People.Find(id);
                if (person != null)
                {
                    person.MoviesList = GetMovies(id);
                    return View(person);
                }
                return HttpNotFound("Could not find the person with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing person id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New()
        {
            PersonContactViewModel personContact = new PersonContactViewModel();
            return View(personContact);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(PersonContactViewModel personContact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactInfo contactInfo = new ContactInfo
                    {
                        ContactEmail = personContact.ContactEmail,
                        ContactPhone = personContact.ContactPhone
                    };

                    Person person = new Person
                    {
                        Name = personContact.Name,
                        Birthday = personContact.Birthday,
                        ContactInfo = contactInfo
                    };

                    db.ContactInfos.Add(contactInfo);
                    db.People.Add(person);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(personContact);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(personContact);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Person person = db.People.Find(id);
            ContactInfo contact = db.ContactInfos.Find(person.ContactInfo.ContactInfoId);
            if (person != null)
            {
                db.ContactInfos.Remove(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Could not find the person with id " + id.ToString() + "!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Person person = db.People.Find(id);

                if (person == null)
                {
                    return HttpNotFound("Could not find the person with id " + id.ToString() + "!");
                }

                PersonContactViewModel personContact = new PersonContactViewModel
                {
                    Name = person.Name,
                    ContactEmail = person.ContactInfo.ContactEmail,
                    ContactPhone = person.ContactInfo.ContactPhone,
                    Birthday = person.Birthday,
                    PersonId = person.PersonId
                };

                return View(personContact);
            }
            return HttpNotFound("Missing person id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, PersonContactViewModel personContact)
        {
            if (ModelState.IsValid)
            {
                Person person = db.People.Find(id);
                ContactInfo contactInfo = db.ContactInfos.Find(person.ContactInfo.ContactInfoId);

                if (TryUpdateModel(contactInfo))
                {
                    contactInfo.ContactEmail = personContact.ContactEmail;
                    contactInfo.ContactPhone = personContact.ContactPhone;

                    db.SaveChanges();
                }

                if (TryUpdateModel(person))
                {
                    person.Name = personContact.Name;
                    person.Birthday = personContact.Birthday;
                    person.PersonId = id;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(personContact);
        }

        private List<Movie> GetMovies(int? id)
        {
            List<Movie> allMovies = db.Movies.ToList();
            List<Movie> selectedMovies = new List<Movie>();

            foreach(Movie movie in allMovies)
            {
                if(movie.Credits.Any(c => c.PersonId == id))
                {
                    selectedMovies.Add(movie);
                }
            }

            return selectedMovies;
        }
    }
}