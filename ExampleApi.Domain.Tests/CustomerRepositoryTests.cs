using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExampleApi.Context;
using ExampleApi.Context.Model;
using ExampleApi.Domain.Mappings;
using ExampleApi.Domain.Model;
using ExampleApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ExampleApi.Domain.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly ExampleContext _context;
        private readonly IMapper _mapper;
        public CustomerRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase("CustomerRepositoryTests");

            _context = new ExampleContext(optionsBuilder.Options);

            // Seed with data
            Customers.ForEach(x => _context.Customers.Add(x));

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>());

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void Get_Null_IfNoCustomerWithId()
        {
            var repository = new CustomerRepository(_context, _mapper);

            var result = repository.Get(13);

            Assert.Null(result);
        }

        [Fact]
        public void Delete_False_IfNoCustomerWithId()
        {
            var repository = new CustomerRepository(_context, _mapper);

            var result = repository.Delete(13);

            Assert.False(result);
        }

        [Fact]
        public void Replace_Null_IfNoCustomerWithId()
        {
            var repository = new CustomerRepository(_context, _mapper);

            var result = repository.Replace(13, SimpleCustomerDto);

            Assert.Null(result);
        }

        private static List<Customer> Customers =>
            new List<Customer>
            {
                new Customer
                    {
                        Id = 1,
                        FirstName = "Jerry",
                        LastName = "Seinfield",
                        DateOfBirth = new DateTime(1954, 4, 29)
                    },
                    new Customer
                    {
                        Id = 2,
                        FirstName = "Dade",
                        LastName = "Murphy",
                        DateOfBirth = new DateTime(1972, 11, 15)
                    }
            };

        private static CustomerDto SimpleCustomerDto =>
            new CustomerDto
            {
                Id = 1337,
                FirstName = "Dadeasdf",
                LastName = "Murphy",
                DateOfBirth = new DateTime(1972, 11, 15)
            };
    }
}
