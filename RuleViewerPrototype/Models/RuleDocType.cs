using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuleViewerPrototype.Models
{
   public class RuleDocType
   {
      public string DocTypeName { get; set; }
      public int SortOrder{ get; set; }
      public List<RuleDoc> RuleDocs { get; set; }

   }
}