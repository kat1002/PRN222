using Assignment01_NewsManagementSystem.DAO;
using Assignment01_NewsManagementSystem.Models;

namespace Assignment01_NewsManagementSystem.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoriesDAO _categoryDAO;

        public CategoryRepository(FunewsManagementContext context)
        {
            _categoryDAO = new CategoriesDAO(context);
        }

        public IEnumerable<Category> GetAll() => _categoryDAO.GetAllCategories();

        public Category? GetById(short id) => _categoryDAO.GetCategoryById(id);

        public void Create(Category article) => _categoryDAO.AddCategory(article);
        public void Update(Category article) => _categoryDAO.UpdateCategory(article);
        public void Delete(short id) => _categoryDAO.DeleteCategory(id);
    }
}
