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
            return new RuleDoc
            {
               DocumentCode = string.Format("{0}-{1}-{2}", poco.DocCodePrefix, poco.DocCodeType, poco.DocCodeSerialNo),
               ServiceCategories = poco.Service.Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToList(),
               IndustryCategories = poco.Industry.Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToList(),
               Amended = poco.Amended.ToMonthYear(),
               Edition = poco.Edition.ToMonthYear(),
               FileName = "unknown.pdf",
               HeadingName = GetHeadingName(poco.DocType),
               Name = poco.DocumentTitle,
               Status = "NEW"
            };
         }
         catch (Exception ex)
         {
            return null; 
         }
      }

      private string GetHeadingName(string docType)
      {
         return docType;
         switch (docType) 
         {
            case "IN": return "";
            case "RP": return "";
            case "RU": return "";
            case "SE": return "";
            case "ST": return "";
            case "RU-SHIP": return "";
            case "RU-HSLC": return "";
            case "RU-YACHTS": return "";
            case "RU-MOU": return "";
            case "RU-NAVAL": return "";
            case "RU-UWT": return "";
            case "RU-FLOAT": return "";
            case "RU-INV": return "";

         }
         return "(UNKNOWN)";
      }
      
      private RuleDocImportPoco ReadLineToImportPoco(string line)
      {
         var elems = line.Split('\t');
         try
         {
            return new RuleDocImportPoco
            {
               DocCodePrefix = elems[00],
               DocCodeType = elems[01],
               DocCodeSerialNo = elems[02],
               DocType = elems[03],
               Service = elems[04],
               Industry = elems[05],
               RUPartNo = elems[06],
               RUChapterNo = elems[07],
               RUSubtype = elems[08],
               DocumentTitle = elems[09],
               Edition = elems[10],
               Amended = elems[11],
               History = elems[12],
               L_DNV_Type = elems[13],
               L_DNV_Code = elems[14],
               Program = elems[15],
               WP_or_Project = elems[16],
            };
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

   }
}