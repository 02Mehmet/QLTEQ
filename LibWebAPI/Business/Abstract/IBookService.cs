using EntityFramework.Models;
using System.Collections.Generic;

namespace LibWebAPI.Business.Abstract
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBookByIsbn(string isbn);
        void CreateBook(Book book);
        Book Update(Book book);
        void DeleteBook(int id);
    }
}