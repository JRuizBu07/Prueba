using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Prueba.Models;
using System.Data;

namespace Prueba.Context
{

    public class DataContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sucursal>().HasMany(s => s.Giros).WithOne(c => c.SucursalDestino).HasForeignKey(c=> c.IdSucursalDestino);


        }
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
        public DbSet<Prueba.Models.Documento>? Documento { get; set; }
        public DbSet<Prueba.Models.Cliente>? Cliente { get; set; }
        public DbSet<Prueba.Models.Sucursal>? Sucursal { get; set; }


        [DbFunction("NTabla")]
        public virtual int NTabla(string tabla)
        {
            var parametroTabla = new SqlParameter("@Carpeta", tabla);
            var resultadoParam = new SqlParameter("@Resultado", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            this.Database.ExecuteSqlRaw("EXEC [dbo].[NTabla] @Carpeta, @Resultado OUTPUT",
        parametroTabla, resultadoParam);


            return (int)resultadoParam.Value;
        }


        public DbSet<Prueba.Models.FCT>? FCT { get; set; }
    }


}
