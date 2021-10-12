using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DtoModels
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Subscription Subscription { get; set; }
        public Roles Role { get; set; }

        public List<MovieDto> Movies { get; set; }
    }
}
