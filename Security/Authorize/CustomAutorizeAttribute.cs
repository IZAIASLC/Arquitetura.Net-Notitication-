using Security.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using static Security.Security.Security;

namespace Security.Authorize
{
    /// <summary>
    /// Filtro customizado de autorização.
    /// </summary>
    public class CustomAutorizeAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Filtro de autorização.
        /// </summary>
        public CustomAutorizeAttribute() : base(typeof(CustomAutorizeFilter))
        {
        }    
    }
    /// <summary>
    /// Customização do filtro de autorização.
    /// </summary>
    public class CustomAutorizeFilter : IAuthorizationFilter
    {

        private AccessManager _accessToken;
        private TokenConfigurations _tokenConfigurations;
        private IConfiguration configuration;
        /// <summary>
        /// Implementação da customização
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            _accessToken = new AccessManager();

            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            configuration = builder.Build();

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);

            _tokenConfigurations = tokenConfigurations;


            var httpContext = context.HttpContext;
            if (httpContext != null && httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var authorizationHeaders = httpContext.Request.Headers.Where(x => x.Key == "Authorization").ToList();
                var bearer = authorizationHeaders.FirstOrDefault(header => header.Key == "Authorization").Value.ToString().Split(' ')[0];

                var token = authorizationHeaders.FirstOrDefault(header => header.Key == "Authorization").Value.ToString().Split(' ')[1];

                bool tokenValido = _accessToken.ValidateToken(token, _tokenConfigurations);

                if (!tokenValido)
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                    return;
                }
                return;
            }
        }
    }
}
