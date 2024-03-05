using Demo.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Models.PostVM
{
    public class CreatePostVM
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

        public List<BlogPostTag> BlogPostTags { get; set; }

        public IFormFile ImageUpload { get; set; }
        public List<SelectListItem> Tag { get; set; }
        public int TagId { get; set; }
    }
}
