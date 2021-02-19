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
    public class ArticlesController : ControllerBase
    {
        private readonly ILogger<StoresController> logger;
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

        // GET: api/<ArticlesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                DTO.Article article = new DTO.Article() { Id = 0, Name = "Articulo 1", Description = "Articulo 1 Descripción" };
                IList<DTO.Article> articlesList = new List<DTO.Article>();
                articlesList.Add(article);
                articlesList.Add(new DTO.Article() { Id = 1, Name = "Articulo 2 ", Description = "Articulo 2 Descripción" });
                return Ok(new DTO.ResponseArticles() { Success = true, Articles = articlesList });
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error interno al obtener los Articulos.", ex);
                return StatusCode(500, new DTO.ResponseError() { ErrorCode = 500, ErrorMessage = "Server Error", Success = false });
            }
        }

        // GET api/<ArticlesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                long identificador = 0;
                if (!long.TryParse(id, out identificador))
                {
                    return BadRequest(new DTO.ResponseError() { ErrorCode = 400, ErrorMessage = "Bad request", Success = false });
                }
                DTO.Article article = new DTO.Article() { Id = 0, Name = "Articulo 1", Description = "Articulo 1 Descripción" };
                return Ok(new DTO.ResponseArticle() { Success = true, Article = article });
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error interno. Id: " + id.ToString(), ex);
                return StatusCode(500, new DTO.ResponseError() { ErrorCode = 500, ErrorMessage = "Server Error", Success = false });
            }
        }

        // POST api/<ArticlesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
