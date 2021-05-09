using LibWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibWebAPI.Business.Abstract
{
    public interface IRegisterService
    {
        string Register(RegisterVM registerVM);
    }
}
