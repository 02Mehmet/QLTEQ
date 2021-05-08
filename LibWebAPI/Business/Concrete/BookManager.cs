using System.Collections.Generic;
using EntityFramework.Models;
using LibWebAPI.Abstract;
using LibWebAPI.Business.Abstract;
using LibWebAPI.Concrete;

namespace LibWebAPI.Business.Concrete
{
    public class BookManager : IBookService
    {
        private IBookRepository _bookRepository;

        public BookManager()
        {
            _bookRepository = new BookRepository();
        }
        public void CreateBook(Book book)
        {
            _bookRepository.CreateBook(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.DeleteBook(id);
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookByIsbn(string isbn)
        {
            return _bookRepository.GetBookByIsbn(isbn);
        }

        public Book Update(Book book)
        {
            return _bookRepository.Update(book);
        }
    }
}