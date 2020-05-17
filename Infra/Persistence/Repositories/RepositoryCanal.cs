using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Repositories
{
    public class RepositoryCanal : IRepositoryCanal
    {
        private readonly EFContexto _context;

        public RepositoryCanal(EFContexto context)
        {
            _context = context;
        }

        public Canal Adicionar(Canal canal)
        {
            _context.Canais.Add(canal);
            return canal;
        }

        public void Excluir(Canal canal)
        {
            _context.Canais.Remove(canal);
        }

        public IEnumerable<Canal> Listar(Guid idUsuario)
        {
            return _context.Canais.Where(x => x.Usuario.Id == idUsuario).AsNoTracking().ToList();
        }

        public Canal Obter(Guid idCanal)
        {
            return _context.Canais.FirstOrDefault(x => x.Id == idCanal);
        }
    }
}
