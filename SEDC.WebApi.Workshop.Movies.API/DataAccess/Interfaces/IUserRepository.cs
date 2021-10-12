using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void RentMovie(int movieId, int userId);
    }
}
