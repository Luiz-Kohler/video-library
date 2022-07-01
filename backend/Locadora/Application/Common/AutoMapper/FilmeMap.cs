using Application.Services.Filmes.Buscar;
using Application.Services.Filmes.Criar;
using Application.Services.Filmes.DTOs;
using Application.Services.Filmes.Importar;
using Application.Services.Locacoes.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    public class FilmeMap : Profile
    {
        public FilmeMap()
        {
            CreateMap<CriarFilmeRequest, Filme>()
                 .ConstructUsing(request => new Filme(request.Id, request.Titulo, request.Classificacao, request.EhLancamento));

            CreateMap<Filme, FilmeResponse>();

            CreateMap<Filme, BuscarFilmeResponse>();

            CreateMap<Filme, FilmeForLocacao>();

            CreateMap<FilmeRecord, Filme>()
                .ConstructUsing(filme => new Filme(filme.Id, filme.Titulo, filme.Classificacao.Value, filme.Lancamento));
        }
    }
}
