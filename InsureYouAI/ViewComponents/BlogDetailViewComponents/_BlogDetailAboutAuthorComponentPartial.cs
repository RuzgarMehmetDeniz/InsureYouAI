using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAINew.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailAboutAuthorComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;
        public _BlogDetailAboutAuthorComponentPartial(InsureContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int id)
        {
            string appUserId = _context.Articles.Where(x => x.ArticleID == id).Select(y => y.AppUserID).FirstOrDefault();
            var userValue = _context.Users.Where(x => x.Id == appUserId).FirstOrDefault();
            return View(userValue);
        }
    }
}