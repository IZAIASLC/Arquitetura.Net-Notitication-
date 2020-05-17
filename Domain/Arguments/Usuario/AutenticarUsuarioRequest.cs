using System;

namespace Domain.Arguments.Usuario
{
    public class AutenticarUsuarioRequest
    {

        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class TokenRequest
    {
        public string Token { get; set; }
    }
}
