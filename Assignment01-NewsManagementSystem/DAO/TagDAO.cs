using Assignment01_NewsManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment01_NewsManagementSystem.DAO
{
    public class TagDAO
    {
        private readonly FunewsManagementContext _context;

        public TagDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        // Get all tags
        public List<Tag> GetAllTags()
        {
            return _context.Tags
                .Include(t => t.NewsArticles)
                .ToList();
        }

        // Get tag by ID
        public Tag GetTagById(int id)
        {
            return _context.Tags
                .Include(t => t.NewsArticles)
                .FirstOrDefault(t => t.TagId == id);
        }

        // Create a new tag
        public void AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        // Update an existing tag
        public void UpdateTag(Tag tag)
        {
            _context.Tags.Update(tag);
            _context.SaveChanges();
        }

        // Delete a tag
        public void DeleteTag(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }
        }

        // Search tags by name
        public List<Tag> SearchTags(string keyword)
        {
            return _context.Tags
                .Where(t => t.TagName.Contains(keyword))
                .Include(t => t.NewsArticles)
                .ToList();
        }
    }
}
