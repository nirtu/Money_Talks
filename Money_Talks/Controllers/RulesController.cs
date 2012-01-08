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
        private AccountDbContext adb = new AccountDbContext();

        //
        // GET: /Rules/

        public ViewResult Index()
        {
            var rls = from r in db.Rules
                      where r.username.Equals(User.Identity.Name)
                      select r;

            return View(rls);
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
            var categoriesFromDB = from r in adb.Transactions
                                   where r.Username.Equals(User.Identity.Name) & r.TransactionType.Equals("Outcome")
                                   select r.Category;


            List<string> listOfOutcomesCategories = new List<string>();

            foreach (string x in categoriesFromDB)
                if (!listOfOutcomesCategories.Contains(x))
                    listOfOutcomesCategories.Add(x);


            ViewBag.listOfOutcomesCategories = listOfOutcomesCategories;
            int sizeOfList = listOfOutcomesCategories.Count;

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
                rules.username = User.Identity.Name;
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
                rules.username = User.Identity.Name;
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

        public ActionResult runRules()
        {

            List<faultsContainer> fList = new List<faultsContainer>();
            var categoryAndRuleBorderSet = from r in db.Rules
                                           where r.username.Equals(User.Identity.Name)
                                           select r;

            foreach (var categoryAndRuleBorder in categoryAndRuleBorderSet)
            {
                var transactions = from t in adb.Transactions
                                   where t.Username.Equals(User.Identity.Name) & t.Category.Equals(categoryAndRuleBorder.Category)
                                   select t;

                int sumAllAmount = 0;

                foreach (var x in transactions)
                {
                    if (x.Category.Equals("Income"))
                        sumAllAmount -= (int)x.Amount;
                    else
                        sumAllAmount += (int)x.Amount;
                }

                if (sumAllAmount > categoryAndRuleBorder.RuleBorder)
                {
                    //
                    // add this to array that contains all faults 
                    //

                    fList.Add(new faultsContainer
                    {
                        deviation = (sumAllAmount - categoryAndRuleBorder.RuleBorder),
                        category = categoryAndRuleBorder.Category
                    });
                }


                sumAllAmount = 0;
            }
            ViewBag.faultList = fList;
            return View("runRules");
        }
    }
}