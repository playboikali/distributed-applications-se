﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text;

namespace MC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello World!";
        }

        [HttpGet("{name}")]
        public string Get([FromRoute] string name)
        {
            return "Hello " + name;
        }

        [HttpGet("{name}/hello")]
        public string Get([FromRoute] string name, [FromQuery] int? counter)
        {
            StringBuilder sb = new();
            for (int i = 0; i < counter; i++)
            {
                sb.AppendLine($"Hello {name}!");
            }

            return sb.ToString();
        }
    }
}
