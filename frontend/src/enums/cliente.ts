export enum ClienteStatus {
    SemPendencias = 0,
    ComPedencias = 1,
    ComPedenciasAtrasadas = 2
}

export const GetClienteStatusLabel = (status: ClienteStatus): string => {
    switch (status){
        case ClienteStatus.ComPedencias:
            return 'Com Pendências'
        case ClienteStatus.ComPedenciasAtrasadas:
            return 'Com Pendências Atrasadas'
        default: 
            return 'Sem Pendências'
    }
}