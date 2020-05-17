using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryPlayList
    {
        PlayList Obter(Guid idPlayList);
        IEnumerable<PlayList> Listar(Guid idUsuario);
        PlayList Adicionar(PlayList playList);
        void Excluir(PlayList playList);
    }
}
