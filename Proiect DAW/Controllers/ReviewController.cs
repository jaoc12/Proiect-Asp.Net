using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Controllers
{
    public class ReviewController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Review
        public ActionResult Index()
        {
            List<Movie> movies = db.Movies.ToList();
            ViewBag.Movies = movies;
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Movie movie = db.Movies.Find(id);
                if (movie != null)
                {
                    return View(movie);
                }
                return HttpNotFound("Could not find the movie with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing movie id parameter!");
        }

        [HttpGet]
        public ActionResult New(int? id)
        {
            if (id.HasValue)
            {
                Review review = new Review();
                review.MovieId = (int)id;
                review.AuthorEmail = User.Identity.Name;
                return View(review);
            }
            return HttpNotFound("Missing movie id parameter!");
        }

        [HttpPost]
        public ActionResult New(Review reviewRequest)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review
                {
                    Content = reviewRequest.Content,
                    Rating = reviewRequest.Rating,
                    MovieId = reviewRequest.MovieId,
                    AuthorEmail = reviewRequest.AuthorEmail
                };
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reviewRequest);
        }

        [HttpDelete]
        public ActionResult Delete(int id, string author)
        {
            Review review = db.Reviews.FirstOrDefault(r => r.AuthorEmail.Equals(author) && r.MovieId == id);
            if (review != null)
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Could not find the review of the movie with id " + id.ToString() +
                " written by author with email " + author + "!");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var author = User.Identity.Name;
                Review review = db.Reviews.FirstOrDefault(r => r.AuthorEmail.Equals(author) && r.MovieId == id);

                if (review == null)
                {
                    return HttpNotFound("Could not find the review of the movie with id " + id.ToString() +
                        " written by author with email " + author + "!");
                }

                return View(review);
            }

            return HttpNotFound("Missing movie id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Review reviewRequest)
        {
            if (ModelState.IsValid)
            {
                var author = User.Identity.Name;
                Review review = db.Reviews.FirstOrDefault(r => r.AuthorEmail.Equals(author) && r.MovieId == id);

                if (TryUpdateModel(review))
                {
                    review.Content = reviewRequest.Content;
                    review.Rating = reviewRequest.Rating;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(reviewRequest);
        }
    }
}