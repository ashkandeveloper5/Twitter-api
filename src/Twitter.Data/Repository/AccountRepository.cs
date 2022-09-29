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

namespace Twitter.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private TwitterContext _context;
        public AccountRepository(TwitterContext context)
        {
            _context = context;
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

        public bool RegisterUser(User user)
        {
            try
            {

                if (_context.Users.SingleOrDefault(user) != null) return false;
                _context.Users.AddAsync(user);
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
