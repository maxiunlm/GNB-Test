using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using Webapi.Model;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using AutoMapper;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IRateService service;
        private readonly IMapper mapper;

        public RateController(IRateService service, ILogger<RateController> logger)
        {
            this.service = service;
            this.logger = logger;

            MapperConfiguration automappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Service.Model.CurrencyConvertion, CurrencyConvertion>();
                config.CreateMap<CurrencyConvertion, Service.Model.CurrencyConvertion>();
            });
            this.mapper = automappingConfiguration.CreateMapper();
        }

        // GET api/Rates
        [HttpGet]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<List<CurrencyConvertion>>> Get()
        {
            try
            {
                List<Service.Model.CurrencyConvertion> oroginData = await service.ListRates();
                List<CurrencyConvertion> rates = mapper.Map<List<CurrencyConvertion>>(oroginData);

                return rates;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Getting all Rates", null);
                throw;
            }
        }
    }
}
