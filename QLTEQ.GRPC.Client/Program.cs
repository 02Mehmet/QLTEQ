using Grpc.Net.Client;
using System;
using System.Net.Http;
//using static QLTEQ.GRPC.Protos.Book;
using QLTEQ.GRPC.Protos;
using System.Threading.Tasks;
using static QLTEQ.GRPC.Protos.Book;

namespace QLTEQ.GRPC.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Güvenilir olmayan/geçersiz sertifikayla gRPC hizmetini çağırma
            HttpClientHandler httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            //Güvenilir olmayan/geçersiz sertifikayla gRPC hizmetini çağırma

            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = httpHandler });

            //BookClient = Proto name + Client
            var client = new BookClient(channel);

            #region Unary
            //SelectAllIDGRPC
            var selectAll = client.SelectAll(new Empty());

            foreach (var item in selectAll.Items)
            {
                Console.WriteLine($"{item.BookID} ,{item.Title},{item.Price},{item.AuthorName}");
            }

            //SelectByIDGRPC
            //BookFilter request = new BookFilter { BookID = 21 };
            //var selectByID =  client.SelectByID(request);

            //InsertGRPC
            //var createdBook = client.Insert(new BookVM()
            //{
            //    BookID = 21,
            //    Title = "",
            //    Price = 65,
            //    AuthorName = "",
            //    AuthorAddress = "",
            //    AuthorBiografi = "",
            //    AuthorBirthDate = "2021-05-30",
            //    PublisherName = "",
            //    PublisherAddress = "",
            //    PublishDate = "2021-05-30"
            //});

            //UpdateGRPC
            //var updatedBook = client.Update(new BookVM()
            //{
            //    BookID = 21,
            //    Title = "test",
            //    Price = 65,
            //    AuthorName = "test",
            //    AuthorAddress = "test",
            //    AuthorBiografi = "test",
            //    AuthorBirthDate = "2021-05-30",
            //    PublisherName = "test",
            //    PublisherAddress = "test",
            //    PublishDate = "2021-05-30"
            //});

            //DeleteGRPC
            //BookFilter request = new BookFilter { BookID = 1 };
            //var deletedBook = client.Delete(request);
            #endregion
        }
    }
}
