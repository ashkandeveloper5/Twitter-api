using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Context;
using Twitter.Domain.Interfaces;
using Twitter.Domain.Models.UserRoles;
using Twitter.Core.Security;
using Twitter.Core.Utilities;

namespace Twitter.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TwitterContext _context;
        public AccountRepository(TwitterContext context)
        {
            _context = context;
        }

        public Task<bool> AddProfile(string userEmail, string path)
        {
            var user = GetUserByEmail(userEmail);
            var Profile = new ProfileUser()
            {
                ProfileId = UniqueCode.generateID(),
                UserId = user.UserId,
                Path = path,
            };
            try
            {
                _context.ProfileUsers.Add(Profile);
                SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> CheckFullImage(string userEmail)
        {
            return _context.ProfileUsers.Where(u => u.UserId == GetUserByEmail(userEmail).UserId).ToList().Count >= 5 ? Task.FromResult(false) : Task.FromResult(true);
        }

        public bool DeleteAccountUser(User user)
        {
            try
            {
                _context.Users.SingleOrDefault(u => u.UserId == user.UserId).IsDelete = true;
                _context.Tags.Where(u => u.UserId == user.UserId).ToList().ForEach(t => { t.IsDelete = true; });
                _context.Tweets.Where(u => u.UserId == user.UserId).ToList().ForEach(t => { t.IsDelete = true; });
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                _context.Users.SingleOrDefault(u => u.UserId == user.UserId).IsDelete = true;
                _context.Tags.Where(u => u.UserId == user.UserId).ToList().ForEach(t => { t.IsDelete = true; });
                _context.Tweets.Where(u => u.UserId == user.UserId).ToList().ForEach(t => { t.IsDelete = true; });
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditPasswordUser(string userEmail, string newPassword)
        {
            try
            {
                User user = _context.Users.SingleOrDefault(u => u.Email == userEmail);
                if (user != null) user.Password = newPassword;
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<string>> GetImagesPath(string userEmail)
        {
            var result = new List<string>();
            var profiles = _context.ProfileUsers.Where(p => p.UserId == GetUserByEmail(userEmail).UserId).ToList();
            foreach (var profile in profiles)
            {
                result.Add(profile.Path);
            }
            return Task.FromResult(result);
        }

        public User GetUserByEmail(string userEmail)
        {
            var user = _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail).Result;
            return user != null ? user : null;
        }

        public User GetUserById(string userId)
        {
            var user = _context.Users.SingleOrDefaultAsync(u => u.UserId == userId).Result;
            return user != null ? user : null;
        }

        public User GetUserByPhoneNumber(string userPhoneNumber)
        {
            var user = _context.Users.SingleOrDefaultAsync(u => u.PhoneNumber == userPhoneNumber).Result;
            return user != null ? user : null;
        }

        public bool IsExistEmail(string userEmail)
        {
            User user = _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail).Result;
            return (user != null) ? true : false;
        }

        public bool IsExistId(string userId)
        {
            User user = _context.Users.SingleOrDefaultAsync(u => u.UserId == userId).Result;
            return (user != null) ? true : false;
        }

        public bool IsExistPhoneNumber(string userPhoneNumber)
        {
            User user = _context.Users.SingleOrDefaultAsync(u => u.PhoneNumber == userPhoneNumber).Result;
            return (user != null) ? true : false;
        }

        public bool IsExistUser(User user)
        {
            User result = _context.Users.FirstOrDefault(user);
            return (result != null) ? true : false;
        }

        public bool MatchPasswordForChange(string userEmail, string oldPassword)
        {
            return _context.Users.SingleOrDefault(u => u.Email == userEmail).Password == oldPassword ? true : false;
        }

        public bool RegisterUserByEmail(User user)
        {
            try
            {
                if (_context.Users.SingleOrDefault(u => u.Email == user.Email) != null) return false;
                user.UserId = UniqueCode.generateID();
                _context.Users.Add(user);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
