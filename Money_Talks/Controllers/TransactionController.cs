using System.Data;
using System.Linq;
using System.Web.Mvc;
using Money_Talks.Models;

namespace Money_Talks.Controllers
{
    public class TransactionController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private AccountDbContext transactionsDb = new AccountDbContext();
        private UserDbContext usersDb = new UserDbContext();
        
        //
        // GET: /Account/

        public ViewResult Index()
        {
            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;
            var userModel = user.ToArray();
            ViewBag.balance = userModel[0].Balance;
            return View(transactionsDb.Transactions.ToList());      //Need to change!!!
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
            transaction.Username = User.Identity.Name;

            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;
            var userModel = user.ToArray();
            if (transaction.TransactionType.Equals("Income"))
                userModel[0].Balance += transaction.Amount;
            else
                userModel[0].Balance -= transaction.Amount;
            usersDb.SaveChanges();

            if (ModelState.IsValid)
            {
                
                transactionsDb.Transactions.Add(transaction);
                transactionsDb.SaveChanges();
                return RedirectToAction("runRules", "Rules");
                //return RedirectToAction("Index");
            }
            
            return View(transaction);
        }

        //
        // GET: /Account/Edit/5

        public ActionResult Edit(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            
            //Decrese old transaction amount from balance
            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;
            var userModel = user.ToArray();
            if(transaction.TransactionType.Equals("Income"))
                userModel[0].Balance -= transaction.Amount;
            else
                userModel[0].Balance += transaction.Amount;
            usersDb.SaveChanges();

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

                var user = from u in usersDb.UsersDB
                           where u.Username.Equals(User.Identity.Name)
                           select u;
                var userModel = user.ToArray();
                if (transaction.TransactionType.Equals("Income"))
                    userModel[0].Balance += transaction.Amount;
                else
                    userModel[0].Balance -= transaction.Amount;
                usersDb.SaveChanges();

                transactionsDb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        //
        // GET: /Account/Delete/5

        public ActionResult Delete(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);

            //Decrese transaction amount from balance
            var user = from u in usersDb.UsersDB
                       where u.Username.Equals(User.Identity.Name)
                       select u;
            var userModel = user.ToArray();
            if (transaction.TransactionType.Equals("Income"))
                userModel[0].Balance -= transaction.Amount;
            else
                userModel[0].Balance += transaction.Amount;
            usersDb.SaveChanges();

            return View(transaction);
        }

        public ActionResult Pop()
        {
            return View();
        }


        //
        // POST: /Account/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionModel transaction = transactionsDb.Transactions.Find(id);
            transactionsDb.Transactions.Remove(transaction);
            
            transactionsDb.SaveChanges();
            return RedirectToAction("Index");
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
    }
}