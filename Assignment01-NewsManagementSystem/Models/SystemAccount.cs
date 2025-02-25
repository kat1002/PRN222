using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment01_NewsManagementSystem.Models;

public partial class SystemAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public short AccountId { get; set; }

    [Required]
    public string AccountName { get; set; }

    [Required]
    [EmailAddress]
    public string AccountEmail { get; set; }

    [Required]
    public int AccountRole { get; set; } // 0: Admin, 1: Lecturer, 2: Staff

    [Required]
    public string AccountPassword { get; set; }
    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();

    public override string ToString()
    {
        return $"{AccountName} {AccountEmail} {AccountPassword} {AccountRole}";
    }
}
