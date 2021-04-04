
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFGetStarted.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFGetStarted

{
   
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
             if (!option.IsConfigured)
            {
            option.UseSqlServer("Server = LAPTOP-O71PKJ1L\\SQLEXPRESS;Database = TestEFCore;Trusted_Connection=True;MultipleActiveResultSets= true");
            }
        }

        
      private readonly Action<BloggingContext, ModelBuilder> _customizeModel;

        #region Constructors
        public BloggingContext()
        {
        }

        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
        }

        public BloggingContext(DbContextOptions<BloggingContext> options, Action<BloggingContext, ModelBuilder> customizeModel)
            : base(options)
        {
            // customizeModel must be the same for every instance in a given application.
            // Otherwise a custom IModelCacheKeyFactory implementation must be provided.
            _customizeModel = customizeModel;
        }
        #endregion

        // protected void SeedingData(ModelBuilder modelBuilder)
        // {

        //     modelBuilder.Entity<Blog>().HasData(new Blog { BlogId = 1, Url = "http://sample.com" });


        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Post>().HasData(
            //     new Post { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" });
            // modelBuilder.Entity<Blog>().HasData(new Blog { BlogId = 1, Url = "http://sample.com" });
            //modelBuilder.ApplyConfiguration(new BlogConfiguration());
           // modelBuilder.Entity<Blog>().HasData(new Blog { BlogId = 1, Url = "gg.com" });

            // modelBuilder.Entity<Post>(
            //     entity =>
            //     {
            //         entity.HasOne(d => d.Blog)
            //             .WithMany(p => p.Posts)
            //             .HasForeignKey("BlogId");
            //     });

            
            // modelBuilder.Entity<Post>().HasData(
            //     new Post { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" });
            

           

           
           

        }
        
    }
 
    
}

