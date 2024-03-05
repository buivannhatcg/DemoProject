using DemoProject.Common;

namespace DemoProject.Models.Tag
{
    public class TagListVM : RequestParameters
    {
        public PaginatedList<TagVM> PaginatedTag { get; set; }
    }
}
