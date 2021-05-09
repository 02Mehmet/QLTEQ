using LibWebAPI.Abstract;
using LibWebAPI.Business.Abstract;
using LibWebAPI.Concrete;
using LibWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibWebAPI.Business.Concrete
{
    public class RegisterManager : IRegisterService
    {
        private IRegisterRepository _registerRepository;

        public RegisterManager()
        {
            _registerRepository = new RegisterRepository();
        }

        public string Register(RegisterVM registerVM)
        {
            return _registerRepository.Register(registerVM);            
        }
    }
}
