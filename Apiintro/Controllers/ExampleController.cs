﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apiintro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<string> students = new List<string>() { "Tunzale", "Semed", "Metanet", "Meryem" };
            return Ok(students);
        }

        [HttpGet]
        public IActionResult Create([FromBody] User user)
        {
            return Ok(user.Surname + "-" + user.Name);
        }

        //[HttpGet]
        //public IActionResult Search([FromQuery] string searchText, [FromQuery] int id, [FromQuery] string userId)
        //{
        //    return Ok(searchText + "-" + id + "-" + userId);
        
        //}


        //[HttpGet("{id}")]
        //public IActionResult GetById([FromRoute] int id) 
        //{ 
        //    return Ok(id + "-this is Id");
        //}

        //[HttpPost]
        //public IActionResult Test1()
        //{
        //    return Ok();
        //}

        //[HttpDelete]
        //public IActionResult Test2()
        //{
        //    return Ok();
        //}
    }
}
