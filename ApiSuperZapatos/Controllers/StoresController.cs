using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        public IActionResult Get()
        {
            try
            {
                Aplicacion.Services.StoresService servicio = new Aplicacion.Services.StoresService();
                IList<Aplicacion.Adaptadores.Store> stores = servicio.GetStoresNoTracking().ToList();
                return Ok(new DTO.ResponseStores() { Success = true, Stores = stores }) ;
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error interno al obtener los Stores.", ex);
                return StatusCode(500, new DTO.ResponseError() { ErrorCode = 500, ErrorMessage = "Server Error", Success = false });
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
                    return BadRequest(new DTO.ResponseError() { ErrorCode = 400, ErrorMessage = "Bad request", Success = false });
                }
                Aplicacion.Services.StoresService servicio = new Aplicacion.Services.StoresService();
                Aplicacion.Adaptadores.Store store = servicio.GetStoreNoTracking(identificador);
                if (store == null)
                {
                    return NotFound(new DTO.ResponseError() { ErrorCode = 404, ErrorMessage = "Record not Found", Success = false });
                }
                return Ok(new DTO.ResponseStore() { Success = true, Store = store });
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error interno. Id: " + id.ToString(), ex);
                return StatusCode(500, new DTO.ResponseError() { ErrorCode = 500, ErrorMessage = "Server Error", Success = false });
            }
        }

        [HttpGet("{id}/Articles")]
        public IActionResult GetArticlesByStore(string id)
        {
            try
            {
                long identificador = 0;
                if (!long.TryParse(id, out identificador))
                {
                    return BadRequest(new DTO.ResponseError() { ErrorCode = 400, ErrorMessage = "Bad request", Success = false });
                }
                Aplicacion.Services.ArticlesService servicio = new Aplicacion.Services.ArticlesService();
                IList<Aplicacion.Adaptadores.Article> articles = servicio.GetArticlesStore(identificador).ToList();
                return Ok(new DTO.ResponseArticles() { Success = true, Articles = articles });
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error interno. Id: " + id.ToString(), ex);
                return StatusCode(500, new DTO.ResponseError() { ErrorCode = 500, ErrorMessage = "Server Error", Success = false });
            }
        }

        // POST api/<StoresController>
        [HttpPost]
        public void Post([FromBody] Aplicacion.Adaptadores.Store value)
        {
            Aplicacion.Services.StoresService servicio = new Aplicacion.Services.StoresService();
            servicio.AddStore(value);
            servicio.SaveChanges();
        }

        // PUT api/<StoresController>/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] Aplicacion.Adaptadores.Store value)
        {
            Aplicacion.Services.StoresService servicio = new Aplicacion.Services.StoresService();
            Aplicacion.Adaptadores.Store store = servicio.GetStoreNoTracking(id);
            if (store != null)
            {
                value.Id = id;
                servicio.UpdateStore(value);
                servicio.SaveChanges();
            }
        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Aplicacion.Services.StoresService servicio = new Aplicacion.Services.StoresService();
            servicio.DeleteStore(id);
            servicio.SaveChanges();
        }
    }
}
