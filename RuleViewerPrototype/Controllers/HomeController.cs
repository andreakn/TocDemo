using System.Web.Mvc;
using RuleViewerPrototype.Models;
using RuleViewerPrototype.Repos;

namespace RuleViewerPrototype.Controllers
{
   public class HomeController:Controller
   {
      private IRuleDocRepo _ruleDocRepo;

      public HomeController(IRuleDocRepo ruleDocRepo)
      {
         _ruleDocRepo = ruleDocRepo;
      }

      public ActionResult Index()
      {
         return View(new RulesModel()
         {
            RuleDocs = _ruleDocRepo.GetAllRuleDocs()
         });
      } 
   }
}