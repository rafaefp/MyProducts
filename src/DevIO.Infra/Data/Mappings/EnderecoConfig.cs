using DevIO.Business.Models.Fornecedores;
using System.Data.Entity.ModelConfiguration;

namespace DevIO.Infra.Data.Mappings
{
    public class EnderecoConfig : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfig()
        {
            HasKey(f => f.Id);

            Property(f => f.Logradouro)
                .IsRequired()
                .HasMaxLength(200);

            Property(f => f.Numero)
                .IsRequired()
                .HasMaxLength(50);

            Property(f => f.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsFixedLength();

            Property(f => f.Complemento)
                .HasMaxLength(250);

            Property(f => f.Bairro)
                .IsRequired()
                .HasMaxLength(100);

            Property(f => f.Cidade)
                .IsRequired()
                .HasMaxLength(100);

            Property(f => f.Estado)
                .IsRequired()
                .HasMaxLength(100);

            ToTable("Enderecos");
        }
    }
}
