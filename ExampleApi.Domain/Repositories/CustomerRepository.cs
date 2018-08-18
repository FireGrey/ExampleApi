using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExampleApi.Context;
using ExampleApi.Context.Model;
using ExampleApi.Domain.Model;

namespace ExampleApi.Domain.Repositories
{
    public class CustomerRepository : IRepository<CustomerDto>
    {
        private readonly ExampleContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ExampleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<CustomerDto> Query()
        {
            return _context.Customers.ProjectTo<CustomerDto>(_mapper.ConfigurationProvider);
        }

        public CustomerDto Get(long id)
        {
            var customer = _context.Customers.Find(id);

            return _mapper.Map<CustomerDto>(customer);
        }

        public CustomerDto Add(CustomerDto item)
        {
            Customer customer = _mapper.Map<Customer>(item);

            _context.Add(customer);

            _context.SaveChanges();

            return _mapper.Map<CustomerDto>(customer);
        }

        public CustomerDto Replace(long id, CustomerDto item)
        {
            Customer customer = _context.Customers.Find(id);

            if (customer == null)
                return null;

            customer = _mapper.Map(item, customer);

            _context.SaveChanges();

            return _mapper.Map<CustomerDto>(customer);
        }

        public bool Delete(long id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return false;

            _context.Customers.Remove(customer);

            _context.SaveChanges();

            return true;
        }
    }
}
