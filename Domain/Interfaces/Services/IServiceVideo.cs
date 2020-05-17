using Domain.Arguments.Base;
using Domain.Arguments.Canal;
using Domain.Arguments.Usuario;
using Domain.Arguments.Video;
using Domain.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Services
{
    public interface IServiceVideo : IServiceBase
    {
        AdicionarVideoResponse AdicionarVideo(AdicionarVideoRequest request, Guid idUsuario);
        IEnumerable<VideoResponse> Listar(Guid idPlayList);
        IEnumerable<VideoResponse> Listar(string tags);  
    }
}
