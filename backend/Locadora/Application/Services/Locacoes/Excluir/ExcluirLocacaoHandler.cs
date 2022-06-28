using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Locacoes.Excluir
{
    public class ExcluirLocacaoHandler : IRequestHandler<ExcluirLocacaoRequest, ExcluirLocacaoResponse>
    {
        private readonly ILocacoesRepository _repository;
        private readonly ILogger<ExcluirLocacaoHandler> _logger;

        public ExcluirLocacaoHandler(ILocacoesRepository locacoesRepository, ILogger<ExcluirLocacaoHandler> logger)
        {
            _repository = locacoesRepository;
            _logger = logger;
        }

        public async Task<ExcluirLocacaoResponse> Handle(ExcluirLocacaoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando exclusão locação");

            var locacao = await _repository.SelecionarUmaPor(locacao => locacao.Id == request.Id && locacao.EhAtivo);

            if (locacao is null)
            {
                var mensagem = $"Locação não encontrada.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            await _repository.Excluir(locacao);

            return new ExcluirLocacaoResponse();
        }
    }
}
