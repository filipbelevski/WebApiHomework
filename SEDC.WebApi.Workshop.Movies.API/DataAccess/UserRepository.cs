using DataAccess.Interfaces;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviesDbContext _context;

        public UserRepository(MoviesDbContext context)
        {
            _context = context;
        }
        public void Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
                
            return user;
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity); //trying shorthand
            _context.SaveChanges();

        }
        public List<Movie> GetAllUserMovies(int userId)
        {
            throw new NotImplementedException();
        }

        public void RentMovie(int movieId, int userId)
        {
            throw new NotImplementedException();

        }
    }
}
