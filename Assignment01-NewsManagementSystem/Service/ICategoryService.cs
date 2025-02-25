using Assignment01_NewsManagementSystem.Models;

namespace Assignment01_NewsManagementSystem.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();

        Category? GetById(short id);
        void Create(Category category);
        void Update(Category category);
        void Delete(short id);
    }
}
