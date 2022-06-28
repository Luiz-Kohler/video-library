using Application.Services.Locacoes.Buscar;
using Application.Services.Locacoes.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    public class LocacaoMap : Profile
    {
        public LocacaoMap()
        {
            CreateMap<Locacao, BuscarLocacaoResponse>()
                .ForMember(response => response.Filme, opt => opt.MapFrom(locacao => locacao.Filme))
                .ForMember(response => response.Cliente, opt => opt.MapFrom(locacao => locacao.Cliente));

            CreateMap<Locacao, LocacaoResponse>()
                .ForMember(response => response.Filme, opt => opt.MapFrom(locacao => locacao.Filme))
                .ForMember(response => response.Cliente, opt => opt.MapFrom(locacao => locacao.Cliente));
        }
    }
}
