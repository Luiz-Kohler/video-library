using Application.Services.Clientes.Atualizar;
using Application.Services.Clientes.Buscar;
using Application.Services.Clientes.Criar;
using Application.Services.Clientes.Excluir;
using Application.Services.Clientes.Listar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("clientes")]
    public class ClientesController : Controller
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarClienteRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _mediator.Send(new ExcluirClienteRequest { Id = id});
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] int id)
        {
            var response = await _mediator.Send(new BuscarClienteRequest { Id = id });
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _mediator.Send(new ListarClientesRequest());
            return Ok(response);
        }
    }
}
