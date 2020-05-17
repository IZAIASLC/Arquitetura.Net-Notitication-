using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Repositories
{
    public class RepositoryImagem : IRepositoryImagem
    {
        private readonly EFContexto _context;

        public RepositoryImagem(EFContexto context)
        {
            _context = context;
        }

        public void Adicionar(Imagem imagem)
        {
            _context.Imagens.Add(imagem);
        }

        public Imagem Obter(Guid idImagem)
        {
          return  _context.Imagens.First(x => x.Id == idImagem);
        }
    }
}
