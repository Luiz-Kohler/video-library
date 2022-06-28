using Application.Common.Exceptions;
using Domain.Common.Enums;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Locacoes.Criar
{
    public class CriarLocacaoHandler : IRequestHandler<CriarLocacaoRequest, CriarLocacaoResponse>
    {
        private readonly ILocacoesRepository _locacoesRepository;
        private readonly IFilmesRepository _filmesRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly ILogger<CriarLocacaoHandler> _logger;

        public CriarLocacaoHandler(
             ILocacoesRepository locacoesRepository,
             IFilmesRepository filmesRepository,
             IClientesRepository clientesRepository,
             ILogger<CriarLocacaoHandler> logger)
        {
            _locacoesRepository = locacoesRepository;
            _filmesRepository = filmesRepository;
            _clientesRepository = clientesRepository;
            _filmesRepository = filmesRepository;
            _logger = logger;
        }

        public async Task<CriarLocacaoResponse> Handle(CriarLocacaoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando cadastro de locaçao.");

            await ValidarExistenciaLocacaoAtiva(request);

            var filme = await BuscarFilme(request.FilmeId);
            var cliente = await BuscarCliente(request.ClienteId);

            var locacao = new Locacao(cliente, filme);

            _logger.LogInformation("Inserindo nova locação no banco");
            await _locacoesRepository.Inserir(locacao);

            return new CriarLocacaoResponse();
        }

        private async Task ValidarExistenciaLocacaoAtiva(CriarLocacaoRequest request)
        {
            var locacao = await _locacoesRepository.SelecionarUmaPor(locacao
                => locacao.FilmeId == request.FilmeId
                && locacao.ClienteId == request.ClienteId
                && locacao.Status == StatusLocacao.Andamento
                && locacao.EhAtivo);

            if (locacao is not null)
            {
                var mensagem = $"Locação com o filme: {request.FilmeId} e cliente: {request.ClienteId} já existe uma ativa no momento.";
                _logger.LogInformation(mensagem);
                throw new DuplicateValueException(mensagem);
            }
        }

        private async Task<Filme> BuscarFilme(int filmeId)
        {
            var filme = await _filmesRepository.SelecionarUmaPor(filme => filme.Id == filmeId && filme.EhAtivo);

            if (filme is null)
            {
                var mensagem = $"Filme não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            return filme;
        }

        private async Task<Cliente> BuscarCliente(int clienteId)
        {
            var cliente = await _clientesRepository.SelecionarUmaPor(cliente => cliente.Id == clienteId && cliente.EhAtivo);

            if (cliente is null)
            {
                var mensagem = $"Cliente não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            return cliente;
        }
    }
}
