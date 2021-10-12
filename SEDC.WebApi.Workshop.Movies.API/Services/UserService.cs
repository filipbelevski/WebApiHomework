
using DataAccess.Interfaces;
using Domain.Enums;
using DtoModels;
using Shared.Validators;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Helpers;
using Services.Interfaces;
using Shared.Mappers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Shared;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IOptions<AppSettings> _options;

        public UserService(IUserRepository repo, IOptions<AppSettings> options)
        {
            _repo = repo;
            _options = options;
        }

        public string Authenticate(LoginDto loginDto)
        {
            loginDto.Password = HashStrings.HashedString(loginDto.Password);
            var user = _repo.GetAll().SingleOrDefault(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);
            if(user == null)
            {
                throw new Exception("Invalid username or password");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                   new[]
                   {
                       new Claim(ClaimTypes.Name, $"{user.FullName}"),
                       new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                       user.Role == Roles.Admin? new Claim("admin_claim", "true") : null
                   }),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public void Create(RegisterUserDto entity)
        {
            var userExists = _repo.GetAll().Exists(x => x.UserName == entity.UserName);
            if (userExists)
            {
                throw new Exception("username is already taken");
            }
            if (string.IsNullOrEmpty(entity.UserName))
            {
                throw new Exception("username can't be null or empty");
            }
            if (string.IsNullOrEmpty(entity.Password))
            {
                throw new Exception("password can't be empty");
            }
            UserValidators.PasswordValidator(entity.Password);
            entity.Password = HashStrings.HashedString(entity.Password);

            var user = new UserDto
            {
                UserName = entity.UserName,
                Password = entity.Password,
                FullName = $"{entity.FirstName} {entity.LastName}",
                Subscription = Subscription.Default
            };
            _repo.Create(user.ToUser());
        }

        public void Delete(int id)
        {
            if(_repo.GetById(id) == null)
            {
                throw new Exception("user doesn't exist");
            }
            _repo.Delete(id);
        }

        public List<UserDto> GetAll()
        {
            var users = _repo.GetAll();
            if(users == null)
            {
                throw new Exception("there are currently no users in the database");
            }
            return users.Select(x => x.ToUserDto()).ToList(); ;
        }

        public UserDto GetById(int id)
        {
            var user = _repo.GetById(id);
            if(user == null)
            {
                throw new Exception($"There's no user with id {id}");
            }
            return user.ToUserDto();
        }

        public void Update(UserDto entity)
        {
            var user = _repo.GetById(entity.Id);
            if(user == null)
            {
                throw new Exception($"User with id: {entity.Id} doesn't exist");
            }
            if (string.IsNullOrEmpty(entity.UserName))
            {
                throw new Exception("username can't be null or empty");
            }
            if(entity.Password != null)
            {
                UserValidators.PasswordValidator(entity.Password);
            }
            user.FullName = entity.FullName == null ? user.FullName : entity.FullName;
            user.Password = entity.Password == null ? user.Password : HashStrings.HashedString(entity.Password);
            user.Subscription = entity.Subscription == default ? user.Subscription : entity.Subscription;
            user.UserName = entity.UserName;

            _repo.Update(user);
        }
        public void RentMovie(RentAMovieDto dto)
        {
            _repo.RentMovie(dto.MovieId, dto.UserId);

        }
    }
}
