using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            modelBuilder.Entity<Tag>().HasOne(t => t.Tweet).WithMany(t => t.Tags).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Tag>().HasOne(t => t.User).WithMany(t => t.Tags).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Token>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            base.OnModelCreating(modelBuilder);
        }
    }
}
