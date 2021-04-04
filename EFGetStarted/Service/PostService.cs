using System.Collections.Generic;
using System.Linq;
using EFGetStarted.Models;

namespace EFGetStarted.Service
{
    public class PostService
    {
        private readonly BloggingContext _context;

        public PostService(BloggingContext context)
        {
            _context = context;
        }

        public void Add(string title,string url)
        {
            var post = new Post { 
                Title = title,
                Blog = new Blog{Url = url}

             };
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> Find(string title)
        {
            return _context.Posts
                .Where(b => b.Title.Contains(title))
                .OrderBy(b => b.PostId)
                .ToList();
        }

        public void Update(int id, string title, string content)
        {
           var result  =  _context.Posts.Find(id);
           result.Title = title;
           result.Content = content;
           _context.SaveChanges();

        }
         public void Delete(int id)
        {
           var result  =  _context.Posts.Find(id);
           _context.Remove(result);
           _context.SaveChanges();

        }

        public IEnumerable<Post> GetAllPost()
        {
            return _context.Posts
                .OrderBy(b => b.PostId)
                .ToList();
        }

        

    }
}