using Security.Authorize;
using Domain.Arguments.Usuario;
using Domain.Interfaces.Services;
using Infra.Transactions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Domain.Arguments.Imagens;
using Security.Security;

namespace Api.Controllers
{

    /// <summary>
    /// Controller de usuário.
    /// </summary>
    [CustomAutorize]
    [Authorize("Bearer")]
    [Route("api")]
    public class UsuarioController : Base.ControllerBase
    {
        private readonly IServiceUsuario _serviceUsuario;
        private readonly IServiceImagem _serviceImagem;
        private readonly AccessManager _accessManager;
        private readonly AuthenticatedUser _authenticatedUser;

        private IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Construtor do usuário.
        /// </summary>
        /// <param name="unitOfWork">O método responsável pelo commit.</param>
        /// <param name="serviceUsuario">O serviço de usuário.</param>
        /// <param name="accessManager">Método responsável por gerar o token</param>
        /// <param name="authenticatedUser">Obtém o usuário autenticado</param>
        public UsuarioController(IUnitOfWork unitOfWork, IServiceUsuario serviceUsuario, IServiceImagem serviceImagem,AccessManager accessManager, AuthenticatedUser authenticatedUser, IHostingEnvironment hostingEnvironment) : base(unitOfWork)
        {
            _serviceUsuario = serviceUsuario;
            _serviceImagem = serviceImagem;
            _accessManager = accessManager;
            _authenticatedUser = authenticatedUser;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Autenticar um usuário.
        /// </summary>
        /// <param name="request">O objeto usuario.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("v1/Usuario/Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody]AutenticarUsuarioRequest request)
        {
            try
            {
                var response = _serviceUsuario.AutenticarUsuario(request);             
                if (response !=null)
                {
                   var token = _accessManager.GenerateToken(response);
                   return await ResponseAsync(token, _serviceUsuario);
                }
                return await ResponseAsync(response, _serviceUsuario);
            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }

        /// <summary>
        /// Autenticar um usuário.
        /// </summary>
        /// <param name="request">O objeto usuario.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("v1/Usuario/ValidarToken")]
        public object ValidarToken([FromBody]TokenRequest request )
        {


            bool credenciaisValidas = false;

            if (request.Token.Length > 0)
                credenciaisValidas = true;

            if (credenciaisValidas)
            {
                return Ok(new { valid = true });
            }
            else
            {

                return BadRequest(new
                {
                    Authenticated = false,
                    Message = "Falha ao autenticar"
                });


            }
        }

        /// <summary>
        /// Autenticar um usuário.
        /// </summary>
        /// <param name="request">O objeto usuario.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("v1/Usuario/Imagem")]
        public async Task<IActionResult> SalvarImagem(IList<IFormFile> arquivos)
        {

            AdicionarImagemRequest imagemRequest = new AdicionarImagemRequest();
            IFormFile imagemEnviada = arquivos.FirstOrDefault();
            if (imagemEnviada != null || imagemEnviada.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                imagemEnviada.OpenReadStream().CopyTo(ms);


                string fileName;

                Imagem imagemEntity = new Imagem()
                {
                    Nome = imagemEnviada.Name,
                    Dados = ms.ToArray(),
                    ContentType = imagemEnviada.ContentType
                };

                var arquivo = imagemEntity.Dados;

                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, "images");
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                //var path = Path.Combine(
                //                Directory.GetCurrentDirectory(), "wwwroot",
                //                imagemEnviada.Name + ".png" );

                var extension = "." + imagemEnviada.FileName.Split('.')[imagemEnviada.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                                                                  //for the file due to security reasons.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var bits = new FileStream(path, FileMode.Create))


                {
                    await imagemEnviada.CopyToAsync(bits);
                }
 

                imagemRequest.Nome = imagemEnviada.Name;
                imagemRequest.Dados = ms.ToArray();
               imagemRequest.ContentType = imagemEnviada.ContentType;
 
             
            }
 
            try
            {
 
                var response = _serviceImagem.AdicionarImagem(imagemRequest);
                return await ResponseAsync(response, _serviceImagem);

            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }

        [Route("v1/Usuario/ExibirImagem")]
        [HttpGet]
        public FileStreamResult Download(Guid idImagem)
        {


          var imagem =  _serviceImagem.Obter(idImagem);

  
            var memory = new MemoryStream(imagem.Dados);

            return new FileStreamResult(memory, "image/png");


        }

        [Route("v1/Usuario/Listar")]
        [HttpGet]
        public async Task<IActionResult> Listar(string filename)
        {
           
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\images", filename);
 

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        public static byte[] streamToByteArray(Stream stream)
        {
            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mSteram = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mSteram.Write(byteArray, 0, bit);
                }
                return mSteram.ToArray();
            }
        }
        [Route("v1/Usuario/Download")]
        [HttpGet]
        public  FileStreamResult Download(string filename)
        {

            

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\images", filename);
 
            FileStream stream1 = System.IO.File.Open(path, FileMode.Open);
            
       
            byte[] buff =   streamToByteArray(stream1);
        
            var memory = new MemoryStream(buff);
 
            return new FileStreamResult(memory, "image/png");

          
        }


        [Route("v1/Usuario/Downloads")]
        [HttpGet]
        public FileStreamResult Download2(byte[] arquivo)
        {

 

            var memory = new MemoryStream(arquivo);

            return new FileStreamResult(memory, "image/png");


        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        /// <summary>
        /// Adicionar um usuário.
        /// </summary>
        /// <param name="request">O objeto usuario.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/Usuario/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarUsuarioRequest request)
        {
            try
            {
                var response = _serviceUsuario.AdicionarUsuario(request);
                return await ResponseAsync(response, _serviceUsuario);
            }
            catch (Exception e)
            {
                return await ResponseExceptionAsync(e);
            }
        }

 
    }
}