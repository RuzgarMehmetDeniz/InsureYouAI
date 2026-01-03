using InsureYouAI.Context;
using InsureYouAINew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class _BlogListAllBlogsComponentPartial : ViewComponent
{
    private readonly InsureContext _context;

    public _BlogListAllBlogsComponentPartial(InsureContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var values = _context.Articles
            .Include(x => x.Category)
            .Include(x => x.Comments)
            .Select(x => new ArticleListViewModel
            {
                ArticleId = x.ArticleID,
                Title = x.Title,
                Content = x.Content,
                ImageUrl = x.CoverImageUrl,
                CreatedDate = x.CreatedDate,
                CategoryName = x.Category.Name,
                Author = x.AppUser.Name + " " + x.AppUser.Surname,
                CommentCount = x.Comments.Count
            })
            .ToList();

        return View(values);
    }
}
