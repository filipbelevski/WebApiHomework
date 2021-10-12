using Domain.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        public List<Movie> GetByGenre(MovieGenre genre);

    }
}
