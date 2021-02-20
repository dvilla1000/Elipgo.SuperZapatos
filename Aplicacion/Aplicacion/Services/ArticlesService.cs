using AutoMapper;
using Elipgo.SuperZapatos.Aplicacion.Adaptadores;
using Elipgo.SuperZapatos.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elipgo.SuperZapatos.Aplicacion.Services
{
    public class ArticlesService
    {
        private InfraestructuraDatos.Data.SuperZapatosDBContext szDBContext = new InfraestructuraDatos.Data.SuperZapatosDBContext();        

        public IEnumerable<Adaptadores.Article> GetArticles()
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            List<Dominio.Entities.Article> articles = articlesRepository.Get().ToList();                
            List<Adaptadores.Article> articlesDTO = ObjectMapper.Mapper.MapList<Dominio.Entities.Article, Adaptadores.Article>(articles);
            return articlesDTO;
        }

        public IEnumerable<Adaptadores.Article> GetArticlesStore(long idStore)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            List<Dominio.Entities.Article> articles = articlesRepository.Get(art => art.StoreId == idStore).ToList();
            List<Adaptadores.Article> articlesDTO = ObjectMapper.Mapper.MapList<Dominio.Entities.Article, Adaptadores.Article>(articles);
            return articlesDTO;
        }

        public Adaptadores.Article GetArticle(long id)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            Dominio.Entities.Article article = articlesRepository.GetById(id);
            Adaptadores.Article articleDTO = ObjectMapper.Mapper.Map<Dominio.Entities.Article, Adaptadores.Article>(article);
            return articleDTO;
        }

        public void AddArticle(Article article)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            Dominio.Entities.Article articleEntity = ObjectMapper.Mapper.Map<Adaptadores.Article, Dominio.Entities.Article>(article);
            articlesRepository.Insert(articleEntity);            
        }

        public void UpdateArticle(Article article)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            Dominio.Entities.Article articleEntity = ObjectMapper.Mapper.Map<Adaptadores.Article, Dominio.Entities.Article>(article);
            articlesRepository.Update(articleEntity);
        }

        public void DeleteArticle(long id)
        {
            IRepository<Dominio.Entities.Article> articlesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Article>(szDBContext);
            articlesRepository.Delete(id);
        }

        public void SaveChanges()
        {            
            szDBContext.SaveChanges();
        }

    }
}
