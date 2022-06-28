using Application.Common.Exceptions;
using Application.Common.Extensions;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Clientes.Criar
{
    public class CriarClienteHandler : IRequestHandler<CriarClienteRequest, CriarClienteResponse>
    {
        private readonly IClientesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CriarClienteHandler> _logger;

        public CriarClienteHandler(IClientesRepository repository, IMapper mapper, ILogger<CriarClienteHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CriarClienteResponse> Handle(CriarClienteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando cadastro de cliente.");

            var cliente = await _repository.SelecionarUmaPor(cliente => cliente.Cpf == request.Cpf.FormatCpf());

            if(cliente is not null)
            {
                var mensagem = $"Cliente com CPF: {request.Cpf}. Já existe na base.";
                _logger.LogInformation(mensagem);
                throw new DuplicateValueException(mensagem);
            }

            cliente = _mapper.Map<Cliente>(request);

            _logger.LogInformation("Inserindo cliente na base");
            await _repository.Inserir(cliente);

            return new CriarClienteResponse();
        }
    }
}
