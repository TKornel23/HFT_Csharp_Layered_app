using HSTUTU_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;

namespace HSTUTU_HFT_2021221.Data
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public SchoolDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HSTUTU_HFT_2021221.Data\HSTUTU_HFT_2021221.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);

            Blog b1 = new Blog() { ID = 1, Title = "Numero Uno" };
            Blog b2 = new Blog() { ID = 2, Title = "Numero Zwei" };

            Post p1 = new Post() { BlogId = b1, Id = 1, IsDeleted = false, Title = "Top ten reasons why my HFT teacher is the best teacher in the world", PostTags = { }, PostContent = "lorem ipsun" };

            Post p2 = new Post() { BlogId = b2, Id = 2, IsDeleted = false, PostContent = "lorem ipsum dolores est", PostTags = { }, Title = "No comments" };

            Tag t1 = new Tag() { Id = 1, IsDeleted = false, Name = "Tag Uno", PostTags = { } };
            Tag t2 = new Tag() { Id = 2, IsDeleted = false, Name = "Tag Zwei", PostTags = { } };

            PostTag pt1 = new PostTag() { PostId = 1, TagId = 1, Post = p1, Tag = t1 };
            PostTag pt2 = new PostTag() { PostId = 2, TagId = 2, Post = p2, Tag = t2 };

            modelBuilder.Entity<Tag>().HasData(t1, t2);
            modelBuilder.Entity<Blog>().HasData(b1, b2);
            modelBuilder.Entity<Post>().HasData(p1, p2);
            modelBuilder.Entity<PostTag>().HasData(pt1, pt2);
        }
    }
}
