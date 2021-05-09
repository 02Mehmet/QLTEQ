using System.Collections.Generic;
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

        public Books GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public BookVM GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public void Update(EntityFramework.Models.Book book)
        {
            _bookRepository.Update(book);
        }
    }
}