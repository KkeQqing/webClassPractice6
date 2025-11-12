namespace Vb.Api.Models
{
    public class RBACNewsAttachment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string NewsId { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public long? FileSize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // 导航属性
        public RBACNews News { get; set; } = null!;
    }
}