using Application.Common.Csv;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Filmes.Importar
{
    public class ImportarFilmesHandler : IRequestHandler<ImportarFilmesRequest, ImportarFilmesResponse>
    {
        private readonly IFilmesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICsvService _csvService;
        private readonly ILogger<ImportarFilmesHandler> _logger;
        private readonly FilmeRecordValidator _validator;

        public ImportarFilmesHandler(IFilmesRepository repository, IMapper mapper, ICsvService csvService, ILogger<ImportarFilmesHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _csvService = csvService;
            _validator = new FilmeRecordValidator();
        }
          
        public async Task<ImportarFilmesResponse> Handle(ImportarFilmesRequest request, CancellationToken cancellationToken)
        {
            var filmesCsv = _csvService.ConvertCsvForRecords<FilmeRecord>(request.ArquivoCsv);

            ValidarFilmes(filmesCsv);

            var filmesComMesmoIds = await _repository.SelecionarVariasPor(filme => filmesCsv.Select(filmeCsv => filmeCsv.Id).Contains(filme.Id));

            var filmesComMesmoIdsAtivos = filmesComMesmoIds.Where(filme => filme.EhAtivo);
            if (filmesComMesmoIdsAtivos.Any())
                throw new DuplicateValueException($"Alguns ids de filmes já estão na base: {String.Join(", ", filmesComMesmoIdsAtivos.Select(filme => filme.Id).ToArray())}");

            var filmesComMesmoIdsInativos = filmesComMesmoIds.Where(filme => !filme.EhAtivo);
            if (filmesComMesmoIdsInativos.Any())
                await AtivarFilmes(filmesComMesmoIdsInativos, filmesCsv);

            var novosFilmes = filmesCsv.Where(filmeCsv => !filmesComMesmoIds.Select(filme => filme.Id).Contains(filmeCsv.Id));

            if(novosFilmes.Any())
                await CriarNovosFilmes(filmesCsv);

            return new ImportarFilmesResponse();
        }

        private async Task CriarNovosFilmes(List<FilmeRecord> filmesNovos)
        {
            var filmes = filmesNovos.Select(filme => _mapper.Map<Filme>(filme)).ToList();
            await _repository.InserirVarios(filmes);
        }

        private void ValidarFilmes(List<FilmeRecord> filmesCsv)
        {
            var errosValidacao = filmesCsv.Select(filmeCsv => _validator.Validate(filmeCsv))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (errosValidacao.Any())
                throw new ValidationException(errosValidacao);

            var temIdsRepetidos = filmesCsv.Select(filme => filme.Id).Count() != filmesCsv.Select(filme => filme.Id).Distinct().Count();

            if (temIdsRepetidos)
                throw new DuplicateValueException("Arquivo .csv não pode conter Ids repetidos");
        }

        private async Task AtivarFilmes(IEnumerable<Filme> filmesInativosDb, List<FilmeRecord> filmesCsv)
        {
            var filmesParaAtivar = filmesInativosDb.Select(filme =>
            {
                var filmeCsv = filmesCsv.First(filmeCsv => filmeCsv.Id == filme.Id);

                filme.Ativar();
                filme.Atualizar(filmeCsv.Titulo, filmeCsv.Classificacao.Value, filmeCsv.Lancamento);

                return filme;
            });

            await _repository.AtualizarVarios(filmesParaAtivar);
        }
    }
}
