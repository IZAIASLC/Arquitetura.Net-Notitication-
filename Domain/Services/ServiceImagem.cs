using Domain.Arguments.Base;
using Domain.Arguments.Canal;
using Domain.Arguments.Imagens;
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
    public class ServiceImagem : Notifiable, IServiceImagem
    {
        private readonly IRepositoryImagem _repositorioImagem;
   

        public ServiceImagem(IRepositoryImagem repositorioImagem)
        {
            _repositorioImagem = repositorioImagem;
            
        }

        public AdicionarImagenResponse AdicionarImagem(AdicionarImagemRequest request)
        {
           

            Imagem imagem = new Imagem(request.Nome, request.Dados, request.ContentType);
 
            _repositorioImagem.Adicionar(imagem);

            return new AdicionarImagenResponse(imagem.Id);

        }

       

        ImagemResponse IServiceImagem.Obter(Guid idImagem)
        {
           var imagem = _repositorioImagem.Obter(idImagem);
            return (ImagemResponse)imagem;
        }
    }
}
