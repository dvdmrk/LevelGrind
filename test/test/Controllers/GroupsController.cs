﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                var user = db.Users.SingleOrDefault(u => u.Id == userId);
                var group = user.Group;
                ViewBag.Group = group;
                if (group != null)
                {
                    foreach (var hero in db.Users.ToList().Where(u => u.Group == group))
                    {
                        int alsoQuesting = 0;

                        foreach (var item in db.Quests.Where(u => u.UserName == hero.Pseudonym))
                        {
                            if (item.AlsoQuesting == null)
                            {
                                continue;
                            }
                            alsoQuesting = alsoQuesting + item.AlsoQuesting.Split(',').Length - 1;
                        }
                        hero.QuestPoints = alsoQuesting + db.Quests.Where(u => u.UserName == hero.Pseudonym).Count();
                    }
                    db.SaveChanges();
                    var users = db.Users.ToList()
                        .OrderByDescending(u => u.QuestPoints)
                        .Where(u => u.Group == group);
                    return View(users);
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
                if (group == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var GroupMessages = db.Quests.Where(u => u.GroupName == group)
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
                var alreadyAQuest = db.Quests.Where(u => u.Date == quest.Date).Count();
                if (alreadyAQuest > 0)
                {
                    var currentQuest = db.Quests.SingleOrDefault(u => u.Date == quest.Date);
                    if (!(currentQuest.UserName.Equals(username)) && currentQuest.AlsoQuesting != null)
                    {
                        var alreadyInQuest = currentQuest.AlsoQuesting.Split(' ');
                        var amIInQuest = alreadyInQuest.Where(u => u == username + ',').Count();
                        if (amIInQuest > 0)
                        {
                            ViewBag.Message = "Invalid";
                            ViewBag.Details = "You are already involved in this quest";
                            return View();
                        }
                        currentQuest.AlsoQuesting = string.Join(" ", alreadyInQuest);
                        currentQuest.AlsoQuesting = currentQuest.AlsoQuesting + " " + username + ", ";
                        db.SaveChanges();
                    }
                    else if (!(currentQuest.UserName == username) && currentQuest.AlsoQuesting == null)
                    {
                        currentQuest.AlsoQuesting = currentQuest.AlsoQuesting + username + ", ";
                        db.SaveChanges();
                    }
                    else
                    {
                        ViewBag.Message = "Invalid";
                        ViewBag.Details = "You are already involved in this quest";
                        return View();
                    }
                }
                else
                {
                    gq.GroupName = group;
                    gq.UserName = username;
                    db.Quests.Add(gq);
                    db.SaveChanges();
                }
                return RedirectToAction("Quests");
            }
        }
    }
}