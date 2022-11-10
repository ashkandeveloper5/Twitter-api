using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Base Services

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter Token",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    option.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

#endregion
#region IoC
RegisterServices(builder.Services);
#endregion
#region Server
builder.Services.AddDbContext<TwitterContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")); }, ServiceLifetime.Transient);
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(optioin =>
{
    optioin.RequireHttpsMetadata = true;
    optioin.SaveToken = true;
    optioin.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidIssuer = "TwitterServer",
        ValidateAudience = true,
        ValidAudience = "Twitter",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TwitterapiTwitterapiTwitterapiTwitterapi")),
        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1111111111111111")),
    };
    //optioin.Events = new JwtBearerEvents
    //{
    //    OnTokenValidated = async (context) =>
    //    {
    //        var claims = (context.Principal.Identity as ClaimsIdentity).Claims.ToList();
    //        var userUid = claims.Where(u => u.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
    //    },
    //};
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