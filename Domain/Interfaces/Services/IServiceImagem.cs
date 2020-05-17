using Domain.Arguments.Base;
using Domain.Arguments.Canal;
using Domain.Arguments.Imagens;
using Domain.Arguments.Usuario;
using Domain.Arguments.Video;
using Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Services
{
    public interface IServiceImagem : IServiceBase
    {
        AdicionarImagenResponse AdicionarImagem(AdicionarImagemRequest request);
        ImagemResponse Obter(Guid IdImagem);
    }
}
