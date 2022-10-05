using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Twitter.Domain.Models.Tweet;
using Twitter.Domain.Models.UserRoles;

namespace Twitter.Data.Context
{
    public class TwitterContext : DbContext
    {
        public TwitterContext(DbContextOptions<TwitterContext> options) : base(options)
        {

        }
        #region UserAndRoles
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ProfileUser> ProfileUsers { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion
        #region Tweets
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<TweetHashtag> TweetHashtags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Query Filters
            Filters(modelBuilder);

            //Relationships
            Relationships(modelBuilder);

            //Set Key
            modelBuilder.Entity<Token>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            base.OnModelCreating(modelBuilder);
        }

        #region Filters Method
        private static void Filters(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Permission>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Tweet>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Tag>().HasQueryFilter(u => !u.IsDelete);
        }
        #endregion
        #region Relationships Method
        private static void Relationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasOne(t => t.Tweet).WithMany(t => t.Tags).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Tag>().HasOne(t => t.User).WithMany(t => t.Tags).OnDelete(DeleteBehavior.NoAction);
        }
        #endregion
    }
}
