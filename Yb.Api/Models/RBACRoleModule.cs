namespace Vb.Api.Models
{
    public class RBACRoleModule
    {
        public string RoleId { get; set; } = null!;
        public string ModuleId { get; set; } = null!;

        // 导航属性
        public RBACRole Role { get; set; } = null!;
        public RBACModule Module { get; set; } = null!;
        public ICollection<RBACRoleModuleDtl> RoleModuleDtls { get; set; } = new List<RBACRoleModuleDtl>();
    }
}