using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Models
{
    public class AuthenticationRequest
    {
        public string Password { get; set; }

        public string UserPhone { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public bool TypeUser { get; set; }
    }
}
