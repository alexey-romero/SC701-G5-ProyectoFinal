using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Models.Models
{
    public class LoginResponse
    {
        public User User {  get; set; }

        public List<Role> Roles { get; set; }

        public LoginResponse(User user, List<Role> roles)
        {
            User = user;
            Roles = roles;
        }
    }
}
