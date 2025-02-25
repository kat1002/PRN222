using Assignment01_NewsManagementSystem.Models;
using Assignment01_NewsManagementSystem.Repository;

namespace Assignment01_NewsManagementSystem.Service
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _systemAccountRepository;

        public SystemAccountService(ISystemAccountRepository systemAccountRepository)
        {
            _systemAccountRepository = systemAccountRepository;
        }

        public IEnumerable<SystemAccount> GetAll()
        {
            return _systemAccountRepository.GetAll();
        }

        public SystemAccount? GetById(short id)
        {
            return _systemAccountRepository.GetById(id);
        }
        public void Create(SystemAccount systemAccount)
        {
            _systemAccountRepository.Create(systemAccount);
        }

        public void Update(SystemAccount systemAccount)
        {
            _systemAccountRepository.Update(systemAccount);
        }
        public void Delete(short id)
        {
            _systemAccountRepository.Delete(id);
        }
    }
}
