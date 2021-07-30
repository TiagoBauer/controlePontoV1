using controleDePontoV1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace controleDePontoV1.Context
{
    public class PontoDBContext : DbContext
    {
        //Construtor
        public PontoDBContext(DbContextOptions<PontoDBContext> options)
           : base(options)
        { }


        public DbSet<Papel> papeis { get; set; }
        public DbSet<Equipe> equipes { get; set; }
        public DbSet<Projeto> projetos { get; set; }
        public DbSet<Colaborador> colaboradores { get; set; }
        public DbSet<HistoricoEquipe> histEquipes { get; set; }
        public DbSet<ControleApontamento> controleApontamento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Papel>(entity =>
            {
                entity.Property(e => e.codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int")
                    .ValueGeneratedNever(); 
            });

            builder.Entity<Equipe>(entity =>
            {
                entity.Property(e => e.codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int")
                    .ValueGeneratedNever(); 
            });

            builder.Entity<Projeto>(entity =>
            {
                entity.Property(e => e.codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int")
                    .ValueGeneratedNever(); 
            });

            builder.Entity<Colaborador>(entity =>
            {
                entity.Property(e => e.codigo)
                    .HasColumnName("codigo")
                    .HasColumnType("int")
                    .ValueGeneratedNever();
            });

            builder.Entity<HistoricoEquipe>(entity =>
            {
                entity.Property(e => e.codigo_Colaborador)
                    .HasColumnName("codigo_Colaborador")
                    .HasColumnType("int")
                    .ValueGeneratedNever();
                entity.Property(e => e.codigo_Equipe)
                    .HasColumnName("codigo_Equipe")
                    .HasColumnType("int")
                    .ValueGeneratedNever();
                entity.Property(e => e.dataDaAlteracao)
                    .HasColumnName("dataDaAlteracao")
                    .HasColumnType("dateTime")
                    .ValueGeneratedNever();
            });

            builder.Entity<ControleApontamento>(entity =>
            {
                entity.Property(e => e.codigo_Colaborador)
                    .HasColumnName("codigo_Colaborador")
                    .HasColumnType("int")
                    .ValueGeneratedNever();
                entity.Property(e => e.codigo_Equipe)
                    .HasColumnName("codigo_Equipe")
                    .HasColumnType("int")
                    .ValueGeneratedNever();
                entity.Property(e => e.codigo_Projeto)
                    .HasColumnName("codigo_Projeto")
                    .HasColumnType("int")
                    .ValueGeneratedNever();
                entity.Property(e => e.dia_Marcao)
                    .HasColumnName("dateTime")
                    .HasColumnType("dateTime")
                    .ValueGeneratedNever();
            });


            builder.Entity<Papel>().HasKey(t => new { t.codigo });
            builder.Entity<Equipe>().HasKey(t => new { t.codigo});
            builder.Entity<Projeto>().HasKey(t => new { t.codigo});
            builder.Entity<Colaborador>().HasKey(t => new { t.codigo });
            builder.Entity<HistoricoEquipe>().HasKey(t => new { t.codigo_Colaborador, t.codigo_Equipe });
            builder.Entity<ControleApontamento>().HasKey(t => new { t.codigo_Colaborador, t.codigo_Equipe, t.codigo_Projeto, t.dia_Marcao});
        }
    }
}
