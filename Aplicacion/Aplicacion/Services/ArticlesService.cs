using AutoMapper;
using Elipgo.SuperZapatos.Aplicacion.Adaptadores;
using Elipgo.SuperZapatos.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elipgo.SuperZapatos.Aplicacion.Services
{
    /// <summary>
    /// Clase de Servicio de aplicación para los Articles
    /// </summary>
    public class ArticlesService
    {
        private InfraestructuraDatos.Data.SuperZapatosDBContext szDBContext = new InfraestructuraDatos.Data.SuperZapatosDBContext();

        /// <summary>
        /// Obtiene una colección completa de articulos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Article> GetArticles()
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            List<Dominio.Entities.Article> articles = articlesRepository.Get().ToList();                
            List<Adaptadores.Article> articlesDTO = ObjectMapper.Mapper.MapList<Dominio.Entities.Article, Adaptadores.Article>(articles);
            return articlesDTO;
        }

        /// <summary>
        /// Obtiene una colección completa de articulos sin realizar tracking en el contexto
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Article> GetArticlesNoTracking()
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            List<Dominio.Entities.Article> articles = articlesRepository.Get(null, null, String.Empty, false).ToList();
            List<Adaptadores.Article> articlesDTO = ObjectMapper.Mapper.MapList<Dominio.Entities.Article, Adaptadores.Article>(articles);
            return articlesDTO;
        }
        /// <summary>
        /// Obtiene los articulos de un store
        /// </summary>
        /// <param name="idStore">Id de Store</param>
        /// <returns></returns>
        public IEnumerable<Article> GetArticlesStore(long idStore)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            List<Dominio.Entities.Article> articles = articlesRepository.Get(art => art.StoreId == idStore, null, string.Empty, false).ToList();
            List<Adaptadores.Article> articlesDTO = ObjectMapper.Mapper.MapList<Dominio.Entities.Article, Article>(articles);
            return articlesDTO;
        }
        /// <summary>
        /// Obtiene un articulo
        /// </summary>
        /// <param name="id">Id del articulo</param>
        /// <returns></returns>
        public Adaptadores.Article GetArticle(long id)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            Dominio.Entities.Article article = articlesRepository.GetById(id);
            Adaptadores.Article articleDTO = ObjectMapper.Mapper.Map<Dominio.Entities.Article, Article>(article);
            return articleDTO;
        }
        /// <summary>
        /// Obtiene un articulo sin realizar tracking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Adaptadores.Article GetArticleNoTracking(long id)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            Dominio.Entities.Article article = articlesRepository.GetById(id, false);
            Adaptadores.Article articleDTO = ObjectMapper.Mapper.Map<Dominio.Entities.Article, Article>(article);
            return articleDTO;
        }
        /// <summary>        
        /// Realiza una adición de un articulo en el repositorio
        /// </summary>
        /// <param name="article"></param>
        public void AddArticle(Article article)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            Dominio.Entities.Article articleEntity = ObjectMapper.Mapper.Map<Article, Dominio.Entities.Article>(article);
            articlesRepository.Insert(articleEntity);            
        }
        /// <summary>
        /// Realiza una actualización en el repositorio
        /// </summary>
        /// <param name="article">Articulo</param>
        public void UpdateArticle(Article article)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            Dominio.Entities.Article articleEntity = ObjectMapper.Mapper.Map<Article, Dominio.Entities.Article>(article);
            articlesRepository.Update(articleEntity);
        }
        /// <summary>
        /// Elimina un articulo en el repositorio
        /// </summary>
        /// <param name="id">Id del articulo</param>
        public void DeleteArticle(long id)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            articlesRepository.Delete(id);
        }
        /// <summary>
        /// Guarda todos los cambios en el contexto
        /// </summary>
        public void SaveChanges()
        {            
            szDBContext.SaveChanges();
        }

    }
}
