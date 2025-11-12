namespace Vb.Api.Models
{
    public class RBACLoginLog
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string UserId { get; set; } = null!;
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        // 导航属性
        public RBACUser User { get; set; } = null!;
    }
}