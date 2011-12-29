using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Money_Talks.Models;

namespace Money_Talks.Controllers
{
    public class RulesController : Controller
    {
        private RulesDbContext db = new RulesDbContext();

        //
        // GET: /Rules/

        public ViewResult Index()
        {
            Rules firstRule = new Rules
            {
                RuleId = 1,
                UserId = 1, //need to fix it -> must be the current user 
                RuleBorder = 5000, // must be dynamic -> current balance of the current user
                DateCreated = DateTime.Now,
                Category = "*",
            };

            db.Rules.Add(firstRule);
            return View(db.Rules.ToList());
        }

        //
        // GET: /Rules/Details/5

        public ViewResult Details(int id)
        {
            Rules rules = db.Rules.Find(id);
            return View(rules);
        }

        //
        // GET: /Rules/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Rules/Create

        [HttpPost]
        public ActionResult Create(Rules rules)
        {
            if (ModelState.IsValid)
            {
                rules.DateCreated = DateTime.Now;
                rules.UserId = 1; // need to dynamic -> fill with the current user that asking to create RULE
                db.Rules.Add(rules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rules);
        }

        //
        // GET: /Rules/Edit/5

        public ActionResult Edit(int id)
        {
            Rules rules = db.Rules.Find(id);
            return View(rules);
        }

        //
        // POST: /Rules/Edit/5

        [HttpPost]
        public ActionResult Edit(Rules rules)
        {
            if (ModelState.IsValid)
            {
                rules.DateCreated = DateTime.Now;
                db.Entry(rules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rules);
        }

        //
        // GET: /Rules/Delete/5

        public ActionResult Delete(int id)
        {
            Rules rules = db.Rules.Find(id);
            return View(rules);
        }

        //
        // POST: /Rules/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rules rules = db.Rules.Find(id);
            db.Rules.Remove(rules);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}