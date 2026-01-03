using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAINew.ViewComponents.DefaultViewComponents
{
    public class _DefaultPricingPlanComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;
        public _DefaultPricingPlanComponentPartial(InsureContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var pricingPlan1 = _context.PricingPlans.Where(x => x.IsFeature == true).FirstOrDefault();
            ViewBag.pricingPlan1Title = pricingPlan1.Title;
            ViewBag.pricingPlan1Price = pricingPlan1.Price;
            ViewBag.pricingPlan1Id = pricingPlan1.PricingPlanID;

            var pricingPlan2 = _context.PricingPlans.Where(x => x.IsFeature == true).OrderByDescending(y => y.PricingPlanID).FirstOrDefault();
            ViewBag.pricingPlan2Title = pricingPlan2.Title;
            ViewBag.pricingPlan2Price = pricingPlan2.Price;
            ViewBag.pricingPlan2Id = pricingPlan2.PricingPlanID;

            var pricingPlanItems = _context.PricingPlanItems.Where(x => x.PricingPlanID == pricingPlan1.PricingPlanID || x.PricingPlanID == pricingPlan2.PricingPlanID).ToList();

            return View(pricingPlanItems);
        }
    }
}