using Assignment01_NewsManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment01_NewsManagementSystem.DAO
{
    public class NewsArticleDAO
    {
        private readonly FunewsManagementContext _context;

        public NewsArticleDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        // Get all news articles
        public List<NewsArticle> GetAllArticles()
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .ToList();
        }

        public IEnumerable<NewsArticle> GetAllArticles(bool? active, short? accountId)
        {
            var list = _context.NewsArticles.Include(n => n.Category)
                                            .Include(n => n.CreatedBy)
                                            .ToList();
            if (active != null)
                list = list.Where(n => n.NewsStatus == active).ToList();

            if (accountId != null)
                list = list.Where(n => n.CreatedById == accountId || n.UpdatedById == accountId).ToList();

            return list;
        }

        // Get article by ID
        public NewsArticle GetArticleById(string id)
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .FirstOrDefault(n => n.NewsArticleId == id);
        }

        // Create a new news article
        public void AddArticle(NewsArticle article)
        {
            _context.NewsArticles.Add(article);
            _context.SaveChanges();
        }

        // Update an existing news article
        public void UpdateArticle(NewsArticle article)
        {
            _context.NewsArticles.Update(article);
            _context.SaveChanges();
        }

        // Delete a news article
        public void DeleteArticle(string id)
        {
            var article = _context.NewsArticles.Find(id);
            if (article != null)
            {
                _context.NewsArticles.Remove(article);
                _context.SaveChanges();
            }
        }

        // Get news articles by category
        public List<NewsArticle> GetArticlesByCategory(short categoryId)
        {
            return _context.NewsArticles
                .Where(n => n.CategoryId == categoryId)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .ToList();
        }

        // Search news articles by title or content
        public List<NewsArticle> SearchArticles(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return _context.NewsArticles
                               .Include(n => n.Category)
                               .Include(n => n.CreatedBy)
                               .ToList();
            }

            return _context.NewsArticles
                .Where(n => n.NewsTitle.Contains(keyword) || n.NewsContent.Contains(keyword))
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .ToList();
        }


        public IEnumerable<NewsArticle> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.NewsArticles
                           .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                           .OrderByDescending(n => n.CreatedDate)
                           .Include(n => n.Category)
                           .Include(n => n.CreatedBy)// Include related data if needed
                           .ToList();
        }
    }
}
