using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Models;

namespace Infrastructure.Context.WebApplication14.Data
{
    public class WebApplication14Context : DbContext
    {
        public WebApplication14Context (DbContextOptions<WebApplication14Context> options)
            : base(options)
        {
        }

        public WebApplication14Context()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=WebApplication14Context-721f8644-0a9a-466e-8f6b-32331c5ff894;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Professor> Professor { get; set; }
        public DbSet<Post> Post { get; set; }
    }
}
