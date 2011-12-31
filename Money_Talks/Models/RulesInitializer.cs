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
            var rules = new List<Rules>
            {
                new Rules
                {
                    RuleId = 1,
                    UserId = 1, //need to fix it -> must be the current user 
                    RuleBorder = 5000, // must be dynamic -> current balance of the current user
                    DateCreated = DateTime.Now,
                    Category = "*",
                    username = "admin",
                }
             };

            rules.ForEach(d => context.Rules.Add(d));
        }
    }
}