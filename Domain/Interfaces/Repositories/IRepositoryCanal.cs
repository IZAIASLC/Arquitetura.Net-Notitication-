using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryCanal
    {
        Canal Obter(Guid idCanal);
        IEnumerable<Canal> Listar(Guid idUsuario);
        Canal Adicionar(Canal canal);
        void Excluir(Canal canal);
    }
}
