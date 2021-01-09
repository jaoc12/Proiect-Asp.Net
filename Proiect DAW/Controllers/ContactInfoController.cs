using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Controllers
{
    public class ContactInfoController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: ContactInfo
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
                Person person = db.People.FirstOrDefault(p => p.ContactInfo.ContactInfoId == id);
                if (person != null)
                {
                    return RedirectToAction("Details", "Person", new { id = person.PersonId });
                }
                return HttpNotFound("Could not find the contact info with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing contact info id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            PersonContactViewModel personContact = new PersonContactViewModel();
            return View(personContact);
        }

        [HttpPost]
        public ActionResult New(PersonContactViewModel personContact)
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
                    ContactInfo = contactInfo,
                    Birthday = personContact.Birthday

                };

                db.ContactInfos.Add(contactInfo);
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personContact);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            ContactInfo contact = db.ContactInfos.Find(id);
            if (contact != null)
            {
                db.ContactInfos.Remove(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Could not find the contact info with id " + id.ToString() + "!");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                ContactInfo contact = db.ContactInfos.Find(id);

                if (contact == null)
                {
                    return HttpNotFound("Could not find the contact info with id " + id.ToString() + "!");
                }

                return View(contact);
            }
            return HttpNotFound("Missing contact info id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, ContactInfo contactRequest)
        {
            if (ModelState.IsValid)
            {
                ContactInfo contactInfo = db.ContactInfos.Find(id);

                if (TryUpdateModel(contactInfo))
                {
                    contactInfo.ContactEmail = contactRequest.ContactEmail;
                    contactInfo.ContactPhone = contactRequest.ContactPhone;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(contactRequest);
        }
    }
}