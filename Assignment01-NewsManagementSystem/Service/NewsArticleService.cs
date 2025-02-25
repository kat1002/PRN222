using Assignment01_NewsManagementSystem.Models;
using Assignment01_NewsManagementSystem.Repository;

namespace Assignment01_NewsManagementSystem.Service
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepository;

        public NewsArticleService(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        public IEnumerable<NewsArticle> GetAll(bool? active, short? accountId)
        {
            return _newsArticleRepository.GetAll(active, accountId);
        }

        public NewsArticle? GetById(string id)
        {
            return _newsArticleRepository.GetById(id);
        }
        public void Create(NewsArticle newsArticle)
        {
            _newsArticleRepository.Create(newsArticle);
        }

        public void Update(NewsArticle newsArticle)
        {
            _newsArticleRepository.Update(newsArticle);
        }
        public void Delete(string id)
        {
            _newsArticleRepository.Delete(id);
        }

        public IEnumerable<NewsArticle> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            return _newsArticleRepository.GetByPeriod(startDate, endDate);
        }

        public IEnumerable<NewsArticle> Search(string keyword)
        {
            return _newsArticleRepository.Search(keyword);
        }
    }
}
