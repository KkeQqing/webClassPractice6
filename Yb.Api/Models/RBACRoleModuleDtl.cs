namespace Vb.Api.Models
{
    public class RBACRoleModuleDtl
    {
        public string RoleModuleRoleId { get; set; } = null!;
        public string RoleModuleModuleId { get; set; } = null!;
        public string ModuleDtlId { get; set; } = null!;

        // 导航属性
        public RBACRoleModule RoleModule { get; set; } = null!;
        public RBACModuleDtl ModuleDtl { get; set; } = null!;
    }
}