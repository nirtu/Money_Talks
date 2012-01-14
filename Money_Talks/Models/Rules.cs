/*
 * This class representing a Rule format in our site. 
 * each rule will contain all of the fields belowe.
 */

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
        //the Rule ID at the database
        public int RuleId { get; set; }

        //the rule limitation
        [DisplayName("Rule Limitation")]
        [Required(ErrorMessage = "Rule Border is required")]
        public int RuleBorder { get; set; }

        //the category that the rule limits 
        [DisplayName("Category")]
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        //the date the Rule created
        [DisplayName("Date Created")]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        //ForeignKey -- the ID of the user that create this Rule
        public int UserId { get; set; }

        //the username of the user that creat the rule 
        public string username { get; set; }

    }
    
    /*
     * This class creat the Data Base for the Rules
     */

    public class RulesDbContext : DbContext
    {
        public DbSet<Rules> Rules { get; set; }
    }
}
