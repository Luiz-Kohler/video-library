using Application.Common.Exceptions;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Filmes.Buscar
{
    public class BuscarFilmeHandler : IRequestHandler<BuscarFilmeRequest, BuscarFilmeResponse>
    {
        private readonly IFilmesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BuscarFilmeHandler> _logger;

        public BuscarFilmeHandler(IFilmesRepository repository, IMapper mapper, ILogger<BuscarFilmeHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BuscarFilmeResponse> Handle(BuscarFilmeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando busca de filme por Id");

            var filme = await _repository.SelecionarUmaPor(filme => filme.Id == request.Id && filme.EhAtivo);

            if (filme is null)
            {
                var mensagem = $"Filme não encontrado.";
                _logger.LogInformation(mensagem);
                throw new NotFoundException(mensagem);
            }

            return _mapper.Map<BuscarFilmeResponse>(filme);
        }
    }
}
