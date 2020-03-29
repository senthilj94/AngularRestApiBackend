using AngularAuthentication.Models.RequestModels;
using AngularAuthentication.Models.ServiceModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularAuthentication.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient("mongodb+srv://senthilj94:senthil123@test-cluster-1-9ylqb.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("Authentication");
            _users = database.GetCollection<User>("Users");
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public User Get(string email)
        {
           return _users.Find<User>(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public string GetToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key for token"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
