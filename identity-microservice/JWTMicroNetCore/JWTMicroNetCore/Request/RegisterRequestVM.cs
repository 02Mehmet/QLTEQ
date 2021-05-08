using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTMicroNetCore.Request
{
    public class RegisterRequestVM
    {
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string userId { get; set; }
    }
}
