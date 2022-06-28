using Application.Common.Exceptions;
using Domain.Common.Enums;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Locacoes.Atualizar
{
    public class AtualizarLocacaoHandler : IRequestHandler<AtualizarLocacaoRequest, AtualizarLocacaoResponse>
    {
        private readonly ILocacoesRepository _locacoesRepository;
        private readonly IFilmesRepository _filmesRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly ILogger<AtualizarLocacaoHandler> _logger;

        public AtualizarLocacaoHandler(
             ILocacoesRepository locacoesRepository,
             IFilmesRepository filmesRepository,
             IClientesRepository clientesRepository,
             ILogger<AtualizarLocacaoHandler> logger)
        {
            _locacoesRepository = locacoesRepository;
            _filmesRepository = filmesRepository;
            _clientesRepository = clientesRepository;
            _filmesRepository = filmesRepository;
            _logger = logger;
        }

        public async Task<AtualizarLocacaoResponse> Handle(AtualizarLocacaoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando cadastro de locaçao.");

            var locacao = await BuscarLocacao(request.LocacaoId);

            await ValidarExistenciaLocacaoIgual(request);

            var filme = await BuscarFilme(request.FilmeId);
            var cliente = await BuscarCliente(request.ClienteId);

            locacao.AtualizarLocacao(cliente, filme);

            _logger.LogInformation("atualizando locação no banco");
            await _locacoesRepository.Atualizar(locacao);

            return new AtualizarLocacaoResponse();
        }

        private async Task ValidarExistenciaLocacaoIgual(AtualizarLocacaoRequest request)
        {
            var locacao = await _locacoesRepository.SelecionarUmaPor(locacao
                => locacao.FilmeId == request.FilmeId
                && locacao.ClienteId == request.ClienteId
                && locacao.Status == StatusLocacao.Andamento
                && locacao.EhAtivo);

            if (locacao is not null)
            {
                var mensagem = $"já existe uma locação ativa no momento com o filme: {request.FilmeId} e cliente: {request.ClienteId}.";
                _logger.LogInformation(mensagem);
                throw new DuplicateValueException(mensagem);
            }
        }

        private async Task<Locacao> BuscarLocacao(int locacaoId)
        {
            var locacao = await _locacoesRepository.SelecionarUmaPor(locacao => locacao.Id == locacaoId && locacao.EhAtivo);

            if (locacao is null)
            {
                var mensagem = $"Locação não encontrada.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            return locacao;
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
