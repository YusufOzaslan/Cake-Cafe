using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cake_Cafe.Models
{
    public class Person : IdentityUser
    {
        public string Ad { get; set; }

        public string Soyad { get; set; }

        public Role Role { get; set; }
    }
    public enum Role
    {
        admin,
        customer
    }
}
