using Domain.Entities;
using System;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryUsuario
    {
        Usuario Obter(string email, string senha);
        Usuario Obter(Guid idUsuario);
        void Salvar(Usuario usuario);
        bool Existe(string email);
    }
}
