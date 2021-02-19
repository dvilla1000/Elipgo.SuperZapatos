using Elipgo.SuperZapatos.Dominio.Entities;
//using Elipgo.SuperZapatos.InfraestructuraDatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Elipgo.SuperZapatos.InfraestructuraDatos.Data
{
    public class SuperZapatosDBContext : DbContext
    {
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        //public SuperZapatosDBContext(DbContextOptions<SuperZapatosDBContext> options) 
        //    : base(options)
        //{
        //    Database.EnsureCreated();
        //}

        //public SuperZapatosDBContext(DbContextOptions options)
        //    : base(options)
        //{
        //    Database.EnsureCreated();
        //}

        #region Métodos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Store>().ToTable("Stores").HasKey(e => e.Id);
            modelBuilder.Entity<Article>().ToTable("Articles").HasKey(e => e.Id);
            modelBuilder.Entity<Article>().Property(e => e.StoreId).ValueGeneratedNever().HasColumnName("storeId");
            base.OnModelCreating(modelBuilder);
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("localDB"));
        }
        #endregion
    }
}
