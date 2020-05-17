using Domain.Arguments.Usuario;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Security.Security
{
    /// <summary>
    /// Classe de usuário autenticado.
    /// </summary>
    public class AuthenticatedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name => _httpContextAccessor.HttpContext.User.Identity.Name;
        /// <summary>
        /// Informa se o usuário está autenticado.
        /// </summary>
        public bool IsAutenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        /// <summary>
        /// Obtém o objeto AutenticarUsuarioResponse.
        /// </summary>
        /// <returns></returns>
        public AutenticarUsuarioResponse ObtertUsuarioAutenticado()
        {
            string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
 
            return JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);
        }
    }
}
