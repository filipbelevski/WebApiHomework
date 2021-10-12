using DtoModels;
using System.Collections.Generic;


namespace Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies();
        List<MovieDto> GetByGenre(string genre);
        public string Delete(int id);
        public string Create(MovieDto movieDto);
        public MovieDto GetById(int id);
        public string Update(MovieDto movie);

    }
}
