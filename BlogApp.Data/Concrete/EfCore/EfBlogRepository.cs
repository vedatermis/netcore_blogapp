using System.Linq;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfBlogRepository: IBlogRepository
    {
        private readonly BlogContext _context;

        public EfBlogRepository(BlogContext context)
        {
            _context = context;
        }

        public Blog GetById(int blogId)
        {
            return _context.Blogs.FirstOrDefault(x => x.Id == blogId);
        }

        public IQueryable<Blog> GetAll()
        {
            return _context.Blogs;
        }

        public void AddBlog(Blog entity)
        {
            _context.Blogs.Add(entity);
        }

        public void UpdateBlog(Blog entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var blog = _context.Blogs.FirstOrDefault(z => z.Id == blogId);

            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
            }
        }
    }
}