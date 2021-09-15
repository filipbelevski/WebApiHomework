using Microsoft.AspNetCore.Mvc;
using SEDC.Homework.Class02.ClientApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.Homework.Class02.ClientApp.Services
{
    public interface IApiService
    {
        public ActionResult<bool> CreateUser(User user);
        public Task<List<User>> GetAll();
    }
}
