using System.Security.AccessControl;
using EFGetStarted.Service;
using System.Linq;
using NUnit.Framework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.Models;


namespace EFGetStarted.Test.PostServiceTest
{
 
    public class BlogServiceTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Add_writes_to_database()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new BloggingContext(options))
                {
                    EnsureCreated(context);
                }

                // Run the test against one instance of the context
                using (var context = new BloggingContext(options))
                {
                 
                    var service = new PostService(context);
                    service.Add("abc","url");
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new BloggingContext(options))
                {
                    Assert.AreEqual(1, context.Posts.Count());
                    Assert.AreEqual("abc", context.Posts.Single().Title);
                    Assert.IsNotNull(context.Posts.Include(post => post.Blog).Single());
                    Assert.AreEqual("url", context.Blogs.Single().Url);
                }
            }
            finally
            {
                connection.Close();
            }
        }
      
        [Test]
        public void Find_searches_title()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new BloggingContext(options))
                {
                    EnsureCreated(context);
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new BloggingContext(options))
                {
                    context.Posts.Add(new Post {Title = "t1",Blog = new Blog {Url = "abc"}});
                    context.Posts.Add(new Post {Title = "t2",Blog = new Blog {Url = "xyz"}});
                    context.Posts.Add(new Post {Title = "t1",Blog = new Blog {Url = "123"}});
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new BloggingContext(options))
                {
                    var service = new PostService(context);
                    var result = service.Find("t1");
                    Assert.AreEqual(2, result.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Test]
         public void Update_write_to_database()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new BloggingContext(options))
                {
                    EnsureCreated(context);
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new BloggingContext(options))
                {
                    context.Posts.Add(new Post {Title = "t1",Content = "ct1",Blog = new Blog {Url = "abc"}});
                    context.Posts.Add(new Post {Title = "t2",Content = "ct2",Blog = new Blog {Url = "xyz"}});
                    context.Posts.Add(new Post {Title = "t1",Content = "ct3",Blog = new Blog {Url = "123"}});
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new BloggingContext(options))
                {
                    var service = new PostService(context);
                    service.Update(1,"title1","content1");
                    context.SaveChanges();
                    
                }
                 using (var context = new BloggingContext(options))
                {
                  Assert.AreEqual(3, context.Posts.Count());
                  Assert.AreEqual("title1", context.Posts.SingleOrDefault(x => x.PostId == 1).Title);
                  Assert.AreEqual("content1", context.Posts.SingleOrDefault(x => x.PostId == 1).Content);
                    
                }
            }
            finally
            {
                connection.Close();
            }
        }

        
        [Test]
         public void Delete_Remove_from_database()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new BloggingContext(options))
                {
                    EnsureCreated(context);
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new BloggingContext(options))
                {
                    context.Posts.Add(new Post {Title = "t1",Content = "ct1",Blog = new Blog {Url = "abc"}});
                    context.Posts.Add(new Post {Title = "t2",Content = "ct2",Blog = new Blog {Url = "xyz"}});
                    context.Posts.Add(new Post {Title = "t1",Content = "ct3",Blog = new Blog {Url = "123"}});
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new BloggingContext(options))
                {
                    var service = new PostService(context);
                    service.Delete(1);
                    context.SaveChanges();
                    
                }
                 using (var context = new BloggingContext(options))
                {
                  Assert.AreEqual(2, context.Posts.Count());
                  Assert.AreEqual(2, context.Posts.FirstOrDefault().PostId);
                  
                    
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Test]
        public void GetAllPost_returns_all_post()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new BloggingContext(options))
                {
                    EnsureCreated(context);
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new BloggingContext(options))
                {
                    context.Posts.Add(new Post {Title = "t1",Blog = new Blog {Url = "abc"}});
                    context.Posts.Add(new Post {Title = "t2",Blog = new Blog {Url = "xyz"}});
                    context.Posts.Add(new Post {Title = "t1",Blog = new Blog {Url = "123"}});
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new BloggingContext(options))
                {
                    var service = new PostService(context);
                    var result = service.GetAllPost();
                    Assert.AreEqual(3, result.Count());
                }
            }
            finally
            {
                connection.Close();
            }
    
        }
            
            
            
        private static void EnsureCreated(BloggingContext context)
        {
            if (context.Database.EnsureCreated())
            {
                using var viewCommand = context.Database.GetDbConnection().CreateCommand();
                viewCommand.CommandText = @"
CREATE VIEW AllPost AS
SELECT Title, Content
FROM Posts;";
                viewCommand.ExecuteNonQuery();
            }
        }
     }
}