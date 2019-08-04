using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Models
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public User User { get; set; }
    }
}
