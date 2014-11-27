using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RuleViewerPrototype.Models;
using IDataReader = RuleViewerPrototype.Utilities.IDataReader;

namespace RuleViewerPrototype.Repos
{

   public interface IRuleDocRepo
   {
      List<RuleDoc> GetAllRuleDocs();
   }

   public class RuleDocRepo:IRuleDocRepo
   {
      private IDataReader _reader;

      public RuleDocRepo(IDataReader reader)
      {
         _reader = reader;
      }

      private static List<RuleDoc> rules; 
      public List<RuleDoc> GetAllRuleDocs()
      {
         if (rules == null)
         {
            rules = _reader.ReadFromFile();
         }
         return rules;
      }
   }
}