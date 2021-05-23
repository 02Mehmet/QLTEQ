using EntityFramework.Models;
using QLTEQ.GRPC.Protos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibWebAPI.Abstract
{
    public interface IBookRepository
    {
        Books GetAllBooks();
        BookVM GetBookById(int id, string token);
        void CreateBook(EntityFramework.Models.Book book);
        void Update(EntityFramework.Models.Book book);
        void DeleteBookById(int id);
        Task<string> GetAllBookBytes();
        //BookVM GetBookByteById(int id);
    }
}