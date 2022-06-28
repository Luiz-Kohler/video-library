using Application.Services.Relatorios.Gerar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace Web.Controllers
{
    [Route("relatorios")]
    public class RelatoriosController : BaseController
    {
        private readonly IMediator _mediator;

        public RelatoriosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Criar()
        {
            var response = await _mediator.Send(new GerarRelatorioRequest());
            return response.Arquivo;
        }
    }
}
