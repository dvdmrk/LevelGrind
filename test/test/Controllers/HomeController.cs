using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tavern()
        {
            return View();
        }

        public ActionResult Ranking()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.ToList().OrderByDescending(u => u.TrainingTotal);
                ViewBag.Message = "Your application description page.";
                return View(user);
            }
        }

        public ActionResult Training()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Training(RegisterViewModel updatetraining)
        {
            if (updatetraining.TrainingTotal > 0 && updatetraining.TrainingTotal <= 60)
            {
                var userId = User.Identity.GetUserId();
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var user = db.Users.SingleOrDefault(u => u.Id == userId);
                    if (user.LastTrainedOn == DateTime.Now.Date)
                    {
                        ViewBag.Message = "Invalid";
                        ViewBag.Details = "Cannot train more than once per day, while that does encourage real life results, it causes in game errors. Sorry";
                        return View();
                    }
                    else
                    {
                        user.TrainingTotal = user.TrainingTotal + updatetraining.TrainingTotal;
                        user.LastTrainedOn = DateTime.Now.Date;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Avatar");
            }
            else
            {
                ViewBag.Message = "Invalid";
                ViewBag.Details = "Cannot train for durations of 0 or less. This may have also occured because while training durations greater than 60 minutes result in real life results, they cause in game errors. Sorry";
                return View();
            }
        }

        public ActionResult Gains()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gains(RegisterViewModel weightloss)
        {
            if (weightloss.CurrentWeight > 0)
            {
                var userId = User.Identity.GetUserId();
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var user = db.Users.SingleOrDefault(u => u.Id == userId);
                    user.CurrentWeight = weightloss.CurrentWeight;
                    db.SaveChanges();
                }
                return RedirectToAction("Avatar");
            }
            else
            {
                ViewBag.Message = "Invalid";
                ViewBag.Details = "You seriously can't weigh less than 0 pounds...";
                return View();
            }
        }

        //public ActionResult Avatar()
        //{
        //    var userId = User.Identity.GetUserId();
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        var user = db.Users.SingleOrDefault(u => u.Id == userId);
        //        ViewBag.Message = "Your application description page.";
        //        return View(user);
        //    }
        //}

        [HttpGet]
        public ActionResult Avatar(string pseudonym)
        {
            var userId = pseudonym;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (userId == null)
                {
                    userId = User.Identity.GetUserId();
                    if (userId == null)
                    {
                        return RedirectToAction("Index");
                    }
                    var uesr = db.Users.SingleOrDefault(u => u.Id == userId);
                    return View(uesr);
                }
                var user = db.Users.SingleOrDefault(u => u.Pseudonym == userId);
                return View(user);
            }
        }
    }
}