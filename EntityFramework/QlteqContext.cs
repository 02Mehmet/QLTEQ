using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework
{
    public class QlteqContext : DbContext
    {
        public QlteqContext()
        {

        }
        public QlteqContext(DbContextOptions<QlteqContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Server=127.0.0.1; port=5991; user id = postgres; password = 02Mehmet; database=QLTEQDB3; pooling = true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(Book).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QlteqContext).Assembly);

        }
    }
}
