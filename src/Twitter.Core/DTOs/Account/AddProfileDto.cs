using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.DTOs.Account
{
    public class AddProfileDto
    {
        public string Email { get; set; }
        public IFormFile Profile { get; set; }
    }
}
