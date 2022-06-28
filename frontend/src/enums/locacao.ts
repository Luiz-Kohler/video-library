export enum LocacaoStatus {
    Andamento = 0,
    Devolvido = 1,
    Atrasado =3
}

export const GetLocacaoStatusLabel = (status: LocacaoStatus): string => {
    switch (status){
        case LocacaoStatus.Atrasado:
            return 'Atrasado'
        case LocacaoStatus.Devolvido:
            return 'Devolvido'
        default: 
            return 'Em andamento'
    }
}