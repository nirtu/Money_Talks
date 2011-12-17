using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Money_Talks.Models
{
    public class Account
    {
        [Key]
        public int TransactionId { get; set; }

        [DisplayName("Transaction")]
        [Required(ErrorMessage = "Transaction is required")]
        public long Transaction { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [DisplayName("Date Created")]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Balance")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public long Balance { get; set; }
    }

    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
    }
}