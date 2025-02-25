using Assignment01_NewsManagementSystem.Models;
using Assignment01_NewsManagementSystem.Repository;

namespace Assignment01_NewsManagementSystem.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category? GetById(short id)
        {
            return _categoryRepository.GetById(id);
        }
        public void Create(Category category)
        {
            _categoryRepository.Create(category);
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
        public void Delete(short id)
        {
            _categoryRepository.Delete(id);
        }
    }
}
