using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using Webapi.Model;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ITransactionService service;
        private readonly IMapper mapper;

        public TransactionController(ITransactionService service, ILogger<TransactionController> logger)
        {
            this.service = service;
            this.logger = logger;

            MapperConfiguration automappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Service.Model.Transaction, Transaction>();
                config.CreateMap<Transaction, Service.Model.Transaction>();
            });
            this.mapper = automappingConfiguration.CreateMapper();
        }

        // GET api/Transactions
        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> Get()
        {
            try
            {
                List<Service.Model.Transaction> oroginData = await service.ListTransactions();
                List<Transaction> transactions = mapper.Map<List<Transaction>>(oroginData);

                return transactions;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Getting all Transactions", null);
                throw;
            }
        }
    }
}
