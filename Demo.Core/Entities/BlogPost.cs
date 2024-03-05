using System.ComponentModel.DataAnnotations;

namespace Demo.Core.Entities
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        [Required]
        [StringLength(50)]
        public string Heading { get; set; }
        [Required]
        [StringLength(50)]
        public string PageTitle { get; set; }
        [Required]
        [StringLength(100)]
        public string Content { get; set; }
        public string ShortDecription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        public string Author { get; set; }
        public Boolean Visible { get; set; } = false;

        public ICollection<BlogPostTag> BlogPostTags { get; set; }



    }
}
