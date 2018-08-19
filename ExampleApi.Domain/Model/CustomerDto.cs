using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleApi.Domain.Model
{
    public class CustomerDto
    {
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}
