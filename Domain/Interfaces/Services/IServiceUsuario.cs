using Domain.Arguments.Usuario;
using Domain.Interfaces.Base;

namespace Domain.Interfaces.Services
{
    public interface IServiceUsuario:IServiceBase
    {
        AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request);

        AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request);
    }
}
