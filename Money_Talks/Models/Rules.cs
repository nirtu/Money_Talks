using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using System.Data.Entity;


namespace Money_Talks.Models
{
    public class Rules
    {
        [Key]
        public int RuleId { get; set; }

        [DisplayName("Rule Border")]
        [Required(ErrorMessage = "Rule Border is required")]
        public int RuleBorder { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [DisplayName("Date Created")]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        //[ForeignKey(need to fill user ID for this attribute)] 
        public int UserId { get; set; }

        public string username { get; set; }

    }

    public class RulesDbContext : DbContext
    {
        public DbSet<Rules> Rules { get; set; }
    }

    public class faultsContainer
    {
        public int deviation { get; set; }

        public string category { get; set; }
    }
}
