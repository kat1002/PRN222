using Assignment01_NewsManagementSystem.DAO;
using Assignment01_NewsManagementSystem.Models;

namespace Assignment01_NewsManagementSystem.Repository
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly NewsArticleDAO _newsArticleDAO;

        public NewsArticleRepository(FunewsManagementContext context)
        {
            _newsArticleDAO = new NewsArticleDAO(context);
        }

        public IEnumerable<NewsArticle> GetAll(bool? active, short? accountId) => _newsArticleDAO.GetAllArticles(active, accountId);

        public NewsArticle? GetById(string id) => _newsArticleDAO.GetArticleById(id);

        public void Create(NewsArticle article) => _newsArticleDAO.AddArticle(article);
        public void Update(NewsArticle article) => _newsArticleDAO.UpdateArticle(article);
        public void Delete(string id) => _newsArticleDAO.DeleteArticle(id);

        public IEnumerable<NewsArticle> GetByPeriod(DateTime startDate, DateTime endDate) => _newsArticleDAO.GetByPeriod(startDate, endDate);

        public IEnumerable<NewsArticle> Search(string keyword) => _newsArticleDAO.SearchArticles(keyword);

    }
}
