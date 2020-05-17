using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Persistence.EF.Map
{
    public class MapVideo : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Video").HasKey(x => x.Id);

            builder.HasOne(x => x.Canal).WithMany().HasForeignKey("IdCanal");
            builder.HasOne(x => x.PlayList).WithMany().HasForeignKey("IdPlayList");
            builder.HasOne(x => x.UsuarioSugeriu).WithMany().HasForeignKey("IdUsuario");

            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Tags).IsRequired().HasMaxLength(50);
            builder.Property(x => x.OrdemNaPlayList).IsRequired();
            builder.Property(x => x.IdVideoYoutube).IsRequired();

            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
 
        }
    }
}
