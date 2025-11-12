namespace Vb.Api.Models
{
    public class RBACModule
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string ModuleName { get; set; } = null!;
        public string ModuleCode { get; set; } = null!;
        public string? ParentId { get; set; }
        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 导航属性
        public ICollection<RBACRoleModule> RoleModules { get; set; } = new List<RBACRoleModule>();
        public ICollection<RBACDesktopShortcut> DesktopShortcuts { get; set; } = new List<RBACDesktopShortcut>();
    }
}