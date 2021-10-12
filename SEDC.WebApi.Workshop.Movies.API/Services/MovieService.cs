using DataAccess.Interfaces;
using Domain.Enums;
using DtoModels;
using Services.Interfaces;
using Shared.Mappers;
using Shared.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<MovieDto> GetAllMovies()
        {
            return _movieRepository.GetAll().Select(movie => movie.ToMovieDto()).ToList();
        }
        public List<MovieDto> GetByGenre(string genre)
        {
            try
            {
                MovieGenre movieGenre = (MovieGenre)Enum.Parse(typeof(MovieGenre), genre);
                return _movieRepository.GetByGenre(movieGenre).Select(movies => movies.ToMovieDto()).ToList();
            }
            catch (Exception)
            {
                throw new Exception($"Couldn't find movie with genre: {genre}");
            }
        }
        public MovieDto GetById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if(movie == null)
            {
                throw new Exception($"theres no movie with id: {id} in the database");
            }
            return movie.ToMovieDto();
        }
        public string Update(MovieDto movieDto)
        {
            movieDto.ValidateMovieDto();
            _movieRepository.Update(movieDto.ToMovie());
            return "Updated movie successfuly";
        }
        public string Delete(int id)
        {
            _movieRepository.Delete(id);
            return $"Successfuly deleted movie with ID: {id}";
        }
        public string Create(MovieDto movieDto)
        {
            _movieRepository.Create(movieDto.ToMovie());
            return $"Successfuly added new movie ID: {movieDto.Id}";
        }
    }
}
