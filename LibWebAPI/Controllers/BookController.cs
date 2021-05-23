using Microsoft.AspNetCore.Mvc;
using LibWebAPI;
using System.Collections.Generic;
using LibWebAPI.Business.Abstract;
using LibWebAPI.Business.Concrete;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using QLTEQ.GRPC.Protos;
using System.Threading.Tasks;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace LibWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;
        private readonly IConfiguration _config;

        public BookController(IConfiguration config)
        {
            _config = config;
            _bookService = new BookManager();
        }

        [HttpGet]
        public Books Get()
        {
            return _bookService.GetAllBooks();
        }

        //[HttpGet]
        //public async Task<string> GetAllBytes()
        //{
        //    return await _bookService.GetAllBookBytes();
        //}

        [HttpGet("{bookId}")]
        public async Task<BookVM> Get(int bookId)
        {
            var token =  await GetTokenFromIS4();
            return _bookService.GetBookById(bookId,token);
        }

        [Authorize]
        [HttpPost("create")]
        public void Create(EntityFramework.Models.Book book)
        {
             _bookService.CreateBook(book);
        }

        [Authorize]
        [HttpPost("update")]
        public void Update(EntityFramework.Models.Book book)
        {
            _bookService.Update(book);
        }

        [Authorize]
        [HttpPost("delete")]
        public void Delete(int bookId)
        {
            _bookService.DeleteBookById(bookId);
        }

        //[HttpGet("{bookId}")]
        //public BookVM GetByte(int bookId)
        //{
        //    return _bookService.GetBookById(bookId);
        //}
        private async Task<string> GetTokenFromIS4()
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_config.GetValue<string>("WorkerService:IdentityServerUrl"));
            if (disco.IsError)
            {
                return string.Empty;
            }
            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,//https:localhost:5005/connect/token
                ClientId = "UserAuthenticationClient",
                ClientSecret = "secret",
                Scope = "Email"
            });
            if (tokenResponse.IsError)
            {
                return string.Empty;
            }
            return tokenResponse.AccessToken;
        }
    }
}