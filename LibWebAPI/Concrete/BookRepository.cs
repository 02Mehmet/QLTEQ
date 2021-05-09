using System.Collections.Generic;
using LibWebAPI.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibWebAPI;
using System.Linq;
using EntityFramework.Models;
using System.Net.Http;
using Grpc.Net.Client;
using static QLTEQ.GRPC.Protos.Book;
using QLTEQ.GRPC.Protos;

namespace LibWebAPI.Concrete
{
    public class BookRepository : IBookRepository
    {
        private readonly HttpClientHandler httpHandler = new HttpClientHandler();
        private BookClient bookClient;
        public BookRepository()
        {

            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = httpHandler });

            bookClient = new BookClient(channel);
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        }

        public void CreateBook(EntityFramework.Models.Book book)
        {
            var createdBook = bookClient.Insert(new BookVM()
            {
                BookID = book.BookID,
                Title = book.Title,
                Price = book.Price,
                AuthorName =book.AuthorName,
                AuthorAddress = book.AuthorAddress,
                AuthorBiografi = book.AuthorBiografi,
                AuthorBirthDate = book.AuthorBirthDate.ToString(),
                PublisherName = book.PublisherName,
                PublisherAddress = book.PublisherAddress,
                PublishDate = book.PublishDate.ToString()
            });
        }
        public void DeleteBookById(int id)
        {
            BookFilter request = new BookFilter { BookID = id };
            var deletedBook = bookClient.Delete(request);
        }
        public Books GetAllBooks()
        {
            var getAll = bookClient.SelectAll(new Empty());
            List<BookVM> books = new List<BookVM>();
            foreach (var book in getAll.Items)
            {
                books.Add(book);
            }

            return getAll;
        }
        public BookVM GetBookById(int id)
        {
            BookFilter request = new BookFilter { BookID = id };
            var book =  bookClient.SelectByID(request);
            return book;
        }

        public void Update(EntityFramework.Models.Book book)
        {
            var createdBook = bookClient.Insert(new BookVM()
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
            });
        }
    }
}