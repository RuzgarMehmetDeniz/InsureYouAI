using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAINew.ViewComponents.BlogDetailViewComponents
{
    public class _BlogDetailNextAndPrevPostComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;
        public _BlogDetailNextAndPrevPostComponentPartial(InsureContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int id)
        {
            var article = _context.Articles.FirstOrDefault(x => x.ArticleID == id);

            // Önceki makale
            var prevArticle = _context.Articles
                .Where(x => x.ArticleID < id)
                .OrderByDescending(x => x.ArticleID)
                .Select(x => x.Title)
                .FirstOrDefault();

            // Sonraki makale
            var nextArticle = _context.Articles
                .Where(x => x.ArticleID > id)
                .OrderBy(x => x.ArticleID)
                .Select(x => x.Title)
                .FirstOrDefault();

            ViewBag.PrevArticleTitle = prevArticle;
            ViewBag.NextArticleTitle = nextArticle;
            return View();
        }
    }
}