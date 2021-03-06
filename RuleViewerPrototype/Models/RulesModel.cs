﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RuleViewerPrototype.Utilities;

namespace RuleViewerPrototype.Models
{
   public class RulesModel
   {
      public List<string> ServiceCategories
      {
         get { return RuleDocs.SelectMany(rd => rd.ServiceCategories).Distinct().OrderBy(ServiceCategoryOrdering).ThenBy(s=>s).ToList(); }
      }
      public List<string> IndustryCategories
      {
         get { return RuleDocs.SelectMany(rd => rd.IndustryCategories).Distinct().OrderBy(IndustryCategoryOrdering).ThenBy(s => s).ToList(); }
      }

      private int IndustryCategoryOrdering(string input)
      {
         if (input.ToLower().StartsWith("mar")) return 1;
         if (input.ToLower().StartsWith("energy")) return 2;
         return 100;
      }

      private int ServiceCategoryOrdering(string input)
      {
         if (input.ToLower().StartsWith("ship")) return 1;
         if (input.ToLower().StartsWith("offsho")) return 2;
         if (input.ToLower().StartsWith("renewa")) return 3;
         return 100;
      }


      public List<string> Editions
      {
         get
         {
            return AllRuleDocs.Select
               (rd => rd.Edition.MonthYear())
               .Where(s => !string.IsNullOrEmpty(s))
               .OrderByDescending(s => s)
               .Distinct()
               .ToList();
         }
      } 

      public List<RuleDoc> RuleDocs { get; set; } //filtered by date
      public List<RuleDoc> AllRuleDocs { get; set; } //ALL

      public DateTime LatestEdition
      {
         get { return AllRuleDocs.Max(rd => rd.Edition ?? DateTime.MinValue); }
      }
      public DateTime LatestEditionInSelection
      {
         get { return RuleDocs.Max(rd => rd.Edition ?? DateTime.MinValue); }
      }
      

      public string InitialIndustryFilter { get; set; }
      public string InitialServiceFilter { get; set; }
      public string InitialFreeTextFilter { get; set; }

      public void CheckFilterSanity()
      {
         if (!string.IsNullOrEmpty(InitialServiceFilter))
         {
            var matchingService = RuleDocs
               .SelectMany(rd => rd.ServiceCategories)
               .Select(s => ("" + s))
               .FirstOrDefault(s => s.Equals(InitialServiceFilter, StringComparison.InvariantCultureIgnoreCase));
            if (matchingService != null)
               InitialServiceFilter = matchingService;
            else
               InitialServiceFilter = "";
         }
         if (!string.IsNullOrEmpty(InitialIndustryFilter))
         {
            var matchingIndustry = RuleDocs
               .SelectMany(rd => rd.IndustryCategories)
               .Select(s => ("" + s))
               .FirstOrDefault(s => s.Equals(InitialIndustryFilter, StringComparison.InvariantCultureIgnoreCase));
            if (matchingIndustry != null)
               InitialIndustryFilter = matchingIndustry;
            else
               InitialIndustryFilter = "";
         }
      }

      public string SelectedIfLatestEdition(string edition)
      {
         return edition.ToMonthYear() == LatestEditionInSelection ? "selected" : "";
      }


      public List<RuleDocType> GroupedAndOrderedRules;

      public void GroupAndOrderRules()
      {
         GroupedAndOrderedRules = new List<RuleDocType>();

         var groups = RuleDocs.GroupBy(rd => rd.HeadingName);
         foreach (var ruleGroup in groups)
         {
            var list =
               ruleGroup.OrderBy(r => r.RulePartNumber).ThenBy(r => r.RuleChapterNumber).ThenBy(r => r.Name).ToList();
            var name = ruleGroup.Key;
            var sortOrder = ruleGroup.First().DocTypeSortOrder;
            GroupedAndOrderedRules.Add(new RuleDocType
            {
               DocTypeName = name,
               SortOrder = sortOrder,
               RuleDocs = list
            });
         }
         GroupedAndOrderedRules =
            GroupedAndOrderedRules.OrderBy(dt => dt.SortOrder).ThenBy(dt => dt.DocTypeName).ToList();
      }

   }
}