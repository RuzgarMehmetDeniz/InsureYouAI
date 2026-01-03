using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DashboardViewComponents
{
    public class _DashboardSecondChartComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _DashboardSecondChartComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.TotalPoliciesCount = _context.Policies.Count();

            int year = DateTime.Now.Year; // İstersen sabit yıl da verebilirsin
            int month = 5; // Mayıs

            var startOfMonth = new DateTime(year, month, 1);
            var startOfNextMonth = startOfMonth.AddMonths(1);

            ViewBag.MayPolicyCount = _context.Policies
                .Where(x => x.StartDate >= startOfMonth &&
                            x.StartDate < startOfNextMonth)
                .Count();

            ViewBag.TotalCommentCount = _context.Comments.Count();
            return View();
        }
    }
}