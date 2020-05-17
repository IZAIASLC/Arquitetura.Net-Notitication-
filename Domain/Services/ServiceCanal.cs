using Domain.Arguments.Base;
using Domain.Arguments.Canal;
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
    public class ServiceCanal : Notifiable, IServiceCanal
    {
        private readonly IRepositoryUsuario _repositorioUsuario;
        private readonly IRepositoryCanal _repositorioCanal;

        public ServiceCanal(IRepositoryUsuario repositorioUsuario, IRepositoryCanal repositorioCanal)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioCanal = repositorioCanal;
        }

        public CanalResponse AdicionarCanal(AdicionarCanalRequest request, Guid idUsuario)
        {
            var usuario = _repositorioUsuario.Obter(idUsuario);

            Canal canal = new Canal(request.Nome, request.UrlLogo, usuario);

            AddNotifications(canal);

            if (IsInvalid()) return null;

            _repositorioCanal.Adicionar(canal);

            return (CanalResponse)canal;

        }

        public Response ExcluirCanal(Guid idCanal)
        {
            bool existe = false;// _repositorioVideo.ExisteCanalAssociado(idCanal);

            if (existe)
            {
                AddNotification("Canal", "Não é possível excluir um canal associado a um vídeo");
                return null;
            }

            Canal canal = _repositorioCanal.Obter(idCanal);

            if (canal == null)
            {
                AddNotification("Canal", "Dados não encontrados");
            }

            if (IsInvalid()) return null;

            _repositorioCanal.Excluir(canal);

            return new Response() { Message = "Operação realizada com sucesso" };
        }

        public IEnumerable<CanalResponse> Listar(Guid idUsuario)
        {
            return _repositorioCanal.Listar(idUsuario).ToList().Select(x => (CanalResponse)x);
        }
    }
}
