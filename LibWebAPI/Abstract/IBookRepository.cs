using EntityFramework.Models;
using QLTEQ.GRPC.Protos;
using System.Collections.Generic;

namespace LibWebAPI.Abstract
{
    public interface IBookRepository
    {
        Books GetAllBooks();
        BookVM GetBookById(int id);
        void CreateBook(EntityFramework.Models.Book book);
        void Update(EntityFramework.Models.Book book);
        void DeleteBookById(int id);
    }
}