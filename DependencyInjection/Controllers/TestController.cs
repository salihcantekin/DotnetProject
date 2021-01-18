﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly INumGenerator numGenerator;

        public TestController(INumGenerator NumGenerator)
        {
            numGenerator = NumGenerator;
        }

        [HttpGet]
        public String Get()
        {
            return numGenerator.RandomValue.ToString();
        }
    }
}
