using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Money_Talks.Models
{

    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }

        public UserModel(){}

        public UserModel( string Username, string Password, string FirstName, string LastName, string Email, int Balance)
        {
            this.Username = Username;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Balance = Balance;
        }
    }

    public class UserDbContext : DbContext
    {
        public DbSet<UserModel> UsersDB { get; set; }
    }
}