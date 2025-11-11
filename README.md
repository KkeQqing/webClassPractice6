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