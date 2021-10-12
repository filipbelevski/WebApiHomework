using Dapper.Contrib.Extensions;
using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public MovieGenre Genre { get; set; }

        [Computed]
        public virtual List<User> Users { get; set; } = new List<User>();
    }
}