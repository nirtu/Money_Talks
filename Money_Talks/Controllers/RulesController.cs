/*
 * THE CONTROLLER - this is where the magic happens  
 * This controller create all the functionalities that a rule can give for a user 
 * 
 * 
 */

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
        //creating a "link" to the DBs

        private static RulesDbContext db = new RulesDbContext();
        private static AccountDbContext adb = new AccountDbContext();
        private static UserDbContext udb = new UserDbContext();

        //presenting the Rules to the User (his own Rules)
        public ViewResult Index()
        {
            var rls = from r in db.Rules
                      where r.username.Equals(User.Identity.Name)
                      select r;

            ViewBag.breakingRules = runRules(this.User.Identity.Name);

            return View(rls);
        }

        //presenting the a detailed look of one rule to the user (his own Rule)
        public ViewResult Details(int id)
        {
            Rules rules = db.Rules.Find(id);
            return View(rules);
        }

        //GET 
        //allows to the user create new Rule
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

        // POST
        // store the Rule in the DB 

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

        // GET
        //allows to the user to reset a specific Rule 

        public ActionResult Edit(int id)
        {
            Rules rules = db.Rules.Find(id);
            return View(rules);
        }

        
        // POST
        // Store the edit changes 
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

        
        // GET
        // allows user to delete a Rule form his list of Rules 

        public ActionResult Delete(int id)
        {
            Rules rules = db.Rules.Find(id);
            return View(rules);
        }

        
        // POST
        // delete from the data base 

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rules rules = db.Rules.Find(id);
            db.Rules.Remove(rules);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        //this function run all the rules and return a list of rules that has been broke
        public static List<string> runRules(string currUser)
        {
            int sumAllAmount;
            string str;
            List<string> res = new List<string>();

            //check if the user has overdraft
            var currBalance = from s in udb.UsersDB
                              where s.Username.Equals(currUser)
                              select s.Balance;

            var currentBalance = currBalance.ToArray();

            if (currentBalance[0] < 0)
            {
                str = "The Main rule limit has been broke - your balance is below zero";
                res.Add(str);
            }

            //checking for any Rules breaking             
            var categoryAndRuleBorderSets = from r in db.Rules
                                           where r.username.Equals(currUser)
                                           select r;

            var categoryAndRuleBorderSet = categoryAndRuleBorderSets.ToArray();

            foreach (var categoryAndRuleBorder in categoryAndRuleBorderSet)
            {
                var transactions = from t in adb.Transactions
                                   where t.Username.Equals(currUser) &
                                         t.Category.Equals(categoryAndRuleBorder.Category) &
                                         t.DateCreated.Month.Equals(DateTime.Now.Month) &
                                         t.DateCreated.Year.Equals(DateTime.Now.Year)
                                   select t;

                sumAllAmount = 0;

                foreach (var x in transactions)
                {
                    if (x.Category.Equals("Income"))
                        sumAllAmount -= (int)x.Amount;
                    else
                        sumAllAmount += (int)x.Amount;
                }

                if (sumAllAmount > categoryAndRuleBorder.RuleBorder)
                {
                    //creatig a MSG for each Rule breaking 
                    str = "The " + categoryAndRuleBorder.Category + " rule limitation has been broke with a deviation of " + (sumAllAmount - categoryAndRuleBorder.RuleBorder) + " NIS";
                    res.Add(str);
                }

                sumAllAmount = 0;
            }

            return res;
        }
    }
}