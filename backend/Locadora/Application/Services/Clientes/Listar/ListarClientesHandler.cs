using Application.Services.Clientes.DTOs;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Clientes.Listar
{
    public class ListarClientesHandler : IRequestHandler<ListarClientesRequest, ListarClientesResponse>
    {
        private readonly IClientesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListarClientesHandler> _logger;

        public ListarClientesHandler(IClientesRepository repository, IMapper mapper, ILogger<ListarClientesHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ListarClientesResponse> Handle(ListarClientesRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Listando clientes");

            var clientes = await _repository.SelecionarVariasPorIncluindoLocacoes(cliente => cliente.EhAtivo);

            if (clientes is null || !clientes.Any())
                return new ListarClientesResponse();

            return new ListarClientesResponse
            {
                Clientes = clientes.Select(cliente => _mapper.Map<ClienteResponse>(cliente)).ToList()
            };
        }
    }
}
