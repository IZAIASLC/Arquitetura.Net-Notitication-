using Domain.Arguments.Base;
using Domain.Arguments.Canal;
using Domain.Arguments.Usuario;
using Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Services
{
    public interface IServiceCanal : IServiceBase
    {
        IEnumerable<CanalResponse> Listar(Guid idUsuario);
        CanalResponse AdicionarCanal(AdicionarCanalRequest request, Guid idUsuario);
        Response ExcluirCanal(Guid idCanal);
      
    }
}
