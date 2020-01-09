using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Models
{
    public class UserContext
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string FavColor { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }
        public string[] Roles { get; set; }
    }
}
