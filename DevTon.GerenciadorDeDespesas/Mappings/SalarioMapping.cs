using DevTon.GerenciadorDeDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTon.GerenciadorDeDespesas.Mappings
{
    public class SalarioMapping : IEntityTypeConfiguration<Salario>
    {
        public void Configure(EntityTypeBuilder<Salario> builder)
        {
            builder.HasKey(s => s.SalarioId);

            builder.Property(s => s.Valor)
                .IsRequired();

            builder.HasOne(s => s.Meses)
                .WithOne(s => s.Salarios)
                .HasForeignKey<Salario>(s => s.MesId);

            builder.ToTable("Salarios");


        }
    }
}
