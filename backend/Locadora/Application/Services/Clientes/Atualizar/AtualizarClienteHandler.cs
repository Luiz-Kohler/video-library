using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Clientes.Atualizar
{
    public class AtualizarClienteHandler : IRequestHandler<AtualizarClienteRequest, AtualizarClienteResponse>
    {
        private readonly IClientesRepository _repository;
        private readonly ILogger<AtualizarClienteHandler> _logger;

        public AtualizarClienteHandler(IClientesRepository repository, ILogger<AtualizarClienteHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<AtualizarClienteResponse> Handle(AtualizarClienteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando atualização de cliente.");

            var cliente = await _repository.SelecionarUmaPor(cliente => cliente.Id == request.Id && cliente.EhAtivo);

            if (cliente is null)
            {
                var mensagem = $"Cliente não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            cliente.Atualizar(request.Nome, request.DataNascimento);

            _logger.LogInformation("Cliente atualizado na base");
            await _repository.Atualizar(cliente);

            return new AtualizarClienteResponse();
        }
    }
}
