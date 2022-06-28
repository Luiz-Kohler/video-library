using Application.Services.Filmes.DTOs;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Filmes.Listar
{
    public class ListarFilmesHandler : IRequestHandler<ListarFilmesRequest, ListarFilmesResponse>
    {
        private readonly IFilmesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ListarFilmesHandler> _logger;

        public ListarFilmesHandler(IFilmesRepository repository, IMapper mapper, ILogger<ListarFilmesHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ListarFilmesResponse> Handle(ListarFilmesRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Listando filmes");

            var filmes = await _repository.SelecionarVariasPor(filme => filme.EhAtivo);

            if (filmes is null || !filmes.Any())
                return new ListarFilmesResponse();

            return new ListarFilmesResponse
            {
                Filmes = filmes.Select(filme => _mapper.Map<FilmeResponse>(filme)).ToList()
            };
        }
    }
}
