using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Business;
using Service.Model;
using Service.Configurations;
using AutoMapper;
using MongoDB.Bson;

namespace Service
{
    public class UserService: IUserService
    {
        private readonly AppSettings appSettings;
        private readonly IMapper mapper;

        public UserService(IOptions<AppSettings> appSettings, IMapper mapper)
        {
            this.appSettings = appSettings.Value;
            this.mapper = mapper;
        }

        public User Authenticate(string username, string password)
        {
            // users dummy for simplicity, everyone is allowed
            User user = new User
            {
                Id = ObjectId.GenerateNewId(),
                Username = username                
            };

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }
    }
}