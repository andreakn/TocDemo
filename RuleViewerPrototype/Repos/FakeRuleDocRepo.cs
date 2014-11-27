using System;
using System.Collections.Generic;
using System.Linq;
using RuleViewerPrototype.Models;

namespace RuleViewerPrototype.Repos
{
   public class FakeRuleDocRepo:IRuleDocRepo
   {
      private static int counter = 0;
      public List<RuleDoc> GetAllRuleDocs()
      {
         return ReadDataSource();
      }

      private List<RuleDoc> ReadDataSource()
      {
         counter = 0;
         var ret = new List<RuleDoc>();
         for (int i = 0; i < 250; i++)
         {
            ret.Add(Rule("MacDonalds", "Commercial", "Service", "Food","Childcare","Career counseling"));
            ret.Add(Rule("NAV", "Government", "Service", "Healthcare","Childcare","Career counseling"));
            ret.Add(Rule("Maemo", "Commercial", "Service", "Food", "Therapy"));
            ret.Add(Rule("Ullevål Sykehus", "Government", "Infrastructure", "Healthcare", "Therapy", "Immunology"));
            ret.Add(Rule("Ullevål Sykehusbarnehage", "Commune", "Service", "Childcare", "Immunology"));
         }

         return ret;
      }

      private RuleDoc Rule(string name, string heading, string industryCategories, params string[] servicecategories)
      {
         counter++;
         return new RuleDoc
         {
            HeadingName = heading,
            Name = name,
            IndustryCategories = new List<string> { industryCategories },
            ServiceCategories = servicecategories.ToList(),
            FileName = "File_" + name + ".doc",
            DocumentCode = string.Format("DNVGL-{0}-{1}", name.Substring(0, 2).ToUpper(), counter.ToString("D4")),
            Edition = DateTime.Now,
            Amended = null,
            Status = "New"
         };
      }
   }
}