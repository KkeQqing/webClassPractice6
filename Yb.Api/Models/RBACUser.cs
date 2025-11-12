namespace Vb.Api.Models
{
    public class RBACUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 导航属性
        public ICollection<RBACUserRole> UserRoles { get; set; } = new List<RBACUserRole>();
        public ICollection<RBACDesktopShortcut> DesktopShortcuts { get; set; } = new List<RBACDesktopShortcut>();
        public ICollection<RBACLoginLog> LoginLogs { get; set; } = new List<RBACLoginLog>();
        public ICollection<RBACNews> NewsAuthored { get; set; } = new List<RBACNews>();
    }
}