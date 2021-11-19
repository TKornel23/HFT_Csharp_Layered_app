using HSTUTU_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;

namespace HSTUTU_HFT_2021221.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public BlogDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\HSTUTU_HFT_2021221.mdf;Integrated Security=True;MultipleActiveResultSets=True");
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
            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Blog)
                .WithMany(pt => pt.PostTags)
                .HasForeignKey(pt => pt.BlogId);

            Blog b1 = new Blog() { Title = "Numero Uno" , ID = 1};
            Blog b2 = new Blog() { Title = "Numero Zwei" , ID = 2 };

            Post p1 = new Post() {Id = 1, Title = "Top ten reasons why my HFT teacher is the best teacher in the world", PostContent = "lorem ipsun" };

            Post p2 = new Post() {Id = 2, PostContent = "lorem ipsum dolores est", Title = "No comments" };

            Tag t1 = new Tag() {  Name = "Tag Uno", Id = 2 };
            Tag t2 = new Tag() {  Name = "Tag Zwei", Id = 1 };
            Tag t3 = new Tag() { Name = "Tag Police", Id = 3 };
            Tag t4 = new Tag() { Name = "Tag Women", Id = 4 };

            PostTag pt1 = new PostTag() { PostId = 1, TagId = 1, BlogId = 1 };
            PostTag pt2 = new PostTag() { PostId = 1, TagId = 2 , BlogId = 2};
            PostTag pt3 = new PostTag() { PostId = 2, TagId = 3 , BlogId = 2};
            PostTag pt4 = new PostTag() { PostId = 2, TagId = 4 , BlogId = 1};

            modelBuilder.Entity<Tag>().HasData(t1, t2,t3,t4);
            modelBuilder.Entity<Blog>().HasData(b1, b2);
            modelBuilder.Entity<Post>().HasData(p1, p2);
            modelBuilder.Entity<PostTag>().HasData(pt1, pt2,pt3,pt4);
        }
    }
}
