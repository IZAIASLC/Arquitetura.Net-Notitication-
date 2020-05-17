using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Security
{
    /// <summary>
    /// Classe de segurança.
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Obter configuração appConfig.
        /// </summary>
        public class TokenConfigurations
        {
            public string Audience { get; set; }
            public string Issuer { get; set; }
            public int Seconds { get; set; }
            public string Key { get; set; }
        }

        /// <summary>
        /// Classe de retorno ao autenticar.
        /// </summary>
        public class Token
        {
            public bool Authenticated { get; set; }
            public string Created { get; set; }
            public string Expiration { get; set; }
            public string AccessToken { get; set; }
            public string Message { get; set; }
            public string PrimeiroNome { get; set; }
        }
    }
}
