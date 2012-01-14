/*
 * This class initiate that date base for the Rules.
 * this data base will be initiated just for the first time.
 * this format of initialization allows to make changes in the DB Structure but must be remembered that every change in the DB Structure will cause to a Completely lost of the INFO
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using System.Web.Mvc;
using Money_Talks.Models;

namespace Money_Talks.Models
{
    public class RulesInitializer : DropCreateDatabaseIfModelChanges<RulesDbContext>
    {
        protected override void Seed(RulesDbContext context)
        {
            //this initiation have no logical meaning - just for the initiation
            var rules = new List<Rules>
            {
                new Rules
                {
                    RuleId = 1,
                    UserId = 1, 
                    RuleBorder = 5000,
                    DateCreated = DateTime.Now,
                    Category = "*",
                    username = "admin",
                }
             };

            rules.ForEach(d => context.Rules.Add(d));
        }
    }
}