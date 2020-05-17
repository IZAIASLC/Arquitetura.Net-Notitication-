using Security.Authorize;
using Domain.Arguments.Video;
using Domain.Interfaces.Services;
using Infra.Transactions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Security.Security;

namespace Api.Controllers
{
    /// <summary>
    /// Controller de vídeo.
    /// </summary>
    [CustomAutorize]
    [Authorize("Bearer")]
    [Route("api")]
    public class VideoController : Base.ControllerBase
    {
        private readonly IServiceVideo _serviceVideo;
        private readonly AuthenticatedUser _authenticatedUser;
        /// <summary>
        /// Construtor do vídeo.
        /// </summary>
        /// <param name="unitOfWork">O método responsável pelo commit.</param>
        /// <param name="serviceVideo">O serviço do vídeo.</param>
        /// <param name="authenticatedUser">Obtém o usuário autenticado</param>
        public VideoController(IUnitOfWork unitOfWork, IServiceVideo serviceVideo, AuthenticatedUser authenticatedUser) : base(unitOfWork)
        {
            _serviceVideo = serviceVideo;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Serviço para listar vídeos de uma playList.
        /// </summary>
        /// <returns>A lista de vídeos.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("v1/Video/Listar/{idPlayList:Guid}")]
        public async Task<IActionResult> Listar(Guid idPlayList)
        {
            try
            {
      
                var response = _serviceVideo.Listar(idPlayList);

                return await ResponseAsync(response, _serviceVideo);

            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }

        /// <summary>
        /// Serviço para listar vídeos de uma tag.
        /// </summary>
        /// <returns>A lista de vídeos.</returns>
        [HttpGet]
        [Route("v1/Video/Listar/{tags}")]
        public async Task<IActionResult> Listar(string tags)
        {
            try
            {
               
                var response = _serviceVideo.Listar(tags);

                return await ResponseAsync(response, _serviceVideo);

            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }
        /// <summary>
        /// Adicionar um novo vídeo.
        /// </summary>
        /// <param name="request">O objeto video.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("v1/Video/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarVideoRequest request, IList<IFormFile> arquivos)
        {
            try
            {
 
                var usuarioLogado = _authenticatedUser.ObtertUsuarioAutenticado();

                var response = _serviceVideo.AdicionarVideo(request, usuarioLogado.Id);
                return await ResponseAsync(response, _serviceVideo);

            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }
 
    }

    public class Imagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Dados { get; set; }
        public string ContentType { get; set; }
    }
}
