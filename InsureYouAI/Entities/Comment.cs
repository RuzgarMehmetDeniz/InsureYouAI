namespace InsureYouAI.Entities
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string CommentDetail { get; set; }
        public DateTime CommentDate { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserID { get; set; }
        public int ArticleID { get; set; }  
        public Article Article { get; set; }
        public string? CommentStatus { get; set; }

    }
}
