using System;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Persistence.EF;

namespace Infra.Persistence.Repositories
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly EFContexto _context;

        public RepositoryUsuario(EFContexto context)
        {
            _context = context;
        }

        public bool Existe(string email)
        {
           return _context.Usuarios.Any(x => x.Email.Endereco == email);
        }

        public Usuario Obter(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.Endereco == email && x.Senha == senha);
        }

        public Usuario Obter(Guid idUsuario)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == idUsuario);
        }

        public void Salvar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }
    }
}
