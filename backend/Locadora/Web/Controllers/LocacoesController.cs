using Application.Services.Locacoes.Atualizar;
using Application.Services.Locacoes.Buscar;
using Application.Services.Locacoes.Criar;
using Application.Services.Locacoes.Devolver;
using Application.Services.Locacoes.Excluir;
using Application.Services.Locacoes.Listar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("locacoes")]
    public class LocacoesController : BaseController
    {
        private readonly IMediator _mediator;

        public LocacoesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarLocacaoRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarLocacaoRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}/devolver")]
        public async Task<IActionResult> Devolver([FromRoute] int id)
        {
            await _mediator.Send(new DevolverFilmeRequest { Id = id });
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _mediator.Send(new ExcluirLocacaoRequest { Id = id });
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] int id)
        {
            var response = await _mediator.Send(new BuscarLocacaoRequest { Id = id });
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _mediator.Send(new ListarLocacoesRequest());
            return Ok(response);
        }
    }
}
