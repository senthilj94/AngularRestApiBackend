using AngularAuthentication.Models.RequestModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAuthentication.Models.ServiceModels
{
    public interface IUserService
    {
        User Create(User user);

        User Get(string email);

        string GetToken(User user);
    }
}
