using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Persistence.EF.Map
{
    public class MapCanal : IEntityTypeConfiguration<Canal>
    {
        public void Configure(EntityTypeBuilder<Canal> builder)
        {
            //Nome da tabela
            builder.ToTable("Canal");
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey("IdUsuario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(50).IsRequired();
            builder.Property(x => x.UrlLogo).HasMaxLength(255).IsRequired();
        }
    }
}
