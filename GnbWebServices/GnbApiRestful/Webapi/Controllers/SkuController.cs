﻿using System;
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
    public class SkuController : ControllerBase
    {
        private readonly ISkuService service;
        private readonly IMapper mapper;

        public SkuController(ISkuService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/Sku
        [HttpGet]
        [EnableCors("MyPolicy")]
        [ServiceFilter(typeof(WebExceptionFilter))]
        [ServiceFilter(typeof(WebLoggerFilter))]
        public ActionResult<List<string>> Get()
        {
            List<string> skus = service.ListSkus();

            return skus;
        }

        // GET api/Sku/summary/C8514
        [HttpGet("summary/{sku}")]
        [EnableCors("MyPolicy")]
        [ServiceFilter(typeof(WebExceptionFilter))]
        [ServiceFilter(typeof(WebLoggerFilter))]
        public async Task<ActionResult<Sku>> Get(string sku)
        {
            Service.Model.Sku oroginData = await service.GetTransactionsBySku(sku);
            Sku skuModel = mapper.Map<Sku>(oroginData);

            return skuModel;
        }

        // POST api/Sku
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Sku/5
        [HttpPut("{sku}")]
        public void Put(string sku, [FromBody] string value)
        {
        }

        // DELETE api/Sku/5
        [HttpDelete("{sku}")]
        public void Delete(string sku)
        {
        }
    }
}
