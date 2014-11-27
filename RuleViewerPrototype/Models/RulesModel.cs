using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuleViewerPrototype.Models
{
   public class RulesModel
   {
      public List<string> FoodCategories
      {
         get { return RuleDocs.SelectMany(rd => rd.FoodCategories).Distinct().OrderBy(s => s).ToList(); }
      }
      public List<string> LegCategories
      {
         get { return RuleDocs.SelectMany(rd => rd.LegCategories).Distinct().OrderBy(s => s).ToList(); }
      }

      public List<RuleDoc> RuleDocs { get; set; }
   }
}