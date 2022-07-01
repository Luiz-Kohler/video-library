using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Filmes.Excluir
{
    public class ExcluirFilmeHandler : IRequestHandler<ExcluirFilmeRequest, ExcluirFilmeResponse>
    {
        private readonly IFilmesRepository _repository;
        private readonly ILocacoesRepository _locacoesRepository;
        private readonly ILogger<ExcluirFilmeHandler> _logger;

        public ExcluirFilmeHandler(IFilmesRepository repository, ILocacoesRepository locacoesRepository, ILogger<ExcluirFilmeHandler> logger)
        {
            _repository = repository;
            _locacoesRepository = locacoesRepository;
            _logger = logger;
        }

        public async Task<ExcluirFilmeResponse> Handle(ExcluirFilmeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando exclusão de filme.");

            var filme = await _repository.SelecionarUmaPorIncluindoLocacoes(filme => filme.Id == request.Id && filme.EhAtivo);

            if (filme is null)
            {
                var mensagem = $"Filme não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            var locacoes = filme.Locacoes.Select(locacao =>
            {
                locacao.Desativar();
                return locacao;
            });

            if (locacoes.Any())
            {
                _logger.LogInformation("Excluindo locações atrelado ao filme");
                await _locacoesRepository.AtualizarVarios(locacoes);
            }

            _logger.LogInformation("Excluindo filme");
            await _repository.Excluir(filme);

            return new ExcluirFilmeResponse();
        }
    }
}
