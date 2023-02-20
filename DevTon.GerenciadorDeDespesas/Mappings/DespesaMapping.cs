using DevTon.GerenciadorDeDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTon.GerenciadorDeDespesas.Mappings
{
    public class DespesaMapping : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.HasKey(d => d.DespesaId);

            builder.Property(d => d.Valor)
                .IsRequired();

            builder.HasOne(d => d.Meses)
                .WithMany(d => d.Despesas)
                .HasForeignKey(d => d.MesId);


            builder.HasOne(d => d.TipoDespesa)
                .WithMany(d => d.Despesas)
                .HasForeignKey(d => d.TipoDespesaId);


            builder.ToTable("Despesas");
        }
    }
}
