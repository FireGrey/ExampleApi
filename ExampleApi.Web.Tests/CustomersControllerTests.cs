using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ExampleApi.Context;
using ExampleApi.Context.Model;
using ExampleApi.Domain.Mappings;
using ExampleApi.Domain.Model;
using ExampleApi.Domain.Repositories;
using ExampleApi.Web.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ExampleApi.Web.Tests
{
    public class CustomersControllerTests
    {
        [Fact]
        public void Get_OnlyCustomersWithFirstName_IfFilterByFirstName()
        {
            var firstName = "Jerry";

            var repository = new Mock<IRepository<CustomerDto>>();
            repository.Setup(x => x.Query()).Returns(Customers);

            var controller = new CustomersController(repository.Object);

            var test = (Microsoft.AspNetCore.Mvc.OkObjectResult)controller.GetCustomers(firstName: firstName).Result;

            Assert.All((List<CustomerDto>)test.Value, x => Assert.Equal(x.FirstName, firstName));
        }

        private static IQueryable<CustomerDto> Customers =>
            new List<CustomerDto>
            {
                new CustomerDto
                    {
                        Id = 1,
                        FirstName = "Jerry",
                        LastName = "Seinfield",
                        DateOfBirth = new DateTime(1954, 4, 29)
                    },
                    new CustomerDto
                    {
                        Id = 2,
                        FirstName = "Dade",
                        LastName = "Murphy",
                        DateOfBirth = new DateTime(1972, 11, 15)
                    },
                    new CustomerDto
                    {
                        Id = 3,
                        FirstName = "Acid",
                        LastName = "Burn",
                        DateOfBirth = new DateTime(1975, 6, 4)
                    }
            }.AsQueryable();

        private static CustomerDto SimpleCustomerDto =>
            new CustomerDto
            {
                Id = 1337,
                FirstName = "Acid",
                LastName = "Burn",
                DateOfBirth = new DateTime(1975, 6, 4)
            };
    }
}
