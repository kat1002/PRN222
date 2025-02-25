using Assignment01_NewsManagementSystem.Models;

namespace Assignment01_NewsManagementSystem.DAO
{
    public class SystemAccountDAO
    {
        private readonly FunewsManagementContext _context;

        public SystemAccountDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        // Get all accounts
        public List<SystemAccount> GetAllAccounts()
        {
            return _context.SystemAccounts.ToList();
        }

        // Get account by ID
        public SystemAccount GetAccountById(int id)
        {
            return _context.SystemAccounts.Find(id);
        }

        // Get account by email and password (for login)
        public SystemAccount GetAccountByCredentials(string email, string password)
        {
            return _context.SystemAccounts
                .FirstOrDefault(u => u.AccountEmail == email && u.AccountPassword == password);
        }

        // Create a new account
        public void AddAccount(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            _context.SaveChanges();
        }

        // Update an existing account
        public void UpdateAccount(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            _context.SaveChanges();
        }

        // Delete an account
        public void DeleteAccount(int id)
        {
            var account = _context.SystemAccounts.Find(id);
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                _context.SaveChanges();
            }
        }
    }
}
