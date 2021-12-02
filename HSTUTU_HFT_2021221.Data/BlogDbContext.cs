using HSTUTU_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

            modelBuilder.Entity<Blog>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.Blog)
                .HasForeignKey(x => x.BlogId)
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>()
                .HasMany(x => x.Tags)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Tag>()
                .HasOne(x => x.Post)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.Restrict);

            Blog b1 = new Blog() { ID = 1, Title = "stockage.blog"};
            Blog b2 = new Blog() { ID = 2, Title = "blognetwork.info"};
            Blog b3 = new Blog() { ID = 3, Title = "seoonline.blog" };
            Blog b4 = new Blog() { ID = 4, Title = "gamesshop.org", };


            Post p1 = new Post() { Id = 1,Title = "17 Reasons the Amish Were Right About Asds", PostContent = "lorem ipsun",BlogId = 1, Likes = 89 };
            Post p2 = new Post() { Id = 2,PostContent = "lorem ipsum dolores est", Title = "The 14 Worst Songs About Fun", BlogId = 2, Likes = 10 };
            Post p3 = new Post() { Id = 3,PostContent = "lorem ipsum dolores est", Title = "How Fun is the New Hotness", BlogId = 2, Likes = 25 };
            Post p4 = new Post() { Id = 4,PostContent = "lorem ipsum dolores est", Title = "Why Fun is More Tempting than a Cinnabon", BlogId = 3, Likes = 120 };                     
            Post p5 = new Post() { Id = 5,Title = "How Businesses Killed the[TBD] Industry.", PostContent = "lorem ipsun", BlogId = 1, Likes = 891};
            Post p6 = new Post() { Id = 6,PostContent = "lorem ipsum dolores est", Title = "How Stocks Can Help You Survive a Filibuster", BlogId = 3, Likes = 130 };                  
            Post p7 = new Post() { Id = 7,PostContent = "lorem ipsum dolores est", Title = "Darth Vader's Guide to Stocks", BlogId = 2, Likes = 25 };
            Post p8 = new Post() { Id = 8,PostContent = "lorem ipsum dolores est", Title = "If You Read One Article About Stocks Read this One", BlogId = 1, Likes = 991 };
            Post p9 = new Post() {  Id = 9, Title = "Why Businesses are Scarier than Getting #Cancelled", PostContent = "lorem ipsun", BlogId = 4, Likes = 289 };
            Post p10 = new Post() { Id = 10, PostContent = "lorem ipsum dolores est", Title = "Why Mom Was Right About Payments", BlogId = 4, Likes = 109 };
            Post p11 = new Post() { Id = 11,  PostContent = "lorem ipsum dolores est", Title = "15 Freaky Reasons Payments Could Get You Fired", BlogId = 1, Likes = 255 };
            Post p12 = new Post() { Id = 12, PostContent = "lorem ipsum dolores est", Title = "12 Secrets About Seos the Government Is Hiding", BlogId = 4, Likes = 997 };


            Tag t1 = new Tag() {  Id = 1,Name = "stocks",  PostId = 1 };
            Tag t2 = new Tag() {  Id = 2,Name = "fun",  PostId = 3 };           
            Tag t3 = new Tag() {  Id = 3,Name = "payment",  PostId = 2};
            Tag t4 = new Tag() {  Id = 4,Name = "seo",  PostId = 10 };
            Tag t5 = new Tag() {  Id = 5,Name = "business",  PostId = 9 };
            Tag t6 = new Tag() {  Id = 6,Name = "mom",  PostId = 8 };
            Tag t7 = new Tag() {  Id = 7,Name = "amish",  PostId = 8 };
            Tag t8 = new Tag() {  Id = 8,Name = "right",  PostId = 9 };
            Tag t9 = new Tag() {  Id = 9,Name = "home",  PostId = 9 };
            Tag t10 = new Tag() { Id = 10,Name = "house",  PostId = 3 };
            Tag t11 = new Tag() { Id = 11,Name = "market", PostId = 4 };
            Tag t12 = new Tag() { Id = 12,Name = "computer", PostId = 6 };
            Tag t13 = new Tag() { Id = 13,Name = "laptop", PostId = 7 };

            modelBuilder.Entity<Post>().HasData(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
            modelBuilder.Entity<Blog>().HasData(b1, b2, b3, b4);
            modelBuilder.Entity<Tag>().HasData(t1, t2,t3,t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
        }
    }
}
