using Demo.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core
{
    public class DemoDbContext : IdentityDbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Key
            modelBuilder.Entity<BlogPostTag>().HasKey(pt => new { pt.PostId, pt.TagId });
            modelBuilder.Entity<BlogPost>().HasKey(bp => bp.BlogPostId);
            modelBuilder.Entity<Tag>().HasKey(t => t.TagId);


            //
            modelBuilder.Entity<BlogPostTag>()
                .HasOne<BlogPost>(pt => pt.BlogPost)
                .WithMany(bp => bp.BlogPostTags)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<BlogPostTag>()
                .HasOne<Tag>(pt => pt.Tag)
                .WithMany(bp => bp.BlogPostTags)
                .HasForeignKey(pt => pt.TagId);
        }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostTag> BlogPostTags { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
