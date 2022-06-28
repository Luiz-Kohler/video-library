using Application.Services.Filmes.Buscar;
using Application.Services.Filmes.Criar;
using Application.Services.Filmes.DTOs;
using Application.Services.Locacoes.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    internal class FilmeMap : Profile
    {
        public FilmeMap()
        {
            CreateMap<CriarFilmeRequest, Filme>()
                 .ConstructUsing(request => new Filme(request.Titulo, request.Classificacao, request.EhLancamento));

            CreateMap<Filme, FilmeResponse>();

            CreateMap<Filme, BuscarFilmeResponse>();

            CreateMap<Filme, FilmeForLocacao>();
        }
    }
}
