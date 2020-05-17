using Domain.Arguments.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

using static Security.Security.Security;

namespace Security.Security
{
    /// <summary>
    /// Classe de geração de token
    /// </summary>
    public class AccessManager
    {
        private TokenConfigurations _tokenConfigurations;
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Construtor do método
        /// </summary>
        /// <param name="tokenConfigurations"></param>
        /// <param name="httpContextAccessor"></param>
        public AccessManager(TokenConfigurations tokenConfigurations, IHttpContextAccessor httpContextAccessor)
        {
            _tokenConfigurations = tokenConfigurations;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public AccessManager()
        { }
        /// <summary>
        /// Método para gerar o token do usuário.
        /// </summary>
        /// <param name="response">Os dados do usuário.</param>
        /// <returns></returns>
        public Token GenerateToken(AutenticarUsuarioResponse response)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
            new GenericIdentity(response.PrimeiroNome, "Id"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, response.Id.ToString("N")),
                      new Claim("Usuario",JsonConvert.SerializeObject(response))
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfigurations.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
 
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = credentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return new Token()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK",
                PrimeiroNome = response.PrimeiroNome
            };
        }


        /// <summary>
        /// Parametros de validação do token
        /// </summary>
        /// <param name="tokenConfigurations"></param>
        /// <returns></returns>
        private TokenValidationParameters GetValidationParameters(TokenConfigurations tokenConfigurations)
        {
            _tokenConfigurations = tokenConfigurations;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfigurations.Key));
 
            return new TokenValidationParameters()
            {
                // Valida a assinatura de um token recebido
                ValidateIssuerSigningKey = true,

                // Verifica se um token recebido ainda é válido
                ValidateLifetime = true,

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                ClockSkew = TimeSpan.Zero,
                
                ValidateAudience = true, 
                ValidateIssuer = true,   
                ValidIssuer = _tokenConfigurations.Issuer,
                ValidAudience = _tokenConfigurations.Audience,
                IssuerSigningKey = securityKey,  
            };
        }

        /// <summary>
        /// Obter o token no cabeçalho http. Método obsoleto;
        /// </summary>
        /// <returns></returns>
        public string ObterToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = string.Empty;
            if (httpContext != null && httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var authorizationHeaders = httpContext.Request.Headers.Where(x => x.Key == "Authorization").ToList();
                var bearer = authorizationHeaders.FirstOrDefault(header => header.Key == "Authorization").Value.ToString().Split(' ')[0];

                token = authorizationHeaders.FirstOrDefault(header => header.Key == "Authorization").Value.ToString().Split(' ')[1];
            }

            return token;
        }

        /// <summary>
        /// Valida o token 
        /// </summary>
        /// <param name="token">O token</param>
        /// <param name="tokenConfiguration">O objeto com os parâmetros de configuração do token</param>
        /// <returns></returns>
        public bool ValidateToken(string token , TokenConfigurations tokenConfiguration)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(tokenConfiguration);

            SecurityToken validatedToken;

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            return validatedToken != null;
        }
    }
}
