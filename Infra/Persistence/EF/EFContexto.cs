using Domain.Entities;
using Domain.ValueObjects;
using Infra.Persistence.EF.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using prmToolkit.NotificationPattern;
using System.Data;
using System.Data.SqlClient;

using System.IO;

namespace Infra.Persistence.EF
{
    public class EFContexto: DbContext
    {

 
        public EFContexto(DbContextOptions<EFContexto> options)
            : base(options)
        {
             
        }


        public DbSet<Canal> Canais { get; set; }
       // public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

            

        //   // optionsBuilder.UseSqlServer(conexao);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Email>();
            modelBuilder.Ignore<Nome>();

            modelBuilder.ApplyConfiguration(new MapCanal());
            modelBuilder.ApplyConfiguration(new MapPlayList());
            modelBuilder.ApplyConfiguration(new MapVideo());
            modelBuilder.ApplyConfiguration(new MapUsuario());
            modelBuilder.ApplyConfiguration(new MapImagem());
            base.OnModelCreating(modelBuilder);
        }
    }
}
