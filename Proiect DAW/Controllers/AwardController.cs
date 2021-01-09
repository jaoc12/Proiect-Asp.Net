using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Controllers
{
    public class AwardController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Award
        public ActionResult Index()
        {
            List<Award> awards = db.Awards.ToList();
            ViewBag.Awards = awards;
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Award award = db.Awards.Find(id);
                if (award != null)
                {
                    award.AwardedMovies = GetAwardedMovies((int)id);
                    return View(award);
                }
                return HttpNotFound("Could not find the JOB with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing job id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Award award = new Award();
            return View(award);
        }

        [HttpPost]
        public ActionResult New(Award awardRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Award award = new Award
                    {
                        Name = awardRequest.Name,
                        Description = awardRequest.Description
                    };
                    db.Awards.Add(award);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(awardRequest);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(awardRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Award award = db.Awards.Find(id);
            if (award != null)
            {
                db.Awards.Remove(award);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Could not find the award with id " + id.ToString() + "!");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Award award = db.Awards.Find(id);

                if (award == null)
                {
                    return HttpNotFound("Could not find the award with id " + id.ToString() + "!");
                }

                return View(award);
            }
            return HttpNotFound("Missing award id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Award awardRequest)
        {
            if (ModelState.IsValid)
            {
                Award award = db.Awards.Find(id);

                if (TryUpdateModel(award))
                {
                    award.Name = awardRequest.Name;
                    award.Description = awardRequest.Description;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(awardRequest);
        }

        [NonAction]
        public List<Movie> GetAwardedMovies(int id)
        {
            List<Movie> movies = db.Movies.ToList();
            List<Movie> awardedMovies = new List<Movie>();

            foreach (Movie movie in movies)
            {
                if (movie.Awards.Any(a => a.AwardId == id))
                {
                    awardedMovies.Add(movie);
                }
            }

            return awardedMovies;
        }
    }
}