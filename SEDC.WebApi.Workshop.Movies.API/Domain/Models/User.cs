using Dapper.Contrib.Extensions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Subscription Subscription { get; set; }
        public Roles Role { get; set; }

        [Computed]
        public virtual List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
