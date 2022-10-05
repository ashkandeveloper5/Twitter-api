using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Base Services

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

#endregion

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
        Builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        //.AllowCredentials()
        //.AllowAnyOrigin()
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

#region Middleware
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("EnableCors");

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();
//app.UseCustom();
#endregion

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}