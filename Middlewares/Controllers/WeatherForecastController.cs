using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middlewares.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public String Get()
        {
            //throw new Exception("Test hatası");

            return "OK";
        }

        [HttpGet("Student")]
        public Student GetStudent()
        {
            return new Student()
            { 
                Id = 1,
                FullName = "Salih Cantekin"
            };
        }

        [HttpPost("CreateStudent")]
        public String CreateStudent(Student Student)
        {


            return "Öğrenci Oluşturuldu";
        }

        
    }

    public class Student
    {
        public int Id { get; set; }
        public String FullName { get; set; }
    }
}
