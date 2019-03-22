using System.Linq;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private BlogContext _context;

        public EfCategoryRepository(BlogContext context)
        {
            _context = context;
        }

        public Category GetById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == categoryId);
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public void AddCategory(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void SaveCategory(Category entity)
        {
            if (entity.Id == 0)
            {
                _context.Categories.Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        public void UpdateCategory(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}