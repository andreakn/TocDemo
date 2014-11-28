using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using RuleViewerPrototype.Models;

namespace RuleViewerPrototype.Utilities
{

   public interface IDataReader
   {
      List<RuleDoc> ReadFromFile();
   }

   public class DataReader:IDataReader
   {
      public List<RuleDoc> ReadFromFile()
      {
         var ret = new List<RuleDoc>(); 
         var unimportable = new List<RuleDocImportPoco>();
         var file = File.ReadAllText(Path.Combine(HttpContext.Current.Server.MapPath("/"), "App_Data","datasource.txt"));
         var lines = file.Split('\n');
         foreach (var line in lines.Skip(1))
         {
            var lineToUse = line.Replace("\r", "");
            var importPoco = ReadLineToImportPoco(lineToUse);
            if(importPoco==null)continue;

            var ruleDoc = TransformToRuleDoc(importPoco);
            if(ruleDoc!=null)
               ret.Add(ruleDoc);
            else
               unimportable.Add(importPoco);
         }
         return ret;
      }

      private RuleDoc TransformToRuleDoc(RuleDocImportPoco poco)
      {
         try
         {
            var rule = new RuleDoc
            {
               DocumentCode = string.Format("{0}-{1}-{2}", poco.DocCodePrefix, poco.DocCodeType, poco.DocCodeSerialNo),
               ServiceCategories = (new List<string>{poco.Service}).Where(s => !string.IsNullOrEmpty(s)).ToList(),
               IndustryCategories = (new List<string>{poco.Industry}).Where(s => !string.IsNullOrEmpty(s)).ToList(),
               Amended = poco.Amended.ToMonthYear(),
               Edition = poco.Edition.ToMonthYear(),
               FileName = "unknown.pdf",
               HeadingName = GetHeadingName(poco.DocType),
               Name = poco.DocumentTitle,
               Status = "NEW",
               RulePartNumber=int.Parse("0"+poco.RUPartNo),
               RuleChapterNumber=int.Parse("0"+poco.RUChapterNo),
               RulePartTitle=poco.RUSubtype,
               DocTypeSortOrder = int.Parse("0"+poco.DocTypeSorting)
            };

            if (poco.DocCodeType == "RU")
            {
               bool duup = false;
            }

            return rule;
         }
         catch (Exception ex)
         {
            return null; 
         }
      }

      private string GetHeadingName(string docType)
      {
         switch (docType) 
         {
            case "IN": return "DNV GL Interpretations";
            case "RP": return "DNV GL recommended practices";
            case "RU": return "DNV GL rules for classification";
            case "SE": return "DNV GL service specifications";
            case "ST": return "DNV GL standards";
            case "RU-SHIP": return "DNV GL rules for classification of ships";
            case "RU-HSLC": return "DNV GL rules for classification of high speed, light crafts and naval surface crafts";
            case "RU-YACHTS": return "DNV GL rules for classification of yachts";
            case "RU-MOU": return "DNV GL rules for classification of mobile offshore units";
            case "RU-NAVAL": return "DNV GL rules for classification of naval vessels";
            case "RU-UWT": return "DNV GL rules for classification of underwater technology and subsea vessels";
            case "RU-FLOAT": return "DNV GL rules for classification of floating docks";
            case "RU-INV": return "DNV GL rules for classification of inland navigation vessels";

         }
         return "(UNKNOWN)";
      }
      
      private RuleDocImportPoco ReadLineToImportPoco(string line)
      {
         var elems = line.Split('\t');
         try
         {
            var created = new RuleDocImportPoco
            {
               DocCodePrefix = elems[00],
               DocCodeType = elems[01],
               DocCodeSerialNo = elems[02],
               DocType = elems[03],
               DocTypeSorting = elems[04],
               Service = elems[05],
               Industry = elems[06],
               RUPartNo = elems[07],
               RUChapterNo = elems[08],
               RUSubtype = elems[09],
               DocumentTitle = elems[10],
               Edition = elems[11],
               Amended = elems[12],
               History = elems[13],
               L_DNV_Type = elems[14],
               L_DNV_Code = elems[15],
               Program = elems[16],
               WP_or_Project  = elems[17],
            };
            return created;
         }
         catch (Exception ex)
         {
            return null;
         }
      }
   }

   internal class RuleDocImportPoco
   {
      public string DocCodePrefix { get; set; }
      public string DocCodeType { get; set; }
      public string DocCodeSerialNo { get; set; }
      public string DocType { get; set; }
      public string Service { get; set; }
      public string Industry { get; set; }
      public string RUPartNo { get; set; }
      public string RUChapterNo { get; set; }
      public string RUSubtype { get; set; }
      public string DocumentTitle { get; set; }
      public string Edition { get; set; }
      public string Amended { get; set; }
      public string History { get; set; }
      public string L_DNV_Type { get; set; }
      public string L_DNV_Code { get; set; }
      public string Program { get; set; }
      public string WP_or_Project { get; set; }
      public string DocTypeSorting { get; set; }
   }
}