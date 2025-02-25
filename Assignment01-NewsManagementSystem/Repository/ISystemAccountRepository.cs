using Assignment01_NewsManagementSystem.Models;

namespace Assignment01_NewsManagementSystem.Repository
{
    public interface ISystemAccountRepository
    {
        IEnumerable<SystemAccount> GetAll();

        SystemAccount? GetById(short id);
        void Create(SystemAccount account);
        void Update(SystemAccount account);
        void Delete(short id);
    }
}
