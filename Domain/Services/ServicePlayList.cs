using Domain.Arguments.Base;
using Domain.Arguments.Canal;
using Domain.Arguments.PlayList;
using Domain.Arguments.Usuario;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Resources;
using Domain.ValueObjects;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class ServicePlayList : Notifiable, IServicePlayList
    {
        private readonly IRepositoryUsuario _repositorioUsuario;
        private readonly IRepositoryPlayList _repositorioPlayList;

        public ServicePlayList(IRepositoryUsuario repositorioUsuario, IRepositoryPlayList repositorioPlayList)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPlayList = repositorioPlayList;
        }

        public PlayListResponse AdicionarPlayList(AdicionarPlayListRequest request, Guid idUsuario)
        {
            var usuario = _repositorioUsuario.Obter(idUsuario);

            PlayList playList = new PlayList(request.Nome, usuario);

            AddNotifications(playList);

            if (IsInvalid()) return null;

            _repositorioPlayList.Adicionar(playList);

            return (PlayListResponse)playList;

        }

        public Response ExcluirPlayList(Guid idPlayList)
        {
            // bool existe =_repositorioVideo.ExistePlayListAssociado(idPlayList)
            bool existe = false;
            if (existe)
            {
                AddNotification("PlayList", "Não é possível excluir uma playlist associada a um vídeo");
                return null;
            }

            PlayList playList = _repositorioPlayList.Obter(idPlayList);

            if(playList==null)
            {
                AddNotification("PlayList", "Dados não encontrados");
            }

            if (IsInvalid()) return null;

            _repositorioPlayList.Excluir(playList);

            return new Response()
            {
                Message = "Operação realizada com sucesso",
            };
        }

        public IEnumerable<PlayListResponse> Listar(Guid idUsuario)
        {
            return _repositorioPlayList.Listar(idUsuario).ToList().Select(x => (PlayListResponse)x);
        }
    }
}
