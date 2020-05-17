using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Arguments.Imagens
{
   
    public class AdicionarImagenResponse
    {
        public AdicionarImagenResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }


    }
}
