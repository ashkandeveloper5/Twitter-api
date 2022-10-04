using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.DTOs.Tweet
{
    public class GetHashtagDto
    {
        public string HahtagId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Views { get; set; }
    }
}
