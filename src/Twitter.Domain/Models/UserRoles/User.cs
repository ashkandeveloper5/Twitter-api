using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Domain.Models.UserRoles
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        public int? Age { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsActive { get; set; } = false;

        #region Relationship
        public IList<UserRole> UserRoles { get; set; }
        public IList<Token> Tokens { get; set; }
        public IList<Claim> Claims { get; set; }
        #endregion
    }
}
