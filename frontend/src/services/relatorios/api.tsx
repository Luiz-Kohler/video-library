import api from '../api';

const CONTROLLER = 'relatorios';

export const BaixarRelatorio = async () => {
    return api.get(`${CONTROLLER}`, {
        responseType: 'blob'
    })
    .then(res => {
        const url = window.URL.createObjectURL(new Blob([res.data]));
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', `Relatorio-${new Date().toLocaleDateString()}.xlsx`);
        document.body.appendChild(link);
        link.click();
        link.remove();
    })
}