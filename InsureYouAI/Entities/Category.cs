namespace InsureYouAI.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public List<Article> Articles { get; set; }
    }
}
