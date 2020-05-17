using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryImagem
    {
        void Adicionar(Imagem imagem);
        Imagem Obter(Guid idImagem);
    }
}
