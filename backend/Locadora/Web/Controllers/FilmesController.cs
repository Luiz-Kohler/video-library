using Application.Services.Filmes.Atualizar;
using Application.Services.Filmes.Buscar;
using Application.Services.Filmes.Criar;
using Application.Services.Filmes.Excluir;
using Application.Services.Filmes.Importar;
using Application.Services.Filmes.Listar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("filmes")]
    public class FilmesController : Controller
    {
        private readonly IMediator _mediator;

        public FilmesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarFilmeRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }


        [HttpPost]
        [Route("csv")]
        public async Task<IActionResult> CriarComCsv([FromForm] IFormFile filmes)
        {
            await _mediator.Send(new ImportarFilmesRequest { ArquivoCsv = filmes });
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarFilmeRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _mediator.Send(new ExcluirFilmeRequest { Id = id });
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] int id)
        {
            var response = await _mediator.Send(new BuscarFilmeRequest { Id = id });
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _mediator.Send(new ListarFilmesRequest());
            return Ok(response);
        }
    }
}
