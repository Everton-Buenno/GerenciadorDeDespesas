using DevTon.GerenciadorDeDespesas.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DevTon.GerenciadorDeDespesas.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }



        public DbSet<Meses> Meses { get; set; }

        public DbSet<Salario> Salario { get; set; }

        public DbSet<Despesa> Despesas { get; set; }

        public DbSet<TipoDespesa> TiposDespesas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TipoDespesaMapping());
            modelBuilder.ApplyConfiguration(new MesMapping());
            modelBuilder.ApplyConfiguration(new SalarioMapping());
            modelBuilder.ApplyConfiguration(new DespesaMapping());

        }
    }
}
