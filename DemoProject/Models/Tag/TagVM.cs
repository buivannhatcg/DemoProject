using System.ComponentModel.DataAnnotations;

namespace DemoProject.Models.Tag
{
    public class TagVM
    {
        public int TagId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string DisplayName { get; set; }

    }
}
