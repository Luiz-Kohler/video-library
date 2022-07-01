using Application.Common.Exceptions;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Filmes.Atualizar
{
    public class AtualizarFilmeHandler : IRequestHandler<AtualizarFilmeRequest, AtualizarFilmeResponse>
    {
        private readonly IFilmesRepository _repository;
        private readonly ILogger<AtualizarFilmeHandler> _logger;

        public AtualizarFilmeHandler(IFilmesRepository repository, ILogger<AtualizarFilmeHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<AtualizarFilmeResponse> Handle(AtualizarFilmeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando atualização de filme.");

            var filme = await _repository.SelecionarUmaPor(filme => filme.Id == request.Id && filme.EhAtivo);

            if (filme is null)
            {
                var mensagem = $"Filme não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            filme.Atualizar(request.Titulo, request.Classificacao, request.EhLancamento);

            await _repository.Atualizar(filme);

            return new AtualizarFilmeResponse();
        }
    }
}
