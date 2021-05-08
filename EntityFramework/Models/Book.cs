using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityFramework.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public string AuthorName { get; set; }

        public string AuthorBiografi { get; set; }

        public string AuthorAddress { get; set; }

        public DateTime AuthorBirthDate { get; set; }

        public string PublisherName { get; set; }

        public string PublisherAddress { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
