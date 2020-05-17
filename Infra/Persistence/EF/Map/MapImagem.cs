using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Persistence.EF.Map
{
   public class MapImagem:IEntityTypeConfiguration<Imagem>
    {
        public void Configure(EntityTypeBuilder<Imagem> builder)
        {
            builder.ToTable("Imagem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Dados).IsRequired();
            builder.Property(x => x.ContentType).HasMaxLength(250).IsRequired();
        }
    }

 
}
