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
    public class TokenManager : ITokenService
    {
        private ITokenRepository _tokenRepository;

        public TokenManager()
        {
            _tokenRepository = new TokenRepository();
        }

        public string GetToken(TokenVM tokenVM)
        {
            return _tokenRepository.GetToken(tokenVM);
        }
    }
}
