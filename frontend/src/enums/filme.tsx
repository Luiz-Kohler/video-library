export enum Classificacao {
    Livre = 0,
    Dez = 10,
    Doze = 12,
    Catorze = 14,
    Dezesseis = 16,
    Dezoito = 18
}

export const GetClassificacaoLabel = (status: Classificacao): string => {
    switch (status){
        case Classificacao.Livre:
            return 'Livre'
        case Classificacao.Dez:
            return '10 anos'
        case Classificacao.Doze:
            return '12 anos'
        case Classificacao.Catorze:
            return '14 anos'
        case Classificacao.Dezesseis:
            return '16 anos'
        default: 
            return '18 anos'
    }
}