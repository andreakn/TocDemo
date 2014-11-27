using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuleViewerPrototype.Models
{
   public class RulesModel
   {
      public List<string> ServiceCategories
      {
         get { return RuleDocs.SelectMany(rd => rd.ServiceCategories).Distinct().OrderBy(s => s).ToList(); }
      }
      public List<string> IndustryCategories
      {
         get { return RuleDocs.SelectMany(rd => rd.IndustryCategories).Distinct().OrderBy(s => s).ToList(); }
      }

      public List<RuleDoc> RuleDocs { get; set; }
   }
}