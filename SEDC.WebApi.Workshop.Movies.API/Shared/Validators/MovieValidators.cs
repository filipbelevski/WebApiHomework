using DtoModels;
using System;

namespace Shared.Validators
{
    public static class MovieValidators
    {
        public static void ValidateMovieDto(this MovieDto movieDto)
        {
            ValidateMovieTitle(movieDto.Title);
        }
        private static void ValidateMovieTitle(string title)
        {
            if (title.Length < 2)
            {
                throw new Exception("Title must be longer than 2 chars");
            }
        }

    }
}
