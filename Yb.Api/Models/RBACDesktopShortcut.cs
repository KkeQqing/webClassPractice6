namespace Vb.Api.Models
{
    public class RBACDesktopShortcut
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string UserId { get; set; } = null!;
        public string ModuleId { get; set; } = null!;
        public int IconOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 导航属性
        public RBACUser User { get; set; } = null!;
        public RBACModule Module { get; set; } = null!;
    }
}