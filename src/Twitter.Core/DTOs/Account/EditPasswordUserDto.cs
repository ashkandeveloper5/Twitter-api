using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.DTOs.Account
{
    public class EditPasswordUserDto
    {
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(100)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(100)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(100)]
        [Compare(nameof(NewPassword))]
        public string RepeatNewPassword { get; set; }
    }
}
