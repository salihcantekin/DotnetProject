using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly INumGenerator2 numGenerator2;
        private readonly INumGenerator numGenerator;

        public WeatherForecastController(INumGenerator NumGenerator)
        {
            numGenerator = NumGenerator;
        }


        [HttpGet]
        public String Get()
        {
            int random1 = numGenerator.RandomValue;

            return $"NumGenerator.RandomValue: {random1}";
        }
    }
}
