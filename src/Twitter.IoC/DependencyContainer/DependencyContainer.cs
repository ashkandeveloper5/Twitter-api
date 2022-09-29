using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Interfaces;
using Twitter.Core.Services;
using Twitter.Data.Repository;
using Twitter.Domain.Interfaces;

namespace Twitter.IoC.DependencyContainer
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddScoped<IAccountRepository,AccountRepository>();
            service.AddScoped<IAccountService,AccountService>();
        }
    }
}
