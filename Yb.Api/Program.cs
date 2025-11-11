using Microsoft.EntityFrameworkCore;
using Yb.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// 从配置读取名为“ConnectionStrings:Default”的连接字符串
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
builder.Services.AddControllers(); // Add controllers
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); // Add API explorer
builder.Services.AddSwaggerGen(); // Add Swagger generator

// 注册 DbContext，使用 MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
