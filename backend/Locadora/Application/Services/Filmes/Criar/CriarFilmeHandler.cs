using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Filmes.Criar
{
    public class CriarFilmeHandler : IRequestHandler<CriarFilmeRequest, CriarFilmeResponse>
    {
        private readonly IFilmesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CriarFilmeHandler> _logger;

        public CriarFilmeHandler(IFilmesRepository repository, IMapper mapper, ILogger<CriarFilmeHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CriarFilmeResponse> Handle(CriarFilmeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando cadastro de filme.");

            var filme = _mapper.Map<Filme>(request);

            _logger.LogInformation("Inserindo filme na base");
            await _repository.Inserir(filme);

            return new CriarFilmeResponse();
        }
    }
}
