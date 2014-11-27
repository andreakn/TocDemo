using System;

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
   }
}