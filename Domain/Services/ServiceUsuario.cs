using Domain.Arguments.Usuario;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Resources;
using Domain.ValueObjects;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;

namespace Domain.Services
{
    public class ServiceUsuario : Notifiable, IServiceUsuario
    {
        private readonly IRepositoryUsuario _repositoriyUsuario;

        public ServiceUsuario(IRepositoryUsuario repositoriyUsuario)
        {
            _repositoriyUsuario = repositoriyUsuario;
        }

        public AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request)
        {
            if (request == null)
            {
                AddNotification("AdicionarUsuarioRequest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("AdicionarUsuarioRequest"));
                return null;
            }

            var nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            var email = new Email(request.Email);
            var usuario = new Usuario(nome, email, request.Senha);
            AddNotifications(usuario);

            if (IsInvalid()) return null;


            _repositoriyUsuario.Salvar(usuario);

            return new AdicionarUsuarioResponse(usuario.Id);
        }

       public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
             if(request == null)
            {
                AddNotification("AdicionarUsuarioRequest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("AutenticarUsuarioResponse"));
                return null;
            }

            var email = new Email(request.Email);
            var usuario = new Usuario(email, request.Senha);
            AddNotifications(usuario,email);

            if (IsInvalid()) return null;

            usuario = _repositoriyUsuario.Obter(usuario.Email.Endereco, usuario.Senha);

            if(usuario == null)
            {
                AddNotification("Usuario", MSG.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            return (AutenticarUsuarioResponse)usuario;
        }
    }
}
