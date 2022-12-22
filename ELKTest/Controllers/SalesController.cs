﻿using ELKTest.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ELKTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
       
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<SalesController> _logger;
       
        public SalesController(IElasticClient elasticClient, ILogger<SalesController> logger)
        {
            _elasticClient = elasticClient;
             _logger = logger;
           
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearch(string streetAddress,int sizeofQuery,string marketValue)
        {
            
            
            var results = await _elasticClient.SearchAsync<Sales>(
                    s =>
                    s.Size(sizeofQuery)
                    .Index("sales")
                    .Query(
                        q => q.Match(m => m
                         .Field(f => f.StreetAddress)
                         .Query("*"+streetAddress+"*"))

                        
                         || q.Match (m => m
                         .Field(f => f.Market)
                         .Query("*"+marketValue+"*"))
                )); 

            return Ok(results.Documents.ToList());

        }


        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {


            var results = await _elasticClient.SearchAsync<Sales>(s => s
            .Index("sales")
            .MatchAll());

            return Ok(results.Documents.ToList());

        }

        [HttpPost]
        public async Task<IActionResult> SalesPost(Sales sales)
        {
            await _elasticClient.IndexDocumentAsync(sales);
            return Ok();
        }

        [HttpDelete]
        public  IActionResult Delete(int propertyId)
        {
            _elasticClient.DeleteByQuery<Sales>(d => d
            .Index("sales")
            .Query(q => q
                .Term (t => t
                    .Field( f => f.PropertyId)
                    .Value(propertyId)
                     )
                 )
            );

            return Ok();
            
        }
    }
}
