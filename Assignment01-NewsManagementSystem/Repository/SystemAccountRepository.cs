using Assignment01_NewsManagementSystem.DAO;
using Assignment01_NewsManagementSystem.Models;

namespace Assignment01_NewsManagementSystem.Repository
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly SystemAccountDAO _systemAccountDAO;

        public SystemAccountRepository(FunewsManagementContext context)
        {
            _systemAccountDAO = new SystemAccountDAO(context);
        }

        public IEnumerable<SystemAccount> GetAll() => _systemAccountDAO.GetAllAccounts();

        public SystemAccount? GetById(short id) => _systemAccountDAO.GetAccountById(id);

        public void Create(SystemAccount article) => _systemAccountDAO.AddAccount(article);
        public void Update(SystemAccount article) => _systemAccountDAO.UpdateAccount(article);
        public void Delete(short id) => _systemAccountDAO.DeleteAccount(id);
    }
}
