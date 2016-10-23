using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class GroupsController : Controller
    {
        public ActionResult Clan()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var usergroup = db.Users.SingleOrDefault(u => u.Id == userId);
                var group = usergroup.Group;
                ViewBag.Group = group;
                if (group != null)
                {
                    var user = db.Users.ToList().OrderByDescending(u => u.TrainingTotal).Where(u => u.Group == group);
                    return View(user);
                }
            }
            return RedirectToAction("Create");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RegisterViewModel joinclan)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                user.Group = joinclan.Group;
                db.SaveChanges();
            }
            return RedirectToAction("Clan");
        }

        public ActionResult Quests()
        {
            var yesterday = DateTime.Now.AddDays(-1);
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var userid = User.Identity.GetUserId();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Id == userid);
                var group = user.Group;
                using (QuestDBContext dbc = new QuestDBContext())
                {
                    var GroupMessages = dbc.Quests.Where(u => u.GroupName == group)
                        .OrderBy(u => u.Date)
                        .Where(u => u.Date >= yesterday)
                        .ToList();
                    return View(GroupMessages);
                }
            }
        }

        public ActionResult CreateQuest()
        {
            return View(new Quest() { Date = DateTime.Now.Date });
        }

        [HttpPost]
        public ActionResult CreateQuest(Quest quest)
        {
            var userId = User.Identity.GetUserId();
            var gq = new Quest();
            gq.Date = quest.Date;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                var username = user.Pseudonym;
                var group = user.Group;
                using (QuestDBContext dbc = new QuestDBContext())
                {
                    gq.GroupName = group;
                    gq.UserName = username;
                    dbc.Quests.Add(gq);
                    dbc.SaveChanges();
                }
                return RedirectToAction("Quests");
            }
        }
    }
}