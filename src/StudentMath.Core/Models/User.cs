using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMath.Core.Domain
{
    public class InMemoryUser
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; 
    }

    public static class UserStore
    {
        public static List<InMemoryUser> Users { get; set; } = new List<InMemoryUser>();
    }
}
 