using System;
using Microsoft.EntityFrameworkCore;
using ExampleApi.Context.Model;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExampleApi.Context
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options): base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
