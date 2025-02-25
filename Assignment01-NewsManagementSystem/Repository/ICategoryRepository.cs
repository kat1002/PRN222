using Assignment01_NewsManagementSystem.Models;

namespace Assignment01_NewsManagementSystem.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();

        Category? GetById(short id);
        void Create(Category account);
        void Update(Category account);
        void Delete(short id);
    }
}
