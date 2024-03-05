using DemoProject.Common;

namespace DemoProject.Models.PostVM
{
    public class PostListVM : RequestParameters
    {
        public PaginatedList<CreatePostVM> PaginatedPost { get; set; }

    }
}
