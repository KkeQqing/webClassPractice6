namespace Vb.Api.Models
{
    public class RBACModuleDtl
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string ActionName { get; set; } = null!;
        public string ActionCode { get; set; } = null!;

        // 导航属性
        public ICollection<RBACRoleModuleDtl> RoleModuleDtls { get; set; } = new List<RBACRoleModuleDtl>();
    }
}