using Assignment01_NewsManagementSystem.DAO;
using Assignment01_NewsManagementSystem.Models;
using Assignment01_NewsManagementSystem.Repository;
using Assignment01_NewsManagementSystem.Service;

namespace Assignment01_NewsManagementSystem.Controllers
{
    public class WebManager
    {
        private static WebManager _instance;
        private static readonly object _lock = new object();

        private static FunewsManagementContext _context;
        private static INewsArticleRepository _newsArticleRepository;
        private static ISystemAccountRepository _systemAccountRepository;
        private static ICategoryRepository _categoryRepository;

        public FunewsManagementContext Context => _context;

        public SystemAccount CurrentAccount { get; set; }

        public TagDAO TagDAO { get; }
        public NewsArticleDAO NewsArticleDAO { get; }
        public SystemAccountDAO SystemAccountDAO { get; }
        public CategoriesDAO CategoryDAO { get; }

        public NewsArticleService NewsArticleService { get; }
        public CategoryService CategoryService { get; }
        public SystemAccountService SystemAccountService { get; }

        // ✅ Private Constructor (Singleton)
        private WebManager()
        {
            if (_context == null || _newsArticleRepository == null ||
                _systemAccountRepository == null || _categoryRepository == null)
            {
                throw new InvalidOperationException("WebManager has not been initialized with required dependencies.");
            }

            // Initialize DAOs
            TagDAO = new TagDAO(_context);
            NewsArticleDAO = new NewsArticleDAO(_context);
            SystemAccountDAO = new SystemAccountDAO(_context);
            CategoryDAO = new CategoriesDAO(_context);

            // Initialize Services
            NewsArticleService = new NewsArticleService(_newsArticleRepository);
            CategoryService = new CategoryService(_categoryRepository);
            SystemAccountService = new SystemAccountService(_systemAccountRepository);
        }

        // ✅ First-Time Initialization with Parameters
        public static void Initialize(FunewsManagementContext context,
            INewsArticleRepository newsArticleRepository,
            ISystemAccountRepository systemAccountRepository,
            ICategoryRepository categoryRepository)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _context = context;
                    _newsArticleRepository = newsArticleRepository;
                    _systemAccountRepository = systemAccountRepository;
                    _categoryRepository = categoryRepository;

                    _instance = new WebManager();
                }
            }
        }

        // ✅ Get Singleton Instance (No Parameters)
        public static WebManager Instance()
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("WebManager has not been initialized. Call Initialize() first.");
            }
            return _instance;
        }
    }
}
