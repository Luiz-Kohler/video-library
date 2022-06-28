using Application.Common.Exceptions;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Clientes.Buscar
{
    public class BuscarClienteHandler : IRequestHandler<BuscarClienteRequest, BuscarClienteResponse>
    {
        private readonly IClientesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BuscarClienteHandler> _logger;

        public BuscarClienteHandler(IClientesRepository repository, IMapper mapper, ILogger<BuscarClienteHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BuscarClienteResponse> Handle(BuscarClienteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando busca de cliente por Id");

            var cliente = await _repository.SelecionarUmaPorIncluindoLocacoes(cliente => cliente.Id == request.Id && cliente.EhAtivo);

            if (cliente is null)
            {
                var mensagem = $"Cliente não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            return _mapper.Map<BuscarClienteResponse>(cliente);
        } 
    }
}
