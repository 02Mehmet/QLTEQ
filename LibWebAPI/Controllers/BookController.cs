using Microsoft.AspNetCore.Mvc;
using LibWebAPI;
using System.Collections.Generic;
using LibWebAPI.Business.Abstract;
using LibWebAPI.Business.Concrete;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using QLTEQ.GRPC.Protos;

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
        public Books Get()
        {
            return _bookService.GetAllBooks();
        }

        [HttpGet("{bookId}")]
        public BookVM Get(int bookId)
        {
            return _bookService.GetBookById(bookId);
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
    }
}