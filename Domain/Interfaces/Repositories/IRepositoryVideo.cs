using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryVideo
    {
        void Adicionar(Video video);
        IEnumerable<Video> Listar(Guid idPlayList);
        IEnumerable<Video> Listar(String tags);
        bool ExisteCanalAssociado(Guid idCanal);
        bool ExistePlayListAssociada(Guid idPlayList);
    }
}
