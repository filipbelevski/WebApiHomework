using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class DapperUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public DapperUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(User entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Insert(entity);
            }
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Delete(user);
            }
        }

        public List<User> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"SELECT * FROM Users u 
                            left join MovieUser mu on mu.UsersId = u.Id
                            left join Movies m on mu.MoviesId = m.Id";

                var users = connection.Query<User, Movie, User>(sqlQuery, (user, movie) =>
                {
                    if (movie?.Id != default)
                    {
                        user.Movies.Add(movie);
                    }
                    return user;
                });

                var result = users.GroupBy(x => x.Id).Select(users =>
                 {
                     var groupedUser = users.FirstOrDefault();
                     groupedUser.Movies = users.SelectMany(x => x.Movies).ToList();
                     return groupedUser;
                });

                return result.ToList();
            }

        }

        public User GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("id", id, DbType.Int32, ParameterDirection.Input);

                var sqlQuery = @"SELECT * FROM Users u WHERE u.Id = @id";

                return connection.QuerySingle<User>(sqlQuery, parameters);
            }
        }

        public void Update(User entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Update(entity);
            }
        }

        public void RentMovie(int movieId, int userId)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("MovieId", movieId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("UserId", userId, DbType.Int32, ParameterDirection.Input);
                connection.Execute(@"INSERT INTO MovieUser (MoviesId, UsersId) VALUES(@MovieId, @UserId)", parameters);
            }
        }
    }
}
