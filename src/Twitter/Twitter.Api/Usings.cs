global using System.IO;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Twitter.Core.Interfaces;
global using Twitter.Core.JwtAuthentication;
global using Twitter.Core.Middlewares;
global using Twitter.Core.Services;
global using Twitter.Data.Context;
global using Twitter.Data.Repository;
global using Twitter.Domain.Interfaces;
global using Twitter.IoC.DependencyContainer;

global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using System.Reflection.Metadata.Ecma335;
global using System.Security.Claims;
global using Twitter.Core.DTOs.Account;
global using Twitter.Domain.Models.UserRoles;
global using Twitter.Core.Security;

global using Twitter.Core.DTOs.Tweet;