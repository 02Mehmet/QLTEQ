using Microsoft.AspNetCore.Mvc;
using LibWebAPI;
using System.Collections.Generic;
using LibWebAPI.Business.Abstract;
using LibWebAPI.Business.Concrete;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace LibWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController()
        {
            _bookService = new BookManager();
        }

        [HttpGet]
        public List<EntityFramework.Models.Book> Get()
        {
            return _bookService.GetAllBooks();
        }

        [HttpGet("{isbn}")]
        public EntityFramework.Models.Book Get(string isbn)
        {
            return _bookService.GetBookByIsbn(isbn);
        }

        [Authorize]
        [HttpPost("create")]
        public void Create(EntityFramework.Models.Book book)
        {
             _bookService.CreateBook(book);
        }
    }
}