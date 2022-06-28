using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Locacoes.Devolver
{
    public class DevolverFilmeHandler : IRequestHandler<DevolverFilmeRequest, DevolverFilmeResponse>
    {
        private readonly ILocacoesRepository _repository;
        private readonly ILogger<DevolverFilmeHandler> _logger;

        public DevolverFilmeHandler(ILocacoesRepository locacoesRepository, ILogger<DevolverFilmeHandler> logger)
        {
            _repository = locacoesRepository;
            _logger = logger;
        }

        public async Task<DevolverFilmeResponse> Handle(DevolverFilmeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando devolulçao filme");

            var locacao = await _repository.SelecionarUmaPor(locacao => locacao.Id == request.Id 
            && !locacao.DataDevolucao.HasValue
            && locacao.EhAtivo);

            if (locacao is null)
            {
                var mensagem = $"Locação não encontrada ou o filme já foi devolvido";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            locacao.DevolverFilme();
            await _repository.Atualizar(locacao);

            return new DevolverFilmeResponse();
        }
    }
}
