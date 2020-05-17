using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class Imagem:EntityBase
    {

        protected Imagem()
        {
        }
        public Imagem(string nome, byte[]dados, string contentyType)
        {
            this.Nome = nome;
            this.Dados = dados;
            this.ContentType = contentyType;
        }

        public string Nome { get; set; }
        public byte[] Dados { get; set; }
        public string ContentType { get; set; }
    }
}
