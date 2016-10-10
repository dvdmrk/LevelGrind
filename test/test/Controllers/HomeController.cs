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
            return View();
        }

        [HttpPost]
        public ActionResult Training(RegisterViewModel updatetraining)
        {
            var userId = User.Identity.GetUserId();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                user.TrainingTotal = user.TrainingTotal + updatetraining.TrainingTotal;
                db.SaveChanges();
            }
            return RedirectToAction("Avatar");
        }

        public ActionResult Gains()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gains(RegisterViewModel weightloss)
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

        public ActionResult Avatar()
        {
            var userId = User.Identity.GetUserId();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                ViewBag.Message = "Your application description page.";
                return View(user);
            }
        }
        
        [HttpGet]
        public ActionResult Avatar(string pseudonym)
        {
            var userId = pseudonym;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Pseudonym == pseudonym);
                ViewBag.Message = "Your application description page.";
                return View(user);
            }
        }
    }
}