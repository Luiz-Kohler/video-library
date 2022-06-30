import api from '../api';

import { ClienteStatus } from '../../enums/cliente';

const CONTROLLER = 'clientes';

export type ClienteResponse = {
    id: number;
    nome: string;
    cpf: string;
    dataNascimento: Date;
    status: ClienteStatus
};

export type BuscarClienteResponse = ClienteResponse;

export const BuscarCliente = async (id : number) : Promise<ClienteResponse> => {
    return api.get<ClienteResponse>(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}

export type ListarClientesResponse = {
    clientes: ClienteResponse[];
}

export const ListarClientes = async () : Promise<ListarClientesResponse>=> {
    return api.get<ListarClientesResponse>(`${CONTROLLER}`)
    .then(res => res.data)
}

export type CriarClienteRequest = {
    nome: string,
    cpf: string,
    dataNascimento: Date,
}

export const CriarCliente = async (request : CriarClienteRequest) => {
    return api.post(`${CONTROLLER}`, request).then(res => res);
}

export type AtualizarClienteRequest = {
    id: number,
    nome: string,
    dataNascimento: Date,
}

export const AtualizarCliente = async (request : AtualizarClienteRequest) => {
    return api.put(`${CONTROLLER}`, request).then(res => res);
}

export const ExcluirCliente = async (id : number) => {
    return api.delete(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}