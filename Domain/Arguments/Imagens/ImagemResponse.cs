using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Arguments.Imagens
{
    public class ImagemResponse
    {
        public Guid IdImagem { get; set; }
        public string Nome { get; set; }
        public byte[] Dados { get; set; }
        public string ContentType { get; set; }

        public static explicit operator ImagemResponse(Imagem entidade)
        {
            return new ImagemResponse()
            {
                IdImagem = entidade.Id,
                Nome = entidade.Nome,
                ContentType = entidade.ContentType,
                Dados = entidade.Dados
            };
        }
    }
}
