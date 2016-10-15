using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                using (GroupDbContext dbc = new GroupDbContext())
                {
                    var GroupMessages = dbc.Group.Where(u => u.GroupName == group)
                        .OrderBy(u => u.Date)
                        .Select(u => u.Date >= DateTime.Now)
                        .ToList();
                    return View(GroupMessages);
                }
            }
        }

        public ActionResult CreateQuest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateQuest(Quests quest)
        {
            var userId = User.Identity.GetUserId();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                var username = user.UserName;
                var group = user.Group;
                using (GroupDbContext dbc = new GroupDbContext())
                {
                    Quests gq = new Quests
                    {
                        GroupName = group,
                        UserName = username,
                        Quest = quest.Quest,
                        Date = quest.Date,
                        Time = quest.Time,
                        AmOrPm = quest.AmOrPm,
                    };
                    dbc.Group.Add(gq);
                    dbc.SaveChanges();
                }
                return RedirectToAction("Quests");
            }
        }
    }
}