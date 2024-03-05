using System.ComponentModel.DataAnnotations;

namespace Demo.Core.Entities
{
    public class Tag
    {
        public int TagId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string DisplayName { get; set; }

        public ICollection<BlogPostTag> BlogPostTags { get; set; }
    }
}
