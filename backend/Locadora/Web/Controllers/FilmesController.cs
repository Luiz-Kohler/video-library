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
    public class FilmesController : BaseController
    {
        private readonly IMediator _mediator;

        public FilmesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Criar filme
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarFilmeRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        /// <summary>
        /// Criar filme com arquivo .csv
        /// </summary>
        /// <param name="filmes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("csv")]
        public async Task<IActionResult> CriarComCsv([FromForm] IFormFile filmes)
        {
            await _mediator.Send(new ImportarFilmesRequest { ArquivoCsv = filmes });
            return Ok();
        }

        /// <summary>
        /// Atualizar filme
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarFilmeRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        /// <summary>
        /// Excluir filme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _mediator.Send(new ExcluirFilmeRequest { Id = id });
            return Ok();
        }

        /// <summary>
        /// Buscar filme pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] int id)
        {
            var response = await _mediator.Send(new BuscarFilmeRequest { Id = id });
            return Ok(response);
        }

        /// <summary>
        /// Listar filmes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _mediator.Send(new ListarFilmesRequest());
            return Ok(response);
        }
    }
}
