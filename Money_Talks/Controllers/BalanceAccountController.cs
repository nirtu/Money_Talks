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
            Account account = db.Accounts.Find(id);
            return View(account);
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
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);

                foreach (var acc in db.Accounts)
                {
                    if (acc != null)
                        account.Balance += acc.Transaction;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        //
        // GET: /Account/Edit/5

        public ActionResult Edit(int id)
        {
            Account account = db.Accounts.Find(id);
            return View(account);
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;

                foreach (var acc in db.Accounts)
                {
                    if (acc != null)
                        account.Balance += acc.Transaction;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        //
        // GET: /Account/Delete/5

        public ActionResult Delete(int id)
        {
            Account account = db.Accounts.Find(id);
            return View(account);
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
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);

            foreach (var acc in db.Accounts)
            {
                if(acc != null)
                    account.Balance += acc.Balance;
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