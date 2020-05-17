
using Domain.Arguments.Canal;
using Domain.Interfaces.Services;
using Infra.Transactions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Authorize;
using Security.Security;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// Controller de canal.
    /// </summary>
    [CustomAutorize]
    [Authorize("Bearer")]
    [Route("api")]
    public class CanalController : Base.ControllerBase
    {
        private readonly IServiceCanal _serviceCanal;
        private readonly AuthenticatedUser _authenticatedUser;

        /// <summary>
        /// Construtor do canal.
        /// </summary>
        /// <param name="unitOfWork">O método responsável pelo commit.</param>
        /// <param name="serviceCanal">O serviço de canal.</param>
        /// <param name="authenticatedUser">Obtém o usuário autenticado</param>
        public CanalController(IUnitOfWork unitOfWork, IServiceCanal serviceCanal, AuthenticatedUser authenticatedUser):base(unitOfWork)
        {
            _serviceCanal = serviceCanal;
            _authenticatedUser = authenticatedUser;
        }
       
        /// <summary>
        /// Serviço para listar canais do usuário logado.
        /// </summary>
        /// <returns>A lista de canais.</returns>
        [HttpGet]
        [Route("v1/Canal/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {

                 var usuarioLogado = _authenticatedUser.ObtertUsuarioAutenticado();
              
             //  var usuario = Guid.Parse("B5653AFF-6AB6-439F-9CD7-20C61E7E0473");
                 var response = _serviceCanal.Listar(usuarioLogado.Id);
               // var response = _serviceCanal.Listar(usuario);
                return await ResponseAsync(response, _serviceCanal); 

            }catch(Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }
        /// <summary>
        /// Adicionar um novo canal.
        /// </summary>
        /// <param name="request">O objeto canal.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/Canal/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarCanalRequest request)
        {
            try
            {

                var usuarioLogado = _authenticatedUser.ObtertUsuarioAutenticado();

                var response = _serviceCanal.AdicionarCanal(request, usuarioLogado.Id);
                return await ResponseAsync(response, _serviceCanal);

            }catch(Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }
        /// <summary>
        /// Excluir um canal.
        /// </summary>
        /// <param name="idCanal">O identificador do canal.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/Canal/Excluir/{idCanal:Guid}")]
        public async Task<IActionResult>Excluir(Guid idCanal)
        {
            try
            {
                var response = _serviceCanal.ExcluirCanal(idCanal);
                return await ResponseAsync(response, _serviceCanal);

            }catch(Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }

    }
}
