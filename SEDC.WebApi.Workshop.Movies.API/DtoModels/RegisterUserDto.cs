﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
