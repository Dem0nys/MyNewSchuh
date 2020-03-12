using Microsoft.AspNetCore.Identity;
using MVSschuh.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVSschuh.Data.Entities
{
    public class User : IdentityUser<string>
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
