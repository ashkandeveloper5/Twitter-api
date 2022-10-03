using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Interfaces;
using Twitter.Core.JwtAuthentication;
using Twitter.Core.Services;
using Twitter.Data.Repository;
using Twitter.Domain.Interfaces;

namespace Twitter.IoC.DependencyContainer
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            //Jwt Authentication
            service.AddScoped<IJwtAuthentication,JwtAuthentication>();

            //Role
            service.AddScoped<IRoleRepository,RoleRepository>();
            service.AddScoped<IRoleService,RoleService>();

            //Tweet
            service.AddScoped<ITweetRepository,TweetRepository>();
            service.AddScoped<ITweetService,TweetService>();

            //Account
            service.AddScoped<IAccountRepository,AccountRepository>();
            service.AddScoped<IAccountService,AccountService>();
        }
    }
}
