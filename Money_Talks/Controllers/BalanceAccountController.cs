using System.Data;
using System.Linq;
using System.Web.Mvc;
using Money_Talks.Models;

namespace Money_Talks.Controllers
{
    public class BalanceAccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private AccountDbContext db = new AccountDbContext();
        
        //
        // GET: /Account/

        public ViewResult Index()
        {
            return View(db.Accounts.ToList());
        }

        //
        // GET: /Account/Details/5

        public ViewResult Details(int id)
        {
            Transaction transaction = db.Accounts.Find(id);
            return View();
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
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(transaction);

                foreach (var acc in db.Accounts)
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
        // GET: /Account/Edit/5

        public ActionResult Edit(int id)
        {
            Transaction transaction = db.Accounts.Find(id);
            return View(transaction);
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;

                foreach (var acc in db.Accounts)
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
            Transaction transaction = db.Accounts.Find(id);
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
            Transaction transaction = db.Accounts.Find(id);
            db.Accounts.Remove(transaction);

            foreach (var acc in db.Accounts)
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