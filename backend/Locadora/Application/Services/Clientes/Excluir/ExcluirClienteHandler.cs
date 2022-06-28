using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Clientes.Excluir
{
    public class ExcluirClienteHandler : IRequestHandler<ExcluirClienteRequest, ExcluirClienteResponse>
    {
        private readonly IClientesRepository _repository;
        private readonly ILogger<ExcluirClienteHandler> _logger;

        public ExcluirClienteHandler(IClientesRepository repository, ILogger<ExcluirClienteHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ExcluirClienteResponse> Handle(ExcluirClienteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando exclusão de cliente.");

            var cliente = await _repository.SelecionarUmaPor(cliente => cliente.Id == request.Id && cliente.EhAtivo);

            if (cliente is null)
            {
                var mensagem = $"Cliente não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            _logger.LogInformation("Excluindo cliente");
            await _repository.Excluir(cliente);

            return new ExcluirClienteResponse();
        }
    }
}
