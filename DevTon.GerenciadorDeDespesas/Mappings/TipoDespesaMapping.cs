using DevTon.GerenciadorDeDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTon.GerenciadorDeDespesas.Mappings
{
    public class TipoDespesaMapping : IEntityTypeConfiguration<TipoDespesa>
    {
        public void Configure(EntityTypeBuilder<TipoDespesa> builder)
        {
            builder.HasKey(td => td.TipoDespesaId);

            builder.Property(td => td.Nome)
                .IsRequired()
                .HasMaxLength(50);


            builder.HasMany(td => td.Despesas)
                .WithOne(td => td.TipoDespesa)
                .HasForeignKey(td => td.TipoDespesaId);

            builder.ToTable("TipoDespesa");
                
        }
    }
}
