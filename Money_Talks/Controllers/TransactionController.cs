using System.Data;
using System.Linq;
using System.Web.Mvc;
using Money_Talks.Models;
using System;

namespace Money_Talks.Controllers
{
    /// <summary>
    /// This class handles all of the user transactions operations
    /// </summary>
    public class TransactionController : Controller
    {
        //creating a "link" to the DBs

        private AccountDbContext transactionsDb = new AccountDbContext();
        private UserDbContext usersDb = new UserDbContext();
        
        public ViewResult Tabs()
        {
            return View();
        }

        // Presenting the transactions to the sser (his own transactions)
        public ActionResult Index()
        {
            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;

            var userModel = user.ToArray();
            ViewBag.balance = userModel[0].Balance;

            // Only the appropriate user transactions
            var userTransactions = from ut in transactionsDb.Transactions
                                   where ut.Username.Equals(User.Identity.Name) &&
                                   (ut.DateCreated.Month == DateTime.Now.Month)
                                   orderby ut.DateCreated
                                   select ut;

            return View(userTransactions);
        }

        /// <summary>
        /// This method provides a support to display transactions by a given date range
        /// </summary>
        /// <param name="startDate">The start date value</param>
        /// <param name="endDate">The end date value</param>
        /// <param name="submit">The submit button value</param>
        /// <returns>The "Index" view with the relevant transactions</returns>
        [HttpPost]
        public ActionResult Index(DateTime startDate, DateTime endDate, string submit)
        {
            var userTransactions = from ut in transactionsDb.Transactions
                                   where ut.Username.Equals(User.Identity.Name) &&
                                   (ut.DateCreated >= startDate && ut.DateCreated <= endDate)
                                   orderby ut.DateCreated
                                   select ut;

            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;

            var userModel = user.ToArray();
            ViewBag.balance = userModel[0].Balance;

            return View(userTransactions);
        }

        /// <summary>
        /// Allows to display transaction details
        /// </summary>
        /// <param name="id">The transaction ID in the database</param>
        /// <returns>The "Details" view</returns>
        public ViewResult Details(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            return View(transaction);
        }

        /// <summary>
        /// Allows to create a transaction
        /// </summary>
        /// <returns>The "Create" view</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Allows to create a transaction
        /// </summary>
        /// <param name="transaction">The transaction to add</param>
        /// <returns>The "Index" view if the operation was valid, the "Create" view otherwise</returns>
        [HttpPost]
        public ActionResult Create(TransactionModel transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Username = User.Identity.Name;
                transactionsDb.Transactions.Add(transaction);
                transactionsDb.SaveChanges();

                UpdateBalanceAfterAddingTransaction(transaction);

                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        /// <summary>
        /// Allows to edit a transaction (serves as a link to another view stage)
        /// </summary>
        /// <param name="id">The transaction ID in the database</param>
        /// <returns>The "Edit" view</returns>
        public ActionResult Edit(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            UpdateBalanceAfterRemovingTransaction(transaction);
            return View(transaction);
        }

        /// <summary>
        /// Allows to edit a transaction
        /// </summary>
        /// <param name="transaction">The transaction to be edited</param>
        /// <returns>The "Index" view if the operation was valid, the "Edit" view otherwise</returns>
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

        /// <summary>
        /// Allows to delete a transaction from the database
        /// </summary>
        /// <param name="id">The record ID to be deleted</param>
        /// <returns>The "Delete" view</returns>
        public ActionResult Delete(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            return View(transaction);
        }

        /// <summary>
        /// Handles delete confirmation
        /// </summary>
        /// <param name="id">The record ID to be delete</param>
        /// <returns>The "Index" view</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            transactionsDb.Transactions.Remove(transaction);
            transactionsDb.SaveChanges();

            UpdateBalanceAfterRemovingTransaction(transaction);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// A link to the "pop" tab view
        /// </summary>
        /// <returns>The "Pop" view</returns>
        public ActionResult Pop()
        {
            return View();
        }

        /// <summary>
        /// A link to the "about" tab view
        /// </summary>
        /// <returns>The "About" view</returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Dispose all managed and unmanaged resources
        /// </summary>
        /// <param name="disposing">If true release both managed and unmanaged resources</param>
        protected override void Dispose(bool disposing)
        {
            transactionsDb.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Update the balance attribute once a transaction is added
        /// </summary>
        /// <param name="transaction">The transaction to be added</param>
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

        /// <summary>
        /// Update the balance attribute once a transaction is removed
        /// </summary>
        /// <param name="transaction">The transaction to be removed</param>
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

        /// <summary>
        /// Check balance addition
        /// </summary>
        /// <param name="balance">The balance to check</param>
        /// <param name="transactionAmount">The amount to add</param>
        /// <returns>The updated balance</returns>
        private int balanceCheckAdd(int balance, int transactionAmount)
        {
            balance += transactionAmount;

            return balance;
        }

        /// <summary>
        /// Check balance subtraction
        /// </summary>
        /// <param name="balance">The balance to check</param>
        /// <param name="transactionAmount">The amount to subtract</param>
        /// <returns>The updated balance</returns>
        private int balanceCheckRemove(int balance, int transactionAmount)
        {
            balance -= transactionAmount;

            return balance;
        }
    }
}