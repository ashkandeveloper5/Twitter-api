using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Twitter.Core.Interfaces;
using Twitter.Core.JwtAuthentication;
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
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();
#region IoC
RegisterServices(builder.Services);
#endregion
#region Server
builder.Services.AddDbContext<TwitterContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
#endregion
#region Cookies
//builder.Services.AddHttpClient("TwitterClient", client =>
//{
//    client.BaseAddress = new Uri("http://localhost:11352");
//});
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
//{
//    options.LoginPath = "";
//    options.LogoutPath = "";
//    options.Cookie.Name = "Auth.j";
//});
#endregion
#region JwtAuthentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey=true,
        ValidIssuer= "http://localhost:11352",
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Verify"))
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCors", Builder =>
    {
        Builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .Build();
    });
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
app.UseRouting();

app.UseCors("EnableCors");

app.UseStaticFiles();
app.UseCookiePolicy();

//app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}