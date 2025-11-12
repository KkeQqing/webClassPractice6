// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using Vb.Api.Models;
using Yb.Api.Models;

namespace Yb.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ========== 原有 DbSet ==========
        public DbSet<YbUser> YbUsers { get; set; }

        // ========== 新增 RBAC 相关 DbSet（追加在这里）==========
        public DbSet<RBACUser> RBACUsers { get; set; }
        public DbSet<RBACRole> RBACRoles { get; set; }
        public DbSet<RBACUserRole> RBACUserRoles { get; set; }
        public DbSet<RBACModule> RBACModules { get; set; }
        public DbSet<RBACModuleDtl> RBACModuleDtls { get; set; }
        public DbSet<RBACRoleModule> RBACRoleModules { get; set; }
        public DbSet<RBACRoleModuleDtl> RBACRoleModuleDtls { get; set; }
        public DbSet<RBACDesktopShortcut> RBACDesktopShortcuts { get; set; }
        public DbSet<RBACLoginLog> RBACLoginLogs { get; set; }
        public DbSet<RBACNews> RBACNews { get; set; }
        public DbSet<RBACNewsAttachment> RBACNewsAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ========== 原有配置 ==========
            modelBuilder.Entity<YbUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50);
            });

            // ========== 新增 RBAC 表配置 ==========

            modelBuilder.Entity<RBACUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(32);
                entity.HasMany(e => e.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.DesktopShortcuts).WithOne(ds => ds.User).HasForeignKey(ds => ds.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.LoginLogs).WithOne(log => log.User).HasForeignKey(log => log.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.NewsAuthored).WithOne(news => news.Author).HasForeignKey(news => news.AuthorId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RBACRole>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(32);
                entity.HasMany(e => e.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.RoleModules).WithOne(rm => rm.Role).HasForeignKey(rm => rm.RoleId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RBACUserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.Property(ur => ur.UserId).HasMaxLength(32);
                entity.Property(ur => ur.RoleId).HasMaxLength(32);
                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
                entity.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<RBACModule>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(32);
                entity.HasMany(e => e.RoleModules).WithOne(rm => rm.Module).HasForeignKey(rm => rm.ModuleId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.DesktopShortcuts).WithOne(ds => ds.Module).HasForeignKey(ds => ds.ModuleId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RBACModuleDtl>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(32);
                entity.HasMany(e => e.RoleModuleDtls).WithOne(rmd => rmd.ModuleDtl).HasForeignKey(rmd => rmd.ModuleDtlId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RBACRoleModule>(entity =>
            {
                entity.HasKey(rm => new { rm.RoleId, rm.ModuleId });
                entity.Property(rm => rm.RoleId).HasMaxLength(32);
                entity.Property(rm => rm.ModuleId).HasMaxLength(32);
                entity.HasOne(rm => rm.Role).WithMany(r => r.RoleModules).HasForeignKey(rm => rm.RoleId);
                entity.HasOne(rm => rm.Module).WithMany(m => m.RoleModules).HasForeignKey(rm => rm.ModuleId);
                entity.HasMany(rm => rm.RoleModuleDtls)
                      .WithOne(rmd => rmd.RoleModule)
                      .HasForeignKey(rmd => new { rmd.RoleModuleRoleId, rmd.RoleModuleModuleId });
            });

            modelBuilder.Entity<RBACRoleModuleDtl>(entity =>
            {
                entity.HasKey(rmd => new { rmd.RoleModuleRoleId, rmd.RoleModuleModuleId, rmd.ModuleDtlId });
                entity.Property(rmd => rmd.RoleModuleRoleId).HasMaxLength(32);
                entity.Property(rmd => rmd.RoleModuleModuleId).HasMaxLength(32);
                entity.Property(rmd => rmd.ModuleDtlId).HasMaxLength(32);
                entity.HasOne(rmd => rmd.RoleModule)
                      .WithMany(rm => rm.RoleModuleDtls)
                      .HasForeignKey(rmd => new { rmd.RoleModuleRoleId, rmd.RoleModuleModuleId });
                entity.HasOne(rmd => rmd.ModuleDtl)
                      .WithMany(md => md.RoleModuleDtls)
                      .HasForeignKey(rmd => rmd.ModuleDtlId);
            });

            modelBuilder.Entity<RBACDesktopShortcut>(entity =>
            {
                entity.HasKey(ds => ds.Id);
                entity.Property(ds => ds.Id).HasMaxLength(32);
                entity.Property(ds => ds.UserId).HasMaxLength(32);
                entity.Property(ds => ds.ModuleId).HasMaxLength(32);
                entity.HasOne(ds => ds.User).WithMany(u => u.DesktopShortcuts).HasForeignKey(ds => ds.UserId);
                entity.HasOne(ds => ds.Module).WithMany(m => m.DesktopShortcuts).HasForeignKey(ds => ds.ModuleId);
            });

            modelBuilder.Entity<RBACLoginLog>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Id).HasMaxLength(32);
                entity.Property(l => l.UserId).HasMaxLength(32);
                entity.HasOne(l => l.User).WithMany(u => u.LoginLogs).HasForeignKey(l => l.UserId);
            });

            modelBuilder.Entity<RBACNews>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Id).HasMaxLength(32);
                entity.Property(n => n.AuthorId).HasMaxLength(32);
                entity.HasOne(n => n.Author).WithMany(u => u.NewsAuthored).HasForeignKey(n => n.AuthorId);
                entity.HasMany(n => n.Attachments).WithOne(a => a.News).HasForeignKey(a => a.NewsId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RBACNewsAttachment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).HasMaxLength(32);
                entity.Property(a => a.NewsId).HasMaxLength(32);
                entity.HasOne(a => a.News).WithMany(n => n.Attachments).HasForeignKey(a => a.NewsId);
            });
        }
    }
}