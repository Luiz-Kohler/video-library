using Application.Services.Locacoes.DTOs;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Locacoes.Listar
{
    public class ListarLocacoesHandler : IRequestHandler<ListarLocacoesRequest, ListarLocacoesResponse>
    {
        private readonly ILocacoesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListarLocacoesHandler> _logger;

        public ListarLocacoesHandler(
             ILocacoesRepository repository,
             IMapper mapper,
             ILogger<ListarLocacoesHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ListarLocacoesResponse> Handle(ListarLocacoesRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Listando locações");

            var locacoes = await _repository.SelecionarVariasComRelacionamentos(locacao => locacao.EhAtivo);

            if(locacoes is null || !locacoes.Any())
                return new ListarLocacoesResponse();

            return new ListarLocacoesResponse
            {
                Locacoes = locacoes.Select(locacao => _mapper.Map<LocacaoResponse>(locacao)).ToList()
            };
        }
    }
}
