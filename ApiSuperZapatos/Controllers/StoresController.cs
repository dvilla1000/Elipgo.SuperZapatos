using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elipgo.SuperZapatos.ApiSuperZapatos.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly ILogger<StoresController> logger;
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

        // GET: api/<StoresController>
        [HttpGet]
        public IActionResult Get() //IEnumerable<DTO.ResponseSuccess>
        {
            try
            {
                DTO.Store store = new DTO.Store() { Id = 0, Name = "Elipo Matriz", Address = "Calle xyz" };
                IList<DTO.Store> storesList = new List<DTO.Store>();
                storesList.Add(store);
                storesList.Add(new DTO.Store() { Id = 1, Name = "Elipo Suc1", Address = "Calle xyz" });
                return Ok(new DTO.ResponseStores() { Success = true, Stores = storesList });
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error interno al obtener los Stores.", ex);
                return StatusCode(500, new DTO.ResponseError() { error_code = 500, error_msg = "Server Error", Success = false });
            }
        }

        // GET api/<StoresController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                long identificador = 0;
                if (!long.TryParse(id,out identificador))
                {                    
                    return BadRequest(new DTO.ResponseError() { error_code = 400, error_msg = "Bad request", Success = false });
                }
                DTO.Store store = new DTO.Store() { Id = 0, Name = "Elipo Matriz", Address = "Calle xyz" };
                return Ok(new DTO.ResponseStore() { Success = true, Store = store });
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error interno. Id: " + id.ToString(), ex);
                return StatusCode(500, new DTO.ResponseError() { error_code = 500, error_msg = "Server Error", Success = false });
            }
        }

        // POST api/<StoresController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StoresController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
