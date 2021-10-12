using Domain.Models;
using Domain.Enums;
using DtoModels;
using System;

namespace Shared.Mappers
{
    public static class MovieMapper
    {
        public static Movie ToMovie(this MovieDto movieDto)
        {
            return new Movie
            {
                Id = movieDto.Id,
                Title = movieDto.Title,
                Description = movieDto.Description,
                Genre = (MovieGenre)Enum.Parse(typeof(MovieGenre), movieDto.Genre),
                Year = movieDto.Year
            };
        }

        public static MovieDto ToMovieDto(this Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre.ToString(),
                Year = movie.Year
                
            };
        }

    }
}
