using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Money_Talks.Models
{
    public class BalanceAccountInitializer : DropCreateDatabaseIfModelChanges<AccountDbContext>
    {
        protected override void Seed(AccountDbContext context)
        {
            var accounts = new List<Account>
                               {
                                   new Account
                                       {
                                           TransactionId = 1,
                                           Amount = 1000,
                                           TransactionType = "Income",
                                           Category = "Cloths",
                                           DateCreated = DateTime.Now
                                       },
                                   new Account
                                       {
                                           TransactionId = 2,
                                           Amount = 200,
                                           TransactionType = "Income",
                                           Category = "Food",
                                           DateCreated = DateTime.Now
                                       },
                                   new Account
                                       {
                                           TransactionId = 3,
                                           Amount = 500,
                                           TransactionType = "Income",
                                           Category = "Bills",
                                           DateCreated = DateTime.Now
                                       },
                                   new Account
                                       {
                                           TransactionId = 4,
                                           Amount = 100,
                                           TransactionType = "Outcome",
                                           Category = "Toys",
                                           DateCreated = DateTime.Now
                                       }
                               };


            // accounts.ForEach(d => context.Accounts.Add(d));
        }
    }
}