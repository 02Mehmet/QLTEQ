using EntityFramework.Models;
using System.Collections.Generic;

namespace LibWebAPI.Abstract
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBookByIsbn(string isbn);
        void CreateBook(Book book);
        Book Update(Book book);
        void DeleteBook(int id);
    }
}