using Demo.Core.Entities;
using DemoProject.Models.Pagination;
using DemoProject.Models.PostVM;

namespace DemoProject.Services
{
    public interface IPostService
    {
        Task<int> CreatePost(CreatePostVM createPostVM);
        Task<IOrderedQueryable<CreatePostVM>> GetPostsAsync(PostListVM model);
        Task<BlogPost?> GetAsync(int id);
        Task<ListResponse<CreatePostVM>> GetPaginated(PostListVM model);
    }
}