using System;
using System.Collections.Generic;

namespace RuleViewerPrototype.Models
{
   public class RuleDoc
   {
      public string HeadingName { get; set; }
      public string Name { get; set; }
      public string FileName { get; set; }
      public string DocumentCode { get; set; }
      public DateTime? Edition { get; set; }
      public DateTime? Amended { get; set; }
      public string Status { get; set; }

      public List<string> IndustryCategories { get; set; }
      public List<string> ServiceCategories { get; set; }
      
   
   }
}