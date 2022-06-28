using Application.Common.Exceptions;
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

            var filme = await _repository.SelecionarUmaPor(filme => filme.Id == request.Id);

            if(filme is not null && filme.EhAtivo)
            {
                var mensagem = "já existe um filme com este id";
                _logger.LogInformation(mensagem);
                throw new DuplicateValueException(mensagem);

            } 
            else if (filme is not null && !filme.EhAtivo)
            {
                filme.Atualizar(request.Titulo, request.Classificacao, request.EhLancamento);
                await _repository.Ativar(filme);
            }
            else
            {
                filme = _mapper.Map<Filme>(request);
                await _repository.Inserir(filme);
            }

            return new CriarFilmeResponse();
        }
    }
}
