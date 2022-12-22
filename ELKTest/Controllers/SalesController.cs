using ELKTest.Models;
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

        [HttpGet("searchAllParameters")]
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

        [HttpGet("searchByStreetAddress")]
        public async Task<IActionResult> GetSearchStreetAddress(string streetAddress)
        {
            // Kelime kendini tamamlar
            var results = await _elasticClient.SearchAsync<Sales>(
                    s =>
                    s.Index("sales")
                    .Query(
                        q => q
                        .Fuzzy(fz => fz.Field(f => f.StreetAddress).Value(streetAddress).Transpositions(true))                                 
                ));

            return Ok(results.Documents.ToList());

        }

        [HttpGet("searchMarket")]
        public async Task<IActionResult> GetSearchByMarket( string marketValue)
        {


            var results = await _elasticClient.SearchAsync<Sales>(
                    s =>
                    s
                    .Index("sales")
                    .Query(
                        q => q.Match(m => m
                         .Field(f => f.Market)
                         .Query("*" + marketValue + "*"))


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

        //[HttpPost]
        //public async Task<IActionResult> SalesUpdatte(Sales sales,int propertyId)
        //{

        //    _elasticClient.Update(sales);
            
        //    return Ok();
        //}

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
