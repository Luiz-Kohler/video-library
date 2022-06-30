import React, { useEffect, useState } from 'react';
import { Button, Form, Modal, Select } from 'antd';
import { CriarLocacao, CriarLocacaoRequest } from '../../../services/locacoes/api';
import { toast } from 'react-toastify';
import { FilmeResponse, ListarFilmes } from '../../../services/filmes/api';
import { ClienteResponse, ListarClientes } from '../../../services/clientes/api';
const { Option } = Select;

type CriarLocacaoModalProps = {
    atualizar: any
}

const CriarlocacaoModal: React.FC<CriarLocacaoModalProps> = ({ atualizar }) => {
    const [isLoading, setIsLoading] = useState(false);
    const [filmes, setFilmes] = useState<FilmeResponse[]>([]);
    const [clientes, setClientes] = useState<ClienteResponse[]>([]);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [form] = Form.useForm();
    const [locacao, setlocacao] = useState<CriarLocacaoRequest>({
        clienteId: 0,
        filmeId: 0
    });
    
    useEffect(() => {
        setIsLoading(true);

        ListarFilmes().then(res => {
            setFilmes(res.filmes)
        });

        ListarClientes().then(res => {
            setClientes(res.clientes)
        });

        setIsLoading(false);
    }, [isModalVisible])


    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        CriarLocacao(locacao).then((res) => {
            if (res.status === 200) {
                setlocacao({ clienteId: 0, filmeId: 0 });
                form.resetFields();
                toast.success("locacao criado com sucesso!")
                setIsModalVisible(false);
                atualizar();
            }
        })
    };

    const handleCancel = () => {
        setlocacao({ clienteId: 0, filmeId: 0 });
        form.resetFields();
        setIsModalVisible(false);
    };

    return (
        <>
            <Button type="primary" onClick={showModal}>
                Cadastrar
            </Button>
            <Modal destroyOnClose title="Cadastro de Locação" visible={isModalVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="criar-locacao-form" key="submit" htmlType="submit">
                    Cadastrar
                </Button>
            ]}>
                <Form
                    id="criar-locacao-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="Cliente" name="clienteId" rules={[{ required: true, message: 'Informe o Cliente' }]}>
                        <Select onChange={(value) => setlocacao({ ...locacao, clienteId: parseInt(value) })} loading={isLoading}>
                            {clientes.map(cliente => (<Option value={`${cliente.id}`}>{`${cliente.id} - ${cliente.nome}`}</Option>))}
                        </Select>
                    </Form.Item>
                    <Form.Item label="Filme" name="filmeId" rules={[{ required: true, message: 'Informe o Filme' }]}>
                        <Select onChange={(value) => setlocacao({ ...locacao, filmeId: parseInt(value) })} loading={isLoading}>
                            {filmes.map(filme => (<Option value={`${filme.id}`}>{`${filme.id} - ${filme.titulo}`}</Option>))}
                        </Select>
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default CriarlocacaoModal;