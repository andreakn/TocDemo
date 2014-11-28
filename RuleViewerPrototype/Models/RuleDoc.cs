using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using StructureMap.Query;

namespace RuleViewerPrototype.Models
{
   public class RuleDoc
   {
      protected bool Equals(RuleDoc other)
      {
         return string.Equals(Name, other.Name) && string.Equals(HeadingName, other.HeadingName) && string.Equals(DocumentCode, other.DocumentCode) && Edition.Equals(other.Edition);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            int hashCode = (Name != null ? Name.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (HeadingName != null ? HeadingName.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (DocumentCode != null ? DocumentCode.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ Edition.GetHashCode();
            return hashCode;
         }
      }

      public RuleDoc()
      {
         DownloadLink = "/Content/Images/notready.png";//hardcoded for now
      }
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
      public string RulePartTitle { get; set; }
      public string DownloadLink { get; set; }


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

      public bool IsFirstPartOfSubGroup(List<RuleDoc> list)
      {
         if (GetRulePartAndChapter() == "") return false;
         if (list.Any(rd =>
            rd.HeadingName == HeadingName
            && rd.RulePartNumber == RulePartNumber
            && rd.RuleChapterNumber < RuleChapterNumber
            ))
         {
            return false;
         }
         if (list.First(r => r.GetRulePartAndChapter() == GetRulePartAndChapter()).Equals(this))
         {
            return true;
         }
         return false;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != this.GetType()) return false;
         return Equals((RuleDoc) obj);
      }


   }
}