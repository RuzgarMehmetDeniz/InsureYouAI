namespace InsureYouAI.Entities
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public string CoverImageUrl { get; set; }
        public string MainCoverImageUrl { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }
        public string? AppUserID { get; set; }
        public AppUser AppUser { get; set; }
    }
}
