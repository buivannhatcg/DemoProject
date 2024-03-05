using Demo.Core;
using Demo.Core.Entities;
using DemoProject.Models.Pagination;
using DemoProject.Models.PostVM;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Services
{
    public class PostService : IPostService
    {
        private readonly DemoDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PostService(DemoDbContext demoDbContext, IWebHostEnvironment environment)
        {
            _context = demoDbContext;
            _environment = environment;
        }
        public async Task<BlogPost?> GetAsync(int id)
        {
            return await _context.Blogs
                    .Include(b => b.BlogPostTags)
                    .FirstOrDefaultAsync(b => b.BlogPostId == id);
        }

        public async Task<IOrderedQueryable<CreatePostVM>> GetPostsAsync(PostListVM model)
        {
            return _context.Blogs.AsNoTracking()
                           .Select(b => new CreatePostVM
                           {
                               BlogPostId = b.BlogPostId,
                               Heading = b.Heading,
                               PageTitle = b.PageTitle,
                               Content = b.Content,
                               FeaturedImageUrl = b.FeaturedImageUrl,
                               ShortDecription = b.ShortDecription,
                               Visible = b.Visible
                           })
                           .OrderBy(b => b.Heading);
        }


        public async Task<int> CreatePost(CreatePostVM createPostVM)
        {
            string imageName;

            if (createPostVM != null)
            {
                imageName = createPostVM.ImageUpload.FileName;
                string filePath = Path.Combine(_environment.WebRootPath, "Image", imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await createPostVM.ImageUpload.CopyToAsync(fs);
                fs.Close();
                createPostVM.FeaturedImageUrl = filePath;
            }
            var post = new BlogPost
            {
                BlogPostId = createPostVM.BlogPostId,
                Heading = createPostVM.Heading,
                PageTitle = createPostVM.PageTitle,
                Content = createPostVM.Content,
                FeaturedImageUrl = createPostVM.FeaturedImageUrl,
                ShortDecription = createPostVM.ShortDecription,
                Visible = createPostVM.Visible,
            };
            post.BlogPostTags = new List<BlogPostTag>
            {
                new BlogPostTag
                {
                PostId = createPostVM.BlogPostId,
                TagId = createPostVM.TagId,
                }
            };
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
            return post.BlogPostId;
        }

        public async Task EditAsync(CreatePostVM model)
        {
            var post = await _context.Blogs.FindAsync(model.BlogPostId);

            post.Heading = model.Heading;
            post.PageTitle = model.PageTitle;
            post.Content = model.Content;
            post.ShortDecription = model.ShortDecription;
            post.Visible = model.Visible;
            _context.Update(post);
            var result = await _context.SaveChangesAsync();

        }
        public async Task<ListResponse<CreatePostVM>> GetPaginated(PostListVM model)

        {
            var getpost = await GetPostsAsync(model);

            var count = getpost.Count();
            var data = getpost.Any() ? await getpost.Skip(model.PageNumber).Take(model.PageSize).ToListAsync() : null;
            return new ListResponse<CreatePostVM>(data, count);
        }

    }
}
