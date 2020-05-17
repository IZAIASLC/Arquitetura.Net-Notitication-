using Domain.Arguments.Base;
using Domain.Arguments.Canal;
using Domain.Arguments.Usuario;
using Domain.Arguments.Video;
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
    public class ServiceVideo : Notifiable, IServiceVideo
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryCanal _repositoryCanal;
        private readonly IRepositoryPlayList _repositoryPlayList;
        private readonly IRepositoryVideo _repositoryVideo;

        public ServiceVideo(IRepositoryUsuario repositoryUsuario, IRepositoryCanal repositoryCanal, IRepositoryPlayList repositoryPlayList, IRepositoryVideo repositoryVideo)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryCanal = repositoryCanal;
            _repositoryPlayList = repositoryPlayList;
            _repositoryVideo = repositoryVideo;
        }

        public AdicionarVideoResponse AdicionarVideo(AdicionarVideoRequest request, Guid idUsuario)
        {
             if(request==null)
            {
                AddNotification("Video", "Objeto AdicionarVideoRequest é obrigatório");
                return null;
            }

            Usuario usuario = _repositoryUsuario.Obter(idUsuario);
            if(usuario == null)
            {
                AddNotification("Usuário", "Objeto Usuário não encontrado");
                return null;
            }
            Canal canal = _repositoryCanal.Obter(request.IdCanal);
            if(canal==null)
            {
                AddNotification("Canal", "Objeto Canal não encontrado");
                return null;
            }

            PlayList playList = null;

            if(request.IdPlayList != Guid.Empty)
            {
                playList = _repositoryPlayList.Obter(request.IdPlayList);

                if (playList == null)
                {
                    AddNotification("PlayList", "Objeto Playlist não encontrado");
                    return null;
                }
            }

            var video = new Video(canal, playList, request.Titulo, request.Descricao, request.Tags, request.OrdemNaPlayList, request.IdVideoYoutube, usuario);

            AddNotifications(video);

            if (IsInvalid()) return null;

            _repositoryVideo.Adicionar(video);

            return new AdicionarVideoResponse(video.Id);
        }

        public IEnumerable<VideoResponse> Listar(Guid idPlayList)
        {
            return _repositoryVideo.Listar(idPlayList).OrderBy(o=>o.OrdemNaPlayList).ToList().Select(x => (VideoResponse)x);
        }

        public IEnumerable<VideoResponse> Listar(string tabs)
        {
            return _repositoryVideo.Listar(tabs).ToList().Select(x => (VideoResponse)x);
        }
    }
}
