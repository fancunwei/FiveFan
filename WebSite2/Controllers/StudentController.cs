using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite2.Models;

namespace WebSite2.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        [HttpGet("GetStudent")]
        public IActionResult GetStudent()
        {
            var student = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                Class = "321",
                Grade = "23",
                Name = "Name001",
                School = "School002"
            };
            return Ok(student);
        }
    }
}
