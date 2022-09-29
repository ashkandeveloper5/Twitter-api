using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.Utilities
{
    public static class UniqueCode
    {
        public static string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
