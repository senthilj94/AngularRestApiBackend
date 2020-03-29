using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAuthentication.Models.RequestModels;
using AngularAuthentication.Models.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            var addedUser = _userService.Create(user);
            var token = _userService.GetToken(addedUser);
            return Ok(new { token });
        }

        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            var fetchedUser = _userService.Get(user.Email);
            if(fetchedUser == null || fetchedUser.Password != user.Password)
            {
                return Unauthorized();
            }
            var token = _userService.GetToken(fetchedUser);
            return Ok(new { token });
        }

        [HttpGet("GetUser/{email}")]
        public IActionResult GetUser(string email)
        {
            var fetchedUser = _userService.Get(email);
            if(fetchedUser == null)
            {
                return Ok("No users found");
            }
            return Ok(fetchedUser);
        }
    }
}