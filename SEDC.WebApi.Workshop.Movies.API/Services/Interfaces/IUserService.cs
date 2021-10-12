using DtoModels;
using System.Collections.Generic;


namespace Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAll();
        UserDto GetById(int id);
        void Delete(int id);
        void Create(RegisterUserDto registerModel);
        void Update(UserDto entity);
        void RentMovie(RentAMovieDto dto);
        string Authenticate(LoginDto loginDto);
    }
}
