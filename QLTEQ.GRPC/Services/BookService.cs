using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework;
using Grpc.Core;
using QLTEQ.GRPC.Protos;

namespace QLTEQ.GRPC.Services
{
    public class BookService : Book.BookBase
    {
        private QlteqContext db = null;

        public BookService(QlteqContext db)
        {
            this.db = db;
        }

        public override Task<Books> SelectAll(Empty requestData, ServerCallContext context)
        {
            Books responseData = new Books();
            var query = db.Books.Select(s => new BookVM()
            {
                BookID = s.BookID,
                Title = s.Title,
                Price = s.Price,
                AuthorName = s.AuthorName,
                AuthorAddress = s.AuthorAddress,
                AuthorBiografi = s.AuthorBiografi,
                AuthorBirthDate = s.AuthorBirthDate.ToString(),
                PublisherName = s.PublisherName,
                PublisherAddress = s.PublisherAddress,
                PublishDate = s.PublishDate.ToString()
            }).AsEnumerable();
            responseData.Items.AddRange(query.ToArray());
            return Task.FromResult(responseData);
        }

        public override Task<BookVM> SelectByID(BookFilter requestData, ServerCallContext context)
        {
            var book = db.Books.Find(requestData.BookID);
            BookVM selectedBook = new BookVM()
            {
                BookID = book.BookID,
                Title = book.Title,
                Price = book.Price,
                AuthorName = book.AuthorName,
                AuthorAddress = book.AuthorAddress,
                AuthorBiografi = book.AuthorBiografi,
                AuthorBirthDate = book.AuthorBirthDate.ToString(),
                PublisherName = book.PublisherName,
                PublisherAddress = book.PublisherAddress,
                PublishDate = book.PublishDate.ToString()
            };
            return Task.FromResult(selectedBook);
        }

        public override Task<Empty> Insert(BookVM requestData, ServerCallContext context)
        {
            db.Books.Add(new EntityFramework.Models.Book()
            {
                BookID = requestData.BookID,
                Title = requestData.Title,
                Price = requestData.Price,
                AuthorName = requestData.AuthorName,
                AuthorAddress = requestData.AuthorAddress,
                AuthorBiografi = requestData.AuthorBiografi,
                AuthorBirthDate = Convert.ToDateTime(requestData.AuthorBirthDate),
                PublisherName = requestData.PublisherName,
                PublisherAddress = requestData.PublisherAddress,
                PublishDate = Convert.ToDateTime(requestData.PublishDate)
            });
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Update(BookVM requestData, ServerCallContext context)
        {
            var book = db.Books.Find(requestData.BookID);

            book.BookID = requestData.BookID;
            book.Title = requestData.Title;
            book.Price = requestData.Price;
            book.AuthorName = requestData.AuthorName;
            book.AuthorAddress = requestData.AuthorAddress;
            book.AuthorBiografi = requestData.AuthorBiografi;
            book.AuthorBirthDate = Convert.ToDateTime(requestData.AuthorBirthDate);
            book.PublisherName = requestData.PublisherName;
            book.PublisherAddress = requestData.PublisherAddress;
            book.PublishDate = Convert.ToDateTime(requestData.PublishDate);
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Delete(BookFilter requestData, ServerCallContext context)
        {
            var data = db.Books.Find(requestData.BookID);
            db.Books.Remove(data);
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }
    }
}
