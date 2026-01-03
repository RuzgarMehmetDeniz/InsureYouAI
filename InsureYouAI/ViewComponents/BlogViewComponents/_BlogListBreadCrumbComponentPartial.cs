using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.BlogViewComponent
{
    public class _BlogListBreadCrumbComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
