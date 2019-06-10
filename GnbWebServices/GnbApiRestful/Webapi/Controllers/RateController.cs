using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class RateController : ControllerBase
    {
        private readonly IRateService service;
        private readonly IMapper mapper;

        public RateController(IRateService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/Rates
        [HttpGet]
        [EnableCors("MyPolicy")]
        [ServiceFilter(typeof(WebExceptionFilter))]
        [ServiceFilter(typeof(WebLoggerFilter))]
        public async Task<ActionResult<List<CurrencyConvertion>>> Get()
        {
            List<Service.Model.CurrencyConvertion> oroginData = await service.ListRates();
            List<CurrencyConvertion> rates = mapper.Map<List<CurrencyConvertion>>(oroginData);

            return rates;
        }
    }
}
