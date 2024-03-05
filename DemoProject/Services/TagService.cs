using Demo.Core;
using Demo.Core.Entities;
using DemoProject.Common;
using DemoProject.Models.Tag;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Services
{

	public class TagService : ITagService
	{
		private readonly DemoDbContext _context;
		public TagService(DemoDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TagVM>> GetTagsAsync()
		{
			var result = await _context.Tags
								.OrderByDescending(t => t.Name)
								.Select(t => new TagVM
								{
									TagId = t.TagId,
									Name = t.Name,
									DisplayName = t.DisplayName
								})
								.ToListAsync();
			return result;
		}
		public async Task<int> CreateTag(TagVM model)
		{
			var tag = new Tag
			{
				TagId = model.TagId,
				Name = model.Name,
				DisplayName = model.DisplayName
			};
			_context.Tags.Add(tag);
			await _context.SaveChangesAsync();
			return tag.TagId;
		}
		public async Task EditTag(TagVM model)
		{
			var entity = await _context.Tags.FindAsync(model.TagId);

			entity.Name = model.Name;
			entity.DisplayName = model.DisplayName;
			_context.Tags.Update(entity);
			await _context.SaveChangesAsync();
		}
		public async Task<TagVM?> GetByIdAsync(int id)
		{
			var entity = await _context.Tags.FirstOrDefaultAsync(x => x.TagId.Equals(id));
			return entity != null ? new TagVM
			{
				TagId = entity.TagId,
				Name = entity.Name,
				DisplayName = entity.DisplayName
			}
			: null;

		}
		public async Task<bool> DeleteTag(int id)
		{
			var entity = await _context.Tags.FindAsync(id);
			if (entity != null)
			{
				_context.Tags.Remove(entity);
			}
			var result = await _context.SaveChangesAsync();
			return result > 0;
		}
		public async Task<TagVM?> GetName(string name)
		{
			var entity = await _context.Tags.FirstOrDefaultAsync(x => x.Name == name);

			return entity != null ? new TagVM
			{
				TagId = entity.TagId,
				Name = entity.Name,
				DisplayName = entity.DisplayName
			}
			: null;
		}
		public async Task<bool> ExistsByNameAsync(string name)
		{
			var entity = await _context.Tags.FirstOrDefaultAsync(x => x.Name.Equals(name));
			return entity != null;
		}
		public async Task<bool> ExitAsync(int id)
		{
			var entity = await _context.Tags.FirstOrDefaultAsync(x => x.TagId.Equals(id));
			return entity != null;
		}
		public async Task<TagListVM> GetPaginatedTag(TagListVM model)
		{
			var result = _context.Tags
						.OrderBy(x => x.Name)
						.Select(x => new TagVM
						{
							TagId = x.TagId,
							Name = x.Name,
							DisplayName = x.DisplayName
						});
			var paginated = await PaginatedList<TagVM>.CreateAsync(result, model.PageNumber, model.PageSize);
			model.PaginatedTag = paginated;
			return model;
		}
	}
}
