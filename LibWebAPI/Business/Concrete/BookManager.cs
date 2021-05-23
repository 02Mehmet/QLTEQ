using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFramework.Models;
using LibWebAPI.Abstract;
using LibWebAPI.Business.Abstract;
using LibWebAPI.Concrete;
using QLTEQ.GRPC.Protos;

namespace LibWebAPI.Business.Concrete
{
    public class BookManager : IBookService
    {
        private IBookRepository _bookRepository;

        public BookManager()
        {
            _bookRepository = new BookRepository();
        }
        public void CreateBook(EntityFramework.Models.Book book)
        {
            _bookRepository.CreateBook(book);
        }

        public void DeleteBookById(int id)
        {
            _bookRepository.DeleteBookById(id);
        }

        public async Task<string> GetAllBookBytes()
        {
            return await _bookRepository.GetAllBookBytes();
        }

        public Books GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public BookVM GetBookById(int id, string token)
        {
            return _bookRepository.GetBookById(id,token);
        }

        public void Update(EntityFramework.Models.Book book)
        {
            _bookRepository.Update(book);
        }
    }
}