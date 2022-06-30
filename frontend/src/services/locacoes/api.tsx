import api from '../api';
import { LocacaoStatus } from '../../enums/locacao';

const CONTROLLER = 'locacoes';


type ClienteForLocacao = {
    id: number,
    nome: string
}

type FilmeForLocacao = {
    id: number,
    titulo: string
}

export type LocacaoResponse = {
    id: number,
    dataLocacao: Date,
    dataDevolucao: Date,
    dataPrazoDevolucao: Date,
    cliente: ClienteForLocacao,
    filme: FilmeForLocacao,
    status: LocacaoStatus

};

export type BuscarLocacaoResponse = LocacaoResponse;

export const BuscarLocacao = async (id : number) : Promise<LocacaoResponse> => {
    return api.get<LocacaoResponse>(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}

export type ListarLocacoesResponse = {
    locacoes: Array<LocacaoResponse>
}

export const ListarLocacoes = async () : Promise<ListarLocacoesResponse> => {
    return api.get<ListarLocacoesResponse>(`${CONTROLLER}`)
    .then(res => res.data)
}

export type CriarLocacaoRequest = {
    clienteId: number,
    filmeId: number,
}

export const CriarLocacao = async (request : CriarLocacaoRequest) => {
    return api.post(`${CONTROLLER}`, request).then(res => res);
}

export type AtualizarLocacaoRequest = {
    locacaoId: number,
    clienteId: number,
    filmeId: number,
}

export const AtualizarLocacao = async (request : AtualizarLocacaoRequest) => {
    return api.put(`${CONTROLLER}`, request).then(res => res);
}

export const DevolverFilme = async (locacaoId : number) => {
    return api.put(`${CONTROLLER}/${locacaoId}/devolver`)
    .then(res => res.data)
}

export const ExcluirLocacao = async (id : number) => {
    return api.delete(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}