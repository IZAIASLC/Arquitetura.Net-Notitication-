using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Persistence.EF.Map
{
    public class MapPlayList : IEntityTypeConfiguration<PlayList>
    {
        public void Configure(EntityTypeBuilder<PlayList> builder)
        {
            builder.ToTable("PlayList");

            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey("IdUsuario");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
