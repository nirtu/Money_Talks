using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Money_Talks.Models
{
    public class BalanceAccountInitializer : DropCreateDatabaseIfModelChanges<AccountDbContext>
    {
        protected override void Seed(AccountDbContext context)
        {
            List<Account> accounts = new List<Account> {  
  
                 new Account { TransactionId = 1,
                               Transaction = -5000,
                               Category = "Cloths",
                               DateCreated = DateTime.Now
                             },

                 new Account { TransactionId = 2,
                               Transaction = 200,
                               Category = "Food",
                               DateCreated = DateTime.Now
                             },
  
                 new Account { TransactionId = 3,
                               Transaction = 500,
                               Category = "Bills",
                               DateCreated = DateTime.Now
                             }, 

                 new Account { TransactionId = 4,
                               Transaction = 100,
                               Category = "Toys",
                               DateCreated = DateTime.Now
                             },
             };

            accounts.ForEach(d => context.Accounts.Add(d));
        }
    }
}