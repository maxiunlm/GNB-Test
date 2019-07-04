using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Webapi.Model;
using Webapi.Filters;
using Microsoft.AspNetCore.Cors;
using AutoMapper;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {   
        private readonly IUserService service;
        private readonly IMapper mapper;

        public LoginController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [EnableCors("MyPolicy")]
        [ServiceFilter(typeof(WebExceptionFilter))]
        [ServiceFilter(typeof(WebLoggerFilter))]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            Service.Model.User originalUser = service.Authenticate(userParam.Username, userParam.Password);
            
            if (originalUser == null)
            {
                return Unauthorized();
            }
            
            User user = mapper.Map<User>(originalUser);

            return Ok(user);
        }
    }
}