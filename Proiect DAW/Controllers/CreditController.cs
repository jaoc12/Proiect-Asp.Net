using Proiect_DAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_DAW.Controllers
{
    public class CreditController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Credit
        public ActionResult Index()
        {
            List<Credit> credits = db.Credits.ToList();
            ViewBag.Credits = credits;
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
    }
}