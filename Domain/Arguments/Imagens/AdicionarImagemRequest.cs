using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Arguments.Imagens
{
   public class AdicionarImagemRequest
    {
        public string Nome { get; set; }
        public byte[] Dados { get; set; }
        public string ContentType { get; set; }
    }
}
