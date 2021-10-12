using DataAccess.Interfaces;
using Domain.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesDbContext _db;

        public MovieRepository(MoviesDbContext db)
        {
            _db = db;
        }
        public void Create(Movie entity)
        {
            _db.Movies.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _db.Movies.SingleOrDefault(x => x.Id == id);
            _db.Movies.Remove(entity);
            _db.SaveChanges();
        }

        public List<Movie> GetAll()
        {
            return _db.Movies.ToList();

        }

        public List<Movie> GetByGenre(MovieGenre genre)
        {
            return _db.Movies.Where(x => x.Genre == genre).ToList();

        }

        public Movie GetById(int id)
        {
            return _db.Movies.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Movie entity)
        {
            var movie = _db.Movies.FirstOrDefault(x => x.Id == entity.Id);

            if(movie != null)
            {
                movie.Title = entity.Title;
                movie.Genre = entity.Genre;
                movie.Description = entity.Description;
                movie.Year = entity.Year;

                _db.SaveChanges();
            }
        }

    }
}
