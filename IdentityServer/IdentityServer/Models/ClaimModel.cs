using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTMicroNetCore.Models
{
    public class ClaimModel
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
