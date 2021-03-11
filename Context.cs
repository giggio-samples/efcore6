using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace efconsole1
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@$"Data Source={System.IO.Path.Combine(System.Environment.CurrentDirectory, "blogging.db")}");
    }

    [EntityTypeConfiguration(typeof(BlogConfiguration))]
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; } = new List<Post>();
    }

    [EntityTypeConfiguration(typeof(PostConfiguration))]
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}