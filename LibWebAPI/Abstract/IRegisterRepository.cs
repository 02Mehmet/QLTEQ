using LibWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibWebAPI.Abstract
{
    public interface IRegisterRepository
    {
        string Register(RegisterVM registerVM);
    }
}
