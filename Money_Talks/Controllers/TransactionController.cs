﻿using System.Data;
using System.Linq;
using System.Web.Mvc;
using Money_Talks.Models;

namespace Money_Talks.Controllers
{
    public class TransactionController : Controller
    {
        private AccountDbContext transactionsDb = new AccountDbContext();
        private UserDbContext usersDb = new UserDbContext();
        
        //
        // GET: /Account/

        public ViewResult Tabs()
        {
            return View();
        }

        public ViewResult Index()
        {
            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;
            var userModel = user.ToArray();
            ViewBag.balance = userModel[0].Balance;

            //Only the appropriate user transactions
            var userTransactions = from ut in transactionsDb.Transactions
                                   where ut.Username.Equals(User.Identity.Name)
                                   select ut;
            
            return View(userTransactions);
        }

        //
        // GET: /Account/Details/5

        public ViewResult Details(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            return View(transaction);
        }

        //
        // GET: /Account/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult Create(TransactionModel transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Username = User.Identity.Name;
                transactionsDb.Transactions.Add(transaction);
                transactionsDb.SaveChanges();

                UpdateBalanceAfterAddingTransaction(transaction);

                //return RedirectToAction("runRules", "Rules");
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        //
        // GET: /Account/Edit/5

        public ActionResult Edit(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            UpdateBalanceAfterRemovingTransaction(transaction);
            return View(transaction);
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        public ActionResult Edit(TransactionModel transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Username = User.Identity.Name;
                transactionsDb.Entry(transaction).State = EntityState.Modified;
                transactionsDb.SaveChanges();

                UpdateBalanceAfterAddingTransaction(transaction);
                
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        //
        // GET: /Account/Delete/5

        public ActionResult Delete(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            return View(transaction);
        }

        //
        // POST: /Account/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            transactionsDb.Transactions.Remove(transaction);
            transactionsDb.SaveChanges();

            UpdateBalanceAfterRemovingTransaction(transaction);

            return RedirectToAction("Index");
        }

        public ActionResult Pop()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            transactionsDb.Dispose();
            base.Dispose(disposing);
        }

        private void UpdateBalanceAfterAddingTransaction(TransactionModel transaction)
        {
            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;
            var userModel = user.ToArray();
            if (transaction.TransactionType.Equals("Income"))
                userModel[0].Balance += transaction.Amount;
            else
                userModel[0].Balance -= transaction.Amount;
            usersDb.SaveChanges();
        }

        private void UpdateBalanceAfterRemovingTransaction(TransactionModel transaction)
        {
            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;
            var userModel = user.ToArray();
            if (transaction.TransactionType.Equals("Income"))
                userModel[0].Balance -= transaction.Amount;
            else
                userModel[0].Balance += transaction.Amount;
            usersDb.SaveChanges();
        }

        //---------------------------------------------------------------------

        private int balanceCheckAdd(int balance, int transactionAmount)
        {
            balance += transactionAmount;

            return balance;
        }

        private int balanceCheckRemove(int balance, int transactionAmount)
        {
            balance -= transactionAmount;

            return balance;
        }
    }
}