using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Money_Talks.Models
{
    /// <summary>
    /// This class represents a user transaction
    /// </summary>
    public class TransactionModel
    {
        /*
         * The transaction ID is a unique value per record.
         * Serves as the key in the database.
         */
        [Key]
        public int TransactionId { get; set; }

        /*
         * Represents the current user-name
         */
        public string Username { get; set; }

        /*
         * Represents the amount given by the user
         */
        [DisplayName("Amount")]
        [Required(ErrorMessage = "Amount is required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "The amount must be bigger than zero!")]
        public long Amount { get; set; }

        /*
         * Represents the transaction-type
         * Possible values are income or outcome
         */
        [DisplayName("Transaction Type")]
        [Required(ErrorMessage = "Transaction type is required")]
        public string TransactionType { get; set; }

        /*
         * Represents the category of the transaction
         */
        [DisplayName("Category")]
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        /*
         * Represents the creation date of the transaction
         */
        [DisplayName("Date Created")]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }
    }

    /// <summary>
    /// Define the database context
    /// </summary>
    public class AccountDbContext : DbContext
    {
        /// <summary>
        /// Define all of the entities in the context
        /// </summary>
        public DbSet<TransactionModel> Transactions { get; set; }   
    }
}