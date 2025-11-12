namespace Vb.Api.Models
{
    public class RBACRole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 导航属性
        public ICollection<RBACUserRole> UserRoles { get; set; } = new List<RBACUserRole>();
        public ICollection<RBACRoleModule> RoleModules { get; set; } = new List<RBACRoleModule>();
    }
}