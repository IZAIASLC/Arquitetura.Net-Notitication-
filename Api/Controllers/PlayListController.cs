using Security.Authorize;
using Domain.Arguments.PlayList;
using Domain.Interfaces.Services;
using Infra.Transactions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Security;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// Controller de playList.
    /// </summary>
    [CustomAutorize]
    [Authorize("Bearer")]
    [Route("api")]
    public class PlayListController : Base.ControllerBase
    {
        private readonly IServicePlayList _servicePlayList;
        private readonly AuthenticatedUser _authenticatedUser;
        /// <summary>
        /// Construtor da playList.
        /// </summary>
        /// <param name="unitOfWork">O método responsável pelo commit.</param>
        /// <param name="servicePlayList">O serviço da playList.</param>
        /// <param name="authenticatedUser">Obtém o usuário autenticado</param>
        public PlayListController(IUnitOfWork unitOfWork, IServicePlayList servicePlayList, AuthenticatedUser authenticatedUser) : base(unitOfWork)
        {
            _servicePlayList = servicePlayList;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Serviço para listar as playLists do usuário logado.
        /// </summary>
        /// <returns>A lista de playLists.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("v1/PlayList/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {

                // var usuarioLogado = _authenticatedUser.ObtertUsuarioAutenticado();
                var usuario = Guid.Parse("B5653AFF-6AB6-439F-9CD7-20C61E7E0473");
                var response = _servicePlayList.Listar(usuario);

                return await ResponseAsync(response, _servicePlayList);

            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }
        /// <summary>
        /// Adicionar uma nova playList.
        /// </summary>
        /// <param name="request">O objeto playList.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/PlayList/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarPlayListRequest request)
        {
            try
            {

                var usuarioLogado = _authenticatedUser.ObtertUsuarioAutenticado();

                var response = _servicePlayList.AdicionarPlayList(request, usuarioLogado.Id);
                return await ResponseAsync(response, _servicePlayList);

            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }
        /// <summary>
        /// Excluir uma playList.
        /// </summary>
        /// <param name="idPlayList">O identificador da playList.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/PlayList/Excluir/{idPlayList:Guid}")]
        public async Task<IActionResult> Excluir(Guid idPlayList)
        {
            try
            {
                var response = _servicePlayList.ExcluirPlayList(idPlayList);
                return await ResponseAsync(response, _servicePlayList);

            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }

    }
}
