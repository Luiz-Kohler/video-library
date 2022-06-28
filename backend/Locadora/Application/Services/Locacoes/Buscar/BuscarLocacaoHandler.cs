using Application.Common.Exceptions;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Locacoes.Buscar
{
    public class BuscarLocacaoHandler : IRequestHandler<BuscarLocacaoRequest, BuscarLocacaoResponse>
    {
        private readonly ILocacoesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BuscarLocacaoHandler> _logger;

        public BuscarLocacaoHandler(
             ILocacoesRepository repository,
             IMapper mapper,
             ILogger<BuscarLocacaoHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BuscarLocacaoResponse> Handle(BuscarLocacaoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Buscando locação pelo Id");

            var locacao = await _repository.SelecionarUmaComRelacionamentos(locacao => locacao.Id == request.Id && locacao.EhAtivo);

            if (locacao is null)
            {
                var mensagem = $"Locação não encontrada.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            return _mapper.Map<BuscarLocacaoResponse>(locacao);
        }
    }
}
