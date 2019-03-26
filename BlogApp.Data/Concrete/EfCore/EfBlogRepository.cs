using System;
using System.Linq;
using BlogApp.Data.Abstract;
using BlogApp.Entity;

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
            _context.SaveChanges();
        }

        public void UpdateBlog(Blog entity)
        {
            var blog = GetById(entity.Id);
            if (blog != null)
            {
                blog.Title = entity.Title;
                blog.Description = entity.Description;
                blog.CategoryId = entity.CategoryId;
                blog.Body = entity.Body;
                blog.Image = entity.Image;
                blog.IsApproved = entity.IsApproved;
                blog.IsHome = entity.IsHome;
                blog.IsSlider = entity.IsSlider;
                _context.SaveChanges();
            }
        }

        public void SaveBlog(Blog entity)
        {
            if (entity.Id == 0)
            {
                entity.Date = DateTime.Now;
                _context.Blogs.Add(entity);
            }
            else
            {
                var blog = GetById(entity.Id);
                if (blog != null)
                {
                    blog.Title = entity.Title;
                    blog.Description = entity.Description;
                    blog.Body = entity.Body;
                    blog.CategoryId = entity.CategoryId;
                    blog.Image = entity.Image;
                    blog.IsApproved = entity.IsApproved;
                    blog.IsHome = entity.IsHome;
                    blog.IsSlider = entity.IsSlider;
                }
            }

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