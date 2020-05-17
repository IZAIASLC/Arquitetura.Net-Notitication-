using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Repositories
{
    public class RepositoryPlayList : IRepositoryPlayList
    {
        private readonly EFContexto _context;

        public RepositoryPlayList(EFContexto context)
        {
            _context = context;
        }

        public PlayList Adicionar(PlayList playList)
        {
            _context.PlayLists.Add(playList);
            return playList;
        }

        public void Excluir(PlayList playList)
        {
            _context.PlayLists.Remove(playList);
        }

        public IEnumerable<PlayList> Listar(Guid idUsuario)
        {
            return _context.PlayLists.Where(x => x.Usuario.Id == idUsuario).AsNoTracking().ToList();
        }

        public PlayList Obter(Guid idPlayList)
        {
            return _context.PlayLists.FirstOrDefault(x => x.Id == idPlayList);
        }
    }
}
