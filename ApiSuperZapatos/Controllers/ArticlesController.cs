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
        private readonly ILogger<ArticlesController> logger;

        public ArticlesController(ILogger<ArticlesController> logger)
        {
            this.logger = logger;
        }


        // GET: api/<ArticlesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Aplicacion.Services.ArticlesService servicio = new Aplicacion.Services.ArticlesService();
                IList<Aplicacion.Adaptadores.Article> articles = servicio.GetArticlesNoTracking().ToList();
                return Ok(new DTO.ResponseArticles() { Success = true, Articles = articles });
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
                Aplicacion.Services.ArticlesService servicio = new Aplicacion.Services.ArticlesService();
                Aplicacion.Adaptadores.Article article = servicio.GetArticleNoTracking(identificador);
                if (article == null)
                {
                    return NotFound(new DTO.ResponseError() { ErrorCode = 404, ErrorMessage = "Record not Found", Success = false });
                }
                return Ok(new DTO.ResponseArticle() { Success = true, Article = article });
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrió un error interno. Id: " + id.ToString(), ex);
                return StatusCode(500, new DTO.ResponseError() { ErrorCode = 500, ErrorMessage = "Server Error", Success = false });
            }
        }

        [HttpGet("Store/{id}")]
        public IActionResult GetByStore(string id)
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

        // POST api/<ArticlesController>
        [HttpPost]
        public void Post([FromBody] Aplicacion.Adaptadores.Article value)
        {
            Aplicacion.Services.ArticlesService servicio = new Aplicacion.Services.ArticlesService();
            servicio.AddArticle(value);
            servicio.SaveChanges();
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] Aplicacion.Adaptadores.Article value)
        {
            Aplicacion.Services.ArticlesService servicio = new Aplicacion.Services.ArticlesService();
            Aplicacion.Adaptadores.Article article = servicio.GetArticleNoTracking(id);
            if (article != null)
            {
                value.Id = id;
                servicio.UpdateArticle(value);
                servicio.SaveChanges();
            }            
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            Aplicacion.Services.ArticlesService servicio = new Aplicacion.Services.ArticlesService();
            servicio.DeleteArticle(id);
            servicio.SaveChanges();
        }
    }
}
