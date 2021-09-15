
using Microsoft.AspNetCore.Mvc;
using SEDC.Homework.Class02.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SEDC.Homework.Class02.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>()
        {
            new User
            {
                Id = 1,
                Age = 15,
                FirstName = "Bob",
                LastName = "Bobski"
            },
            new User
            {
                Id = 2,
                Age = 21,
                FirstName = "John",
                LastName = "Doe"
            },
            new User
            {
                Id = 3,
                Age = 31,
                FirstName = "John",
                LastName = "Johnson"
            }
        };

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return users;
        }

        [HttpGet("index/{index:int}")]
        public ActionResult<User> GetUserByIndex(int index)
        {
            
            try
            {
               return users[index];
            }
            catch(Exception)
            {
                return NotFound(new { Message = $"User with index {index} was not found" });
            }
        }

        [HttpGet("id/{id}")]
        public ActionResult<string> CheckIfUserIsAdultById(int id)
        {

            var user = users.FirstOrDefault(x => x.Id == id);

            if(user != null)
            {
                var msg = IsAdult(user) ? $"{user.FirstName} is an adult": $"{user.FirstName} is not an adult";

                return Ok(new { Message = msg });
            }

            return NotFound(new { Message = $"No user found with ID: {id}" });
            
        }

        [HttpPost("create-user")]
        public int CreateUser([FromBody] User user)
        {
            int userId = users.Count + 1;
            user.Id = userId;
            users.Add(user);
            return userId;
        }

        private static bool IsAdult (User user)
        {
            return user.Age >= 18;
        }

    }
}
