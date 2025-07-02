using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteTecnicoEffecti.Src.Models;

namespace TesteTecnicoEffecti.Src.Data.Mappings;

public class LicitacaoMapping : IEntityTypeConfiguration<Licitacao>
{
    public void Configure(EntityTypeBuilder<Licitacao> builder)
    {
            builder.ToTable("Licitacoes");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Orgao)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Universidade)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Instituicao)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.CodigoUASG)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.NumeroPregao)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Objeto)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.DataDisponibilizacaoEdital)
                .HasColumnType("date");

            builder.Property(e => e.HoraInicioEdital)
                .HasColumnType("time");

            builder.Property(e => e.HoraFimEdital)
                .HasColumnType("time");

            builder.Property(e => e.EnderecoEntrega)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Telefone)
                .HasMaxLength(255);

            builder.Property(e => e.Fax)
                .HasMaxLength(255);

            builder.Property(e => e.DataEntregaProposta)
                .HasColumnType("date");

            builder.Property(e => e.HoraEntregaProposta)
                .HasColumnType("time");

    }
}
