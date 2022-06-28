using Application.Common.Extensions;
using Application.Services.Clientes.Buscar;
using Application.Services.Clientes.Criar;
using Application.Services.Clientes.DTOs;
using Application.Services.Locacoes.DTOs;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    public class ClienteMap : Profile
    {
        public ClienteMap()
        {
            CreateMap<CriarClienteRequest, Cliente>()
                .ConstructUsing(request => new Cliente(request.Cpf.FormatCpf(), request.Nome, request.DataNascimento));

            CreateMap<Cliente, ClienteResponse>()
                .ForMember(response => response.Status, opt => opt.MapFrom(cliente => BuscarClienteStatus(cliente)));

            CreateMap<Cliente, BuscarClienteResponse>();

            CreateMap<Cliente, ClienteForLocacao>();
        }

        public StatusCliente BuscarClienteStatus(Cliente clienteComLocacoes)
        {
            if (clienteComLocacoes.Locacoes.Any(locacao => locacao.Status == StatusLocacao.Atrasado && locacao.EhAtivo))
                return StatusCliente.ComPendenciasAtrasadas;
            else if (clienteComLocacoes.Locacoes.Any(locacao => locacao.Status == StatusLocacao.Andamento && locacao.EhAtivo))
                return StatusCliente.ComPendencias;
            else
                return StatusCliente.SemPendencias;
        }

    }
}
