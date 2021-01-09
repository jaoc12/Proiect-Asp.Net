using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Controllers
{
    public class MovieController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Movie
        [HttpGet]
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
                    movie = SetCredits(movie);
                    movie.Studio = db.Studios.Find(movie.StudioId);
                    movie.JobsList = db.Jobs.ToList();
                    return View(movie);
                }
                return HttpNotFound("Could not find the movie with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing movie id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Movie movie = new Movie();
            movie.StudiosList = GetAllStudios();
            movie.PeopleList = GetAllPeople();
            movie.JobsList = db.Jobs.ToList();
            movie.CreditsList = GetAllPossibleCredits();
            movie.AwardsList = GetAllAwards();
            movie.Awards = new List<Award>();

            return View(movie);
        }

        [HttpPost]
        public ActionResult New(Movie movieRequest)
        {
            movieRequest.StudiosList = GetAllStudios();
            movieRequest.PeopleList = GetAllPeople();
            movieRequest.JobsList = db.Jobs.ToList();
            var selectedCredits = movieRequest.CreditsList.Where(m => m.Checked).ToList();
            var selectedAwards = movieRequest.AwardsList.Where(m => m.Checked).ToList();
            if (ModelState.IsValid)
            {
                movieRequest.Awards = new List<Award>();
                for (int i = 0; i < selectedAwards.Count(); i++)
                {
                    Award award = db.Awards.Find(selectedAwards[i].Id);
                    movieRequest.Awards.Add(award);
                }

                movieRequest.Credits = new List<Credit>();
                for (int k = 0; k < selectedCredits.Count(); k++)
                {
                    int i = selectedCredits[k].Id / db.People.ToList().Count();
                    int j = selectedCredits[k].Id % db.People.ToList().Count();

                    Credit credit = new Credit
                    {
                        JobId = i + 1,
                        PersonId = j + 1
                    };
                    movieRequest.Credits.Add(credit);
                }

                db.Movies.Add(movieRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieRequest);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Movie movie = db.Movies.Find(id);

                if (movie == null)
                {
                    return HttpNotFound("Could not find the movie with id " + id.ToString() + "!");
                }

                movie.StudiosList = GetAllStudios();
                movie.PeopleList = GetAllPeople();
                movie.JobsList = db.Jobs.ToList();
                movie.CreditsList = GetAllPossibleCredits();
                movie.AwardsList = GetAllAwards();

                foreach (Award checkedAward in movie.Awards)
                {
                    movie.AwardsList.FirstOrDefault(a => a.Id == checkedAward.AwardId).Checked = true;
                }

                foreach (CheckBoxViewModel check in movie.CreditsList)
                {
                    int i = check.Id / movie.PeopleList.Count();
                    int j = check.Id % movie.PeopleList.Count();

                    if(movie.Credits.Any(c => c.JobId == i + 1 && c.PersonId == j + 1))
                    {
                        check.Checked = true;
                    }
                }

                return View(movie);
            }
            return HttpNotFound("Missing movie id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Movie movieRequest)
        {
            movieRequest.StudiosList = GetAllStudios();
            movieRequest.PeopleList = GetAllPeople();
            movieRequest.JobsList = db.Jobs.ToList();
            var selectedCredits = movieRequest.CreditsList.Where(m => m.Checked).ToList();
            var selectedAwards = movieRequest.AwardsList.Where(m => m.Checked).ToList();
            Movie movie = db.Movies.Find(id);

            if (ModelState.IsValid)
            {
                if (TryUpdateModel(movie))
                {
                    movie.Title = movieRequest.Title;
                    movie.StudioId = movieRequest.StudioId;
                    movie.Description = movieRequest.Description;

                    for (int i = 0; i < selectedAwards.Count(); i++)
                    {
                        Award award = db.Awards.Find(selectedAwards[i].Id);
                        movie.Awards.Add(award);
                    }

                    List<Credit> credits = db.Credits.Where(c => c.MovieId == movie.MovieId).ToList();
                    foreach (Credit credit in credits)
                    {
                        db.Credits.Remove(credit);
                    }
                    movie.Credits = new List<Credit>();

                    for (int k = 0; k < selectedCredits.Count(); k++)
                    {
                        int i = selectedCredits[k].Id / db.People.ToList().Count();
                        int j = selectedCredits[k].Id % db.People.ToList().Count();

                        Credit credit = new Credit
                        {
                            JobId = i + 1,
                            PersonId = j + 1
                        };
                        movie.Credits.Add(credit);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(movieRequest);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            List<Credit> credits = db.Credits.Where(c => c.MovieId == movie.MovieId).ToList();
            if (movie != null)
            {
                db.Movies.Remove(movie);
                foreach(Credit credit in credits)
                {
                    db.Credits.Remove(credit);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Could not find the person with id " + id.ToString() + "!");
        }

        [NonAction]
        public Movie SetCredits(Movie movie)
        {
            foreach(var credit in movie.Credits)
            {
                credit.Person = db.People.Find(credit.PersonId);
                credit.Job = db.Jobs.Find(credit.JobId);
            }
            return movie;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllStudios()
        {
            var selectList = new List<SelectListItem>();
            foreach (var studio in db.Studios.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = studio.StudioId.ToString(),
                    Text = studio.Name
                });
            }
            return selectList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllJobs()
        {
            var selectList = new List<SelectListItem>();
            foreach (var job in db.Jobs.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = job.JobId.ToString(),
                    Text = job.Type
                });
            }
            return selectList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllPeople()
        {
            var selectList = new List<SelectListItem>();
            foreach (var person in db.People.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = person.PersonId.ToString(),
                    Text = person.Name
                });
            }
            return selectList;
        }

        [NonAction]
        public List<CheckBoxViewModel> GetAllPossibleCredits()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            for (int i = 0; i < db.Jobs.ToList().Count(); i++)
            {
                for (int j = 0; j < db.People.ToList().Count(); j++)
                {
                    checkboxList.Add(new CheckBoxViewModel
                    {
                        Id = i * db.People.ToList().Count() + j,
                        Name = db.People.ToList()[j].Name,
                        Checked = false
                    }); ;
                }
            }
            return checkboxList;
        }

        [NonAction]
        public List<CheckBoxViewModel> GetAllAwards()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var award in db.Awards.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = award.AwardId,
                    Name = award.Name,
                    Checked = false
                });
            }
            return checkboxList;
        }
    }
}