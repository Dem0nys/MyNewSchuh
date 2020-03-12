using MVSschuh.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVSschuh.Data.Model
{
    public class UserProfile
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }
        public string Number { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual User User { get; set; }
    }
}
