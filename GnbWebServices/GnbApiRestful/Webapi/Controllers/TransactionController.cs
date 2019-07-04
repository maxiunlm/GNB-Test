using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Webapi.Model;
using Webapi.Filters;
using Microsoft.AspNetCore.Cors;
using AutoMapper;

namespace Webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService service;
        private readonly IMapper mapper;

        public TransactionController(ITransactionService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/Transactions
        [HttpGet]
        [EnableCors("MyPolicy")]
        [ServiceFilter(typeof(WebExceptionFilter))]
        [ServiceFilter(typeof(WebLoggerFilter))]
        public async Task<ActionResult<List<Transaction>>> Get()
        {
            List<Service.Model.Transaction> oroginData = await service.ListTransactions();
            List<Transaction> transactions = mapper.Map<List<Transaction>>(oroginData);

            return transactions;
        }
    }
}
