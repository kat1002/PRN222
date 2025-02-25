using Assignment01_NewsManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment01_NewsManagementSystem.DAO
{
    public class CategoriesDAO
    {

        private readonly FunewsManagementContext _context;

        public CategoriesDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.Include(c => c.ParentCategory).ToList();
        }

        public Category GetCategoryById(short id)
        {
            return _context.Categories.Include(c => c.ParentCategory)
                                      .FirstOrDefault(c => c.CategoryId == id);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(short id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public bool CategoryExists(short id)
        {
            return _context.Categories.Any(c => c.CategoryId == id);
        }
    }
}
