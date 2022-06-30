import api from '../api';
import { Classificacao } from '../../enums/filme';

const CONTROLLER = 'filmes';

export type FilmeResponse = {
    id: number,
    titulo: string,
    classificacao: Classificacao,
    ehLancamento: boolean,
};

export type BuscarFilmeResponse = FilmeResponse;

export const BuscarFilme = async (id: number): Promise<FilmeResponse> => {
    return api.get<FilmeResponse>(`${CONTROLLER}/${id}`)
        .then(res => res.data)
}

export type ListarFilmesResponse = {
    filmes: Array<FilmeResponse>
}

export const ListarFilmes = async (): Promise<ListarFilmesResponse> => {
    return api.get<ListarFilmesResponse>(`${CONTROLLER}`)
        .then(res => res.data)
}

export type CriarFilmeRequest = {
    id: number,
    titulo: string,
    classificacao: Classificacao,
    ehLancamento: boolean,
}

export const CriarFilme = async (request: CriarFilmeRequest) => {
    return api.post(`${CONTROLLER}`, request).then(res => res);
}

export const ImportarFilmes = async (arquivoCsv: any) => {
    let formData = new FormData();
    formData.append("filmes", arquivoCsv)

    return api.post(`${CONTROLLER}/csv`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    }).then(res => res);
}

export type AtualizarFilmeRequest = {
    id: number,
    titulo: string,
    classificacao: Classificacao,
    ehLancamento: boolean,
}

export const AtualizarFilme = async (request: AtualizarFilmeRequest) => {
    return api.put(`${CONTROLLER}`, request).then(res => res);
}

export const ExcluirFilme = async (id : number) => {
    return api.delete(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}