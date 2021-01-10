using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Controllers
{
    public class StudioController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Studio
        public ActionResult Index()
        {
            List<Studio> studios = db.Studios.ToList();
            ViewBag.Studios = studios;
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Studio studio = db.Studios.Find(id);
                if (studio != null)
                {
                    List<Movie> movies = db.Movies.ToList().FindAll(m => m.StudioId == id);
                    if (movies.Count() != 0)
                    {
                        studio.Movies = movies;
                    }
                    return View(studio);
                }
                return HttpNotFound("Could not find the studio with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing studio id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New()
        {
            Studio studio = new Studio();
            return View(studio);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(Studio studioRequest)
        {
            if (ModelState.IsValid)
            {
                Studio studio = new Studio
                {
                    Name = studioRequest.Name,
                    CEO = studioRequest.CEO,
                    FoundingDate = studioRequest.FoundingDate
                };
                db.Studios.Add(studio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studioRequest);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Studio studio = db.Studios.Find(id);
            if (studio != null)
            {
                db.Studios.Remove(studio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Could not find the studio with id " + id.ToString() + "!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Studio studio = db.Studios.Find(id);

                if (studio == null)
                {
                    return HttpNotFound("Could not find the studio with id " + id.ToString() + "!");
                }

                return View(studio);
            }
            return HttpNotFound("Missing studio id parameter!");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Studio studioRequest)
        {
            if (ModelState.IsValid)
            {

                Studio studio = db.Studios.Find(id);

                if (TryUpdateModel(studio))
                {
                    studio.Name = studioRequest.Name;
                    studio.FoundingDate = studioRequest.FoundingDate;
                    studio.CEO = studioRequest.CEO;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(studioRequest);
        }
    }
}