namespace Vb.Api.Models
{
    public class RBACNews
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string AuthorId { get; set; } = null!;
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
        public bool IsPublished { get; set; } = false;

        // 导航属性
        public RBACUser Author { get; set; } = null!;
        public ICollection<RBACNewsAttachment> Attachments { get; set; } = new List<RBACNewsAttachment>();
    }
}