using DevTon.GerenciadorDeDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTon.GerenciadorDeDespesas.Mappings
{
    public class MesMapping : IEntityTypeConfiguration<Meses>
    {
        public void Configure(EntityTypeBuilder<Meses> builder)
        {

            builder.HasKey(m => m.MesId);
            builder.Property(m => m.MesId).ValueGeneratedNever();

            builder.Property(m => m.Nome)
                .IsRequired()
                .HasMaxLength(50);


            builder.HasMany(m => m.Despesas)
                .WithOne(m => m.Meses)
                .HasForeignKey(m => m.MesId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Salarios)
                .WithOne(m => m.Meses)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Meses");
        }
    }
}
