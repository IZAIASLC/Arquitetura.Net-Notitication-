using Domain.Interfaces.Base;
using Infra.Transactions.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Base
{
    /// <summary>
    /// Controller base
    /// </summary>
    public class ControllerBase:Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IServiceBase _serviceBase;

        /// <summary>
        /// Contrutor da controller base
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ControllerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Retorna o resultado da requisição.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="serviceBase"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ResponseAsync(object result, IServiceBase serviceBase)
        {
            _serviceBase = serviceBase;

            if(!serviceBase.Notifications.Any())
            {
                try
                {
                     _unitOfWork.Commit();
                    return Ok(result);
                }
                catch(Exception ex)
                {
                    return  BadRequest($"Houve um problema interno no servidor. Entre em contato com o administrador do sistema" + ex.Message);
                }
            }else
            {
                var listaErros = new List<string>();
                 
                foreach(var erro in serviceBase.Notifications)
                {
                    listaErros.Add(erro.Message);
                }


                return BadRequest(new { errors = listaErros });
            }
                
        }
        /// <summary>
        /// Retorna o resultado da requisição caso haja exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ResponseExceptionAsync(Exception ex)
        {
            return BadRequest(new { errors = ex.Message });
        }

        /// <summary>
        /// Destrutor padrão.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if(_serviceBase!=null)
            {
                _serviceBase.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
