using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Controllers
{
    public class JobController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Job
        public ActionResult Index()
        {
            List<Job> jobs = db.Jobs.ToList();
            ViewBag.Jobs = jobs;
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Job job = db.Jobs.Find(id);
                if (job != null)
                {
                    return View(job);
                }
                return HttpNotFound("Could not find the JOB with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing job id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New()
        {
            Job job = new Job();
            return View(job);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult New(Job jobRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Job job = new Job
                    {
                        Type = jobRequest.Type,
                        Description = jobRequest.Description
                    };
                    db.Jobs.Add(job);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(jobRequest);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(jobRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Job job = db.Jobs.Find(id);
            if(job != null)
            {
                db.Jobs.Remove(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Could not find the job with id " + id.ToString() + "!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Job job = db.Jobs.Find(id);

                if (job == null)
                {
                    return HttpNotFound("Could not find the job with id " + id.ToString() + "!");
                }

                return View(job);
            }
            return HttpNotFound("Missing job id parameter!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Job jobRequest)
        {
            if (ModelState.IsValid)
            {
                Job job = db.Jobs.Find(id);

                if (TryUpdateModel(job))
                {
                    job.JobId = jobRequest.JobId;
                    job.Type = jobRequest.Type;
                    job.Description = jobRequest.Description;

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(jobRequest);
        }
    }
}