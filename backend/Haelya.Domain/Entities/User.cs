using Haelya.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Entities
{
    public class User
    {

        public long Id { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public Role Role { get; set; }

    }
}
