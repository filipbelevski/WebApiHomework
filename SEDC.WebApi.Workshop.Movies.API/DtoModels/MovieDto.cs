using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DtoModels
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
    }
}
