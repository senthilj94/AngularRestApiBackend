using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAuthentication.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpGet("GetEvents")]
        public IActionResult GetEvents()
        {
            List<Event> events = FetchEventList();

            return Ok(events);
        }

        [Authorize]
        [HttpGet("GetSpecialEvents")]
        public IActionResult GetSpecialEvents()
        {
            List<Event> events = FetchEventList();

            return Ok(events);
        }

        private List<Event> FetchEventList()
        {
            return new List<Event>
            {
                new Event
                {
                    _id = "1",
                    Name = "Auto Expo",
                    Description = "lorem ipsum",
                    Date = DateTime.Now.ToString()
                },
                new Event
                {
                    _id = "2",
                    Name = "Auto Expo",
                    Description = "lorem ipsum",
                    Date = DateTime.Now.ToString()
                },
                new Event
                {
                    _id = "3",
                    Name = "Auto Expo",
                    Description = "lorem ipsum",
                    Date = DateTime.Now.ToString()
                },
                new Event
                {
                    _id = "4",
                    Name = "Auto Expo",
                    Description = "lorem ipsum",
                    Date = DateTime.Now.ToString()
                },
                new Event
                {
                    _id = "5",
                    Name = "Auto Expo",
                    Description = "lorem ipsum",
                    Date = DateTime.Now.ToString()
                }
            };
        }
    }
}