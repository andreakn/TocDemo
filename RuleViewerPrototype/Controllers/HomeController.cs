using System;
using System.Linq;
using System.Web.Mvc;
using RuleViewerPrototype.Models;
using RuleViewerPrototype.Repos;
using RuleViewerPrototype.Utilities;

namespace RuleViewerPrototype.Controllers
{
   public class HomeController:Controller
   {
      private IRuleDocRepo _ruleDocRepo;

      public HomeController(IRuleDocRepo ruleDocRepo)
      {
         _ruleDocRepo = ruleDocRepo;
      }

      public ActionResult Index(string currentAsOf, string industryFilter, string serviceFilter, string freeTextFilter)
      {
         var currentAsOfDate = currentAsOf.ToMonthYear() ?? DateTime.MaxValue;
         var allRules = _ruleDocRepo.GetAllRuleDocs();
         var model = new RulesModel()
         {
            AllRuleDocs = allRules,
            RuleDocs = allRules.Where(r=>r.Edition<=currentAsOfDate).ToList(),
            InitialIndustryFilter = industryFilter,
            InitialServiceFilter  = serviceFilter,
            InitialFreeTextFilter = freeTextFilter
         };
         model.CheckFilterSanity();
         return View(model);
      } 
   }
}