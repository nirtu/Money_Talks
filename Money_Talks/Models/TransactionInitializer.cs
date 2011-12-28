using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Money_Talks.Models
{
    public class TransactionInitializer : DropCreateDatabaseIfModelChanges<AccountDbContext>
    {
        protected override void Seed(AccountDbContext context)
        {
            var transactions = new List<TransactionModel>
            {
                new TransactionModel
                {
                    TransactionId = 1,
                    Username = "yanivgal123",
                    Amount = 1000,
                    TransactionType = "Outcome",
                    Category = "Cloths",
                    DateCreated = DateTime.Now
                },
                new TransactionModel
                {
                    TransactionId = 2,
                    Username = "yanivgal123",
                    Amount = 200,
                    TransactionType = "Outcome",
                    Category = "Food",
                    DateCreated = DateTime.Now
                },
                new TransactionModel
                {
                    TransactionId = 3,
                    Username = "yanivgal123",
                    Amount = 500,
                    TransactionType = "Outcome",
                    Category = "Bills",
                    DateCreated = DateTime.Now
                },
                new TransactionModel
                {
                    TransactionId = 4,
                    Username = "yanivgal123",
                    Amount = 100,
                    TransactionType = "Outcome",
                    Category = "Toys",
                    DateCreated = DateTime.Now
                },
                new TransactionModel
                {
                    TransactionId = 5,
                    Username = "yanivgal123",
                    Amount = 5000,
                    TransactionType = "Income",
                    Category = "Salary",
                    DateCreated = DateTime.Now
                }

            };
            transactions.ForEach(d => context.Transactions.Add(d));
        }
    }
}