using System.ComponentModel.Design.Serialization;
using System.Threading;
using System;
using System.Linq;

namespace EFGetStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            //  using (var context = new BloggingContext())
            // {
                
            //     var blog = new Blog {Url = "http://abc"};
            //     context.AddAsync(blog);
            //     context.SaveChanges();
            // }
            // using (var context = new DataContext())
            // {
            //     var blog = context.Blogs.Where(x => x.BlogId == 4).FirstOrDefault();
            //     var blogs = context.Blogs.AsQueryable();
            //     blog.Url = "opop.com";
            //     context.SaveChangesAsync();
            //     foreach(var item in blogs)
            //     {
            //         Console.WriteLine($"Blog Url : {item.Url}");
            //     }
            // }
            //  using (var context = new models())
            // {
            //     var blog = context.Blogs.Where(x => x.BlogId == 6).FirstOrDefault();
            //     var blogs = context.Blogs.ToList();
            //     context.Remove(blog);
            //     context.SaveChanges();
            //      foreach(var item in blogs)
            //     {
            //         Console.WriteLine($"Blog Url : {item.Url}");
            //     }
            // }
        }
    }
}
