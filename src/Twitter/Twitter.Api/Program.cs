using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Twitter.Core.Interfaces;
using Twitter.Core.Services;
using Twitter.Data.Context;
using Twitter.Data.Repository;
using Twitter.Domain.Interfaces;
using Twitter.IoC.DependencyContainer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region IoC
RegisterServices(builder.Services);
#endregion
#region Server
builder.Services.AddDbContext<TwitterContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
#endregion
#region Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/api/Account/Login";
    option.LogoutPath = "/api/Account/Logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(5);
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}