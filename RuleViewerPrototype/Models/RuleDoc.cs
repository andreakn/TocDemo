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
      public int RulePartNumber{ get; set; }
      public int RuleChapterNumber { get; set; }


      public string LatestEditionClass(DateTime? latestEdition)
      {
         if (latestEdition == null) return "";
         if (latestEdition == Edition) return "latestEdition";
         return "";
      }

      public string GetRulePartAndChapter(bool useComma = false)
      {
         var separator = useComma?",":".";

         if (RulePartNumber > 0)
         {
            return string.Format("{0}{1}{2}", RulePartNumber,separator, RuleChapterNumber);
         }
         return "";
      }
   }
}