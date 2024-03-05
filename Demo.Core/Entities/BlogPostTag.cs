namespace Demo.Core.Entities
{
    public class BlogPostTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int PostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
