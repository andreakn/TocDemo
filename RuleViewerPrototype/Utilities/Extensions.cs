using System;
using System.Globalization;

namespace RuleViewerPrototype.Utilities
{
   public static class Extensions
   {
      public static string MonthYear(this DateTime? dateTime)
      {
         if (dateTime == null) return "&nbsp;";
         return MonthYear(dateTime.Value);
      }
      public static string MonthYear(this DateTime dateTime)
      {
         return dateTime.ToString("yyyy-MM");
      }
      public static DateTime? ToMonthYear(this string input)
      {
         if (input == null) return null;
         try
         {
            return DateTime.ParseExact(input, "yyyy-MM", CultureInfo.InvariantCulture);
         }
         catch(Exception ex)
         {
            return null;
         }
      }
   }
}