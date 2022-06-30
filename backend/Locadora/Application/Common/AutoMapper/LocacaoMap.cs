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
                .ForMember(response => response.Cliente, opt => opt.MapFrom(locacao => locacao.Cliente))
                .ForMember(response => response.Cliente, opt => opt.MapFrom(locacao => locacao.Cliente))
                .ForMember(response => response.DataLocacao, opt => opt.MapFrom(loacao => loacao.DataLocacao.ToLocalTime()))
                .ForMember(response => response.Status, opt => opt.MapFrom(loacao => loacao.BuscarStatus()))
                .ForMember(response => response.DataDevolucao, opt =>
                {
                    opt.Condition(locacao => locacao.DataDevolucao.HasValue);
                    opt.MapFrom(loacao => loacao.DataDevolucao);
                });

            CreateMap<Locacao, LocacaoResponse>()
                .ForMember(response => response.Filme, opt => opt.MapFrom(locacao => locacao.Filme))
                .ForMember(response => response.Cliente, opt => opt.MapFrom(locacao => locacao.Cliente))
                .ForMember(response => response.DataLocacao, opt => opt.MapFrom(loacao => loacao.DataLocacao.ToLocalTime()))
                .ForMember(response => response.DataPrazoDevolucao, opt => opt.MapFrom(loacao => loacao.DataPrazoDevolucao.ToLocalTime()))
                .ForMember(response => response.Status, opt => opt.MapFrom(loacao => loacao.BuscarStatus()))
                .ForMember(response => response.DataDevolucao, opt =>
                {
                    opt.Condition(locacao => locacao.DataDevolucao.HasValue);
                    opt.MapFrom(loacao => loacao.DataDevolucao);
                });
        }
    }
}
