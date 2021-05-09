using LibWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibWebAPI.Business.Abstract
{
    public interface ITokenService
    {
        string GetToken(TokenVM tokenVM);
    }
}
