using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Money_Talks.Models
{
    
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        public string Username { get; set; }

        [DisplayName("Amount")]
        [Required(ErrorMessage = "Amount is required")]
        public long Amount { get; set; }

        [DisplayName("Transaction Type")]
        [Required(ErrorMessage = "Transaction type is required")]
        public string TransactionType { get; set; }

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
        public DbSet<TransactionModel> Transactions { get; set; }   
    }
}