using Domain.Arguments.Base;
using Domain.Arguments.Canal;
using Domain.Arguments.PlayList;
using Domain.Arguments.Usuario;
using Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Services
{
    public interface IServicePlayList : IServiceBase
    {
        IEnumerable<PlayListResponse> Listar(Guid idUsuario);
        PlayListResponse AdicionarPlayList(AdicionarPlayListRequest request, Guid idUsuario);
        Response ExcluirPlayList(Guid idPlayList);
    }
}
