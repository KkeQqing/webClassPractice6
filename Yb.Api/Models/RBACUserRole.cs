namespace Vb.Api.Models
{
    public class RBACUserRole
    {
        public string UserId { get; set; } = null!;
        public string RoleId { get; set; } = null!;

        // 导航属性
        public RBACUser User { get; set; } = null!;
        public RBACRole Role { get; set; } = null!;
    }
}