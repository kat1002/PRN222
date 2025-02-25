using Assignment01_NewsManagementSystem.Models;

namespace Assignment01_NewsManagementSystem.Repository
{
    public interface INewsArticleRepository
    {
        IEnumerable<NewsArticle> GetAll(bool? active, short? accountId);

        NewsArticle? GetById(string id);
        void Create(NewsArticle article);
        void Update(NewsArticle article);
        void Delete(string id);

        public IEnumerable<NewsArticle> GetByPeriod(DateTime startDate, DateTime endDate);
        public IEnumerable<NewsArticle> Search(string keyword);
    }

}
