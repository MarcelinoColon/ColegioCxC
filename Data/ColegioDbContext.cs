using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ColegioDbContext : DbContext
    {
        public ColegioDbContext()
        {
        }
        public ColegioDbContext(DbContextOptions<ColegioDbContext> options) : base(options)
        {
        }
        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Concepto> Conceptos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<TransaccionPago> TransaccionPagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
