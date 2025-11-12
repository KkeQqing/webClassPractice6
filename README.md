# Yb.Api - 基于 EF Core 的 MySQL Web API 项目

本项目是一个使用 **ASP.NET Core Web API** 搭配 **EF Core + MySQL** 实现的用户管理接口服务。

---

## 安装所需 NuGet 包

> EF Core 默认不支持 MySQL，需安装第三方提供程序。

### 1. 安装 Pomelo.EntityFrameworkCore.MySql（推荐）

在 Visual Studio 中：
- 右键点击项目 `Yb.Api`
- 选择 **“管理 NuGet 包”**
- 切换到 “浏览” 标签
- 搜索并安装：  
  `Pomelo.EntityFrameworkCore.MySql`

> 安装对应 .NET 版本即可。

---

## 安装 EF Core 工具包（用于迁移）

在 **包管理器控制台（PMC）** 中运行以下命令：

```powershell
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.10
💡 为什么选 8.0.10？因为它是 EF Core 8.0 兼容版本的工具包，确保 Add-Migration 和 Update-Database 能正常工作。

确保选择了正确的默认项目
在 包管理器控制台（PMC） 的顶部，有一个下拉菜单：“默认项目”。

✅ 请确认它选择的是你的 Web API 项目：Yb.Api

❌ 不要选错成解决方案或其他类库！

⚠️ 如果没选对，后面执行命令会失败。

重启包管理器控制台
安装完工具后，必须重启 PMC 才能识别新安装的命令。

配置连接字符串（使用 User Secrets）
1. 启用 User Secrets
在项目根目录（含 .csproj 的地方）运行：

powershell
dotnet user-secrets init
这会在 Yb.Api.csproj 中自动生成 UserSecretsId。

2. 设置 MySQL 连接字符串
运行：

powershell
dotnet user-secrets set "ConnectionStrings:Default" "server=localhost;port=3306;database=RGZN;user=root;password=your_password"
💡 替换 your_password 为你的真实 MySQL 密码。

3. 验证是否成功
运行：

powershell
dotnet user-secrets list
应看到输出包含你的连接字符串。

启用 EF Core 迁移（自动建表）
1. 添加初始迁移
在 包管理器控制台（PMC） 中执行：

powershell
Add-Migration InitialCreate
2. 更新数据库（自动建表）
powershell
Update-Database
✅ 成功后，EF Core 会在 MySQL 的 RGZN 数据库中自动创建 YbUsers 表！

⚠️ 注意：如果 RGZN 数据库不存在，EF Core 会尝试创建它（需要 root 权限）。


## 基于角色的权限管理系统（Role-Based Access Control, RBAC）数据库表设计

### MySQL 建表语句
-- 1. 用户表
CREATE TABLE RBACUser (
    Id CHAR(32) NOT NULL PRIMARY KEY,
    UserName VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(150),
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 2. 角色表
CREATE TABLE RBACRole (
    Id CHAR(32) NOT NULL PRIMARY KEY,
    RoleName VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(255),
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 3. 用户-角色关联表（支持多角色）
CREATE TABLE RBACUserRole (
    UserId CHAR(32) NOT NULL,
    RoleId CHAR(32) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES RBACUser(Id) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES RBACRole(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 4. 模块/资源表（如菜单项）
CREATE TABLE RBACModule (
    Id CHAR(32) NOT NULL PRIMARY KEY,
    ModuleName VARCHAR(100) NOT NULL,
    ModuleCode VARCHAR(100) NOT NULL UNIQUE,
    ParentId CHAR(32) DEFAULT NULL,
    SortOrder INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 5. 模块操作细节（权限动作：读、写、删等）
CREATE TABLE RBACModuleDtl (
    Id CHAR(32) NOT NULL PRIMARY KEY,
    ActionName VARCHAR(50) NOT NULL,   -- 如 "查看", "编辑"
    ActionCode VARCHAR(20) NOT NULL    -- 如 "READ", "WRITE"
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 6. 角色-模块关联表
CREATE TABLE RBACRoleModule (
    RoleId CHAR(32) NOT NULL,
    ModuleId CHAR(32) NOT NULL,
    PRIMARY KEY (RoleId, ModuleId),
    FOREIGN KEY (RoleId) REFERENCES RBACRole(Id) ON DELETE CASCADE,
    FOREIGN KEY (ModuleId) REFERENCES RBACModule(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 7. 角色-模块-操作权限表
CREATE TABLE RBACRoleModuleDtl (
    RoleModuleRoleId CHAR(32) NOT NULL,
    RoleModuleModuleId CHAR(32) NOT NULL,
    ModuleDtlId CHAR(32) NOT NULL,
    PRIMARY KEY (RoleModuleRoleId, RoleModuleModuleId, ModuleDtlId),
    FOREIGN KEY (RoleModuleRoleId, RoleModuleModuleId) 
        REFERENCES RBACRoleModule(RoleId, ModuleId) ON DELETE CASCADE,
    FOREIGN KEY (ModuleDtlId) REFERENCES RBACModuleDtl(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 8. 用户桌面快捷图标（系统表）
CREATE TABLE RBACDesktopShortcut (
    Id CHAR(32) NOT NULL PRIMARY KEY,
    UserId CHAR(32) NOT NULL,
    ModuleId CHAR(32) NOT NULL,
    IconOrder INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES RBACUser(Id) ON DELETE CASCADE,
    FOREIGN KEY (ModuleId) REFERENCES RBACModule(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 9. 登录日志表（系统表）
CREATE TABLE RBACLoginLog (
    Id CHAR(32) NOT NULL PRIMARY KEY,
    UserId CHAR(32) NOT NULL,
    LoginTime DATETIME DEFAULT CURRENT_TIMESTAMP,
    IpAddress VARCHAR(45),  -- 支持 IPv6
    UserAgent TEXT,
    FOREIGN KEY (UserId) REFERENCES RBACUser(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 10. 新闻表（业务表）
CREATE TABLE RBACNews (
    Id CHAR(32) NOT NULL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Content LONGTEXT,
    AuthorId CHAR(32) NOT NULL,
    PublishedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    IsPublished BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (AuthorId) REFERENCES RBACUser(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 11. 新闻附件表（业务表）
CREATE TABLE RBACNewsAttachment (
    Id CHAR(32) NOT NULL PRIMARY KEY,
    NewsId CHAR(32) NOT NULL,
    FileName VARCHAR(255) NOT NULL,
    FilePath VARCHAR(500) NOT NULL,
    FileSize BIGINT,
    UploadedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (NewsId) REFERENCES RBACNews(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

### 或
执行 EF 迁移
如果希望 EF 自动建表（而非手动运行 SQL）：
dotnet ef migrations add AddRBACTables
dotnet ef database update