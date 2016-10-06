using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLoginRegistration.Models;

namespace MvcLoginRegistration.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList().OrderBy( u => u.Age));
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.UserName + " has entered the arena!";
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var valid = db.userAccount.Single(u => u.UserName == user.UserName && u.Password == user.Password);
                if (valid != null)
                {
                    Session["UserId"] = valid.UserId;
                    Session["UserName"] = valid.UserName.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password... defeated!");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {

                string userName = (string)(Session["UserName"]);
                using (OurDbContext db = new OurDbContext())
                {
                    return View(db.userAccount.Where(u => u.UserName == userName).Select(u => u).Single());
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}