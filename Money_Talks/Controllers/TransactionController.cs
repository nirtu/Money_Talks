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
        private AccountDbContext db = new AccountDbContext();
        
        //
        // GET: /Account/

        public ViewResult Index()
        {
            return View(db.Transactions.ToList());
        }

        //
        // GET: /Account/Details/5

        public ViewResult Details(int id)
        {
            TransactionModel transaction = db.Transactions.Find(id);
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

            if (ModelState.IsValid)
            {
                
                db.Transactions.Add(transaction);

                foreach (var trans in db.Transactions)
                {
                    if (trans != null)
                        transaction.Balance += trans.Amount;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        //
        // GET: /Account/Edit/5

        public ActionResult Edit(int id)
        {
            TransactionModel transaction = db.Transactions.Find(id);
            return View(transaction);
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        public ActionResult Edit(TransactionModel transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;

                foreach (var acc in db.Transactions)
                {
                    if (acc != null)
                        transaction.Balance += acc.Amount;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        //
        // GET: /Account/Delete/5

        public ActionResult Delete(int id)
        {
            TransactionModel transaction = db.Transactions.Find(id);
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
            TransactionModel transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);

            foreach (var acc in db.Transactions)
            {
                if(acc != null)
                    transaction.Balance += acc.Balance;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}