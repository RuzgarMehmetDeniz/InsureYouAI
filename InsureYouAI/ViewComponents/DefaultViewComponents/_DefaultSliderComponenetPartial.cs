using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DefaultViewComponent
{
    public class _DefaultSliderComponenetPartial:ViewComponent
    {
        private readonly InsureContext _context;

        public _DefaultSliderComponenetPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Sliders.ToList();
            return View(values);
        }
    }
}
