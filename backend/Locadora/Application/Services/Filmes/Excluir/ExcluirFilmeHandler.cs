using Application.Common.Exceptions;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Filmes.Excluir
{
    internal class ExcluirFilmeHandler : IRequestHandler<ExcluirFilmeRequest, ExcluirFilmeResponse>
    {
        private readonly IFilmesRepository _repository;
        private readonly ILogger<ExcluirFilmeHandler> _logger;

        public ExcluirFilmeHandler(IFilmesRepository repository, ILogger<ExcluirFilmeHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ExcluirFilmeResponse> Handle(ExcluirFilmeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando exclusão de filme.");

            var filme = await _repository.SelecionarUmaPor(filme => filme.Id == request.Id && filme.EhAtivo);

            if (filme is null)
            {
                var mensagem = $"Filme não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            await _repository.Excluir(filme);

            return new ExcluirFilmeResponse();
        }
    }
}
