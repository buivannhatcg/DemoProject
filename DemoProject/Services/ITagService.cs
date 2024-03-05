using DemoProject.Models.Tag;

namespace DemoProject.Services
{
    public interface ITagService
    {
        Task<int> CreateTag(TagVM model);
        Task<bool> DeleteTag(int id);
        Task EditTag(TagVM model);
        Task<TagVM?> GetName(string name);
        Task<bool> ExistsByNameAsync(string name);
        Task<TagListVM> GetPaginatedTag(TagListVM model);
        Task<IEnumerable<TagVM>> GetTagsAsync();
        Task<bool> ExitAsync(int id);
        Task<TagVM?> GetByIdAsync(int id);
    }
}