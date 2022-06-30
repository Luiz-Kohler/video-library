import React, { useEffect, useState } from 'react';
import { Button, Form, Modal, Select } from 'antd';
import { AtualizarLocacao, AtualizarLocacaoRequest, BuscarLocacao } from '../../../services/locacoes/api';
import { toast } from 'react-toastify';
import { FilmeResponse, ListarFilmes } from '../../../services/filmes/api';
import { ClienteResponse, ListarClientes } from '../../../services/clientes/api';
const { Option } = Select;

type AtualizarlocacaoModalProps = {
    isVisible: boolean;
    setVisableFalse: any;
    id: number;
    atualizar: any
}
const AtualizarlocacaoModal: React.FC<AtualizarlocacaoModalProps> = ({ isVisible, setVisableFalse, atualizar, id }) => {
    const [form] = Form.useForm();

    const [isLoading, setIsLoading] = useState(false);
    const [filmes, setFilmes] = useState<FilmeResponse[]>([]);
    const [clientes, setClientes] = useState<ClienteResponse[]>([]);

    const [locacao, setlocacao] = useState<AtualizarLocacaoRequest>({
        locacaoId: id,
        clienteId: 0,
        filmeId: 0
    });
    

    useEffect(() => {
        if (id !== 0) {
            setIsLoading(true);

            BuscarLocacao(id).then(res => {
                setlocacao({ locacaoId: id, clienteId: res.cliente.id, filmeId: res.filme.id })
                form.setFieldsValue({ locacaoId: id, clienteId: res.cliente.id, filmeId: res.filme.id})
            });

            ListarFilmes().then(res => {
                setFilmes(res.filmes)
            });

            ListarClientes().then(res => {
                setClientes(res.clientes)
            });
            
            setIsLoading(false);
        }
    }, [id, false])

    const handleOk = () => {
        AtualizarLocacao(locacao).then((res) => {
            if (res.status === 200) {
                setlocacao({ locacaoId: 0, clienteId: 0, filmeId: 0 });
                setVisableFalse(false);
                atualizar();
                toast.success("Locação atualizada com sucesso!")
            }
        })
    };

    const handleCancel = () => {
        setlocacao({ locacaoId: 0, clienteId: 0, filmeId: 0 });
        setVisableFalse();
    };

    return (
        <>
            <Modal title="Atualização de locacao" visible={isVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="atualizar-locacao-form" key="submit" htmlType="submit">
                    Atualizar
                </Button>
            ]}>
                <Form
                    id="atualizar-locacao-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="Cliente" name="clienteId" rules={[{ required: true, message: 'Informe o Cliente' }]}>
                        <Select value={locacao.clienteId} onChange={(value) => setlocacao({ ...locacao, clienteId: value })} loading={isLoading}>
                            {clientes.map(cliente => (<Option value={`${cliente.id}`}>{`${cliente.id} - ${cliente.nome}`}</Option>))}
                        </Select>
                    </Form.Item>
                    <Form.Item label="Filme" name="filmeId" rules={[{ required: true, message: 'Informe o Filme' }]}>
                        <Select value={locacao.filmeId} onChange={(value) => setlocacao({ ...locacao, filmeId: value })} loading={isLoading}>
                            {filmes.map(filme => (<Option value={`${filme.id}`}>{`${filme.id} - ${filme.titulo}`}</Option>))}
                        </Select>
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default AtualizarlocacaoModal;