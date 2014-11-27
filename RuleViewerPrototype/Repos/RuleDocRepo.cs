using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RuleViewerPrototype.Models;

namespace RuleViewerPrototype.Repos
{

   public interface IRuleDocRepo
   {
      List<RuleDoc> GetAllRuleDocs();
   }

   public class RuleDocRepo:IRuleDocRepo
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
            ret.Add(Rule("Hest", "Pattedyr", "Firbeint", "Planteeter"));
            ret.Add(Rule("Ku", "Pattedyr", "Firbeint", "Planteeter"));
            ret.Add(Rule("Sebra", "Pattedyr", "Firbeint", "Planteeter"));
            ret.Add(Rule("Spurv", "Fugl", "Tobeint", "Planteeter"));
            ret.Add(Rule("Due", "Fugl", "Tobeint", "Planteeter"));
            ret.Add(Rule("Alligator", "Reptil", "Firbeint", "Kjøtteter"));
            ret.Add(Rule("Frosk", "Reptil", "Tobeint", "Kjøtteter","Planteeter"));
            ret.Add(Rule("Hund", "Pattedyr", "Firbeint", "Kjøtteter","Planteeter"));
            ret.Add(Rule("Menneske", "Pattedyr", "Tobeint", "Kjøtteter","Planteeter"));
            ret.Add(Rule("Ekorn", "Pattedyr", "Tobeint","Planteeter"));
         }

         return ret;
      }

      private RuleDoc Rule(string name,string heading, string legCat, params string[] foodcategories)
      {
         counter++;
         return new RuleDoc
         {
            HeadingName = heading,
            Name = name,
            LegCategories = new List<string>{legCat},
            FoodCategories = foodcategories.ToList(),
            FileName = "File_"+name+".doc",
            DocumentCode = string.Format("DNVGL-{0}-{1}", name.Substring(0,2).ToUpper(), counter.ToString("D4")),
            Edition = DateTime.Now,
            Amended = null,
            Status = "New"
         };
      }
   }
}